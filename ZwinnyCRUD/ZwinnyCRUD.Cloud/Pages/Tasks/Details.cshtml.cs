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

namespace ZwinnyCRUD.Cloud.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly ITaskDatabase _context;

        public DetailsModel(ITaskDatabase context)
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
            Task = await _context.FindOrDefault(id.Value);
            
            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
