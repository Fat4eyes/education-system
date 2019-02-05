using System;
using AutoMapper;
using EducationSystem.Database.Models.Source;
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
            return Mapper.Map<User>(GetUser(email));
        }

        public UserWithGroup GetUserWithGroupByEmail(string email)
        {
            return Mapper.Map<UserWithGroup>(GetUser(email));
        }

        public UserWithGroupAndStudyPlan GetUserWithGroupAndStudyPlanByEmail(string email)
        {
            return Mapper.Map<UserWithGroupAndStudyPlan>(GetUser(email));
        }

        public UserWithTestResults GetUserWithTestResultsByEmail(string email)
        {
            return Mapper.Map<UserWithTestResults>(GetUser(email));
        }

        private DatabaseUser GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));

            return RepositoryUser.GetByEmail(email) ??
                throw new EducationSystemNotFoundException($"Пользователь не найден. Электронная почта: {email}.");
        }
    }
}