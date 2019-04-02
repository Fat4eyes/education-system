﻿using AutoMapper;
using EducationSystem.Implementations.Managers.Files.Basics;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Managers.Files;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models.Files;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Managers.Files
{
    public sealed class ManagerImage : ManagerFile<Image>, IManagerImage
    {
        public ManagerImage(
            IMapper mapper,
            ILogger<ManagerImage> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<Image> validatorFile,
            IRepositoryFile repositoryFile)
            : base(
                mapper,
                logger,
                helperPath,
                helperFile,
                helperFolder,
                validatorFile,
                repositoryFile)
        { }
    }
}