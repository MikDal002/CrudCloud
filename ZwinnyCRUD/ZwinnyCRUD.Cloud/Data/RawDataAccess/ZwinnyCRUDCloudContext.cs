using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data
{
    public class ZwinnyCRUDCloudContext : IdentityDbContext
    {
        public ZwinnyCRUDCloudContext (DbContextOptions<ZwinnyCRUDCloudContext> options)
            : base(options)
        {
        }

        public DbSet<ZwinnyCRUD.Common.Models.Project> Project { get; set; }
        public DbSet<ZwinnyCRUD.Common.Models.Task> Task { get; set; }
        public DbSet<ZwinnyCRUD.Common.Models.File> File { get; set; }
    }
}
