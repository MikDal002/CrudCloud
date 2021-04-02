﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ZwinnyCRUD.Cloud.Data;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Cloud.Hubs;
using System.IO;
using Microsoft.Extensions.FileProviders;

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
            services.AddRazorPages();
            services.AddSignalR();
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            services.AddDbContext<ZwinnyCRUDCloudContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ZwinnyCRUDCloudContext")), ServiceLifetime.Transient);
            services.AddTransient<IProjectDatabase, ProjectDatabaseFromEFContext>();
            services.AddTransient<ITaskDatabase, TaskDatabaseFromEFContext>();
            services.AddTransient<IFileDatabase, FileDatabaseFromEFContext>();

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
