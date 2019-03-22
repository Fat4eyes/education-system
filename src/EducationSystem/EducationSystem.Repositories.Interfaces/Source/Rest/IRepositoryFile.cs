using System;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryFile : IRepository<DatabaseFile>
    {
        DatabaseFile GetByGuid(Guid guid);

        bool IsFilesExists(List<int> fileIds);
    }
}