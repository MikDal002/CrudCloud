using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Api
{
    public class ProjectDto
    {
        [Required]
        [StringLength(31, ErrorMessage = "Tytuł jest zbyt długi.")]
        public string Title { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Opis jest zbyt długi.")]
        public string Description { get; set; }

    }

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectDatabase _projectDatabase;

        public ProjectController(IProjectDatabase projectDatabase)
        {
            _projectDatabase = projectDatabase;
        }

        [HttpGet("")]
        public ActionResult<List<Project>> Get()
        {
            return _projectDatabase.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get([Required] int id)
        {
            var proj = await _projectDatabase.FindOrDefault(id);
            if (proj == null) return NotFound();
            else return proj;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([Required] int id)
        {
            var deletedProject = await _projectDatabase.Delete(id);
            return deletedProject == null ? (StatusCodeResult)NotFound() : NoContent();
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> Create([Required] ProjectDto project)
        {
            var myProj = new Project { Description = project.Description, Title = project.Title, CreationDate = DateTimeOffset.Now };
            await _projectDatabase.Add(myProj);
            return myProj.Id;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([Required] int id, string? title, string? description)
        {
            if (string.IsNullOrWhiteSpace(title) && description == null)
            {
                return BadRequest("You have to provide title or desciption!");
            }

            var projectToUpdate = await _projectDatabase.FindOrDefault(id);
            if (projectToUpdate == null) return NotFound("Project with this id doesn't exists!");
            if (!string.IsNullOrWhiteSpace(title))
            {
                projectToUpdate.Title = title;
            }
            if (description != null)
            {
                projectToUpdate.Description = description;
            }
            await _projectDatabase.AddOrUpdate(projectToUpdate);

            return NoContent();
        }
    }
}
