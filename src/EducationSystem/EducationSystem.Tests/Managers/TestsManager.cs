﻿using AutoMapper;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Mapping;
using Microsoft.Extensions.Logging;
using Moq;

namespace EducationSystem.Tests.Managers
{
    public abstract class TestsManager<TManager> where TManager : class
    {
        protected IMapper Mapper { get; }

        protected Mock<ILogger<TManager>> LoggerMock { get; }
        protected Mock<IHelperUserRole> MockHelperUser { get; }
        protected Mock<IExceptionFactory> MockExceptionFactory { get; }

        protected TestsManager()
        {
            Mapper = new Mapper(new MapperConfiguration(MappingConfigurator.Configure));

            LoggerMock = new Mock<ILogger<TManager>>();
            MockHelperUser = new Mock<IHelperUserRole>();
            MockExceptionFactory = new Mock<IExceptionFactory>();
        }
    }
}