using System.Threading.Tasks;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerStudent
    {
        Task<Student> GetStudent(int id);
    }
}