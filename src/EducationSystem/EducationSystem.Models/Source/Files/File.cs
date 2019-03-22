using System;
using System.IO;
using EducationSystem.Enums.Source;

namespace EducationSystem.Models.Source.Files
{
    public class File : IDisposable
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public Guid? Guid { get; set; }

        public string Name { get; set; }

        public FileType? Type { get; set; }

        public Stream Stream { get; set; }

        public File() { }

        public File(FileType type)
        {
            Type = type;
        }

        public File(int id)
        {
            Id = id;
        }

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

        public File(int id, Guid guid, string path)
        {
            Id = id;
            Guid = guid;
            Path = path;
        }

        public File(string path)
        {
            Path = path;
        }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}