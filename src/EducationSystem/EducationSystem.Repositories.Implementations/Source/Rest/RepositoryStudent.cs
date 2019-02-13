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
    public class RepositoryStudent : RepositoryReadOnly<DatabaseUser, OptionsStudent>, IRepositoryStudent
    {
        public RepositoryStudent(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseUser> Students) GetStudents(OptionsStudent options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public (int Count, List<DatabaseUser> Students) GetStudentsByGroupId(int groupId, OptionsStudent options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .Where(x => x.StudentGroup.GroupId == groupId)
                .ApplyPaging(options);

        public DatabaseUser GetStudentById(int id, OptionsStudent options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.Id == id);

        protected override IQueryable<DatabaseUser> IncludeByOptions(IQueryable<DatabaseUser> query, OptionsStudent options)
        {
            if (options.WithGroup)
            {
                query = query
                    .Include(x => x.StudentGroup)
                    .ThenInclude(x => x.Group);
            }

            if (options.WithRoles)
            {
                query = query
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role);
            }

            if (options.WithTestResults)
                query = query.Include(x => x.TestResults);

            return query;
        }
    }
}