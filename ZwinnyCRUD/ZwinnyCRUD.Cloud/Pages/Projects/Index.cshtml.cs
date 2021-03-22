using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;

        public IndexModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var projects = from n in _context.Project
                           select n;
            if (!string.IsNullOrEmpty(SearchString))
            {
                projects = projects.Where(m => (m.Title.Contains(SearchString) || m.Description.Contains(SearchString)));
            }
            Project = await projects.ToListAsync();
        }
    }
}
