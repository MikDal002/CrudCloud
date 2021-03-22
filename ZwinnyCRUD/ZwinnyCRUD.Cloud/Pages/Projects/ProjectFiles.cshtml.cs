using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ZwinnyCRUD.Common.Models;
using System.Net.Mime;
using System.Net;

namespace ZwinnyCRUD.Cloud.Pages.Projects
{
    public class ProjectFilesModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;
        private readonly IFileProvider _fileProvider;

        public ProjectFilesModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context, IFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        [BindProperty]
        public File File { get; set; }

        public IList<File> DatabaseFiles { get; private set; }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
            File = await _context.File.FirstOrDefaultAsync(m => m.Project.Id == id);
            var files = from n in _context.File
                         select n;
            files = files.Where(m => (m.ProjectId.Equals(Project.Id)));
            DatabaseFiles = await files.ToListAsync();

            if (Project == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnGetDownload(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var Helper = await _context.File.SingleOrDefaultAsync(m => m.Id == id);
            var downloadFile = _fileProvider.GetFileInfo(Helper.Name);

            if (downloadFile == null)
            {
                return Page();
            }
            return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, Helper.Name);
        }
    }
}
