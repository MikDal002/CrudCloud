using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZwinnyCRUD.Cloud.Data.FascadeDefinitions
{
    public interface ILogoutDatabase
    {
        System.Threading.Tasks.Task Logout();
    }
}
