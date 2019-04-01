using System;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperUser : IHelperUser
    {
        private readonly IRepositoryRole _repositoryRole;

        public HelperUser(IRepositoryRole repositoryRole)
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