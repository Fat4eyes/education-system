using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("api/Tests/Data")]
    public class TamerTestData : Tamer
    {
        private readonly IServiceTestData _serviceTestData;

        public TamerTestData(IServiceTestData serviceTestData)
        {
            _serviceTestData = serviceTestData;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetTestsData([FromQuery] int[] ids)
        {
            return await Ok(() => _serviceTestData.GetTestsDataAsync(ids));
        }
    }
}