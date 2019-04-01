using System.Linq;
using EducationSystem.Database.Models;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryStudyPlan : RepositoryReadOnly<DatabaseStudyPlan>, IRepositoryStudyPlan
    {
        public RepositoryStudyPlan(DatabaseContext context)
            : base(context) { }

        public DatabaseStudyPlan GetStudyPlanByStudentId(int studentId)
        {
            return AsQueryable()
                .FirstOrDefault(a => a.Groups
                    .Any(b => b.GroupStudents
                    .Any(c => c.StudentId == studentId)));
        }
    }
}