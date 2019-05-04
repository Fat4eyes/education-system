using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerTest : Manager, IManagerTest
    {
        private readonly IServiceTest _serviceTest;

        public ManagerTest(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceTest serviceTest)
            : base(
                executionContext,
                exceptionFactory)
        {
            _serviceTest = serviceTest;
        }

        public async Task<PagedData<Test>> GetTestsAsync(FilterTest filter)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceTest.GetTestsAsync(filter);

            if (CurrentUser.IsLecturer())
                return await _serviceTest.GetLecturerTestsAsync(CurrentUser.Id, filter);

            if (CurrentUser.IsStudent())
                return await _serviceTest.GetStudentTestsAsync(CurrentUser.Id, filter);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Test> GetTestAsync(int id)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceTest.GetTestAsync(id);

            if (CurrentUser.IsLecturer())
                return await _serviceTest.GetLecturerTestAsync(id, CurrentUser.Id);

            if (CurrentUser.IsStudent())
                return await _serviceTest.GetStudentTestAsync(id, CurrentUser.Id);

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteTestAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceTest.DeleteTestAsync(id);
        }

        public async Task UpdateTestAsync(int id, Test test)
        {
            if (CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceTest.UpdateTestAsync(id, test);
        }

        public async Task<int> CreateTestAsync(Test test)
        {
            if (CurrentUser.IsLecturer())
                return await _serviceTest.CreateTestAsync(test);

            throw ExceptionFactory.NoAccess();
        }
    }
}