using System.Collections.Generic;
using EducationSystem.Database.Models.Source;
using EducationSystem.Models.Source.Options;

namespace EducationSystem.Repositories.Interfaces.Source.Rest
{
    public interface IRepositoryUser : IRepositoryReadOnly<DatabaseUser>
    {
        (int Count, List<DatabaseUser> Users) GetUsers(OptionsUser options);

        (int Count, List<DatabaseUser> Users) GetUsersByRoleId(int roleId, OptionsUser options);

        DatabaseUser GetUserById(int id, OptionsUser options);

        DatabaseUser GetUserByEmail(string email, OptionsUser options);
    }
}