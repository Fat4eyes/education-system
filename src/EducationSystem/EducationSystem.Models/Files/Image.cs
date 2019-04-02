using System.IO;
using EducationSystem.Enums;
using File = EducationSystem.Models.Files.Basics.File;

namespace EducationSystem.Models.Files
{
    public class Image : File
    {
        public Image()
            : base(FileType.Image) { }

        public Image(int id)
            : base(FileType.Image, id) { }

        public Image(string name, Stream stream)
            : base(FileType.Image, name, stream) { }
    }
}