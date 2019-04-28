using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Materials")]
    public class TamerMaterial : Tamer
    {
        private readonly IManagerMaterial _managerMaterial;

        public TamerMaterial(IManagerMaterial managerMaterial)
        {
            _managerMaterial = managerMaterial;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetMaterials(
            [FromQuery] OptionsMaterial options,
            [FromQuery] FilterMaterial filter)
        {
            return Ok(await _managerMaterial.GetMaterials(options, filter));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMaterial([FromRoute] int id, [FromQuery] OptionsMaterial options)
        {
            return Ok(await _managerMaterial.GetMaterial(id, options));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
        {
            return Ok(await _managerMaterial.CreateMaterial(material));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateMaterial([FromRoute] int id, [FromBody] Material material)
        {
            return Ok(await _managerMaterial.UpdateMaterial(id, material));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteMaterial([FromRoute] int id)
        {
            await _managerMaterial.DeleteMaterial(id);

            return Ok();
        }
    }
}