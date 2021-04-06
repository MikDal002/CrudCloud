using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;

namespace ZwinnyCRUD.Cloud.Data
{
    public class LogoutDatabaseFromEFContext : ILogoutDatabase
    {
        private readonly ILogger<RegisterDatabaseFromEFContext> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutDatabaseFromEFContext(ILogger<RegisterDatabaseFromEFContext> logger,
                                            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public async System.Threading.Tasks.Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
