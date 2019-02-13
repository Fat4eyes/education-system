using System.Collections.Generic;
using AutoMapper;
using EducationSystem.Constants.Source;
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
                    string.Format(Messages.Discipline.NotFoundById, id),
                    new EducationSystemPublicException(Messages.Discipline.NotFoundPublic));

            return Mapper.Map<Discipline>(discipline);
        }
    }
}