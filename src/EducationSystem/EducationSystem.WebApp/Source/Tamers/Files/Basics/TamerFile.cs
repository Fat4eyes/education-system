﻿using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Helpers;
using EducationSystem.Interfaces.Services.Files;
using EducationSystem.Models.Files.Basics;
using EducationSystem.Models.Filters;
using EducationSystem.WebApp.Source.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.WebApp.Source.Tamers.Files.Basics
{
    public abstract class TamerFile<TFile> : Tamer where TFile : File, new()
    {
        protected IServiceFile<TFile> ServiceFile { get; }

        protected TamerFile(IServiceFile<TFile> serviceFile)
        {
            ServiceFile = serviceFile;
        }

        [HttpGet]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> GetFiles([FromQuery] FilterFile filter)
        {
            return await Ok(() => ServiceFile.GetFilesAsync(filter));
        }

        [HttpGet("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer, UserRoles.Student)]
        public async Task<IActionResult> GetFile([FromRoute] int id)
        {
            return await Ok(() => ServiceFile.GetFileAsync(id));
        }

        [HttpPost]
        [Transaction]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> CreateFile(IFormFile file)
        {
            if (file == null)
                throw ExceptionHelper.CreatePublicException("Не указан файл.");

            using (var stream = file.OpenReadStream())
            {
                var model = new TFile
                {
                    Name = file.FileName,
                    Stream = stream
                };

                return await Ok(() => ServiceFile.CreateFileAsync(model));
            }
        }

        [Transaction]
        [HttpDelete("{id:int}")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public async Task<IActionResult> DeleteFile([FromRoute] int id)
        {
            return await Ok(() => ServiceFile.DeleteFileAsync(id));
        }

        [HttpGet("Extensions")]
        [Roles(UserRoles.Admin, UserRoles.Lecturer)]
        public abstract IActionResult GetExtensions();
    }
}