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
        private readonly IHelperPath _helperPath;
        private readonly IHelperFile _helperFile;
        private readonly IHelperFolder _helperFolder;
        private readonly IValidator<TFile> _validatorFile;
        private readonly IRepository<DatabaseFile> _repositoryFile;

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
            _helperPath = helperPath;
            _helperFile = helperFile;
            _helperFolder = helperFolder;
            _validatorFile = validatorFile;
            _repositoryFile = repositoryFile;
        }

        public async Task DeleteFileAsync(int id)
        {
            var model = await _repositoryFile.FindFirstAsync(new FilesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseFile>(id);

            if (CurrentUser.IsNotAdmin() && !new FilesByOwnerId(CurrentUser.Id).IsSatisfiedBy(model))
                throw ExceptionFactory.NoAccess();

            var file = Mapper.Map<TFile>(model);

            await _repositoryFile.RemoveAsync(model);

            if (await _helperFile.FileExistsAsync(file) == false)
                return;

            var path = await _helperPath.GetAbsoluteFilePathAsync(file);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            await _repositoryFile.SaveChangesAsync();
        }

        public async Task<TFile> GetFileAsync(int id)
        {
            var model = await _repositoryFile.FindFirstAsync(new FilesById(id)) ??
                throw ExceptionFactory.NotFound<DatabaseFile>(id);

            var file = Mapper.Map<TFile>(model);

            if (await _helperFile.FileExistsAsync(file) == false)
                throw ExceptionFactory.NotFound<File>(id);

            return file;
        }

        public virtual async Task<TFile> CreateFileAsync(TFile file)
        {
            await _validatorFile.ValidateAsync(file);

            var guid = Guid.NewGuid();
            var name = guid + Path.GetExtension(file.Name);

            var path = Path.Combine(_helperPath.GetContentPath(), Directories.Files);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, _helperFolder.GetFolderName(file.Type));

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.Stream.CopyToAsync(stream);

            var model = Mapper.Map<DatabaseFile>(file);

            model.Guid = guid.ToString();
            model.OwnerId = CurrentUser.Id;

            await _repositoryFile.AddAsync(model, true);

            return Mapper.Map<TFile>(model);
        }
    }
}