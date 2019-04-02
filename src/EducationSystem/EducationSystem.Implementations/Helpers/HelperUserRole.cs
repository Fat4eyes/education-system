using System;
using EducationSystem.Constants;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperUserRole : IHelperUserRole
    {
        private readonly IRepositoryRole _repositoryRole;

        public HelperUserRole(IRepositoryRole repositoryRole)
        {
            _repositoryRole = repositoryRole;
        }

        public void CheckRoleStudent(int userId)
        {
            var role = _repositoryRole.GetRoleByUserId(userId) ??
                throw ExceptionHelper.CreateException(
                    $"Не удалось получить роль пользователя. " +
                    $"Идентификатор пользователя: {userId}.");

            if (string.Equals(role.Name, UserRoles.Student, StringComparison.CurrentCultureIgnoreCase))
                return;

            throw ExceptionHelper.CreateException(
                $"Пользователь не является студентом. Идентификатор пользователя: {userId}.",
                $"Пользователь не является студентом.");
        }
    }
}