using AutoMapper;
using EducationSystem.Managers.Interfaces.Source.Export;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Export
{
    public class ManagerExport : Manager<ManagerExport>, IManagerExport
    {
        public ManagerExport(
            IMapper mapper,
            ILogger<ManagerExport> logger)
            : base(mapper, logger)
        { }
    }
}