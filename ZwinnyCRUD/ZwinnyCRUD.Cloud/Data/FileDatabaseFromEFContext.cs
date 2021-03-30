using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data
{
    public class FileDatabaseFromEFContext : IFileDatabase
    {
        private readonly ZwinnyCRUDCloudContext _context;

        public FileDatabaseFromEFContext(ZwinnyCRUDCloudContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<File> SingleOrDefaultAsync(int id)
        {
            var removeFile = await _context.File.SingleOrDefaultAsync(m => m.Id == id);
            return removeFile;
        }

        public async Task<File> Delete(int id)
        {
            var removeFile = await _context.File.FindAsync(id);
            if (removeFile == null) return null;

            var RemoveFilePhysical = await _context.File.SingleOrDefaultAsync(m => m.Id == id);
            _context.File.Remove(removeFile);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(RemoveFilePhysical.FilePath);

            return removeFile;
        }

        public async Task<File> FindProjectFile(int id)
        {
            var File = await _context.File.FirstOrDefaultAsync(m => m.ProjectId == id);
            return File;
        }

        public async System.Threading.Tasks.Task Add(File file)
        {
            _context.File.Add(file);
            await _context.SaveChangesAsync();
        }
       
        public IEnumerable<File> FindAll(Func<File, bool> p)
        {
            return _context.File.Where(p);
        }
    }
}
