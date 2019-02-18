using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryDiscipline : RepositoryReadOnly<DatabaseDiscipline>, IRepositoryDiscipline
    {
        public RepositoryDiscipline(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(Filter options) =>
                AsQueryable().ApplyPaging(options);

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesByStudentId(int studentId, Filter filter)
        {
            return AsQueryable()
                .Where(x => x.Themes.Any(y => y.Questions.Any()))
                .Where(x => x.Tests.Any(y => y.IsActive == 1 && y.TestThemes.Any(z => z.Theme.Questions.Any())))
                .Where(x => x.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))))
                .ApplyPaging(filter);
        }

        public DatabaseDiscipline GetDisciplineById(int id) => GetById(id);
    }
}