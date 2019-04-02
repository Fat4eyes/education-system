using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
{
    public sealed class ManagerTest : Manager<ManagerTest>, IManagerTest
    {
        private readonly IHelperUser _helperUser;
        private readonly IValidator<Test> _chekerTest;

        private readonly IRepositoryTest _repositoryTest;
        private readonly IRepositoryTestTheme _repositoryTestTheme;

        public ManagerTest(
            IMapper mapper,
            ILogger<ManagerTest> logger,
            IHelperUser helperUser,
            IValidator<Test> chekerTest,
            IRepositoryTest repositoryTest,
            IRepositoryTestTheme repositoryTestTheme)
            : base(mapper, logger)
        {
            _helperUser = helperUser;
            _chekerTest = chekerTest;
            _repositoryTest = repositoryTest;
            _repositoryTestTheme = repositoryTestTheme;
        }

        public PagedData<Test> GetTests(OptionsTest options, FilterTest filter)
        {
            var (count, tests) = _repositoryTest.GetTests(filter);

            return new PagedData<Test>(tests.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Test> GetTestsByDisciplineId(int disciplineId, OptionsTest options, FilterTest filter)
        {
            var (count, tests) = _repositoryTest.GetTestsByDisciplineId(disciplineId, filter);

            return new PagedData<Test>(tests.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Test> GetTestsForStudent(int studentId, OptionsTest options, FilterTest filter)
        {
            _helperUser.CheckRoleStudent(studentId);

            var (count, tests) = _repositoryTest.GetTestsForStudent(studentId, filter);

            return new PagedData<Test>(tests.Select(x => MapForStudent(x, options)).ToList(), count);
        }

        public Test GetTestById(int id, OptionsTest options)
        {
            var test = _repositoryTest.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест не найден. Идентификатор теста: {id}.",
                    $"Тест не найден.");

            return Map(test, options);
        }

        public Test GetTestForStudentById(int id, int studentId, OptionsTest options)
        {
            _helperUser.CheckRoleStudent(studentId);

            var test = _repositoryTest.GetTestForStudentById(id, studentId) ??
               throw ExceptionHelper.CreateNotFoundException(
                   $"Тест не найден. Идентификатор теста: {id}. Идентификатор студента: {studentId}.",
                   $"Тест не найден.");

            return MapForStudent(test, options);
        }

        public async Task DeleteTestByIdAsync(int id)
        {
            var test = _repositoryTest.GetById(id) ??
               throw ExceptionHelper.CreateNotFoundException(
                   $"Тест для удаления не найден. Идентификатор теста: {id}.",
                   $"Тест для удаления не найден.");

            await _repositoryTest.RemoveAsync(test, true);
        }

        public async Task<Test> CreateTestAsync(Test test)
        {
            _chekerTest.Validate(test);

            FormatTest(test);

            var model = Mapper.Map<DatabaseTest>(test);

            await _repositoryTest.AddAsync(model, true);

            return Mapper.Map<DatabaseTest, Test>(model);
        }

        public async Task<Test> UpdateTestAsync(int id, Test test)
        {
            _chekerTest.Validate(test);

            var model = _repositoryTest.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тест для обновления не найден. Идентификатор теста: {id}.",
                    $"Тест для обновления не найден.");

            FormatTest(test);

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

        private Test MapForStudent(DatabaseTest test, OptionsTest options)
        {
            return Mapper.Map<DatabaseTest, Test>(test, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithThemes)
                    {
                        d.Themes = s.TestThemes
                            .Where(y => y.Theme?.Questions.IsNotEmpty() == true)
                            .Select(y => Mapper.Map<Theme>(y.Theme))
                            .ToList();
                    }
                });
            });
        }

        private static void FormatTest(Test test)
        {
            test.Subject = test.Subject.Trim();
        }
    }
}