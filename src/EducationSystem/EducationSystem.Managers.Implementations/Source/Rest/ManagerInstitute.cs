using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerInstitute : Manager<ManagerInstitute>, IManagerInstitute
    {
        protected IRepositoryInstitute RepositoryInstitute { get; }

        public ManagerInstitute(
            IMapper mapper,
            ILogger<ManagerInstitute> logger,
            IRepositoryInstitute repositoryInstitute)
            : base(mapper, logger)
        {
            RepositoryInstitute = repositoryInstitute;
        }

        public Institute GetInstituteByUserId(int userId, OptionsInstitute options)
        {
            var institute = RepositoryInstitute.GetInstituteByUserId(userId, options) ??
               throw new EducationSystemNotFoundException(
                   $"Институт не найден. Идентификатор пользователя: {userId}.",
                   new EducationSystemPublicException("Институт не найден."));

            return Mapper.Map<Institute>(institute);
        }
    }
}