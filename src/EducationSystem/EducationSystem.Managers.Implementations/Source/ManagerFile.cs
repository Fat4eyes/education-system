using System;
using System.IO;
using AutoMapper;
using EducationSystem.Helpers.Interfaces.Source.Files;
using EducationSystem.Managers.Interfaces.Source;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using File = EducationSystem.Models.Source.Files.File;

namespace EducationSystem.Managers.Implementations.Source
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

        public virtual File SaveFile(File file)
        {
            _helperFile.ValidateFile(file);

            var guid = Guid.NewGuid();
            var name = guid.ToString("N") + Path.GetExtension(file.Name);

            // Files/...
            // Files/Images
            // Files/Documents

            var path = Path.Combine(_environment.ContentRootPath, "Files");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FolderName);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            using (var stream = new FileStream(path, FileMode.Create))
                file.Stream.CopyTo(stream);

            path = Path
                .Combine("Files", FolderName, name)
                .Replace("\\", "/");

            return new File(guid, path);
        }
    }
}