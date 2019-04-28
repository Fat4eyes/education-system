using System.IO;
using EducationSystem.Enums;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Models.Files
{
    public class Document : File
    {
        public Document()
            : base(FileType.Document) { }

        public Document(int id)
            : base(FileType.Document, id) { }

        public Document(string name, Stream stream)
            : base(FileType.Document, name, stream) { }
    }
}