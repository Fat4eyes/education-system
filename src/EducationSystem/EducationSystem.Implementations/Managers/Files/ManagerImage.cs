using EducationSystem.Implementations.Managers.Files.Basics;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Models.Files;

namespace EducationSystem.Implementations.Managers.Files
{
    public sealed class ManagerImage : ManagerFile<Image>
    {
        public ManagerImage(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceFile<Image> serviceFile)
            : base(
                executionContext,
                exceptionFactory,
                serviceFile)
        { }
    }
}