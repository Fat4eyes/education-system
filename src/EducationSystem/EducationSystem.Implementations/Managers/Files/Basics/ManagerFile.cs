using System.Threading.Tasks;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Implementations.Managers.Files.Basics
{
    public class ManagerFile<TFile> : Manager, IManagerFile<TFile> where TFile : File
    {
        private readonly IServiceFile<TFile> _serviceFile;

        public ManagerFile(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceFile<TFile> serviceFile)
            : base(
                executionContext,
                exceptionFactory)
        {
            _serviceFile = serviceFile;
        }

        public async Task DeleteFileAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceFile.DeleteFileAsync(id);
        }

        public async Task<TFile> GetFileAsync(int id)
        {
            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer() || CurrentUser.IsStudent())
                return await _serviceFile.GetFileAsync(id);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<TFile> CreateFileAsync(TFile file)
        {
            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer())
                return await _serviceFile.CreateFileAsync(file);

            throw ExceptionFactory.NoAccess();
        }
    }
}