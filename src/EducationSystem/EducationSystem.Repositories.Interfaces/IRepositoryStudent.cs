using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryStudent : IRepositoryReadOnly<DatabaseUser> { }
}