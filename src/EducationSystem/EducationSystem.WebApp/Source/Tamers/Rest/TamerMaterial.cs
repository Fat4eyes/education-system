using System.Threading.Tasks;
using EducationSystem.Constants.Source;
using EducationSystem.Interfaces.Managers.Rest;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Materials")]
    [Roles(UserRoles.Admin, UserRoles.Employee, UserRoles.Lecturer)]
    public class TamerMaterial : Tamer
    {
        private readonly IManagerMaterial _managerMaterial;

        public TamerMaterial(IManagerMaterial managerMaterial)
        {
            _managerMaterial = managerMaterial;
        }

        [HttpGet("")]
        public IActionResult GetMaterials(
            [FromQuery] OptionsMaterial options,
            [FromQuery] FilterMaterial filter)
            => Ok(_managerMaterial.GetMaterials(options, filter));

        [Transaction]
        [HttpPost("")]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
            => Ok(await _managerMaterial.CreateMaterialAsync(material));

        [Transaction]
        [HttpPut("{materialId:int}")]
        public async Task<IActionResult> UpdateMaterial([FromRoute] int materialId, [FromBody] Material material)
            => Ok(await _managerMaterial.UpdateMaterialAsync(materialId, material));

        [Transaction]
        [HttpDelete("{materialId:int}")]
        public IActionResult DeleteMaterial([FromRoute] int materialId)
            => Ok(async () => await _managerMaterial.DeleteMaterialByIdAsync(materialId));
    }
}