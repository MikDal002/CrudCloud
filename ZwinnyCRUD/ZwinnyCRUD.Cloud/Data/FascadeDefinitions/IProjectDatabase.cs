using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface IProjectDatabase
    {
        Task Add(Project project);
        Task<Project> FindOrDefault(int id);
        Task<Project> Delete(int id);
        Task AddOrUpdate(Project project);
        Task<IList<Project>> GetAll();
    }
}
