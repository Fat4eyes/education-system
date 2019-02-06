﻿using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryTestResult : RepositoryReadOnly<DatabaseTestResult, OptionsTestResult>, IRepositoryTestResult
    {
        public RepositoryTestResult(EducationSystemDatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResults(OptionsTestResult options)
        {
            return FilterByOptions(GetQueryableWithInclusions(options), options).ApplyPaging(options);
        }

        public (int Count, List<DatabaseTestResult> TestResults) GetTestResultsByUserId(int userId, OptionsTestResult options)
        {
            return FilterByOptions(GetQueryableWithInclusions(options), options)
                .Where(x => x.UserId == userId)
                .ApplyPaging(options);
        }

        public DatabaseTestResult GetTestResultById(int id, OptionsTestResult options)
        {
            return GetQueryableWithInclusions(options).FirstOrDefault(x => x.TestId == id);
        }

        protected override IQueryable<DatabaseTestResult> GetQueryableWithInclusions(OptionsTestResult options)
        {
            var query = AsQueryable();

            if (options.WithTest)
            {
                query = query.Include(x => x.Test);
            }

            if (options.WithThemes)
            {
                query = query
                    .Include(x => x.Test)
                        .ThenInclude(x => x.TestThemes)
                            .ThenInclude(x => x.Theme);
            }

            return query;
        }

        protected override IQueryable<DatabaseTestResult> FilterByOptions(IQueryable<DatabaseTestResult> query, OptionsTestResult options)
        {
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

            return query;
        }
    }
}