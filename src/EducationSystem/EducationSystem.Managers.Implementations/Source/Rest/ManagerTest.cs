using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerTest : Manager<ManagerTest>, IManagerTest
    {
        protected IRepositoryTest RepositoryTest { get; }

        public ManagerTest(
            IMapper mapper,
            ILogger<ManagerTest> logger,
            IRepositoryTest repositoryTest)
            : base(mapper, logger)
        {
            RepositoryTest = repositoryTest;
        }

        public PagedData<Test> GetTests(OptionsTest options, FilterTest filter)
        {
            var (count, tests) = RepositoryTest.GetTests(filter);

            return new PagedData<Test>(tests.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Test> GetTestsByDisciplineId(int disciplineId, OptionsTest options, FilterTest filter)
        {
            var (count, tests) = RepositoryTest.GetTestsByDisciplineId(disciplineId, filter);

            return new PagedData<Test>(tests.Select(x => Map(x, options)).ToList(), count);
        }

        public Test GetTestById(int id, OptionsTest options)
        {
            var test = RepositoryTest.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Test.NotFoundById(id),
                    Messages.Test.NotFoundPublic);

            return Mapper.Map<Test>(Map(test, options));
        }

        public void DeleteTestById(int id)
        {
            if (RepositoryTest.GetById(id) == null)
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Test.NotFoundById(id),
                    Messages.Test.NotFoundPublic);

            RepositoryTest.Delete(id);
            RepositoryTest.SaveChanges();
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