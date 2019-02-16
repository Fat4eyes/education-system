using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryDiscipline : RepositoryReadOnly<DatabaseDiscipline, OptionsDiscipline>, IRepositoryDiscipline
    {
        public RepositoryDiscipline(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(OptionsDiscipline options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesByStudentId(int studentId, OptionsDiscipline options)
        {
            var query = FilterByOptions(IncludeByOptions(AsQueryable(), options), options);

            query = query
                .Where(x => x.Tests.Any(y => y.IsActive == 1))
                .Where(x => x.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))));

            return query.ApplyPaging(options);
        }

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