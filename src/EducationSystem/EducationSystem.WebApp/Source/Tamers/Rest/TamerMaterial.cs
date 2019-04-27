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

        [Authorize]
        [HttpGet("")]
        public IActionResult GetMaterials(
            [FromQuery] OptionsMaterial options,
            [FromQuery] FilterMaterial filter)
            => Ok(_managerMaterial.GetMaterials(options, filter));

        [Authorize]
        [HttpGet("{materialId:int}")]
        public IActionResult GetMaterial(
            [FromRoute] int materialId,
            [FromQuery] OptionsMaterial options)
            => Ok(_managerMaterial.GetMaterialById(materialId, options));

        [Transaction]
        [HttpPost("")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
            => Ok(await _managerMaterial.CreateMaterialAsync(material));

        [Transaction]
        [HttpPut("{materialId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateMaterial([FromRoute] int materialId, [FromBody] Material material)
            => Ok(await _managerMaterial.UpdateMaterialAsync(materialId, material));

        [Transaction]
        [HttpDelete("{materialId:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public IActionResult DeleteMaterial([FromRoute] int materialId)
            => Ok(async () => await _managerMaterial.DeleteMaterialByIdAsync(materialId));
    }
}