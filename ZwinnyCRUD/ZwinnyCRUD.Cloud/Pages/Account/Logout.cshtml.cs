using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;

namespace ZwinnyCRUD.Cloud.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly ILogoutDatabase _context;

        public LogoutModel(ILogoutDatabase context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Logout();

            return Page();
        }
    }
}
