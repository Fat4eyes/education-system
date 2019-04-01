using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryStudyProfile : RepositoryReadOnly<DatabaseStudyProfile>, IRepositoryStudyProfile
    {
        public RepositoryStudyProfile(DatabaseContext context)
            : base(context) { }

        public DatabaseStudyProfile GetStudyProfileByStudentId(int studentId)
        {
            return AsQueryable()
                .FirstOrDefault(a => a.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId))));
        }
    }
}