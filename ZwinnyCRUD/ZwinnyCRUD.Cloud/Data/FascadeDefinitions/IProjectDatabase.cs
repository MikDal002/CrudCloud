using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface IProjectDatabase
    {
        System.Threading.Tasks.Task Add(Project project);
        Task<Project> FindOrDefault(int id);
        Task<Project> Delete(int id);
        System.Threading.Tasks.Task AddOrUpdate(Project project);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> FindAll(Func<Project, bool> p);
    }
}
