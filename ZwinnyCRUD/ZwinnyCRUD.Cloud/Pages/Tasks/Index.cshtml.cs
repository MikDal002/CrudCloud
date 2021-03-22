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
    public class IndexModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;

        public IndexModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context)
        {
            _context = context;
        }

        public IList<Common.Models.Task> Task { get;set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Task = await _context.Task
                .Include(t => t.Project).ToListAsync();
        }
    }
}
