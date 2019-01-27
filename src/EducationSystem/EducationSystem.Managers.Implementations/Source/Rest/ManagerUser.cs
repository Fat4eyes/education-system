using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerUser : Manager, IManagerUser
    {
        protected IRepositoryUser RepositoryUser { get; }

        public ManagerUser(
            IMapper mapper,
            IRepositoryUser repositoryUser)
            : base(mapper)
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
                throw new EducationSystemNotFoundException($"Пользователь не найден. Идентификатор пользователя: {id}.");

            return Mapper.Map<User>(user);
        }
    }
}