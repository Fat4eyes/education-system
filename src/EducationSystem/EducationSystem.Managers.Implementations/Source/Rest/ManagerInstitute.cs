using AutoMapper;
using System.Collections.Generic;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Менеджер по работе с институтами.
    /// </summary>
    public class ManagerInstitute : Manager, IManagerInstitute
    {
        protected IRepositoryInstitute RepositoryInstitute { get; }

        public ManagerInstitute(
            IMapper mapper,
            IRepositoryInstitute repositoryInstitute)
            : base(mapper)
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