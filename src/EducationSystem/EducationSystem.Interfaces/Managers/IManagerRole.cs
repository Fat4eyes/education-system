﻿using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Rest;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerRole
    {
        PagedData<Role> GetRoles(OptionsRole options, FilterRole filter);

        Role GetRoleById(int id, OptionsRole options);
        Role GetRoleByUserId(int userId, OptionsRole options);
    }
}