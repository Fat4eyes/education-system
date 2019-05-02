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
        public async Task<IActionResult> GetMaterials([FromQuery] OptionsMaterial options, [FromQuery] FilterMaterial filter)
        {
            return await Ok(() => _managerMaterial.GetMaterialsAsync(options, filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetMaterial([FromRoute] int id, [FromQuery] OptionsMaterial options)
        {
            return await Ok(() => _managerMaterial.GetMaterialAsync(id, options));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
        {
            return await Ok(() => _managerMaterial.CreateMaterialAsync(material));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateMaterial([FromRoute] int id, [FromBody] Material material)
        {
            return await Ok(() => _managerMaterial.UpdateMaterialAsync(id, material));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteMaterial([FromRoute] int id)
        {
            return await Ok(() => _managerMaterial.DeleteMaterialAsync(id));
        }
    }
}