using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data
{
    public class ProjectDatabaseFromEFContext : IProjectDatabase
    {
        private readonly ZwinnyCRUDCloudContext _context;

        public ProjectDatabaseFromEFContext(ZwinnyCRUDCloudContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Project project)
        {
            if (project.CreationDate == null) throw new ArgumentOutOfRangeException("Project creation date must be set!");
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> Delete(int id)
        {
            var project = await FindOrDefault(id);
            if (project == null) return null;

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project> FindOrDefault(int id)
        {
            var project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
            return project;
        }
    }
}
