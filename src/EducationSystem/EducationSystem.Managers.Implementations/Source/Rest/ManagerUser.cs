﻿using System;
using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Base;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;
using Crypt = BCrypt.Net.BCrypt;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с пользователями.
    /// </summary>
    public class ManagerUser : Manager<ManagerUser>, IManagerUser
    {
        protected IRepositoryUser RepositoryUser { get; }

        public ManagerUser(
            IMapper mapper,
            ILogger<ManagerUser> logger,
            IRepositoryUser repositoryUser)
            : base(mapper, logger)
        {
            RepositoryUser = repositoryUser;
        }

        /// <inheritdoc />
        public List<User> GetAll()
        {
            var users = RepositoryUser.GetAll();

            return Mapper.Map<List<User>>(users);
        }

        /// <inheritdoc />
        public User GetById(int id)
        {
            var user = RepositoryUser.GetById(id) ??
                throw new EducationSystemNotFoundException($"Пользователь не найден. Идентификатор: {id}.");

            return Mapper.Map<User>(user);
        }

        /// <inheritdoc />
        public User GetByEmailAndPassword(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EducationSystemException("Не указана элеткронная почта.");

            if (string.IsNullOrWhiteSpace(password))
                throw new EducationSystemException("Не указан пароль.");

            var user = RepositoryUser.GetByEmail(email) ??
                throw new EducationSystemNotFoundException($"Пользователь не найден. Электронная почта: {email}.");

            ValidatePassword(user.Email, password, user.Password);

            return Mapper.Map<User>(user);
        }

        private void ValidatePassword(string email, string password, string hash)
        {
            try
            {
                if (Crypt.Verify(password, hash))
                    return;

                var message = $"Пользователь найден, но пароль указан неверно. " +
                              $"Электронная почта: {email}.";

                Logger.LogError(message);

                throw new EducationSystemException(message);
            }
            catch (Exception ex)
            {
                throw new EducationSystemException("Неверная электронная почта или пароль.", ex);
            }
        }
    }
}