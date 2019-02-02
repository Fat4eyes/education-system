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
    public class StudyProfileManager : Manager<StudyProfileManager>, IStudyProfileManager
    {
        protected IStudyProfileRepository StudyProfileRepository { get; }

        public StudyProfileManager(
            IMapper mapper,
            ILogger<StudyProfileManager> logger,
            IStudyProfileRepository studyProfileRepository)
            : base(mapper, logger)
        {
            StudyProfileRepository = studyProfileRepository;
        }

        /// <inheritdoc />
        public List<StudyProfile> GetAll()
        {
            var profiles = StudyProfileRepository.GetAll();

            return Mapper.Map<List<StudyProfile>>(profiles);
        }
        
        /// <inheritdoc />
        public StudyProfile GetById(int id)
        {
            var profile = StudyProfileRepository.GetById(id) ??
                throw new EducationSystemNotFoundException($"Профиль обучения не найден. Идентификатор: {id}.");

            return Mapper.Map<StudyProfile>(profile);
        }
    }
}