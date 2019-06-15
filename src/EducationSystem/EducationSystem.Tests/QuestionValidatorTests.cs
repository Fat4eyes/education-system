using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Implementations.Validators.Questions;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Code;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Mapping.Profiles;
using EducationSystem.Models.Code;
using EducationSystem.Models.Rest;
using EducationSystem.Resolvers;
using EducationSystem.Specifications;
using EducationSystem.Tests.Helpers;
using Moq;
using Xunit;

namespace EducationSystem.Tests
{
    public class QuestionValidatorTests
    {
        private readonly IQuestionValidatorClosedOneAnswer _questionValidatorClosedOneAnswer;
        private readonly IQuestionValidatorClosedManyAnswers _questionValidatorClosedManyAnswers;
        private readonly IQuestionValidatorOpenedOneString _questionValidatorOpenedOneString;
        private readonly IQuestionValidatorWithProgram _questionValidatorWithProgram;

        private readonly Mock<IContext> _context = new Mock<IContext>();
        private readonly Mock<IHashComputer> _hashComputer = new Mock<IHashComputer>();
        private readonly Mock<IRepository<DatabaseQuestion>> _repositoryQuestion = new Mock<IRepository<DatabaseQuestion>>();
        private readonly Mock<ICodeRunner> _codeRunner = new Mock<ICodeRunner>();

        private readonly List<DatabaseQuestion> _questions = CreateDatabaseQuestions();

        private void ConfigureMapper(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<DatabaseQuestion, Question>()
                .ForMember(d => d.Answers, o => o.MapFrom(new ResolverQuestionAnswers(_context.Object)))
                .ForMember(d => d.Hash, o => o.MapFrom(new ResolverQuestionHash(_context.Object, _hashComputer.Object)))
                .ForMember(d => d.MaterialAnchors, o => o.MapFrom(s => s.MaterialAnchors.Select(x => x.MaterialAnchor)))
                .ForMember(d => d.TestId, o => o.Ignore())
                .ForMember(d => d.Right, o => o.Ignore());

            expression.AddProfile<ProfileAnswer>();
            expression.AddProfile<ProfileProgram>();
            expression.AddProfile<ProfileProgramData>();
        }

        public QuestionValidatorTests()
        {
            var mapper = new Mapper(new MapperConfiguration(ConfigureMapper));

            _questionValidatorClosedOneAnswer = new QuestionValidatorClosedOneAnswer(
                mapper,
                _context.Object,
                _hashComputer.Object,
                _repositoryQuestion.Object);

            _questionValidatorClosedManyAnswers = new QuestionValidatorClosedManyAnswers(
                mapper,
                _context.Object,
                _hashComputer.Object,
                _repositoryQuestion.Object);

            _questionValidatorOpenedOneString = new QuestionValidatorOpenedOneString(
                mapper,
                _context.Object,
                _hashComputer.Object,
                _repositoryQuestion.Object);

            _questionValidatorWithProgram = new QuestionValidatorWithProgram(
                mapper,
                _context.Object,
                _hashComputer.Object,
                _repositoryQuestion.Object,
                _codeRunner.Object);

            _context
                .Setup(x => x.GetCurrentUserAsync())
                .ReturnsAsync(ModelsCreationHelper.CreateStudent);

            _context
                .Setup(x => x.GetCurrentUser())
                .Returns(ModelsCreationHelper.CreateStudent);

            _hashComputer
                .Setup(x => x.ComputeForQuestionAsync(It.IsAny<DatabaseQuestion>()))
                .ReturnsAsync("hash");

            _repositoryQuestion
                .Setup(x => x.FindFirstAsync(It.IsAny<ISpecification<DatabaseQuestion>>()))
                .ReturnsAsync((ISpecification<DatabaseQuestion> x) => _questions.AsQueryable().FirstOrDefault(x.ToExpression()));
        }

        [Fact]
        public async Task ClosedOneAnswer_OnlyRightAnswer()
        {
            var question = new Question
            {
                Id = 1,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Id = 1 }
                }
            };

            var result = await _questionValidatorClosedOneAnswer.ValidateAsync(question);

            Assert.True(result.Right);

            Assert.Equal(AnswerStatus.Right, result.Answers[0].Status);

