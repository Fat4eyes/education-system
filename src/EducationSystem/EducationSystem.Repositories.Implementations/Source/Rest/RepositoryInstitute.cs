using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public sealed class RepositoryInstitute : RepositoryReadOnly<DatabaseInstitute>, IRepositoryInstitute
    {
        public RepositoryInstitute(DatabaseContext context)
            : base(context) { }

        public DatabaseInstitute GetInstituteByStudentId(int studentId)
        {
            return AsQueryable()
                .FirstOrDefault(a => a.StudyProfiles
                    .Any(b => b.StudyPlans
                    .Any(c => c.Groups
                    .Any(d => d.GroupStudents
                    .Any(e => e.StudentId == studentId)))));
        }
    }
}