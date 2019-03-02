using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public sealed class ManagerTest : Manager<ManagerTest>, IManagerTest
    {
        private readonly IUserHelper _userHelper;
        private readonly ITestHelper _testHelper;
        private readonly IRepositoryTest _repositoryTest;

        public ManagerTest(
            IMapper mapper,
            ILogger<ManagerTest> logger,
            IUserHelper userHelper,
            ITestHelper testHelper,
            IRepositoryTest repositoryTest)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _testHelper = testHelper;
            _repositoryTest = repositoryTest;
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
            _userHelper.CheckRoleStudent(studentId);

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
            _userHelper.CheckRoleStudent(studentId);

            var test = _repositoryTest.GetTestForStudentById(id, studentId) ??
               throw ExceptionHelper.CreateNotFoundException(
                   $"Тест не найден. Идентификатор теста: {id}. Идентификатор студента: {studentId}.",
                   $"Тест не найден.");

            return MapForStudent(test, options);
        }

        public void DeleteTestById(int id)
        {
            _repositoryTest.Delete(id);
            _repositoryTest.SaveChanges();
        }

        public Test CreateTest(Test test)
        {
            _testHelper.ValidateTest(test);

            var model = Mapper.Map<DatabaseTest>(test);

            _repositoryTest.Add(model);
            _repositoryTest.SaveChanges();

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
    }
}