﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceQuestion
    {
        Task<PagedData<Question>> GetQuestionsAsync(FilterQuestion filter);

        Task<Question> GetQuestionAsync(int id);

        Task DeleteQuestionAsync(int id);
        Task UpdateQuestionAsync(int id, Question question);
        Task<int> CreateQuestionAsync(Question question);

        Task UpdateThemeQuestionsAsync(int id, List<Question> questions);

        Task<Question> ProcessTestQuestionAsync(int id, Question question);
    }
}