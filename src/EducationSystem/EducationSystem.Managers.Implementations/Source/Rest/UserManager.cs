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
    public class UserManager : Manager<UserManager>, IUserManager
    {
        protected IUserRepository UserRepository { get; }

        public UserManager(
            IMapper mapper,
            ILogger<UserManager> logger,
            IUserRepository userRepository)
            : base(mapper, logger)
        {
            UserRepository = userRepository;
        }

        /// <inheritdoc />
        public List<User> GetAll()
        {
            var users = UserRepository.GetAll();

            return Mapper.Map<List<User>>(users);
        }

        /// <inheritdoc />
        public User GetById(int id)
        {
            var user = UserRepository.GetById(id) ??
                throw new EducationSystemNotFoundException($"Пользователь не найден. Идентификатор: {id}.");

            return Mapper.Map<User>(user);
        }

        /// <inheritdoc />
        public User GetByEmailAndPassword(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new EducationSystemPublicException("Не указана элеткронная почта.");

            if (string.IsNullOrWhiteSpace(password))
                throw new EducationSystemPublicException("Не указан пароль.");

            var user = UserRepository.GetByEmail(email) ??
                throw new EducationSystemNotFoundException(
                    $"Пользователь не найден. Электронная почта: {email}.",
                    new EducationSystemPublicException("Неверная электронная почта или пароль."));

            ValidatePassword(user.Email, password, user.Password);

            return Mapper.Map<User>(user);
        }

        private void ValidatePassword(string email, string password, string hash)
        {
            if (Crypt.Verify(password, hash))
                return;

            var message = $"Пользователь найден, но пароль указан неверно. " +
                          $"Электронная почта: {email}.";

            Logger.LogError(message);

            throw new EducationSystemPublicException("Неверная электронная почта или пароль.");
        }
    }
}