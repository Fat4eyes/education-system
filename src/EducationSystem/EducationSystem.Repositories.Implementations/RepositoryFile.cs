﻿using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public class RepositoryFile : Repository<DatabaseFile>, IRepositoryFile
    {
        public RepositoryFile(DatabaseContext context)
            : base(context) { }

        public DatabaseFile GetByGuid(Guid guid)
        {
            return AsQueryable().FirstOrDefault(x => string.Equals(
                x.Guid, guid.ToString(), StringComparison.CurrentCultureIgnoreCase));
        }

        public bool IsFileExists(int id)
        {
            return AsQueryable().Any(x => x.Id == id);
        }
    }
}