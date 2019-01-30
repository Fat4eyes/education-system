using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Base;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с профилями обучения.
    /// </summary>
    public class ManagerStudyProfile : Manager<ManagerStudyProfile>, IManagerStudyProfile
    {
        protected IRepositoryStudyProfile RepositoryStudyProfile { get; }

        public ManagerStudyProfile(
            IMapper mapper,
            ILogger<ManagerStudyProfile> logger,
            IRepositoryStudyProfile repositoryStudyProfile)
            : base(mapper, logger)
        {
            RepositoryStudyProfile = repositoryStudyProfile;
        }

        /// <inheritdoc />
        public List<StudyProfile> GetAll()
        {
            var profiles = RepositoryStudyProfile.GetAll();

            return Mapper.Map<List<StudyProfile>>(profiles);
        }
        
        /// <inheritdoc />
        public StudyProfile GetById(int id)
        {
            var profile = RepositoryStudyProfile.GetById(id) ??
                throw new EducationSystemNotFoundException($"Профиль обучения не найден. Идентификатор: {id}.");

            return Mapper.Map<StudyProfile>(profile);
        }
    }
}