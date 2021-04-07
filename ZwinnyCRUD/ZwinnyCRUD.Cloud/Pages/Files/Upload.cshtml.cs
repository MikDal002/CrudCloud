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
using ZwinnyCRUD.Cloud.Services;

namespace ZwinnyCRUD.Cloud.Pages.Files
{
    public class UploadModel : PageModel
    {
        private readonly FileUploadService _uploadService;
        private readonly IProjectDatabase _projectContext;
        private readonly IFileDatabase _fileContext;

        public UploadModel(IProjectDatabase projectContext, IFileDatabase fileContext, FileUploadService uploadService)
        {
            _projectContext = projectContext;
            _fileContext = fileContext;
            _uploadService = uploadService;
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

            var fileToUpload = await _uploadService.Upload(FileUpload.FormFile.FileName, FileUpload.FormFile.Length, FileUpload.FormFile.OpenReadStream(), id.Value);
            if (fileToUpload == null)
            {
                Result = "There is already file with that name in this project";
                return Page();
            }

            await _fileContext.Add(fileToUpload);

            return Redirect("/Projects/Details?id=" + fileToUpload.ProjectId);
        }
    }

    public class Upload
    {
        [Required]
        [Display(Name = "Wybierz plik")]
        public IFormFile FormFile { get; set; }
    }
}
