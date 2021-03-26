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

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                Project = _context.GetAll().ToList();
            } else
            {
                Project = _context.FindAll(m => (m.Title.Contains(SearchString) || m.Description.Contains(SearchString))).ToList();
            }
        }
}
}
