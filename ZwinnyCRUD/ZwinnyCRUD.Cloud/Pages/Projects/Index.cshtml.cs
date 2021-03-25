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
    public class IndexModel : PageModel
    {
        private readonly IProjectDatabase _context;

        public IndexModel(IProjectDatabase context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
        
           Project = await projects.ToListAsync();
    }
}
}
