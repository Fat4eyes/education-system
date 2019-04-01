﻿using System.Threading.Tasks;
using EducationSystem.Models.Source.Files;

namespace EducationSystem.Interfaces.Managers.Files
{
    public interface IManagerFile
    {
        Task<File> AddFileAsync(File file);

        Task<File> GetFileById(int id);

        Task DeleteFileByIdAsync(int id);
    }
}