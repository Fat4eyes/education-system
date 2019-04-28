﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerDiscipline : Manager<ManagerDiscipline>, IManagerDiscipline
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IRepositoryDiscipline _repositoryDiscipline;

        public ManagerDiscipline(
            IMapper mapper,
            ILogger<ManagerDiscipline> logger,
            IHelperUserRole helperUserRole,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _repositoryDiscipline = repositoryDiscipline;
        }

        public Task<PagedData<Discipline>> GetDisciplines(OptionsDiscipline options, FilterDiscipline filter)
        {
            var (count, disciplines) = _repositoryDiscipline.GetDisciplines(filter);

            var items = disciplines
                .Select(x => Map(x, options))
                .ToList();

            return Task.FromResult(new PagedData<Discipline>(items, count));
        }

        public Task<PagedData<Discipline>> GetDisciplinesByStudentId(int studentId, OptionsDiscipline options, FilterDiscipline filter)
        {
            _helperUserRole.CheckRoleStudent(studentId);

            var (count, disciplines) = _repositoryDiscipline.GetDisciplinesForStudent(studentId, filter);

            var items = disciplines
                .Select(x => MapForStudent(x, options))
                .ToList();

            return Task.FromResult(new PagedData<Discipline>(items, count));
        }

        public Task<Discipline> GetDiscipline(int id, OptionsDiscipline options)
        {
            var discipline = _repositoryDiscipline.GetById(id) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Дисциплина не найдена. Идентификатор дисциплины: {id}.",
                    $"Дисциплина не найдена.");

            return Task.FromResult(Map(discipline, options));
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

                    if (options.WithLecturers)
                        d.Lecturers = Mapper.Map<List<User>>(s.Lecturers.Select(y => y.Lecturer));
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