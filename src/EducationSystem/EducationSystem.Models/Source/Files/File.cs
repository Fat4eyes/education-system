using System;
using System.IO;

namespace EducationSystem.Models.Source.Files
{
    public class File
    {
        public string Path { get; set; }

        public Guid? Guid { get; set; }

        public string Name { get; set; }

        public Stream Stream { get; set; }

        public File() { }

        public File(string name, Stream stream)
        {
            Name = name;
            Stream = stream;
        }

        public File(Guid guid, string path)
        {
            Guid = guid;
            Path = path;
        }

        public File(string path)
        {
            Path = path;
        }
    }
}