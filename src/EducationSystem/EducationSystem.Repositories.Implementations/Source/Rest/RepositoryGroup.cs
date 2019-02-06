using System;
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
    public class RepositoryGroup : RepositoryReadOnly<DatabaseGroup>, IRepositoryGroup
    {
        public RepositoryGroup(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseGroup> Groups) GetGroups(OptionsGroup options)
        {
            var query = GetQueryableByOptions(options);

            if (string.IsNullOrWhiteSpace(options.Name) == false)
            {
                query = query.Where(x => x.Name.Contains(options.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            return query.ApplyPaging(options);
        }

        public DatabaseGroup GetGroupById(int id, OptionsGroup options)
        {
            return GetQueryableByOptions(options).FirstOrDefault(x => x.Id == id);
        }

        public DatabaseGroup GetGroupByUserId(int userId, OptionsGroup options)
        {
            return GetQueryableByOptions(options)
                .FirstOrDefault(x => x.GroupStudents.Any(y => y.Student.Id == userId));
        }

        private IQueryable<DatabaseGroup> GetQueryableByOptions(OptionsGroup options)
        {
            var query = AsQueryable();

            if (options.WithStudyPlan)
            {
                query = query.Include(x => x.StudyPlan);
            }

            return query;
        }
    }
}