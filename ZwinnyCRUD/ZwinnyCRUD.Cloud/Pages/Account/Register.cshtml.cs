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
    public class RegisterModel : PageModel
    {
        private readonly IRegisterDatabase _context;

        public RegisterModel(IRegisterDatabase context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Register Register { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<string> errors = await _context.Register(Register);

            if (errors.Count() != 0)
            {
                foreach (string error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
