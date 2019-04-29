using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations
{
    public sealed class RepositoryInstitute : RepositoryReadOnly<DatabaseInstitute>, IRepositoryInstitute
    {
        public RepositoryInstitute(DatabaseContext context)
            : base(context) { }

        public Task<DatabaseInstitute> GetInstituteByStudentId(int studentId)
        {
            return AsQueryable()
                .FirstOrDefaultAsync(a => a.StudyProfiles
                    .Any(b => b.StudyPlans
                    .Any(c => c.Groups
                    .Any(d => d.GroupStudents
                    .Any(e => e.StudentId == studentId)))));
        }
    }
}