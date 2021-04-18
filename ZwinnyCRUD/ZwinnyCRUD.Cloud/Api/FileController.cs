using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Cloud.Services;

namespace ZwinnyCRUD.Cloud.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
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

        /// <summary>
        /// Pobiera plik z podanej sciezki
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult GetDownload([Required] string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath)) return BadRequest("You mast provide a file path!!");
            return PhysicalFile(FilePath, MediaTypeNames.Application.Octet);
        }

        /// <summary>
        /// Usuwa plik o podanym ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("")]
        public async Task<ActionResult> Delete([Required] int id)
        {
            var deletedFile = await _fileDatabase.Delete(id);
            return deletedFile == null ? (StatusCodeResult)NotFound() : NoContent();
        }

        /// <summary>
        /// Przesyła plik i przypisuje go do projektu o podanym ID
        /// </summary>
        /// <param name="file"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
