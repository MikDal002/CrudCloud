using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly IProjectDatabase _projectContext;
        private readonly ITaskDatabase _taskContext;
        private readonly IFileDatabase _fileContext;

        public DetailsModel(IProjectDatabase projectContext, ITaskDatabase taskContext, IFileDatabase fileContext)
        {
            _projectContext = projectContext;
            _taskContext = taskContext;
            _fileContext = fileContext;
        }

        public Project Project { get; set; }

        public ZwinnyCRUD.Common.Models.File File { get; set; }

        public IList<ZwinnyCRUD.Common.Models.File> DatabaseFiles { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _projectContext.FindOrDefault(id.Value);
         
            if (Project == null)
            {
                return NotFound();
            }

            Project.Tasks = _taskContext.FindAll(e => e.ProjectId == id).ToList();
            File = await _fileContext.FindProjectFile(id.Value);
            DatabaseFiles = _fileContext.FindAll(m => (m.ProjectId.Equals(Project.Id))).ToList();

            return Page();
        }
        public async Task<IActionResult> OnGetDownload(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var downloadfile = await _fileContext.SingleOrDefaultAsync(id.Value);

            return PhysicalFile(downloadfile.FilePath, MediaTypeNames.Application.Octet, downloadfile.Name);
        }
    }
}

