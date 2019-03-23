using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Extensions.Source;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Source.Files.File;

namespace EducationSystem.Managers.Implementations.Source.Files
{
    public abstract class ManagerFile : Manager<ManagerFile>, IManagerFile
    {
        protected abstract string FolderName { get; }

        private readonly IHelperFile _helperFile;
        private readonly IHostingEnvironment _environment;

        protected ManagerFile(
            IMapper mapper,
            ILogger<ManagerFile> logger,
            IHelperFile helperFile,
            IHostingEnvironment environment)
            : base(mapper, logger)
        {
            _helperFile = helperFile;
            _environment = environment;
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

            path = Path.Combine(path, FolderName);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.Stream.CopyToAsync(stream);

            path = Path
                .Combine(Directories.Files, FolderName, name)
                .Replace("\\", "/");

            return new File(guid, path);
        }

        public bool FileExists(File file) => _helperFile.FileExists(file);
    }
}