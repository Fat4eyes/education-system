﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Rest
{
    public sealed class ManagerStudent : Manager<ManagerStudent>, IManagerStudent
    {
        private readonly IHelperUser _helperUser;
        private readonly IRepositoryStudent _repositoryStudent;

        public ManagerStudent(
            IMapper mapper,
            ILogger<ManagerStudent> logger,
            IHelperUser helperUser,
            IRepositoryStudent repositoryStudent)
            : base(mapper, logger)
        {
            _helperUser = helperUser;
            _repositoryStudent = repositoryStudent;
        }

        public PagedData<Student> GetStudents(OptionsStudent options, FilterStudent filter)
        {
            var (count, students) = _repositoryStudent.GetStudents(filter);

            return new PagedData<Student>(students.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options, FilterStudent filter)
        {
            var (count, students) = _repositoryStudent.GetStudentsByGroupId(groupId, filter);

            return new PagedData<Student>(students.Select(x => Map(x, options)).ToList(), count);
        }

        public Student GetStudentById(int id, OptionsStudent options)
        {
            _helperUser.CheckRoleStudent(id);

            var student = _repositoryStudent.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Студент не найден. Идентификатор студента: {id}.",
                    $"Студент не найден.");

            return Map(student, options);
        }

        private Student Map(DatabaseUser student, OptionsStudent options)
        {
            return Mapper.Map<DatabaseUser, Student>(student, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithGroup)
                        d.Group = Mapper.Map<Group>(s.StudentGroup.Group);

                    if (options.WithTestResults)
                        d.TestResults = Mapper.Map<List<TestResult>>(s.TestResults);
                });
            });
        }
    }
}