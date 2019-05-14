using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Specifications.Files;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Implementations.Services.Files.Basics
{
    public abstract class ServiceFile<TFile> : Service<ServiceFile<TFile>>, IServiceFile<TFile> where TFile : File
    {
        protected IHelperPath HelperPath { get; }
        protected IHelperFile HelperFile { get; }
        protected IHelperFolder HelperFolder { get; }
        protected IValidator<TFile> ValidatorFile { get; }
        protected IRepository<DatabaseFile> RepositoryFile { get; }

        protected ServiceFile(
            IMapper mapper,
            ILogger<ServiceFile<TFile>> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<TFile> validatorFile,
            IExecutionContext executionContext,
            IRepository<DatabaseFile> repositoryFile)
            : base(
                mapper,
                logger,
                executionContext)
        {
            HelperPath = helperPath;
            HelperFile = helperFile;
            HelperFolder = helperFolder;
            ValidatorFile = validatorFile;
            RepositoryFile = repositoryFile;
        }

        public virtual async Task<PagedData<TFile>> GetFilesAsync(FilterFile filter)
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsAdmin())
            {
                var specification = new FilesByType(filter.Type);

                var (count, files) = await RepositoryFile.FindPaginatedAsync(specification, filter);

                return new PagedData<TFile>(Mapper.Map<List<TFile>>(files), count);
            }

            if (user.IsLecturer())
            {
                var specification =
                    new FilesByType(filter.Type) &
                    new FilesByOwnerId(user.Id);

                var (count, files) = await RepositoryFile.FindPaginatedAsync(specification, filter);

                return new PagedData<TFile>(Mapper.Map<List<TFile>>(files), count);
            }

            throw ExceptionHelper.NoAccess();
        }

        public async Task DeleteFileAsync(int id)
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            var model = await RepositoryFile.FindFirstAsync(new FilesById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseFile>(id);

            if (user.IsNotAdmin() && !new FilesByOwnerId(user.Id).IsSatisfiedBy(model))
                throw ExceptionHelper.NoAccess();

            var file = Mapper.Map<TFile>(model);

            await RepositoryFile.RemoveAsync(model);

            if (await HelperFile.FileExistsAsync(file) == false)
                return;

            var path = await HelperPath.GetAbsoluteFilePathAsync(file);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            await RepositoryFile.SaveChangesAsync();
        }

        public async Task<TFile> GetFileAsync(int id)
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && user.IsNotLecturer() && user.IsNotStudent())
                throw ExceptionHelper.NoAccess();

            var model = await RepositoryFile.FindFirstAsync(new FilesById(id)) ??
                throw ExceptionHelper.NotFound<DatabaseFile>(id);

            if (user.IsNotAdmin() && user.IsNotStudent() && !new FilesByOwnerId(user.Id).IsSatisfiedBy(model))
                throw ExceptionHelper.NoAccess();

            var file = Mapper.Map<TFile>(model);

            if (await HelperFile.FileExistsAsync(file) == false)
                throw ExceptionHelper.NotFound<File>(id);

            return file;
        }

        public virtual async Task<TFile> CreateFileAsync(TFile file)
        {
            var user = await ExecutionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && user.IsNotLecturer())
                throw ExceptionHelper.NoAccess();

            await ValidatorFile.ValidateAsync(file);

            var guid = Guid.NewGuid();
            var name = guid + Path.GetExtension(file.Name);

            var path = Path.Combine(HelperPath.GetContentPath(), Directories.Files);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, HelperFolder.GetFolderName(file.Type));

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.Stream.CopyToAsync(stream);

            var model = Mapper.Map<DatabaseFile>(file);

            model.Guid = guid.ToString();
            model.OwnerId = user.Id;

            await RepositoryFile.AddAsync(model, true);

            return Mapper.Map<TFile>(model);
        }
    }
}