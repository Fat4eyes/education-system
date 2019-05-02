using EducationSystem.Implementations.Managers.Files.Basics;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Models.Files;

namespace EducationSystem.Implementations.Managers.Files
{
    public sealed class ManagerDocument : ManagerFile<Document>
    {
        public ManagerDocument(
            IExecutionContext executionContext,
            IExceptionFactory exceptionFactory,
            IServiceFile<Document> serviceFile)
            : base(
                executionContext,
                exceptionFactory,
                serviceFile)
        { }
    }
}