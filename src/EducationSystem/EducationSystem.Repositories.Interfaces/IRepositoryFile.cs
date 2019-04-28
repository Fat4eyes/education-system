using System;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Interfaces
{
    public interface IRepositoryFile : IRepository<DatabaseFile>
    {
        DatabaseFile GetByGuid(Guid guid);

        bool IsFileExists(int id);
    }
}