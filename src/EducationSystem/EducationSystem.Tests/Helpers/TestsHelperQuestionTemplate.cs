using System;
using System.Collections.Generic;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Interfaces.Helpers;
using Xunit;

namespace EducationSystem.Tests.Helpers
{
    public class TestsHelperQuestionTemplate
    {
        protected readonly Random Random = new Random();

        private readonly IHelperQuestionTemplate _helperQuestionTemplate;

        public TestsHelperQuestionTemplate()
        {
            _helperQuestionTemplate = new HelperQuestionTemplate();
        }

        [Fact]
        public void GetTemplates_FewQuestions()
        {
            var questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, 7);
            questions = AppendTo(questions, QuestionType.WithProgram, 2);

            var templates = _helperQuestionTemplate.GetTemplates(TestSize.XS, questions);

            Assert.True(templates.ContainsKey(QuestionType.ClosedManyAnswers));
            Assert.True(templates.ContainsKey(QuestionType.WithProgram));

            Assert.Equal(2, templates.Count);
            Assert.Equal(2, templates[QuestionType.WithProgram]);
            Assert.Equal(7, templates[QuestionType.ClosedManyAnswers]);
        }

        [Fact]
        public void GetTemplates_SameQuestions()
        {
            var questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, 15);

            var templates = _helperQuestionTemplate.GetTemplates(TestSize.XS, questions);

