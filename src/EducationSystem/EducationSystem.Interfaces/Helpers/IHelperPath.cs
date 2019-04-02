﻿using EducationSystem.Models.Files.Basics;

namespace EducationSystem.Interfaces.Helpers
{
    public interface IHelperPath
    {
        string GetContentPath();

        string GetAbsoluteFilePath(File file);

        string GetRelativeFilePath(File file);
    }
}