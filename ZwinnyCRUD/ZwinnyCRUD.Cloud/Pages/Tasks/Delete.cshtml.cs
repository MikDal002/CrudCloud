using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;

        public DeleteModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Common.Models.Task Task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _context.Task
                .Include(t => t.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (Task == null)
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

            Task = await _context.Task.FindAsync(id);

            if (Task != null)
            {
                _context.Task.Remove(Task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
