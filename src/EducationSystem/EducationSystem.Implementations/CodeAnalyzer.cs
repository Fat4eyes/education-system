using System;
using System.Threading.Tasks;
using AutoMapper;
using CodeExecutionSystem.Contracts.Abstractions;
using CodeExecutionSystem.Contracts.Data;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Programs;
using Microsoft.Extensions.Logging;
using CodeAnalysisResult = EducationSystem.Models.Code.CodeAnalysisResult;

namespace EducationSystem.Implementations
{
    public sealed class CodeAnalyzer : ICodeAnalyzer
    {
        private readonly IMapper _mapper;
        private readonly IContext _context;
        private readonly ILogger<CodeAnalyzer> _logger;
        private readonly ICodeAnalysisApi _codeAnalysisApi;
        private readonly IRepository<DatabaseProgram> _repositoryProgram;

        public CodeAnalyzer(
            IMapper mapper,
            IContext context,
            ILogger<CodeAnalyzer> logger,
            ICodeAnalysisApi codeAnalysisApi,
            IRepository<DatabaseProgram> repositoryProgram)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _codeAnalysisApi = codeAnalysisApi;
            _repositoryProgram = repositoryProgram;
        }

        public async Task<CodeAnalysisResult> AnalyzeAsync(Program program)
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

                var response = await _codeAnalysisApi.AnalyzeCodeAsync(code);

                return _mapper.Map<CodeAnalysisResult>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Не удалось выполнить запрос на анализ кода. " +
                    $"Идентификатор пользователя: {user.Id}. " +
                    $"Идентификатор программы: {program.Id}.", ex);

                throw ExceptionHelper.CreatePublicException("Не удалось выполнить запрос на анализ кода.");
            }
        }
    }
}