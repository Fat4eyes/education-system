using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerStudent : Manager<ManagerStudent>, IManagerStudent
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryStudent RepositoryStudent { get; }

        public ManagerStudent(
            IMapper mapper,
            ILogger<ManagerStudent> logger,
            IUserHelper userHelper,
            IRepositoryStudent repositoryStudent)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryStudent = repositoryStudent;
        }

        public PagedData<Student> GetStudents(OptionsStudent options)
        {
            var (count, students) = RepositoryStudent.GetStudents(options);

            return new PagedData<Student>(Mapper.Map<List<Student>>(students), count);
        }

        public PagedData<Student> GetStudentsByGroupId(int groupId, OptionsStudent options)
        {
            var (count, students) = RepositoryStudent.GetStudentsByGroupId(groupId, options);

            return new PagedData<Student>(Mapper.Map<List<Student>>(students), count);
        }

        public Student GetStudentById(int id, OptionsStudent options)
        {
            if (!UserHelper.IsStudent(id))
                throw new EducationSystemPublicException(
                    $"Пользователь не является студентом. Идентификатор: {id}.");

            var student = RepositoryStudent.GetStudentById(id, options) ??
               throw new EducationSystemException(
                   $"Студент (пользователь) не найден. Идентификатор: {id}.",
                   new EducationSystemPublicException("Студент (пользователь) не найден."));

            return Mapper.Map<Student>(student);
        }
    }
}