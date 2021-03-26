using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data
{
    public class TaskDatabaseFromEFContext : ITaskDatabase
    {
        private readonly ZwinnyCRUDCloudContext _context;
        private readonly IProjectDatabase _projectContext;
        private readonly ILogger<TaskDatabaseFromEFContext> _logger;

        public TaskDatabaseFromEFContext(ZwinnyCRUDCloudContext dbContext, IProjectDatabase projectContext, ILogger<TaskDatabaseFromEFContext> logger)
        {
            _context = dbContext;
            _projectContext = projectContext;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task Add(Task task)
        {
            if (task.CreationDate == null) throw new ArgumentOutOfRangeException("Task creation date must be set!");
            if (_projectContext.FindOrDefault(task.ProjectId) == null) throw new ArgumentException("Task's parent project doesn't exists!");
            _context.Task.Add(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<Task> Delete(int id)
        {
            var task = await FindOrDefault(id);
            if (task == null) return null;

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async System.Threading.Tasks.Task<Task> FindOrDefault(int id)
        {
            var task = await _context.Task.Include(t => t.Project).FirstOrDefaultAsync(m => m.Id == id);
            return task;
        }

        public async System.Threading.Tasks.Task AddOrUpdate(Task task)
        {
            try
            {
                if (TaskExists(task.Id))
                {
                    _context.Attach(task).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Add(task);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Cannot update task entry {task.Id} in database becouse of unknown error!");
                throw;
            }
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _context.Task.Include(d => d.Project);
        }

        public IEnumerable<Task> FindAll(Func<Task, bool> p)
        {
            return _context.Task.Where(p);
        }
    }
}
