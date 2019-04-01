﻿using System.Linq;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models.Basics;
using EducationSystem.Repositories.Interfaces.Basics;

namespace EducationSystem.Repositories.Implementations.Basics
{
    public abstract class RepositoryReadOnly<TModel> : IRepositoryReadOnly<TModel>
        where TModel : DatabaseModel
    {
        protected DatabaseContext Context { get; }

        protected RepositoryReadOnly(DatabaseContext context)
        {
            Context = context;
        }

        protected IQueryable<TModel> AsQueryable()  => Context.Set<TModel>();

        public TModel GetById(int id) => AsQueryable()
            .FirstOrDefault(x => x.Id == id);
    }
}