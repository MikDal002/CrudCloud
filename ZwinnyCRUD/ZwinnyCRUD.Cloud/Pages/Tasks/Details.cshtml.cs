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
    public class DetailsModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;

        public DetailsModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context)
        {
            _context = context;
        }

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
    }
}
