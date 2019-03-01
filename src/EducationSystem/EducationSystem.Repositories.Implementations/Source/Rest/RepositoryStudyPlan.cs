using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
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