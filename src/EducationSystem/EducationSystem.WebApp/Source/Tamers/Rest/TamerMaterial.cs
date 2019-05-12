using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Interfaces.Services;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Rest;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Rest
{
    [Route("Api/Materials")]
    public class TamerMaterial : Tamer
    {
        private readonly IServiceMaterial _serviceMaterial;

        public TamerMaterial(IServiceMaterial serviceMaterial)
        {
            _serviceMaterial = serviceMaterial;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetMaterials([FromQuery] FilterMaterial filter)
        {
            return await Ok(() => _serviceMaterial.GetMaterialsAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetMaterial([FromRoute] int id)
        {
            return await Ok(() => _serviceMaterial.GetMaterialAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
        {
            return await Ok(() => _serviceMaterial.CreateMaterialAsync(material));
        }

        [Transaction]
        [HttpPut("{id:int}")]
        [Roles(UserRoles.Lecturer)]
        public async Task<IActionResult> UpdateMaterial([FromRoute] int id, [FromBody] Material material)
        {
            return await Ok(() => _serviceMaterial.UpdateMaterialAsync(id, material));
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteMaterial([FromRoute] int id)
        {
            return await Ok(() => _serviceMaterial.DeleteMaterialAsync(id));
        }
    }
}