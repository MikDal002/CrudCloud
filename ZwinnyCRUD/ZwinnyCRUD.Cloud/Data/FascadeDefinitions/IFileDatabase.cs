using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface IFileDatabase
    {
        Task<File> SingleOrDefaultAsync(int id);
        Task<File> Delete(int id);
        Task<File> FindProjectFile(int id);
        IEnumerable<File> FindAll(Func<File, bool> p);
        System.Threading.Tasks.Task Add(File file);
    }
}
