using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
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
    public sealed class ManagerDiscipline : Manager<ManagerDiscipline>, IManagerDiscipline
    {
        private readonly IUserHelper _userHelper;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ManagerDiscipline(
            IMapper mapper,
            ILogger<ManagerDiscipline> logger,
            IUserHelper userHelper,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            _userHelper = userHelper;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public PagedData<Discipline> GetDisciplines(OptionsDiscipline options, Filter filter)
        {
            var (count, disciplines) = _repositoryDiscipline.GetDisciplines(filter);

            return new PagedData<Discipline>(disciplines.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Discipline> GetDisciplinesByStudentId(int studentId, OptionsDiscipline options, Filter filter)
        {
            if (!_userHelper.IsStudent(studentId))
                throw ExceptionHelper.CreateException(
                    Messages.User.NotStudent(studentId),
                    Messages.User.NotStudentPublic);

            var (count, disciplines) = _repositoryDiscipline.GetDisciplinesByStudentId(studentId, filter);

            return new PagedData<Discipline>(disciplines.Select(x => MapForStudent(x, options)).ToList(), count);
        }

        public Discipline GetDisciplineById(int id, OptionsDiscipline options)
        {
            var discipline = _repositoryDiscipline.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    Messages.Discipline.NotFoundById(id),
                    Messages.Discipline.NotFoundPublic);

            return Mapper.Map<Discipline>(Map(discipline, options));
        }

        private Discipline Map(DatabaseDiscipline discipline, OptionsDiscipline options)
        {
            return Mapper.Map<DatabaseDiscipline, Discipline>(discipline, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithTests)
                        d.Tests = Mapper.Map<List<Test>>(s.Tests);

                    if (options.WithThemes)
                        d.Themes = Mapper.Map<List<Theme>>(s.Themes);
                });
            });
        }

        private Discipline MapForStudent(DatabaseDiscipline discipline, OptionsDiscipline options)
        {
            return Mapper.Map<DatabaseDiscipline, Discipline>(discipline, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithTests)
                    {
                        d.Tests = s.Tests
                            .Where(y => y.IsActive == 1)
                            .Where(y => y.TestThemes.IsNotEmpty(z => z.Theme?.Questions.IsNotEmpty() == true))
                            .Select(y => Mapper.Map<Test>(y))
                            .ToList();
                    }

                    if (options.WithThemes)
                    {
                        d.Themes = s.Themes
                            .Where(y => y.Questions.IsNotEmpty())
                            .Select(y => Mapper.Map<Theme>(y))
                            .ToList();
                    }
                });
            });
        }
    }
}