﻿using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Extensions.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public sealed class RepositoryDiscipline : RepositoryReadOnly<DatabaseDiscipline>, IRepositoryDiscipline
    {
        public RepositoryDiscipline(DatabaseContext context)
            : base(context) { }

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplines(FilterDiscipline filter)
        {
            var query = AsQueryable();

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPaging(filter);
        }

        public (int Count, List<DatabaseDiscipline> Disciplines) GetDisciplinesForStudent(int studentId, FilterDiscipline filter)
        {
            var query = AsQueryable()
                .Where(x => x.Themes.Any(y => y.Questions.Any()))
                .Where(x => x.Tests.Any(y => y.IsActive == 1 && y.TestThemes.Any(z => z.Theme.Questions.Any())))
                .Where(x => x.StudyProfiles
                    .Any(a => a.StudyProfile.StudyPlans
                    .Any(b => b.Groups
                    .Any(c => c.GroupStudents
                    .Any(d => d.StudentId == studentId)))));

            if (string.IsNullOrWhiteSpace(filter.Name) == false)
                query = query.Where(x => x.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            return query.ApplyPaging(filter);
        }
    }
}