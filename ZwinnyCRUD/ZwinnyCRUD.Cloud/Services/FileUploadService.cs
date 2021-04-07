using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Services
{
    public class FileUploadService
    {
        private readonly IProjectDatabase _projectContext;
        private readonly string _targetFilePath;

        public FileUploadService(IConfiguration config, IProjectDatabase projectContext)
        {
            _projectContext = projectContext;
            _targetFilePath = config.GetValue<string>(WebHostDefaults.ContentRootKey) + config.GetValue<string>("StoredFilesPath");
        }

        public async Task<ZwinnyCRUD.Common.Models.File> Upload(string FileName, long Length, Stream Content, int id)
        {
            var Project = await _projectContext.FindOrDefault(id);
            var dirPath = Path.Combine(_targetFilePath, Convert.ToString(Project.Id));
            var filePath = Path.Combine(dirPath, FileName);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            if (System.IO.File.Exists(filePath))
            {
                return null;
            }

            var myFile = new Common.Models.File
            {
                FilePath = filePath,
                Name = FileName,
                SizeinBytes = Length,
                Uploaded = DateTimeOffset.UtcNow,
                ProjectId = Project.Id
            };

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await Content.CopyToAsync(fileStream);
            }

            return myFile;
        }
    }
}