            Assert.Null(result.Answers[1].Status);
            Assert.Null(result.Answers[2].Status);
        }

        [Fact]
        public async Task ClosedOneAnswer_WithRightAnswer()
        {
            var question = new Question
            {
                Id = 1,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Id = 1 },
                    new Answer { Id = 2 }
                }
            };

            var result = await _questionValidatorClosedOneAnswer.ValidateAsync(question);

            Assert.False(result.Right);

            Assert.Equal(AnswerStatus.Right, result.Answers[0].Status);
            Assert.Equal(AnswerStatus.Wrong, result.Answers[1].Status);

            Assert.Null(result.Answers[2].Status);
        }

        [Fact]
        public async Task ClosedOneAnswer_NoRightAnswer()
        {
            var question = new Question
            {
                Id = 1,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Id = 2 }
                }
            };

            var result = await _questionValidatorClosedOneAnswer.ValidateAsync(question);

            Assert.False(result.Right);

            Assert.Equal(AnswerStatus.Ignore, result.Answers[0].Status);
            Assert.Equal(AnswerStatus.Wrong, result.Answers[1].Status);

            Assert.Null(result.Answers[2].Status);
        }

        [Fact]
        public async Task ClosedManyAnswers_OnlyRightAnswers()
        {
            var question = new Question
            {
                Id = 2,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Id = 4 },
                    new Answer { Id = 5 }
                }
            };

            var result = await _questionValidatorClosedManyAnswers.ValidateAsync(question);

            Assert.True(result.Right);

            Assert.Equal(AnswerStatus.Right, result.Answers[0].Status);
            Assert.Equal(AnswerStatus.Right, result.Answers[1].Status);

            Assert.Null(result.Answers[2].Status);
            Assert.Null(result.Answers[3].Status);
        }

        [Fact]
        public async Task ClosedManyAnswers_WithRightAnswers()
        {
            var question = new Question
            {
                Id = 2,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Id = 4 },
                    new Answer { Id = 6 }
                }
            };

            var result = await _questionValidatorClosedManyAnswers.ValidateAsync(question);

            Assert.False(result.Right);

            Assert.Equal(AnswerStatus.Right, result.Answers[0].Status);
            Assert.Equal(AnswerStatus.Ignore, result.Answers[1].Status);
            Assert.Equal(AnswerStatus.Wrong, result.Answers[2].Status);

            Assert.Null(result.Answers[3].Status);
        }

        [Fact]
        public async Task ClosedManyAnswers_NoRightAnswers()
        {
            var question = new Question
            {
                Id = 2,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Id = 6 },
                    new Answer { Id = 7 },
                }
            };

            var result = await _questionValidatorClosedManyAnswers.ValidateAsync(question);

            Assert.False(result.Right);

            Assert.Equal(AnswerStatus.Ignore, result.Answers[0].Status);
            Assert.Equal(AnswerStatus.Ignore, result.Answers[1].Status);
            Assert.Equal(AnswerStatus.Wrong, result.Answers[2].Status);
            Assert.Equal(AnswerStatus.Wrong, result.Answers[3].Status);
        }

        [Fact]
        public async Task OpenedOneString_RightAnswer_1()
        {
            var question = new Question
            {
                Id = 3,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Text = "css" }
                }
            };

            var result = await _questionValidatorOpenedOneString.ValidateAsync(question);

            Assert.True(result.Right);
        }

        [Fact]
        public async Task OpenedOneString_RightAnswer_2()
        {
            var question = new Question
            {
                Id = 3,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Text = "cascading-style-sheets" }
                }
            };

            var result = await _questionValidatorOpenedOneString.ValidateAsync(question);

            Assert.True(result.Right);
        }

        [Fact]
        public async Task OpenedOneString_NotRightAnswer()
        {
            var question = new Question
            {
                Id = 3,
                Hash = "hash",
                Answers = new List<Answer> {
                    new Answer { Text = "HTML" }
                }
            };

            var result = await _questionValidatorOpenedOneString.ValidateAsync(question);

            Assert.False(result.Right);
        }

        [Fact]
        public async Task WithProgram_Success()
        {
            _codeRunner
                .Setup(x => x.RunAsync(It.IsAny<Program>()))
                .ReturnsAsync(CreateCodeRunningResult(true));

            var question = new Question
            {
                Id = 4,
                Hash = "hash",
                Program = new Program {
                    Id = 1,
                    Source = "source"
                }
            };

            var result = await _questionValidatorWithProgram.ValidateAsync(question);

            Assert.True(result.Right);
        }

        [Fact]
        public async Task WithProgram_Failure()
        {
            _codeRunner
                .Setup(x => x.RunAsync(It.IsAny<Program>()))
                .ReturnsAsync(CreateCodeRunningResult(false));

            var question = new Question
            {
                Id = 4,
                Hash = "hash",
                Program = new Program {
                    Id = 1,
                    Source = "source"
                }
            };

            var result = await _questionValidatorWithProgram.ValidateAsync(question);

            Assert.True(result.Right);
        }

        private static List<DatabaseQuestion> CreateDatabaseQuestions()
        {
            return new List<DatabaseQuestion> {
                CreateClosedOneAnswerDatabaseQuestion(),
                CreateClosedManyAnswerDatabaseQuestion(),
                CreateOpenedOneStringDatabaseQuestion(),
                CreateWithProgramDatabaseQuestion()
            };
        }

        private static DatabaseQuestion CreateClosedOneAnswerDatabaseQuestion()
        {
            var question = CreateDatabaseQuestion(1, QuestionType.ClosedOneAnswer);

            question.Answers = new List<DatabaseAnswer> {
                CreateDatabaseAnswer(1),
                CreateDatabaseAnswer(2, false),
                CreateDatabaseAnswer(3, false)
            };

            return question;
        }

        private static DatabaseQuestion CreateClosedManyAnswerDatabaseQuestion()
        {
            var question = CreateDatabaseQuestion(2, QuestionType.ClosedManyAnswers);

            question.Answers = new List<DatabaseAnswer> {
                CreateDatabaseAnswer(4),
                CreateDatabaseAnswer(5),
                CreateDatabaseAnswer(6, false),
                CreateDatabaseAnswer(7, false)
            };

            return question;
        }

        private static DatabaseQuestion CreateOpenedOneStringDatabaseQuestion()
        {
            var question = CreateDatabaseQuestion(3, QuestionType.OpenedOneString);

            question.Answers = new List<DatabaseAnswer> {
                CreateDatabaseAnswer(8, text: "Cascading Style Sheets"),
                CreateDatabaseAnswer(9, text: "CSS")
            };

            return question;
        }

        private static DatabaseQuestion CreateWithProgramDatabaseQuestion()
        {
            var question = CreateDatabaseQuestion(4, QuestionType.WithProgram);

            question.Program = new DatabaseProgram {
                Id = 1,
                LanguageType = LanguageType.PHP,
                MemoryLimit = 6000,
                TimeLimit = 10,
                ProgramDatas = new List<DatabaseProgramData> {
                    new DatabaseProgramData { Input = "1", ExpectedOutput = "1" },
                    new DatabaseProgramData { Input = "2", ExpectedOutput = "2" }
                }
            };

            return question;
        }

        private static DatabaseQuestion CreateDatabaseQuestion(int id, QuestionType type)
        {
            var question = new DatabaseQuestion
            {
                Id = id,
                Type = type,
                Theme = new DatabaseTheme {
                    Discipline = ModelsCreationHelper.CreateDatabaseDiscipline(),
                    ThemeTests = ModelsCreationHelper.CreateDatabaseTestThemes()
                }
            };

            question.Theme.ThemeTests.ForEach(x => x.Test = new DatabaseTest { IsActive = true });

            return question;
        }

        private static DatabaseAnswer CreateDatabaseAnswer(int id, bool right = true, string text = null)
        {
            return new DatabaseAnswer { Id = id, IsRight = right, Text = text };
        }

        private static CodeRunningResult CreateCodeRunningResult(bool success)
        {
            var result = new CodeRunningResult
            {
                CodeAnalysisResult = new CodeAnalysisResult(),
                CodeExecutionResult = new CodeExecutionResult()
            };

            if (success)
            {
                result.CodeExecutionResult.Results = new List<CodeRunResult>
                {
                    new CodeRunResult { ExpectedOutput = "1", UserOutput = "1", Status = CodeRunStatus.Success }
                };
            }

            return result;
        }
    }
}