using AutoMapper;
using EducationSystem.Interfaces.Managers;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerRole : Manager<ManagerRole>, IManagerRole
    {
        public ManagerRole(
            IMapper mapper,
            ILogger<ManagerRole> logger)
            : base(mapper, logger)
        { }
    }
}