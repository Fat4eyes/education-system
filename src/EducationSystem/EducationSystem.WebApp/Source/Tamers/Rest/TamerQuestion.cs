﻿using EducationSystem.Constants.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Questions")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerQuestion : Tamer
    {
        private readonly IManagerQuestion _managerQuestion;

        public TamerQuestion(IManagerQuestion managerQuestion)
        {
            _managerQuestion = managerQuestion;
        }

        [HttpGet("")]
        public IActionResult GetQuestions(
            [FromQuery] OptionsQuestion options,
            [FromQuery] FilterQuestion filter)
            => Ok(_managerQuestion.GetQuestions(options, filter));

        [Transaction]
        [HttpPost("")]
        public IActionResult CreateQuestion([FromBody] Question question)
            => Ok(_managerQuestion.CreateQuestion(question));

        [HttpGet("{questionId:int}")]
        public IActionResult GetQuestion(
            [FromRoute] int questionId,
            [FromQuery] OptionsQuestion options)
            => Ok(_managerQuestion.GetQuestionById(questionId, options));

        [Transaction]
        [HttpPut("{questionId:int}")]
        public IActionResult UpdateQuestion([FromRoute] int questionId, [FromBody] Question question)
            => Ok(_managerQuestion.UpdateQuestion(questionId, question));

        [Transaction]
        [HttpDelete("{questionId:int}")]
        public IActionResult DeleteQuestion([FromRoute] int questionId) =>
            Ok(() => _managerQuestion.DeleteQuestionById(questionId));
    }
}