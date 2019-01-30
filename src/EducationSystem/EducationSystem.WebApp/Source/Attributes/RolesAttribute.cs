using Microsoft.AspNetCore.Authorization;

namespace EducationSystem.WebApp.Source.Attributes
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
            => Roles = string.Join(",", roles);
    }
}