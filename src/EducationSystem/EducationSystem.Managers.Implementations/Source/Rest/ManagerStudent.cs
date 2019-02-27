using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public sealed class ManagerStudent : Manager<ManagerStudent>, IManagerStudent
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryStudent _repositoryStudent;

        public ManagerStudent(
            IMapper mapper,
            ILogger<ManagerStudent> logger,
            IUserHelper userHelper,
            IRepositoryStudent repositoryStudent)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryStudent = repositoryStudent;
        }

        public PagedData<Student> GetStudents(OptionsStudent options, Filter filter)
        {
            var (count, students) = _repositoryStudent.GetStudents(filter);

            return new PagedData<Student>(students.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options, Filter filter)
        {
            var (count, students) = _repositoryStudent.GetStudentsByGroupId(groupId, filter);

            return new PagedData<Student>(students.Select(x => Map(x, options)).ToList(), count);
        }

        public Student GetStudentById(int id, OptionsStudent options)
        {
            if (!_userHelper.IsStudent(id))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(id),
                    Messages.User.NotStudentPublic);

            var student = _repositoryStudent.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Student.NotFoundById(id),
                    Messages.Student.NotFoundPublic);

            return Mapper.Map<Student>(Map(student, options));
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