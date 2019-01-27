using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с профилями обучения.
    /// </summary>
    public class ManagerStudyProfile : Manager, IManagerStudyProfile
    {
        protected IRepositoryStudyProfile RepositoryStudyProfile { get; }

        public ManagerStudyProfile(
            IMapper mapper,
            IRepositoryStudyProfile repositoryStudyProfile)
            : base(mapper)
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