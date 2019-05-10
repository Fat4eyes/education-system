using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;
using ExecutableProgram = EducationSystem.Models.Rest.Program;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Code")]
    public class TamerCode : Tamer
    {
        private readonly ICodeExecutor _codeExecutor;

        public TamerCode(ICodeExecutor codeExecutor)
        {
            _codeExecutor = codeExecutor;
        }

        [HttpPost]
        [Route("Execute")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> Execute([FromBody] ExecutableProgram program)
        {
            return await Ok(() => _codeExecutor.ExecuteAsync(program));
        }
    }
}