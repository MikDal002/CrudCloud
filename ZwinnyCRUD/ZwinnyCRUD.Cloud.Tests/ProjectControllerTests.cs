using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NLog;
using NUnit.Framework;
using System;
using System.Data.Common;
using ZwinnyCRUD.Cloud.Data;

namespace ZwinnyCRUD.Cloud.Tests
{

    [TestFixture(Category = "Integration")]
    public class ProjectControllerTests : DatabaseIntegrationTest
    {
        [Test]
        public async System.Threading.Tasks.Task CreateNewTask_ShouldSucceed()
        {
            var iproject = new ProjectDatabaseFromEFContext(Context, new NullLogger<ProjectDatabaseFromEFContext>());
            var projectController = new Pages.Projects.CreateModel(iproject);
            projectController.Project = new Common.Models.Project()
            {
                Description = "That is a description",
                 Title = "My title"
            };
            await projectController.OnPostAsync();
            (await Context.Project.CountAsync()).Should().Be(1);            
        }

        [TearDown]
        public async System.Threading.Tasks.Task Dispose()
        {
            var list = await Context.Project.ToListAsync();
            foreach (var proj in list)
                Context.Project.Remove(proj);
        }
    }

    [TestFixture(Category = "Integration")]
    public class TaskControllerTests : DatabaseIntegrationTest
    {
        [Test]
        public async System.Threading.Tasks.Task CreateTask_WithoutParentProject_DoeasntSucceed()
        {
            var iproject = new ProjectDatabaseFromEFContext(Context, new NullLogger<ProjectDatabaseFromEFContext>());
            var itask = new TaskDatabaseFromEFContext(Context, iproject, new NullLogger<TaskDatabaseFromEFContext>());
            var projectController = new Pages.Tasks.CreateModel(itask, iproject);
            projectController.Task = new Common.Models.Task()
            {
                Description = "That is a description",
                Title = "My title"
            };
            Func<System.Threading.Tasks.Task> exe = async () => await projectController.OnPostAsync();
            exe.Should().Throw<DbUpdateException>();
        }

        [TearDown]
        public async System.Threading.Tasks.Task Dispose()
        {
            var list = await Context.Task.ToListAsync();
            foreach (var proj in list)
                Context.Task.Remove(proj);
        }
    }
}