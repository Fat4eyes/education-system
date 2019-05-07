using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Datas;
using EducationSystem.Specifications.Tests;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTestData : Service<ServiceTestData>, IServiceTestData
    {
        private readonly IRepository<DatabaseTest> _repositoryTest;

        public ServiceTestData(
            IMapper mapper,
            ILogger<ServiceTestData> logger,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseTest> repositoryTest)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            _repositoryTest = repositoryTest;
        }

        public async Task<List<TestData>> GetTestsDataAsync(int[] ids)
        {
            const string message = "Один или несколько указанных тестов не существуют или недоступны.";

            if (CurrentUser.IsAdmin())
            {
                var specification = new TestsByIds(ids);

                var models = await _repositoryTest.FindAllAsync(specification);

                if (models.Count != ids.Length)
                    throw ExceptionHelper.CreatePublicException(message);

                return Mapper.Map<List<TestData>>(models);
            }

            if (CurrentUser.IsLecturer())
            {
                var specification =
                    new TestsByIds(ids) &
                    new TestsByLecturerId(CurrentUser.Id);

                var models = await _repositoryTest.FindAllAsync(specification);

                if (models.Count != ids.Length)
                    throw ExceptionHelper.CreatePublicException(message);

                return Mapper.Map<List<TestData>>(models);
            }

            if (CurrentUser.IsStudent())
            {
                var specification =
                    new TestsByIds(ids) &
                    new TestsByStudentId(CurrentUser.Id) &
                    new TestsForStudents();

                var models = await _repositoryTest.FindAllAsync(specification);

                if (models.Count != ids.Length)
                    throw ExceptionHelper.CreatePublicException(message);

                return Mapper.Map<List<TestData>>(models);
            }

            throw ExceptionFactory.NoAccess();
        }
    }
}