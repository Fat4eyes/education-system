using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Extensions;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Rest;
using EducationSystem.Specifications.Questions;
using EducationSystem.Specifications.Tests;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services
{
    public sealed class ServiceTestProcess : Service<ServiceTestProcess>, IServiceTestProcess
    {
        private readonly IHashComputer _hashComputer;
        private readonly IRepository<DatabaseTest> _repositoryTest;
        private readonly IRepository<DatabaseQuestion> _repositoryQuestion;

        public ServiceTestProcess(
            IMapper mapper,
            ILogger<ServiceTestProcess> logger,
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IHashComputer hashComputer,
            IRepository<DatabaseTest> repositoryTest,
            IRepository<DatabaseQuestion> repositoryQuestion)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            _hashComputer = hashComputer;
            _repositoryTest = repositoryTest;
            _repositoryQuestion = repositoryQuestion;
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            if (CurrentUser.IsNotStudent())
                throw ExceptionFactory.NoAccess();

            await ValidateTestAsync(id);

            var specification =
                new QuestionsByTestId(id) &
                new QuestionsForStudents() &
                new QuestionsByStudentId(CurrentUser.Id, false);

            var question = await _repositoryQuestion.FindFirstAsync(specification);

            return Mapper.Map<Question>(question);
        }

        public async Task<Question> ProcessQuestionAsync(int id, Question question)
        {
            if (CurrentUser.IsNotStudent())
                throw ExceptionFactory.NoAccess();

            await ValidateTestAsync(id);
            await ValidateQuestionAsync(id, question);

            var model = await _repositoryQuestion.FindFirstAsync(new QuestionsById(question.Id)) ??
                throw ExceptionFactory.NotFound<DatabaseQuestion>(question.Id);

            var result = Mapper.Map<Question>(model);

            model.Answers.ForEach(x =>
            {
                var a = result.Answers.FirstOrDefault(y => y.Id == x.Id);
                var b = question.Answers.FirstOrDefault(y => y.Id == x.Id);

                // Если такой ответ был указан.
                if (a != null && b != null)
                {
                    // Проставляем статус в зависимости от правильности.
                    a.Status = x.IsRight
                        ? AnswerStatus.Right
                        : AnswerStatus.Wrong;
                }

                // Если такой ответ не был указан (а он правильный).
                if (a != null && b == null && x.IsRight)
                    a.Status = AnswerStatus.Ignore;
            });

            // TODO: Дорабатывать.

            if (question.Save == true)
                await SaveQuestionAsync(model);

            return result;
        }

        private async Task ValidateQuestionAsync(int id, Question question)
        {
            if (question == null)
                throw ExceptionHelper.CreatePublicException("Не указан вопрос.");

            var model = await _repositoryQuestion.FindFirstAsync(new QuestionsById(question.Id)) ??
                throw ExceptionFactory.NotFound<DatabaseQuestion>(question.Id);

            var specification =
                new QuestionsByTestId(id) &
                new QuestionsForStudents() &
                new QuestionsByStudentId(CurrentUser.Id, false);

            if (specification.IsSatisfiedBy(model) == false)
                throw ExceptionFactory.NoAccess();

            var hash = await _hashComputer.ComputeForQuestionAsync(model);

            if (string.Equals(hash, question.Hash, StringComparison.InvariantCulture) == false)
                throw ExceptionFactory.NoAccess();

            if (QuestionTypes.Supported.Contains(model.Type) == false)
                throw ExceptionHelper.CreatePublicException("Данный тип вопроса не поддерживается.");

            if (model.Type == QuestionType.WithProgram)
                return;

            if (question.Answers.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Не указаны ответы на вопрос.");

            if (!question.Answers
                .Select(x => x.Id)
                .All(x => model.Answers
                    .Select(y => y.Id)
                    .Contains(x)))
            {
                throw ExceptionHelper.CreatePublicException("Один из указанных вариантов ответа не принадлежит вопросу.");
            }
        }

        private async Task ValidateTestAsync(int id)
        {
            var test = await _repositoryTest.FindFirstAsync(new TestsById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseTest>(id);

            var specification =
                new TestsById(id) &
                new TestsByStudentId(CurrentUser.Id) &
                new TestsForStudents();

            if (specification.IsSatisfiedBy(test) == false)
                throw ExceptionFactory.NoAccess();
        }

        private async Task SaveQuestionAsync(DatabaseQuestion question)
        {
            question.QuestionStudents.Add(new DatabaseQuestionStudent
            {
                Passed = true,
                StudentId = CurrentUser.Id
            });

            await _repositoryQuestion.UpdateAsync(question, true);
        }
    }
}