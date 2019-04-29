using System.Threading.Tasks;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperUserRole
    {
        Task CheckRoleStudent(int userId);
    }
}