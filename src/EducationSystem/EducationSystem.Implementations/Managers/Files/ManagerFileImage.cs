﻿using AutoMapper;
using EducationSystem.Enums;
using EducationSystem.Interfaces.Helpers.Files;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Files
{
    public class ManagerFileImage : ManagerFile, IManagerFileImage
    {
        protected override FileType FileType => FileType.Image;

        public ManagerFileImage(
            IMapper mapper,
            ILogger<ManagerFileImage> logger,
            IHelperFileImage helperFile,
            IHostingEnvironment environmen,
            IRepositoryFile repositoryFile)
            : base(
                mapper,
                logger,
                helperFile,
                environmen,
                repositoryFile)
        { }
    }
}