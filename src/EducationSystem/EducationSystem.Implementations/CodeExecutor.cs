using System;
using System.Threading.Tasks;
using AutoMapper;
using CodeExecutionSystem.Contracts.Abstractions;
using CodeExecutionSystem.Contracts.Data;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations
{
    public sealed class CodeExecutor : ICodeExecutor
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CodeExecutor> _logger;
        private readonly ICodeExecutionApi _codeExecutionApi;
        private readonly IExecutionContext _executionContext;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IRepository<DatabaseProgram> _repositoryProgram;

        public CodeExecutor(
            IMapper mapper,
            ILogger<CodeExecutor> logger,
            ICodeExecutionApi codeExecutionApi,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseProgram> repositoryProgram)
        {
            _mapper = mapper;
            _logger = logger;
            _codeExecutionApi = codeExecutionApi;
            _executionContext = executionContext;
            _exceptionFactory = exceptionFactory;
            _repositoryProgram = repositoryProgram;
        }

        public async Task<CodeExecutionResponse> ExecuteAsync(CodeExecutionRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Source))
                throw ExceptionHelper.CreatePublicException("Не указан исходный код программы.");

            if (request.Program == null)
                throw ExceptionHelper.CreatePublicException("Не указана программа (окружение программы).");

            await AddProgramToRequestAsync(request);

            try
            {
                var code = _mapper.Map<TestingCode>(request);

                // На данный момент интеграция не работает.
                // Сейчас используем фейковые ответы.

                var response = await _codeExecutionApi.ExecuteCodeAsync(code);

                return _mapper.Map<CodeExecutionResponse>(response);
            }
            catch (Exception ex)
            {
                var user = await _executionContext.GetCurrentUserAsync();

                _logger.LogError(
                    $"Не удалось выполнить запрос на выполнение кода. " +
                    $"Идентификатор пользователя: {user.Id}. " +
                    $"Идентификатор программы: {request.Program.Id}.", ex);

                throw ExceptionHelper.CreatePublicException("Не удалось выполнить запрос на выполнение кода.");
            }
        }

        public async Task AddProgramToRequestAsync(CodeExecutionRequest request)
        {
            var program = await _repositoryProgram.FindFirstAsync(new ProgramsById(request.Program.Id)) ??
                throw _exceptionFactory.NotFound<DatabaseProgram>(request.Program.Id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsStudent() && !new ProgramsByStudentId(user.Id).IsSatisfiedBy(program))
                throw _exceptionFactory.NoAccess();

            if (user.IsLecturer() && !new ProgramsByLecturerId(user.Id).IsSatisfiedBy(program))
                throw _exceptionFactory.NoAccess();

            request.Program = _mapper.Map<Program>(program);
        }
    }
}