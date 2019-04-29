using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerTest : Manager<ManagerTest>, IManagerTest
    {
        private readonly IValidator<Test> _validatorTest;

        private readonly IRepositoryTest _repositoryTest;
        private readonly IRepositoryTestTheme _repositoryTestTheme;

        public ManagerTest(
            IMapper mapper,
            ILogger<ManagerTest> logger,
            IValidator<Test> validatorTest,
            IRepositoryTest repositoryTest,
            IRepositoryTestTheme repositoryTestTheme)
            : base(mapper, logger)
        {
            _validatorTest = validatorTest;
            _repositoryTest = repositoryTest;
            _repositoryTestTheme = repositoryTestTheme;
        }

        public async Task<PagedData<Test>> GetTests(OptionsTest options, FilterTest filter)
        {
            var (count, tests) = await _repositoryTest.GetTests(filter);

            var items = tests
                .Select(x => Map(x, options))
                .ToList();

            return new PagedData<Test>(items, count);
        }

        public async Task<PagedData<Test>> GetTestsByDisciplineId(int disciplineId, OptionsTest options, FilterTest filter)
        {
            var (count, tests) = await _repositoryTest.GetTestsByDisciplineId(disciplineId, filter);

            var items = tests
                .Select(x => Map(x, options))
                .ToList();

            return new PagedData<Test>(items, count);
        }

        public async Task<Test> GetTest(int id, OptionsTest options)
        {
            var test = await _repositoryTest.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест не найден. Идентификатор теста: {id}.",
                    $"Тест не найден.");

            return Map(test, options);
        }

        public async Task DeleteTest(int id)
        {
            var test = await _repositoryTest.GetById(id) ??
               throw ExceptionHelper.CreateNotFoundException(
                   $"Тест для удаления не найден. Идентификатор теста: {id}.",
                   $"Тест для удаления не найден.");

            await _repositoryTest.RemoveAsync(test, true);
        }

        public async Task<Test> CreateTest(Test test)
        {
            _validatorTest.Validate(test.Format());

            var model = Mapper.Map<DatabaseTest>(test);

            await _repositoryTest.AddAsync(model, true);

            return Mapper.Map<DatabaseTest, Test>(model);
        }

        public async Task<Test> UpdateTest(int id, Test test)
        {
            _validatorTest.Validate(test.Format());

            var model = await _repositoryTest.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест для обновления не найден. Идентификатор теста: {id}.",
                    $"Тест для обновления не найден.");

            Mapper.Map(Mapper.Map<DatabaseTest>(test), model);

            await _repositoryTest.UpdateAsync(model, true);

            if (model.TestThemes.Any())
                await _repositoryTestTheme.RemoveAsync(model.TestThemes, true);

            model.TestThemes = Mapper.Map<List<DatabaseTestTheme>>(test.Themes);

            await _repositoryTestTheme.AddAsync(model.TestThemes, true);

            return Mapper.Map<DatabaseTest, Test>(model);
        }

        private Test Map(DatabaseTest test, OptionsTest options)
        {
            return Mapper.Map<DatabaseTest, Test>(test, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithThemes)
                        d.Themes = Mapper.Map<List<Theme>>(s.TestThemes.Select(y => y.Theme));
                });
            });
        }
    }
}