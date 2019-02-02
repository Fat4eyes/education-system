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
    /// Менеджер по работе с институтами.
    /// </summary>
    public class InstituteManager : Manager<InstituteManager>, IInstituteManager
    {
        protected IInstituteRepository InstituteRepository { get; }

        public InstituteManager(
            IMapper mapper,
            ILogger<InstituteManager> logger,
            IInstituteRepository instituteRepository)
            : base(mapper, logger)
        {
            InstituteRepository = instituteRepository;
        }

        /// <inheritdoc />
        public List<Institute> GetAll()
        {
            var institutes = InstituteRepository.GetAll();

            return Mapper.Map<List<Institute>>(institutes);
        }

        /// <inheritdoc />
        public Institute GetById(int id)
        {
            var institute = InstituteRepository.GetById(id) ??
                throw new EducationSystemNotFoundException($"Институт не найден. Идентификатор: {id}.");

            return Mapper.Map<Institute>(institute);
        }
    }
}