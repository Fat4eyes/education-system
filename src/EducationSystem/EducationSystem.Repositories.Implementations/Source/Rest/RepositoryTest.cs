using System;
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
    public class RepositoryTest : RepositoryReadOnly<DatabaseTest, OptionsTest>, IRepositoryTest
    {
        public RepositoryTest(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTest> Tests) GetTests(OptionsTest options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public (int Count, List<DatabaseTest> Tests) GetTestsByDisciplineId(int disciplineId, OptionsTest options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .Where(x => x.DisciplineId == disciplineId)
                .ApplyPaging(options);

        public DatabaseTest GetTetsById(int id, OptionsTest options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.Id == id);

        protected override IQueryable<DatabaseTest> IncludeByOptions(IQueryable<DatabaseTest> query, OptionsTest options)
        {
            if (options.WithThemes)
            {
                query = query
                    .Include(x => x.TestThemes)
                    .ThenInclude(x => x.Theme);
            }

            return query;
        }

        protected override IQueryable<DatabaseTest> FilterByOptions(IQueryable<DatabaseTest> query, OptionsTest options)
        {
            if (options.DisciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == options.DisciplineId);

            if (options.OnlyActive)
                query = query.Where(x => x.IsActive == 1);

            if (string.IsNullOrWhiteSpace(options.Name) == false)
                query = query.Where(x => x.Subject.Contains(options.Name, StringComparison.CurrentCultureIgnoreCase));

            return query;
        }
    }
}