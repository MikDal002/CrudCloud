using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZwinnyCRUD.Common.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;

namespace ZwinnyCRUD.Cloud.Pages.Files
{
    public class UploadModel : PageModel
    {
        private readonly IProjectDatabase _projectContext;
        private readonly IFileDatabase _fileContext;
        private readonly string _targetFilePath;

        public UploadModel(IProjectDatabase projectContext,
            IConfiguration config, IFileDatabase fileContext)
        {
            _projectContext = projectContext;
            _targetFilePath = config.GetValue<string>(WebHostDefaults.ContentRootKey) + config.GetValue<string>("StoredFilesPath");
            _fileContext = fileContext;
        }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public string Result { get; private set; }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            Project = await _projectContext.FindOrDefault(id.Value);

            if (Project == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }
            Project = await _projectContext.FindOrDefault(id.Value);

            if (Project == null)
            {
                return RedirectToPage("/Index");
            }

            var dirPath = Path.Combine(_targetFilePath, Convert.ToString(Project.Id));
            var filePath = Path.Combine(dirPath, FileUpload.FormFile.FileName);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            if (System.IO.File.Exists(filePath))
            {
                Result = $"Plik o nazwie {FileUpload.FormFile.FileName} ju¿ istnieje w tym projekcie.";

                return Page();
            }

            var file = new Common.Models.File
            {
                FilePath = filePath,
                Name = FileUpload.FormFile.FileName,
                SizeinBytes = FileUpload.FormFile.Length,
                Uploaded = DateTimeOffset.UtcNow,
                ProjectId = Project.Id
            };

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await FileUpload.FormFile.CopyToAsync(fileStream);
            }

            await _fileContext.Add(file);

            return Redirect("/Projects/Details?id=" + file.ProjectId);
        }
    }

    public class Upload
    {
        [Required]
        [Display(Name = "Wybierz plik")]
        public IFormFile FormFile { get; set; }
    }
}
