﻿using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Implementations.Services.Files.Basics;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Files;
using EducationSystem.Models.Filters;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Implementations.Services.Files
{
    public sealed class ServiceDocument : ServiceFile<Document>
    {
        public ServiceDocument(
            IMapper mapper,
            IContext context,
            ILogger<ServiceDocument> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<Document> validatorDocument,
            IRepository<DatabaseFile> repositoryFile)
            : base(
                mapper,
                context,
                logger,
                helperPath,
                helperFile,
                helperFolder,
                validatorDocument,
                repositoryFile)
        { }

        public override async Task<PagedData<Document>> GetFilesAsync(FilterFile filter)
        {
            return await base.GetFilesAsync(filter.SetFileType(FileType.Document));
        }
    }
}