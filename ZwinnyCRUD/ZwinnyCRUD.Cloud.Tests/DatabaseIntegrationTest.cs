using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using ZwinnyCRUD.Cloud.Data;

namespace ZwinnyCRUD.Cloud.Tests
{
    [TestFixture]
    public abstract class DatabaseIntegrationTest
    {
        public static ZwinnyCRUDCloudContext Context { get; private set; }

        [SetUp]
        public void Setup()
        {
            if (Context != null) return;
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ZwinnyCRUDCloudContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=ZwinnyCRUDCloudContext-{Guid.NewGuid()};Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseInternalServiceProvider(serviceProvider);

            Context = new ZwinnyCRUDCloudContext(builder.Options);
            Context.Database.Migrate();
        }
    }
}