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
    public sealed class ManagerMaterial : Manager, IManagerMaterial
    {
        private readonly IServiceMaterial _serviceMaterial;

        public ManagerMaterial(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceMaterial serviceMaterial)
            : base(
                executionContext,
                exceptionFactory)
        {
            _serviceMaterial = serviceMaterial;
        }

        public async Task<PagedData<Material>> GetMaterialsAsync(FilterMaterial filter)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceMaterial.GetMaterialsAsync(filter);

            if (CurrentUser.IsLecturer())
                return await _serviceMaterial.GetLecturerMaterialsAsync(CurrentUser.Id, filter);

            throw ExceptionFactory.NoAccess();
        }

        public async Task<Material> GetMaterialAsync(int id)
        {
            if (CurrentUser.IsAdmin())
                return await _serviceMaterial.GetMaterialAsync(id);

            if (CurrentUser.IsLecturer())
                return await _serviceMaterial.GetLecturerMaterialAsync(id, CurrentUser.Id);

            if (CurrentUser.IsStudent())
                return await _serviceMaterial.GetStudentMaterialAsync(id, CurrentUser.Id);

            throw ExceptionFactory.NoAccess();
        }

        public async Task DeleteMaterialAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceMaterial.DeleteMaterialAsync(id);
        }

        public async Task<int> CreateMaterialAsync(Material material)
        {
            if (CurrentUser.IsAdmin() || CurrentUser.IsLecturer())
                return await _serviceMaterial.CreateMaterialAsync(material);

            throw ExceptionFactory.NoAccess();
        }

        public async Task UpdateMaterialAsync(int id, Material material)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            await _serviceMaterial.UpdateMaterialAsync(id, material);
        }
    }
}