using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using Newtonsoft.Json;

namespace EducationSystem.Implementations
{
    public sealed class HashComputer : IHashComputer
    {
        private readonly IExecutionContext _executionContext;

        public HashComputer(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        public string Compute(object @object)
        {
            var json = JsonConvert.SerializeObject(@object);

            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(json));

                return Convert.ToBase64String(bytes);
            }
        }

        public async Task<string> ComputeForQuestionAsync(DatabaseQuestion question)
        {
            var user = await _executionContext.GetCurrentUserAsync();

            var ojbect = new
            {
                question.Id,
                question.ThemeId,
                question.Theme.DisciplineId,
                question.Type,
                question.Text,

                UserId = user.Id
            };

            return Compute(ojbect);
        }
    }
}