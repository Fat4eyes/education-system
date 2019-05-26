using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;
using ProgramModel = EducationSystem.Models.Rest.Program;

namespace EducationSystem.WebApp.Source.Tamers
{
    [Route("api/Code")]
    public class TamerCode : Tamer
    {
        private readonly ICodeExecutor _codeExecutor;
        private readonly ICodeAnalyzer _codeAnalyzer;

        public TamerCode(ICodeExecutor codeExecutor, ICodeAnalyzer codeAnalyzer)
        {
            _codeExecutor = codeExecutor;
            _codeAnalyzer = codeAnalyzer;
        }

        [HttpPost]
        [Route("Execute")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> Execute([FromBody] ProgramModel program)
        {
            return await Ok(() => _codeExecutor.ExecuteAsync(program));
        }

        [HttpPost]
        [Route("Analyze")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> Analyze([FromBody] ProgramModel program)
        {
            return await Ok(() => _codeAnalyzer.AnalyzeAsync(program));
        }
    }
}