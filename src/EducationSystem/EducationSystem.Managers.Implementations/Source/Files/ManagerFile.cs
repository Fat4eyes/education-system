using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Enums.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Files;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Source.Files.File;

namespace EducationSystem.Managers.Implementations.Source.Files
{
    public abstract class ManagerFile : Manager<ManagerFile>, IManagerFile
    {
        protected abstract FileType FileType { get; }

        private readonly IHelperFile _helperFile;
        private readonly IHostingEnvironment _environment;
        private readonly IRepositoryFile _repositoryFile;

        protected ManagerFile(
            IMapper mapper,
            ILogger<ManagerFile> logger,
            IHelperFile helperFile,
            IHostingEnvironment environment,
            IRepositoryFile repositoryFile)
            : base(mapper, logger)
        {
            _helperFile = helperFile;
            _environment = environment;
            _repositoryFile = repositoryFile;
        }

        public File SaveFile(File file) => SaveFileAsync(file).WaitTask();

        public virtual async Task<File> SaveFileAsync(File file)
        {
            _helperFile.ValidateFile(file);

            var guid = Guid.NewGuid();
            var name = guid.ToString("N") + Path.GetExtension(file.Name);

            // Files/...
            // Files/Images
            // Files/Documents

            var path = Path.Combine(_environment.ContentRootPath, Directories.Files);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, _helperFile.GetFolderName(FileType));

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.Stream.CopyToAsync(stream);

            path = Path
                .Combine(Directories.Files, _helperFile.GetFolderName(FileType), name)
                .Replace("\\", "/");

            var model = Mapper.Map<DatabaseFile>(file);

            model.Guid = guid;
            model.Type = FileType;
            model.Name = file.Name;

            await _repositoryFile.AddAsync(model, true);

            return new File(model.Id, guid, path);
        }

        public bool FileExists(File file) => _helperFile.FileExists(file);

        public string GetFilePath(File file) => _helperFile.GetFilePath(file);
    }
}