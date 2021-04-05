using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskDatabase _taskContext;
        private readonly IProjectDatabase _projectContext;

        public CreateModel(ITaskDatabase tasks, IProjectDatabase projects)
        {
            _taskContext = tasks;
            _projectContext = projects;
        }
        
        [HttpGet("id")]
        public IActionResult OnGet(int Id)
        {
            ViewData["ProjectId"] = Id;
            var project_details = _projectContext.FindAll(e => e.Id == Id).Single();
            ViewData["ProjectTitle"] = project_details.Title;

            return Page();
        }

        [BindProperty]
        public Common.Models.Task Task { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskContext.Add(Task);

            return Redirect("/Projects/Details?id=" + Task.ProjectId);
        }
    }
}
