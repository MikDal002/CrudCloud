using System;
using System.Collections.Generic;
using System.Linq;
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

        public DetailsModel(IProjectDatabase projectContext, ITaskDatabase taskContext)
        {
            _projectContext = projectContext;
            _taskContext = taskContext;
        }

        public Project Project { get; set; }
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

            return Page();
        }
    }
}
