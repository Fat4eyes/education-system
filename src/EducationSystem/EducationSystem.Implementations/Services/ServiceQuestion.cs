using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
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
    public sealed class ServiceQuestion : Service<ServiceQuestion>, IServiceQuestion
    {
        private readonly IHelperUserRole _helperUserRole;
        private readonly IValidator<Question> _validatorQuestion;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;

        private readonly IRepositoryAnswer _repositoryAnswer;
        private readonly IRepositoryProgram _repositoryProgram;
        private readonly IRepositoryQuestion _repositoryQuestion;
        private readonly IRepositoryProgramData _repositoryProgramData;

        public ServiceQuestion(
            IMapper mapper,
            ILogger<ServiceQuestion> logger,
            IHelperUserRole helperUserRole,
            IValidator<Question> validatorQuestion,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepositoryAnswer repositoryAnswer,
            IRepositoryProgram repositoryProgram,
            IRepositoryQuestion repositoryQuestion,
            IRepositoryProgramData repositoryProgramData)
            : base(mapper, logger)
        {
            _helperUserRole = helperUserRole;
            _validatorQuestion = validatorQuestion;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryAnswer = repositoryAnswer;
            _repositoryProgram = repositoryProgram;
            _repositoryQuestion = repositoryQuestion;
            _repositoryProgramData = repositoryProgramData;
        }

        public async Task<PagedData<Question>> GetQuestionsAsync(OptionsQuestion options, FilterQuestion filter)
        {
            var (count, questions) = await _repositoryQuestion.GetQuestionsAsync(filter);

            return new PagedData<Question>(questions.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<PagedData<Question>> GetLecturerQuestionsAsync(int lecturerId, OptionsQuestion options, FilterQuestion filter)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var (count, questions) = await _repositoryQuestion.GetLecturerQuestionsAsync(lecturerId, filter);

            return new PagedData<Question>(questions.Select(x => Map(x, options)).ToList(), count);
        }

        public async Task<Question> GetQuestionAsync(int id, OptionsQuestion options)
        {
            var question = await _repositoryQuestion.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            return Map(question, options);
        }

        public async Task<Question> GetLecturerQuestionAsync(int id, int lecturerId, OptionsQuestion options)
        {
            await _helperUserRole.CheckRoleLecturerAsync(lecturerId);

            var question = await _repositoryQuestion.GetLecturerQuestionAsync(id, lecturerId) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            return Map(question, options);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repositoryQuestion.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && question.Theme?.Discipline?.Lecturers.All(x => x.LecturerId != user.Id) != false)
                throw _exceptionFactory.NoAccess();

            await _repositoryQuestion.RemoveAsync(question, true);
        }

        public async Task<int> CreateQuestionAsync(Question question)
        {
            await _validatorQuestion.ValidateAsync(question.Format());

            var model = Mapper.Map<DatabaseQuestion>(question);

            model.Order = int.MaxValue;

            switch (question.Type)
            {
                case QuestionType.OpenedOneString:
                case QuestionType.ClosedOneAnswer:
                case QuestionType.ClosedManyAnswers:
                    model.Answers = Mapper.Map<List<DatabaseAnswer>>(question.Answers);
                    break;
                case QuestionType.WithProgram:
                    model.Program = Mapper.Map<DatabaseProgram>(question.Program);
                    break;
            }

            await _repositoryQuestion.AddAsync(model, true);

            return model.Id;
        }

        public async Task UpdateQuestionAsync(int id, Question question)
        {
            await _validatorQuestion.ValidateAsync(question.Format());

            var model = await _repositoryQuestion.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseQuestion>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (model.Theme?.Discipline?.Lecturers?.All(x => x.LecturerId != user.Id) != false)
                throw _exceptionFactory.NoAccess();

            Mapper.Map(Mapper.Map<DatabaseQuestion>(question), model);

            if (model.Answers.Any())
                await _repositoryAnswer.RemoveAsync(model.Answers, true);

            model.Answers = null;

            await _repositoryQuestion.UpdateAsync(model, true);

            switch (question.Type)
            {
                case QuestionType.OpenedManyStrings:
                {
                    model.Program = null;

                    await _repositoryQuestion.UpdateAsync(model, true);

                    break;
                }
                case QuestionType.OpenedOneString:
                case QuestionType.ClosedOneAnswer:
                case QuestionType.ClosedManyAnswers:
                {
                    model.Program = null;

                    await _repositoryQuestion.UpdateAsync(model, true);

                    model.Answers = Mapper.Map<List<DatabaseAnswer>>(question.Answers);

                    await _repositoryAnswer.AddAsync(model.Answers, true);

                    break;
                }
                case QuestionType.WithProgram when model.Program == null:
                {
                    model.Program = Mapper.Map<DatabaseProgram>(question.Program);

                    await _repositoryProgram.AddAsync(model.Program, true);

                    break;
                }
                case QuestionType.WithProgram:
                {
                    Mapper.Map(Mapper.Map<DatabaseProgram>(question.Program), model.Program);

                    await _repositoryProgram.UpdateAsync(model.Program, true);

                    if (model.Program.ProgramDatas.Any())
                        await _repositoryProgramData.RemoveAsync(model.Program.ProgramDatas, true);

                    model.Program.ProgramDatas = Mapper.Map<List<DatabaseProgramData>>(question.Program.ProgramDatas);

                    await _repositoryProgramData.AddAsync(model.Program.ProgramDatas, true);

                    break;
                }
            }
        }

        private Question Map(DatabaseQuestion question, OptionsQuestion options)
        {
            return Mapper.Map<DatabaseQuestion, Question>(question, x =>
            {
                x.AfterMap((s, d) =>
                {
                    if (options.WithAnswers)
                        d.Answers = Mapper.Map<List<Answer>>(s.Answers);
                });
            });
        }
    }
}