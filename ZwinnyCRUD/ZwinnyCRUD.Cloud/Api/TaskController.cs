using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZwinnyCRUD.Cloud.Api
{
    public class TaskDto
    {
        [Required]
        [StringLength(31, ErrorMessage = "Title is too long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }
    }

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskDatabase _taskDatabase;
        public TaskController(ITaskDatabase taskDatabase)
        {
            _taskDatabase = taskDatabase;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> Get([Required] int id)
        {
            var task = await _taskDatabase.FindOrDefault(id);

            if (task == null) return NotFound();
            else
            {
                var taskDto = new TaskDto();
                taskDto.Title = task.Title;
                taskDto.Description = task.Description;
                return taskDto;
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> Create([Required] TaskDto task)
        {
            var myTask = new Common.Models.Task { Description = task.Description, Title = task.Title };
            await _taskDatabase.Add(myTask);

            return myTask.Id;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([Required] int id, string? title, string? description)
        {
            if (string.IsNullOrWhiteSpace(title) && description == null)
            {
                return BadRequest("You have to provide title or desciption!");
            }

            var taskToUpdate = await _taskDatabase.FindOrDefault(id);
            if (taskToUpdate == null) return NotFound("Task with this id doesn't exists!");
            if (!string.IsNullOrWhiteSpace(title))
            {
                taskToUpdate.Title = title;
            }
            if (description != null)
            {
                taskToUpdate.Description = description;
            }
            await _taskDatabase.AddOrUpdate(taskToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([Required] int id)
        {
            var deletedTask = await _taskDatabase.Delete(id);

            return deletedTask == null ? (StatusCodeResult)NotFound() : NoContent();
        }

    }
}
