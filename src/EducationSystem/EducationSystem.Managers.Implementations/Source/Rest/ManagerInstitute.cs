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

        /// <inheritdoc />
        public List<Institute> GetAll()
        {
            var institutes = RepositoryInstitute.GetAll();

            return Mapper.Map<List<Institute>>(institutes);
        }

        /// <inheritdoc />
        public Institute GetById(int id)
        {
            var institute = RepositoryInstitute.GetById(id) ??
                throw new EducationSystemNotFoundException($"Институт не найден. Идентификатор: {id}.");

            return Mapper.Map<Institute>(institute);
        }
    }
}