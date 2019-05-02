using System;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces.Repositories.Basics;

namespace EducationSystem.Interfaces.Repositories
{
    public interface IRepositoryFile : IRepository<DatabaseFile>
    {
        Task<DatabaseFile> GetFileAsync(Guid guid);
    }
}