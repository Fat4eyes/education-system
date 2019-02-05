using System;
using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
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

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));

            var user = RepositoryUser.GetByEmail(email) ??
                throw new EducationSystemNotFoundException($"Пользователь не найден. Электронная почта: {email}.");

            return Mapper.Map<User>(user);
        }
    }
}