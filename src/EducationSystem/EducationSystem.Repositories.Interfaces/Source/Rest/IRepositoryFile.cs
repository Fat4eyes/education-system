using System;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryFile : IRepository<DatabaseFile>
    {
        DatabaseFile GetByGuid(Guid guid);
    }
}