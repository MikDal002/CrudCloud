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

        public IActionResult OnGet()
        {
        ViewData["ProjectId"] = new SelectList(_projectContext.GetAll(), "Id", "Description");
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

            return RedirectToPage("./Index");
        }
    }
}
