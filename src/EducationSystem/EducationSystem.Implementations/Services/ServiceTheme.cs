﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Services;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTheme : Service<ServiceTheme>, IServiceTheme
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Theme> _validatorTheme;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;
        private readonly IRepositoryTheme _repositoryTheme;

        public ServiceTheme(
            IMapper mapper,
            ILogger<ServiceTheme> logger,
            IHelperUserRole helperUserRole,
            IValidator<Theme> validatorTheme,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepositoryTheme repositoryTheme)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorTheme = validatorTheme;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryTheme = repositoryTheme;
        }

        public async Task<PagedData<Theme>> GetThemesAsync(OptionsTheme options, FilterTheme filter)
        {
            var (count, themes) = await _repositoryTheme.GetThemesAsync(filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<PagedData<Theme>> GetLecturerThemesAsync(int lecturerId, OptionsTheme options, FilterTheme filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var (count, themes) = await _repositoryTheme.GetLecturerThemesAsync(lecturerId, filter);

            return new PagedData<Theme>(themes.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<Theme> GetThemeAsync(int id, OptionsTheme options)
        {
            var theme = await _repositoryTheme.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            return Map(theme, options);
        }

        public async Task<Theme> GetLecturerThemeAsync(int id, int lecturerId, OptionsTheme options)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var theme = await _repositoryTheme.GetLecturerThemeAsync(id, lecturerId) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            return Map(theme, options);
        }

        public async Task DeleteThemeAsync(int id)
        {
            var theme = await _repositoryTheme.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && theme.Discipline?.Lecturers?.All(x => x.Id != user.Id) != false)
                throw _exceptionFactory.NoAccess();

            await _repositoryTheme.RemoveAsync(theme, true);
        }

        public async Task<int> CreateThemeAsync(Theme theme)
        {
            await _validatorTheme.ValidateAsync(theme.Format());

            var model = Mapper.Map<DatabaseTheme>(theme);

            model.Order = int.MaxValue;

            await _repositoryTheme.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateThemeAsync(int id, Theme theme)
        {
            await _validatorTheme.ValidateAsync(theme.Format());

            var model = await _repositoryTheme.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseTheme>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (model.Discipline?.Lecturers?.All(x => x.Id != user.Id) != false)
                throw _exceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseTheme>(theme), model);

            await _repositoryTheme.UpdateAsync(model, true);
        }

        private Theme Map(DatabaseTheme theme, OptionsTheme options)
        {
            return Mapper.Map<DatabaseTheme, Theme>(theme, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithQuestions)
                        d.Questions = Mapper.Map<List<Question>>(s.Questions);
                });
            });
        }
    }
}