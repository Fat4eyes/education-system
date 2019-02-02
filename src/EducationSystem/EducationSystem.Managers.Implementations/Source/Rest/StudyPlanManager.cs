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
    /// Менеджер по работе с группами.
    /// </summary>
    public class StudyPlanManager : Manager<StudyPlanManager>, IStudyPlanManager
    {
        protected IStudyPlanRepository StudyPlanRepository { get; }

        public StudyPlanManager(
            IMapper mapper,
            ILogger<StudyPlanManager> logger,
            IStudyPlanRepository studyPlanRepository)
            : base(mapper, logger)
        {
            StudyPlanRepository = studyPlanRepository;
        }

        /// <inheritdoc />
        public List<StudyPlan> GetAll()
        {
            var plans = StudyPlanRepository.GetAll();

            return Mapper.Map<List<StudyPlan>>(plans);
        }

        /// <inheritdoc />
        public StudyPlan GetById(int id)
        {
            var plan = StudyPlanRepository.GetById(id) ??
                throw new EducationSystemNotFoundException($"Учеьный план не найден. Идентификатор: {id}.");

            return Mapper.Map<StudyPlan>(plan);
        }
    }
}