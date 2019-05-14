﻿using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;

namespace EducationSystem.Implementations.Validators.Questions
{
    public sealed class QuestionValidatorClosedOneAnswer : QuestionValidatorClosedAnswer, IQuestionValidatorClosedOneAnswer
    {
        public QuestionValidatorClosedOneAnswer(
            IMapper mapper,
            IContext context,
            IHashComputer hashComputer,
            IRepository<DatabaseQuestion> repositoryQuestion)
            : base(
                mapper,
                context,
                hashComputer,
                repositoryQuestion)
        { }
    }
}