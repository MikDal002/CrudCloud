using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data
{
    public class LoginDatabaseFromEFContext : ILoginDatabase
    {
        private readonly ILogger<RegisterDatabaseFromEFContext> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginDatabaseFromEFContext(ILogger<RegisterDatabaseFromEFContext> logger,
                                            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public async System.Threading.Tasks.Task<bool> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(
                    login.Email, login.Password, login.RememberMe, false);

            return result.Succeeded;
        }
    }
}
