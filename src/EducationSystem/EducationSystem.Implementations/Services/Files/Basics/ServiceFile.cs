using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Specifications;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Interfaces.Validators;
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
            IExceptionFactory exceptionFactory,
            IRepository<DatabaseFile> repositoryFile)
            : base(
                mapper,
                logger,
                executionContext,
                exceptionFactory)
        {
            HelperPath = helperPath;
            HelperFile = helperFile;
            HelperFolder = helperFolder;
            ValidatorFile = validatorFile;
            RepositoryFile = repositoryFile;
        }

        public async Task DeleteFileAsync(int id)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

            var model = await RepositoryFile.FindFirstAsync(new FilesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseFile>(id);

            if (CurrentUser.IsNotAdmin() && !new FilesByOwnerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionFactory.NoAccess();

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
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer() && CurrentUser.IsNotStudent())
                throw ExceptionFactory.NoAccess();

            var model = await RepositoryFile.FindFirstAsync(new FilesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseFile>(id);

            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotStudent() && !new FilesByOwnerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionFactory.NoAccess();

            var file = Mapper.Map<TFile>(model);

            if (await HelperFile.FileExistsAsync(file) == false)
                throw ExceptionFactory.NotFound<File>(id);

            return file;
        }

        public virtual async Task<TFile> CreateFileAsync(TFile file)
        {
            if (CurrentUser.IsNotAdmin() && CurrentUser.IsNotLecturer())
                throw ExceptionFactory.NoAccess();

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
            model.OwnerId = CurrentUser.Id;

            await RepositoryFile.AddAsync(model, true);

            return Mapper.Map<TFile>(model);
        }
    }
}