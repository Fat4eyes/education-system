using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Factories;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Interfaces.Validators;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Implementations.Services.Files.Basics
{
    public abstract class ServiceFile<TFile> : Service<ServiceFile<TFile>>, IServiceFile<TFile>
        where TFile : File
    {
        private readonly IHelperPath _helperPath;
        private readonly IHelperFile _helperFile;
        private readonly IHelperFolder _helperFolder;
        private readonly IValidator<TFile> _validatorFile;
        private readonly IExceptionFactory _exceptionFactory;
        private readonly IExecutionContext _executionContext;
        private readonly IRepositoryFile _repositoryFile;

        protected ServiceFile(
            IMapper mapper,
            ILogger<ServiceFile<TFile>> logger,
            IHelperPath helperPath,
            IHelperFile helperFile,
            IHelperFolder helperFolder,
            IValidator<TFile> validatorFile,
            IExceptionFactory exceptionFactory,
            IExecutionContext executionContext,
            IRepositoryFile repositoryFile)
            : base(mapper, logger)
        {
            _helperPath = helperPath;
            _helperFile = helperFile;
            _helperFolder = helperFolder;
            _validatorFile = validatorFile;
            _exceptionFactory = exceptionFactory;
            _executionContext = executionContext;
            _repositoryFile = repositoryFile;
        }

        public async Task DeleteFileAsync(int id)
        {
            var model = await _repositoryFile.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseFile>(id);

            var user = await _executionContext.GetCurrentUserAsync();

            if (user.IsNotAdmin() && model.OwnerId != user.Id)
                throw _exceptionFactory.NoAccess();

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
            var model = await _repositoryFile.GetByIdAsync(id) ??
                throw _exceptionFactory.NotFound<DatabaseFile>(id);

            var file = Mapper.Map<TFile>(model);

            if (await _helperFile.FileExistsAsync(file) == false)
                throw _exceptionFactory.NotFound<File>(id);

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

            var user = await _executionContext.GetCurrentUserAsync();

            model.Guid = guid.ToString();
            model.OwnerId = user.Id;

            await _repositoryFile.AddAsync(model, true);

            var result = Mapper.Map<TFile>(model);

            return result;
        }
    }
}