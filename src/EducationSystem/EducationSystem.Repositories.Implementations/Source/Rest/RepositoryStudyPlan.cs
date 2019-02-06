using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryStudyPlan : RepositoryReadOnly<DatabaseStudyPlan>, IRepositoryStudyPlan
    {
        public RepositoryStudyPlan(EducationSystemDatabaseContext context)
            : base(context) { }

        public DatabaseStudyPlan GetStudyPlanByUserId(int userId, OptionsStudyPlan options)
        {
            return GetQueryableByOptions(options)
                .FirstOrDefault(a => a.Groups.Any(b => b.GroupStudents.Any(c => c.StudentId == userId)));
        }

        private IQueryable<DatabaseStudyPlan> GetQueryableByOptions(OptionsStudyPlan options)
        {
            var query = AsQueryable();

            if (options.WithStudyProfile)
            {
                query = query.Include(x => x.StudyProfile);
            }

            return query;
        }
    }
}