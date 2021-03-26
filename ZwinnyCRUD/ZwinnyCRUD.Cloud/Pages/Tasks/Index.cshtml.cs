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
    public class IndexModel : PageModel
    {
        private readonly ITaskDatabase _context;

        public IndexModel(ITaskDatabase context)
        {
            _context = context;
        }

        public IList<Common.Models.Task> Task { get;set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Task = _context.GetAll().ToList();
        }
    }
}
