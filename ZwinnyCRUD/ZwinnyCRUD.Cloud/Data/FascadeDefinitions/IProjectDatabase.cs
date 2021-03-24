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
    }
}
