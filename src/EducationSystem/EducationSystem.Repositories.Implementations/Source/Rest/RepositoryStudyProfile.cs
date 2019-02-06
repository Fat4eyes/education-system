using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryStudyProfile : RepositoryReadOnly<DatabaseStudyProfile, OptionsStudyProfile>, IRepositoryStudyProfile
    {
        public RepositoryStudyProfile(EducationSystemDatabaseContext context)
            : base(context) { }

        public DatabaseStudyProfile GetStudyProfileByUserId(int userId, OptionsStudyProfile options)
        {
            return GetQueryableWithInclusions(options)
                .FirstOrDefault(a => a.StudyPlans
                    .Any(b => b.Groups
                        .Any(c => c.GroupStudents
                            .Any(d => d.StudentId == userId))));
        }

        protected override IQueryable<DatabaseStudyProfile> GetQueryableWithInclusions(OptionsStudyProfile options)
        {
            var query = AsQueryable();

            if (options.WithInstitute)
            {
                query = query.Include(x => x.Institute);
            }

            return query;
        }
    }
}