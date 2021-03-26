using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Models;
using Task = ZwinnyCRUD.Common.Models.Task;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface ITaskDatabase
    {
        System.Threading.Tasks.Task Add(Task project);
        Task<Task> FindOrDefault(int id);
        Task<Task> Delete(int id);
        System.Threading.Tasks.Task AddOrUpdate(Task project);
        IEnumerable<Task> GetAll();
        IEnumerable<Task> FindAll(Func<Task, bool> p);
    }
}
