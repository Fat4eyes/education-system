using System.Threading.Tasks;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperUserRole
    {
        Task CheckRoleStudentAsync(int userId);

        Task CheckRoleLecturerAsync(int userId);
    }
}