            Assert.Single(templates);
            Assert.True(templates.ContainsKey(QuestionType.ClosedManyAnswers));
            Assert.Equal((int) TestSize.XS, templates[QuestionType.ClosedManyAnswers]);
        }

        [Fact]
        public void GetTemplates_TwoQuestionTypes()
        {
            var questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, 11);
            questions = AppendTo(questions, QuestionType.WithProgram, 2);

            var templates = _helperQuestionTemplate.GetTemplates(TestSize.XS, questions);

            Assert.True(templates.ContainsKey(QuestionType.ClosedManyAnswers));
            Assert.True(templates.ContainsKey(QuestionType.WithProgram));

            Assert.Equal(02, templates.Count);
            Assert.Equal(02, templates[QuestionType.WithProgram]);
            Assert.Equal(10, templates[QuestionType.ClosedManyAnswers]);

            questions = AppendTo(questions, QuestionType.WithProgram, 3);

            templates = _helperQuestionTemplate.GetTemplates(TestSize.XS, questions);

            Assert.Equal(05, templates[QuestionType.WithProgram]);
            Assert.Equal(07, templates[QuestionType.ClosedManyAnswers]);

            questions = AppendTo(questions, QuestionType.WithProgram, 1);

            templates = _helperQuestionTemplate.GetTemplates(TestSize.XS, questions);

            Assert.Equal(06, templates[QuestionType.WithProgram]);
            Assert.Equal(06, templates[QuestionType.ClosedManyAnswers]);

            questions = AppendTo(questions, QuestionType.WithProgram, 1);

            templates = _helperQuestionTemplate.GetTemplates(TestSize.XS, questions);

            Assert.Equal(02, templates.Count);
            Assert.Equal(06, templates[QuestionType.WithProgram]);
            Assert.Equal(06, templates[QuestionType.ClosedManyAnswers]);
        }

        [Fact]
        public void GetTemplates_ThreeQuestionTypes()
        {
            var questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, 15);
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, 5);
            questions = AppendTo(questions, QuestionType.WithProgram, 3);

            var templates = _helperQuestionTemplate.GetTemplates(TestSize.S, questions);

            Assert.True(templates.ContainsKey(QuestionType.ClosedManyAnswers));
            Assert.True(templates.ContainsKey(QuestionType.ClosedOneAnswer));
            Assert.True(templates.ContainsKey(QuestionType.WithProgram));

            Assert.Equal(03, templates.Count);
            Assert.Equal(03, templates[QuestionType.WithProgram]);
            Assert.Equal(12, templates[QuestionType.ClosedManyAnswers]);
            Assert.Equal(05, templates[QuestionType.ClosedOneAnswer]);

            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, 10);
            questions = AppendTo(questions, QuestionType.WithProgram, 12);

            templates = _helperQuestionTemplate.GetTemplates(TestSize.S, questions);

            Assert.True(templates.ContainsKey(QuestionType.ClosedManyAnswers));
            Assert.True(templates.ContainsKey(QuestionType.ClosedOneAnswer));
            Assert.True(templates.ContainsKey(QuestionType.WithProgram));

            Assert.Equal(03, templates.Count);
            Assert.Equal(06, templates[QuestionType.WithProgram]);
            Assert.Equal(07, templates[QuestionType.ClosedManyAnswers]);
            Assert.Equal(07, templates[QuestionType.ClosedOneAnswer]);

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, 1);

            templates = _helperQuestionTemplate.GetTemplates(TestSize.S, questions);

            Assert.True(templates.ContainsKey(QuestionType.ClosedManyAnswers));
            Assert.True(templates.ContainsKey(QuestionType.ClosedOneAnswer));
            Assert.True(templates.ContainsKey(QuestionType.WithProgram));

            Assert.Equal(03, templates.Count);
            Assert.Equal(06, templates[QuestionType.WithProgram]);
            Assert.Equal(07, templates[QuestionType.ClosedManyAnswers]);
            Assert.Equal(07, templates[QuestionType.ClosedOneAnswer]);
        }

        [Theory]
        [InlineData(TestSize.XS)]
        [InlineData(TestSize.S)]
        [InlineData(TestSize.M)]
        [InlineData(TestSize.L)]
        [InlineData(TestSize.XL)]
        [InlineData(TestSize.XXL)]
        public void GetTemplates_All(TestSize testSize)
        {
            var questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, Random.Next(10));
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, Random.Next(10));
            questions = AppendTo(questions, QuestionType.WithProgram, Random.Next(10));
            questions = AppendTo(questions, QuestionType.OpenedOneString, Random.Next(10));

            // Не должно быть исключения.
            _helperQuestionTemplate.GetTemplates(testSize, questions);

            questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, Random.Next(20));
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, Random.Next(20));
            questions = AppendTo(questions, QuestionType.WithProgram, Random.Next(20));
            questions = AppendTo(questions, QuestionType.OpenedOneString, Random.Next(20));

            // Не должно быть исключения.
            _helperQuestionTemplate.GetTemplates(testSize, questions);

            questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, Random.Next(30));
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, Random.Next(30));
            questions = AppendTo(questions, QuestionType.WithProgram, Random.Next(30));
            questions = AppendTo(questions, QuestionType.OpenedOneString, Random.Next(30));

            // Не должно быть исключения.
            _helperQuestionTemplate.GetTemplates(testSize, questions);

            questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, Random.Next(40));
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, Random.Next(40));
            questions = AppendTo(questions, QuestionType.WithProgram, Random.Next(40));
            questions = AppendTo(questions, QuestionType.OpenedOneString, Random.Next(40));

            // Не должно быть исключения.
            _helperQuestionTemplate.GetTemplates(testSize, questions);

            questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, Random.Next(50));
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, Random.Next(50));
            questions = AppendTo(questions, QuestionType.WithProgram, Random.Next(50));
            questions = AppendTo(questions, QuestionType.OpenedOneString, Random.Next(50));

            // Не должно быть исключения.
            _helperQuestionTemplate.GetTemplates(testSize, questions);

            questions = new List<DatabaseQuestion>();

            questions = AppendTo(questions, QuestionType.ClosedManyAnswers, Random.Next(60));
            questions = AppendTo(questions, QuestionType.ClosedOneAnswer, Random.Next(60));
            questions = AppendTo(questions, QuestionType.WithProgram, Random.Next(60));
            questions = AppendTo(questions, QuestionType.OpenedOneString, Random.Next(60));

            // Не должно быть исключения.
            _helperQuestionTemplate.GetTemplates(testSize, questions);
        }

        private static List<DatabaseQuestion> AppendTo(List<DatabaseQuestion> questions, QuestionType type, int count)
        {
            for (var i = 0; i < count; i++)
                questions.Add(new DatabaseQuestion { Type = type });

            return questions;
        }
    }
}