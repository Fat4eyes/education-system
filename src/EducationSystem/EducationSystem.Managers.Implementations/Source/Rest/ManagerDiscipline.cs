using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    public class ManagerDiscipline : Manager<ManagerDiscipline>, IManagerDiscipline
    {
        protected IRepositoryDiscipline RepositoryDiscipline { get; }

        public ManagerDiscipline(
            IMapper mapper,
            ILogger<ManagerDiscipline> logger,
            IRepositoryDiscipline repositoryDiscipline)
            : base(mapper, logger)
        {
            RepositoryDiscipline = repositoryDiscipline;
        }

        public PagedData<Discipline> GetDisciplines(OptionsDiscipline options)
        {
            var (count, disciplines) = RepositoryDiscipline.GetDisciplines(options);

            return new PagedData<Discipline>(Mapper.Map<List<Discipline>>(disciplines), count);
        }

        public Discipline GetDisciplineById(int id, OptionsDiscipline options)
        {
            var discipline = RepositoryDiscipline.GetDisciplineById(id, options) ??
               throw new EducationSystemException(
                   $"Дисциплина не найдена. Идентификатор: {id}.",
                   new EducationSystemPublicException("Дисциплина не найдена."));

            return Mapper.Map<Discipline>(discipline);
        }
    }
}