using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Models;
using Register = ZwinnyCRUD.Common.Models.Register;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface IRegisterDatabase
    {
        System.Threading.Tasks.Task<List<string>> Register(Register register);
    }
}
