using System;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Helpers.Implementations.Source
{
    public sealed class UserHelper : IUserHelper
    {
        private readonly IRepositoryRole _repositoryRole;

        public UserHelper(IRepositoryRole repositoryRole)
        {
            _repositoryRole = repositoryRole;
        }

        public bool IsStudent(int userId) =>
            string.Equals(GetUserRole(userId).Name, UserRoles.Student,
                StringComparison.CurrentCultureIgnoreCase);

        public bool IsAdmin(int userId) =>
            string.Equals(GetUserRole(userId).Name, UserRoles.Admin,
                StringComparison.CurrentCultureIgnoreCase);

        public bool IsLecturer(int userId) =>
            string.Equals(GetUserRole(userId).Name, UserRoles.Lecturer,
                StringComparison.CurrentCultureIgnoreCase);

        public bool IsEmployee(int userId) =>
            string.Equals(GetUserRole(userId).Name, UserRoles.Employee,
                StringComparison.CurrentCultureIgnoreCase);

        private DatabaseRole GetUserRole(int userId) =>
            _repositoryRole.GetRoleByUserId(userId);
    }
}