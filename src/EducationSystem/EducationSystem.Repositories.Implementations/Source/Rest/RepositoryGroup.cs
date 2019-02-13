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
    public class RepositoryGroup : RepositoryReadOnly<DatabaseGroup, OptionsGroup>, IRepositoryGroup
    {
        public RepositoryGroup(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseGroup> Groups) GetGroups(OptionsGroup options) =>
            FilterByOptions(IncludeByOptions(AsQueryable(), options), options)
                .ApplyPaging(options);

        public DatabaseGroup GetGroupById(int id, OptionsGroup options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.Id == id);

        public DatabaseGroup GetGroupByStudentId(int studentId, OptionsGroup options) =>
            IncludeByOptions(AsQueryable(), options)
                .FirstOrDefault(x => x.GroupStudents.Any(y => y.Student.Id == studentId));

        protected override IQueryable<DatabaseGroup> FilterByOptions(IQueryable<DatabaseGroup> query, OptionsGroup options)
        {
            if (string.IsNullOrWhiteSpace(options.Name) == false)
                query = query.Where(x => x.Name.Contains(options.Name, StringComparison.CurrentCultureIgnoreCase));

            return query;
        }

        protected override IQueryable<DatabaseGroup> IncludeByOptions(IQueryable<DatabaseGroup> query, OptionsGroup options)
        {
            if (options.WithStudyPlan)
                query = query.Include(x => x.StudyPlan);

            return query;
        }
    }
}