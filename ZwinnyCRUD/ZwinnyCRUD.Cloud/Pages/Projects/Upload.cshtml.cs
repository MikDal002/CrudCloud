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

namespace ZwinnyCRUD.Cloud.Pages.Projects
{
    public class UploadModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".txt" };
        private readonly string _targetFilePath;

        public UploadModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context,
            IConfiguration config)
        {
            _context = context;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
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

            Project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);

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
            Project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            
            if (Project == null)
            {
                return RedirectToPage("/Index");
            }


            var filePath = Path.Combine(
                _targetFilePath, FileUpload.FormFile.FileName);


            var file = new Common.Models.File
            {
                FilePath = filePath,
                Name = FileUpload.FormFile.FileName,
                Note = FileUpload.Note,
                Size = FileUpload.FormFile.Length,
                UploadDT = DateTime.UtcNow,
                ProjectId = Project.Id
            };

            using (var fileStream = System.IO.File.Create(filePath))
            {
                await FileUpload.FormFile.CopyToAsync(fileStream);
            }

            _context.File.Add(file);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

    public class Upload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }

    }
}
