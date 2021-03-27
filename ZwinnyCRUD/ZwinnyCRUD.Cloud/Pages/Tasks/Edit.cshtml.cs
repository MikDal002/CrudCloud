using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskDatabase _taskContext;
        private readonly IProjectDatabase _projectDatabase;

        public EditModel(ITaskDatabase context, IProjectDatabase projectDatabase)
        {
            _taskContext = context;
            _projectDatabase = projectDatabase;
        }

        [BindProperty]
        public Common.Models.Task Task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _taskContext.FindOrDefault(id.Value);
            
            if (Task == null)
            {
                return NotFound();
            }
           ViewData["ProjectId"] = new SelectList(_projectDatabase.GetAll(), "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskContext.AddOrUpdate(Task);

            return Redirect("./Details?id=" + Task.Id);
        }
    }
}
