using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;

namespace ZwinnyCRUD.Cloud.Pages.Files
{
    public class DeleteFileModel : PageModel
    {
        private readonly IFileDatabase _fileContext;
        private readonly IProjectDatabase _projectContext;


        public DeleteFileModel(IFileDatabase fileContext, IProjectDatabase projectContext)
        {
            _fileContext = fileContext;
            _projectContext = projectContext;
        }

        [BindProperty]
        public ZwinnyCRUD.Common.Models.File RemoveFile { get; private set; }

        [BindProperty]
        public Project Project { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RemoveFile = await _fileContext.SingleOrDefaultAsync(id.Value);
            Project = await _projectContext.FindOrDefault(RemoveFile.ProjectId);

            if (RemoveFile == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RemoveFile = await _fileContext.Delete(id.Value);

            return Redirect("/Projects/Details?id=" + RemoveFile.ProjectId);
        }
    }
}
