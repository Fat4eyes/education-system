using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryTestResult : RepositoryReadOnly<DatabaseTestResult, OptionsTestResult>, IRepositoryTestResult
    {
        public RepositoryTestResult(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResults(OptionsTestResult options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByStudentId(int studentId, OptionsTestResult options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .Where(x => x.UserId == studentId)
                .ApplyPaging(options);

        public DatabaseTestResult GetTestResultById(int id, OptionsTestResult options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.TestId == id);

        protected override IQueryable<DatabaseTestResult> IncludeByOptions(IQueryable<DatabaseTestResult> query, OptionsTestResult options)
        {
            if (options.WithTest)
                query = query.Include(x => x.Test);

            if (options.WithThemes)
            {
                query = query
                    .Include(x => x.Test)
                    .ThenInclude(x => x.TestThemes)
                    .ThenInclude(x => x.Theme);
            }

            if (options.WithGivenAnswers)
                query = query.Include(x => x.GivenAnswers);

            return query;
        }

        protected override IQueryable<DatabaseTestResult> FilterByOptions(IQueryable<DatabaseTestResult> query, OptionsTestResult options)
        {
            if (options.ProfileId.HasValue)
                query = query.Where(x => x.User.StudentGroup.Group.StudyPlan.StudyProfileId == options.ProfileId);

            if (options.DisciplineId.HasValue)
                query = query.Where(x => x.Test.DisciplineId == options.DisciplineId);

            if (options.GroupId.HasValue)
                query = query.Where(x => x.User.StudentGroup.GroupId == options.GroupId);

            if (options.TestId.HasValue)
                query = query.Where(x => x.TestId == options.TestId);

            return query;
        }
    }
}