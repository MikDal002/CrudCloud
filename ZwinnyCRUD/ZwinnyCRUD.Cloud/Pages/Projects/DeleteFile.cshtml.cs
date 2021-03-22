using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Common.Models;
using Microsoft.Extensions.FileProviders;

namespace ZwinnyCRUD.Cloud.Pages.Projects
{
    public class DeleteFileModel : PageModel
    {
        private readonly ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext _context;
        private readonly IFileProvider _fileProvider;


        public DeleteFileModel(ZwinnyCRUD.Cloud.Data.ZwinnyCRUDCloudContext context, IFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        [BindProperty]
        public File RemoveFileDB { get; private set; }
        public IFileInfo RemoveFilePhysical { get; private set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            RemoveFileDB = await _context.File.SingleOrDefaultAsync(m => m.Id == id);

            if (RemoveFileDB == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            RemoveFileDB = await _context.File.FindAsync(id);
            var Helper = await _context.File.SingleOrDefaultAsync(m => m.Id == id);
            RemoveFilePhysical = _fileProvider.GetFileInfo(Helper.Name);

            if (RemoveFileDB != null)
            {
                System.IO.File.Delete(RemoveFilePhysical.PhysicalPath);
                _context.File.Remove(RemoveFileDB);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
