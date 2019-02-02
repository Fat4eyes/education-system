using System.Collections.Generic;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IUserManager
    {
        List<User> GetAll();

        User GetById(int id);

        User GetByEmailAndPassword(string email, string password);
    }
}