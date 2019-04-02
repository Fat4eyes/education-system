using System;
using System.IO;
using EducationSystem.Enums;

namespace EducationSystem.Models.Files.Basics
{
    public abstract class File : IDisposable
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public Guid? Guid { get; set; }

        public string Name { get; set; }

        public FileType Type { get; set; }

        public Stream Stream { get; set; }

        protected File(FileType type)
        {
            Type = type;
        }

        protected File(FileType type, int id) : this(type)
        {
            Id = id;
        }

        protected File(FileType type, string name, Stream stream)
        {
            Type = type;
            Name = name;
            Stream = stream;
        }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}