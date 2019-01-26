﻿using System.Linq;
using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Repositories.Interfaces.Source;

namespace EducationSystem.Repositories.Implementations.Source
{
    public class RepositoryReadOnly<TModel> : IRepositoryReadOnly<TModel>
        where TModel : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        public RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        public IQueryable<TModel> AsQueryable() => Context.Set<TModel>();

        public IEnumerable<TModel> GetAll()
        {
            return Context
                .Set<TModel>()
                .AsEnumerable();
        }

        public TModel GetById(int id)
        {
            return Context
                .Set<TModel>()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}