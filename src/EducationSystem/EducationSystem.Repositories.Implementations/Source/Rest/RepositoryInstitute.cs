using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryInstitute : RepositoryReadOnly<DatabaseInstitute, OptionsInstitute>, IRepositoryInstitute
    {
        public RepositoryInstitute(EducationSystemDatabaseContext context)
            : base(context) { }

        public DatabaseInstitute GetInstituteByStudentId(int studentId, OptionsInstitute options)
        {
            return IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(a => a.StudyProfiles
                    .Any(b => b.StudyPlans
                    .Any(c => c.Groups
                    .Any(d => d.GroupStudents
                    .Any(e => e.StudentId == studentId)))));
        }
    }
}