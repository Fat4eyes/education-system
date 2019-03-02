using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        private readonly IHelperUser _helperUser;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ManagerDiscipline(
            IMapper mapper,
            ILogger<ManagerDiscipline> logger,
            IHelperUser helperUser,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            _helperUser = helperUser;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public PagedData<Discipline> GetDisciplines(OptionsDiscipline options, FilterDiscipline filter)
        {
            var (count, disciplines) = _repositoryDiscipline.GetDisciplines(filter);

            return new PagedData<Discipline>(disciplines.Select(x => Map(x, options)).ToList(), count);
        }

        public PagedData<Discipline> GetDisciplinesForStudent(int studentId, OptionsDiscipline options, FilterDiscipline filter)
        {
            _helperUser.CheckRoleStudent(studentId);

            var (count, disciplines) = _repositoryDiscipline.GetDisciplinesForStudent(studentId, filter);

            return new PagedData<Discipline>(disciplines.Select(x => MapForStudent(x, options)).ToList(), count);
        }

        public Discipline GetDisciplineById(int id, OptionsDiscipline options)
        {
            var discipline = _repositoryDiscipline.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Дисциплина не найдена. Идентификатор дисциплины: {id}.",
                    $"Дисциплина не найдена.");

            return Map(discipline, options);
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