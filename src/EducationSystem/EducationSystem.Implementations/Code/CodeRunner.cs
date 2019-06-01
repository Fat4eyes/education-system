using System;
using System.Threading.Tasks;
using AutoMapper;
using CodeExecutionSystem.Contracts.Abstractions;
using CodeExecutionSystem.Contracts.Data;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Code;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Programs;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Code
{
    public sealed class CodeRunner : ICodeRunner
    {
        private readonly IMapper _mapper;
        private readonly IContext _context;
        private readonly ILogger<CodeExecutor> _logger;
        private readonly ICodeTesterApi _codeTesterApi;
        private readonly IRepository<DatabaseProgram> _repositoryProgram;

        public CodeRunner(
            IMapper mapper,
            IContext context,
            ILogger<CodeExecutor> logger,
            ICodeTesterApi codeTesterApi,
            IRepository<DatabaseProgram> repositoryProgram)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _codeTesterApi = codeTesterApi;
            _repositoryProgram = repositoryProgram;
        }

        public async Task<CodeRunningResult> RunAsync(Program program)
        {
            if (program == null)
                throw ExceptionHelper.CreatePublicException("Не указана программа.");

            if (string.IsNullOrWhiteSpace(program.Source))
                throw ExceptionHelper.CreatePublicException("Не указан исходный код программы.");

            var model = await _repositoryProgram.FindFirstAsync(new ProgramsById(program.Id)) ??
                throw ExceptionHelper.NotFound<DatabaseProgram>(program.Id);

            var user = await _context.GetCurrentUserAsync();

            if (user.IsStudent() && !new ProgramsByStudentId(user.Id).IsSatisfiedBy(model))
                throw ExceptionHelper.NoAccess();

            if (user.IsLecturer() && !new ProgramsByLecturerId(user.Id).IsSatisfiedBy(model))
                throw ExceptionHelper.NoAccess();

            _mapper.Map(_mapper.Map<Program>(model), program);

            try
            {
                var code = _mapper.Map<TestingCode>(program);

                var response = await _codeTesterApi.TestCode(code);

                return _mapper.Map<CodeRunningResult>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Не удалось выполнить запрос на запуск кода. " +
                    $"Идентификатор пользователя: {user.Id}. " +
                    $"Идентификатор программы: {program.Id}.", ex);

                throw ExceptionHelper.CreatePublicException("Не удалось выполнить запрос на запуск кода.");
            }
        }
    }
}