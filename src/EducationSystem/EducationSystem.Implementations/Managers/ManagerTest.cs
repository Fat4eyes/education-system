using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Factories;
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
        private readonly IExceptionFactory _exceptionFactory;

        private readonly IRepositoryTest _repositoryTest;
        private readonly IRepositoryTestTheme _repositoryTestTheme;

        public ManagerTest(
            IMapper mapper,
            ILogger<ManagerTest> logger,
            IValidator<Test> validatorTest,
            IExceptionFactory exceptionFactory,
            IRepositoryTest repositoryTest,
            IRepositoryTestTheme repositoryTestTheme)
            : base(mapper, logger)
        {
            _validatorTest = validatorTest;
            _exceptionFactory = exceptionFactory;
            _repositoryTest = repositoryTest;
            _repositoryTestTheme = repositoryTestTheme;
        }

        public async Task<PagedData<Test>> GetTestsAsync(OptionsTest options, FilterTest filter)
        {
            var (count, tests) = await _repositoryTest.GetTestsAsync(filter);

            var items = tests
                .Select(x => Map(x, options))
                .ToList();

            return new PagedData<Test>(items, count);
        }

        public async Task<PagedData<Test>> GetTestsByDisciplineIdAsync(int disciplineId, OptionsTest options, FilterTest filter)
        {
            var (count, tests) = await _repositoryTest.GetTestsByDisciplineIdAsync(disciplineId, filter);

            var items = tests
                .Select(x => Map(x, options))
                .ToList();

            return new PagedData<Test>(items, count);
        }

        public async Task<Test> GetTestAsync(int id, OptionsTest options)
        {
            var test = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            return Map(test, options);
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

            await _repositoryTest.RemoveAsync(test, true);
        }

        public async Task<Test> CreateTestAsync(Test test)
        {
            await _validatorTest.ValidateAsync(test.Format());

            var model = Mapper.Map<DatabaseTest>(test);

            await _repositoryTest.AddAsync(model, true);

            return Mapper.Map<DatabaseTest, Test>(model);
        }

        public async Task<Test> UpdateTestAsync(int id, Test test)
        {
            await _validatorTest.ValidateAsync(test.Format());

            var model = await _repositoryTest.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTest>(id);

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