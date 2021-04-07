using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Cloud.Services;

namespace ZwinnyCRUD.Cloud.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly FileUploadService _uploadService;
        private readonly IProjectDatabase _projectDatabase;
        private readonly IFileDatabase _fileDatabase;

        public FileController(IFileDatabase fileDatabase, IProjectDatabase projectDatabase, FileUploadService uploadService)
        {
            _uploadService = uploadService;
            _fileDatabase = fileDatabase;
            _projectDatabase = projectDatabase;
        }

        [HttpGet("")]
        public ActionResult GetDownload([Required] string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath)) return BadRequest("You mast provide a file path!!");
            return PhysicalFile(FilePath, MediaTypeNames.Application.Octet);
        }

        [HttpDelete("")]
        public async Task<ActionResult> Delete([Required] int id)
        {
            var deletedFile = await _fileDatabase.Delete(id);
            return deletedFile == null ? (StatusCodeResult)NotFound() : NoContent();
        }

        [HttpPost("")]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile file, int id)
        {
            var Project = await _projectDatabase.FindOrDefault(id);
            if (Project == null) return NotFound("Project with this id doesn't exist!");
            var fileToUpload = await _uploadService.Upload(file.FileName, file.Length, file.OpenReadStream(), id);
            if (fileToUpload == null) return Conflict("There is already file with that name in this project");
            await _fileDatabase.Add(fileToUpload);
            return NoContent();
        }
    }
}
