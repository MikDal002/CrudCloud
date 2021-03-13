using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ZwinnyCRUDCloudContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ZwinnyCRUDCloudContext>>()))
            {
                if (context.Project.Any())
                {
                    return;   // DB has been seeded
                }

                context.Project.AddRange(
                    new Project
                    {
                        Title = "GitHub Notifier",
                        Description = "Listen for events from GitHub and notify you.",
                        CreationDate = DateTime.Parse("2020-10-12")
                    },

                    new Project
                    {
                        Title = "Food log",
                        Description = "Keep track of everything you eat with a simple submission form.",
                        CreationDate = DateTime.Parse("2020-10-29")
                    },

                    new Project
                    {
                        Title = "RSS aggregator",
                        Description = "Poll RSS feeds for new articles and make a new feed that combines them.",
                        CreationDate = DateTime.Parse("2020-11-16")
                    },

                    new Project
                    {
                        Title = "Weather App",
                        Description = "Use the Forecast.io api to display the weather near you.",
                        CreationDate = DateTime.Parse("2020-12-15")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
