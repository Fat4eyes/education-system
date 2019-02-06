using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryTestResult : RepositoryReadOnly<DatabaseTestResult>, IRepositoryTestResult
    {
        public RepositoryTestResult(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResultByUserId(int userId, OptionsTestResult options)
        {
            var query = GetQueryableByOptions(options);

            if (options.ProfileId.HasValue)
            {
                query = query.Where(x => x.User.StudentGroup.Group.StudyPlan.StudyProfileId == options.ProfileId);
            }

            if (options.DisciplineId.HasValue)
            {
                query = query.Where(x => x.Test.DisciplineId == options.DisciplineId);
            }

            if (options.GroupId.HasValue)
            {
                query = query.Where(x => x.User.StudentGroup.GroupId == options.GroupId);
            }

            if (options.TestId.HasValue)
            {
                query = query.Where(x => x.TestId == options.TestId);
            }

            return query.ApplyPaging(options);
        }

        private IQueryable<DatabaseTestResult> GetQueryableByOptions(OptionsTestResult options)
        {
            var query = AsQueryable();

            if (options.WithTest)
            {
                query = query.Include(x => x.Test);
            }

            return query;
        }
    }
}