using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerDiscipline : Manager<ManagerDiscipline>, IManagerDiscipline
    {
        protected IUserHelper UserHelper { get; }
        protected IRepositoryDiscipline RepositoryDiscipline { get; }

        public ManagerDiscipline(
            IMapper mapper,
            ILogger<ManagerDiscipline> logger,
            IUserHelper userHelper,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            UserHelper = userHelper;
            RepositoryDiscipline = repositoryDiscipline;
        }

        public PagedData<Discipline> GetDisciplines(OptionsDiscipline options)
        {
            var (count, disciplines) = RepositoryDiscipline.GetDisciplines(options);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public PagedData<Discipline> GetDisciplinesByStudentId(int studentId, OptionsDiscipline options)
        {
            if (!UserHelper.IsStudent(studentId))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(studentId),
                    Messages.User.NotStudentPublic);

            var (count, disciplines) = RepositoryDiscipline.GetDisciplinesByStudentId(studentId, options);

            disciplines.ForEach(x => x.Tests = x.Tests
                .Where(y => y.IsActive == 1)
                .ToList());

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public Discipline GetDisciplineById(int id, OptionsDiscipline options)
        {
            var discipline = RepositoryDiscipline.GetDisciplineById(id, options) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Discipline.NotFoundById(id),
                    Messages.Discipline.NotFoundPublic);

            return Mapper.Map<Discipline>(discipline);
        }
    }
}