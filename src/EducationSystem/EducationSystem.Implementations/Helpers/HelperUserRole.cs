﻿using System;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Helpers
{
    public sealed class HelperUserRole : IHelperUserRole
    {
        private readonly IRepository<DatabaseRole> _repositoryRole;

        public HelperUserRole(IRepository<DatabaseRole> repositoryRole)
        {
            _repositoryRole = repositoryRole;
        }

        public async Task CheckRoleStudentAsync(int userId)
        {
            var role = await GetRole(userId);

            if (string.Equals(role.Name, UserRoles.Student, StringComparison.InvariantCultureIgnoreCase))
                return;

            throw ExceptionHelper.CreateException(
                $"Пользователь не является студентом. Идентификатор пользователя: {userId}.",
                $"Пользователь не является студентом.");
        }

        public async Task CheckRoleLecturerAsync(int userId)
        {
            var role = await GetRole(userId);

            if (string.Equals(role.Name, UserRoles.Lecturer, StringComparison.InvariantCultureIgnoreCase))
                return;

            throw ExceptionHelper.CreateException(
                $"Пользователь не является преподавателем. Идентификатор пользователя: {userId}.",
                $"Пользователь не является преподавателем.");
        }

        private Task<DatabaseRole> GetRole(int userId)
        {
            return _repositoryRole.FindFirstAsync(new RolesByUserId(userId)) ??
                throw ExceptionHelper.CreateException($"Не удалось получить роль пользователя. Идентификатор пользователя: {userId}.");
        }
    }
}