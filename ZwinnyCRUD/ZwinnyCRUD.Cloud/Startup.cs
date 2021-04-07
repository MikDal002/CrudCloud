using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Cloud.Hubs;
using ZwinnyCRUD.Cloud.Services;

namespace ZwinnyCRUD.Cloud
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ZwinnyCRUDCloudContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("ZwinnyCRUDCloudContext")), ServiceLifetime.Transient);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ZwinnyCRUDCloudContext>();

            services.AddRazorPages();
            services.AddSignalR();
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddTransient<IProjectDatabase, ProjectDatabaseFromEFContext>();
            services.AddTransient<ITaskDatabase, TaskDatabaseFromEFContext>();
            services.AddTransient<IRegisterDatabase, RegisterDatabaseFromEFContext>();
            services.AddTransient<ILoginDatabase, LoginDatabaseFromEFContext>();
            services.AddTransient<ILogoutDatabase, LogoutDatabaseFromEFContext>();
            services.AddTransient<IFileDatabase, FileDatabaseFromEFContext>();

            services.AddTransient<FileUploadService>();

            var filePath = Configuration.GetValue<string>(WebHostDefaults.ContentRootKey) + Configuration.GetValue<string>("StoredFilesPath");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var physicalProvider = new PhysicalFileProvider(filePath);

            services.AddSingleton<IFileProvider>(physicalProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>($"/{nameof(MessageHub)}");
            });
        }
    }
}
