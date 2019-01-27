using AutoMapper;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Managers.Implementations.Source.Rest
{
    /// <summary>
    /// Базовый менеджер.
    /// </summary>
    public abstract class Manager<TManager> where TManager : class
    {
        protected IMapper Mapper { get; }

        protected ILogger<TManager> Logger { get; }

        protected Manager(IMapper mapper, ILogger<TManager> logger)
        {
            Mapper = mapper;
            Logger = logger;
        }
    }
}