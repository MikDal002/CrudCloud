using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILoginDatabase _context;

        public LoginModel(ILoginDatabase context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Login Login { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!await _context.Login(Login))
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
