using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
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

        public PagedData<Test> GetTests(OptionsTest options)
        {
            var (count, tests) = RepositoryTest.GetTests(options);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public PagedData<Test> GetTestsByDisciplineId(int disciplineId, OptionsTest options)
        {
            var (count, tests) = RepositoryTest.GetTestsByDisciplineId(disciplineId, options);

            return new PagedData<Test>(Mapper.Map<List<Test>>(tests), count);
        }

        public Test GetTestById(int id, OptionsTest options)
        {
            var test = RepositoryTest.GetTetsById(id, options) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Test.NotFoundById(id),
                    Messages.Test.NotFoundPublic);

            return Mapper.Map<Test>(test);
        }
    }
}