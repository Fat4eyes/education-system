using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryStudyProfile : RepositoryReadOnly<DatabaseStudyProfile>, IRepositoryStudyProfile
    {
        public RepositoryStudyProfile(DatabaseContext context)
            : base(context) { }

        public DatabaseStudyProfile GetStudyProfileByStudentId(int studentId)
        {
            return AsQueryable().FirstOrDefault(
                a => a.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId))));
        }
    }
}