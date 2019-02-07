using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryDiscipline : RepositoryReadOnly<DatabaseDiscipline, OptionsDiscipline>, IRepositoryDiscipline
    {
        public RepositoryDiscipline(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(OptionsDiscipline options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public DatabaseDiscipline GetDisciplineById(int id, OptionsDiscipline options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.Id == id);

        protected override IQueryable<DatabaseDiscipline> IncludeByOptions(IQueryable<DatabaseDiscipline> query, OptionsDiscipline options)
        {
            if (options.WithTests)
                query = query.Include(x => x.Tests);

            if (options.WithThemes)
                query = query.Include(x => x.Themes);

            return query;
        }
    }
}