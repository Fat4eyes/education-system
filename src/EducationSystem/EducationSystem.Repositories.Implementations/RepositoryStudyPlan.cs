using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryStudyPlan : RepositoryReadOnly<DatabaseStudyPlan>, IRepositoryStudyPlan
    {
        public RepositoryStudyPlan(DatabaseContext context)
            : base(context) { }

        public Task<DatabaseStudyPlan> GetStudyPlanByStudentId(int studentId)
        {
            return AsQueryable()
                .FirstOrDefaultAsync(a => a.Groups
                    .Any(b => b.GroupStudents
                    .Any(c => c.StudentId == studentId)));
        }
    }
}