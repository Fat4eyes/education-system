using System;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryFile : IRepository<DatabaseFile>
    {
        Task<DatabaseFile> GetByGuid(Guid guid);

        Task<bool> IsFileExists(int id);
    }
}