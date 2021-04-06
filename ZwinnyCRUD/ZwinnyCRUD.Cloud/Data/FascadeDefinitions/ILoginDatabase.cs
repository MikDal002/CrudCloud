using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login = ZwinnyCRUD.Common.Models.Login;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface ILoginDatabase
    {
        System.Threading.Tasks.Task<bool> Login(Login login);
    }
}
