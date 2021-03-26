using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ProjectDatabaseFromEFContext> _logger;

        public ProjectDatabaseFromEFContext(ZwinnyCRUDCloudContext dbContext, ILogger<ProjectDatabaseFromEFContext> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task Add(Project project)
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

        public async System.Threading.Tasks.Task AddOrUpdate(Project project)
        {
            try
            {
                if (ProjectExists(project.Id))
                {
                    _context.Attach(project).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Add(project);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Cannot update project entry {project.Id} in database becouse of unknown error!");
                throw;
            }
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Project;
        }

        public IEnumerable<Project> FindAll(Func<Project, bool> p)
        {
            return _context.Project.Where(p);
        }
    }
}
