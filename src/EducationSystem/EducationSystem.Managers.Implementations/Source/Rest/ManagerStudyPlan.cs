using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с группами.
    /// </summary>
    public class ManagerStudyPlan : Manager<ManagerStudyPlan>, IManagerStudyPlan
    {
        protected IRepositoryStudyPlan RepositoryStudyPlan { get; }

        public ManagerStudyPlan(
            IMapper mapper,
            ILogger<ManagerStudyPlan> logger,
            IRepositoryStudyPlan repositoryStudyPlan)
            : base(mapper, logger)
        {
            RepositoryStudyPlan = repositoryStudyPlan;
        }

        /// <inheritdoc />
        public List<StudyPlan> GetAll()
        {
            var plans = RepositoryStudyPlan.GetAll();

            return Mapper.Map<List<StudyPlan>>(plans);
        }

        /// <inheritdoc />
        public StudyPlan GetById(int id)
        {
            var plan = RepositoryStudyPlan.GetById(id) ??
                throw new EducationSystemNotFoundException($"Учеьный план не найден. Идентификатор: {id}.");

            return Mapper.Map<StudyPlan>(plan);
        }
    }
}