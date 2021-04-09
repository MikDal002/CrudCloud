using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Cloud.Data.FascadeDefinitions;
using ZwinnyCRUD.Common.Dtos;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Cloud.Api
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectDatabase _projectDatabase;
        private readonly IFileDatabase _fileDatabase;
        private readonly ITaskDatabase _taskDatabase;

        public ProjectController(IProjectDatabase projectDatabase, IFileDatabase fileDatabase, ITaskDatabase taskDatabase)
        {
            _projectDatabase = projectDatabase;
            _fileDatabase = fileDatabase;
            _taskDatabase = taskDatabase;
        }

        /// <summary>
        /// Zwraca wszystkie dostępne projekty.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<List<ProjectDto>> Get()
        {
            return _projectDatabase.GetAll().Select(d => ProjectDto.FromProject(d)).ToList();
        }

        /// <summary>
        /// Zwraca projekt o podanym ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> Get([Required] int id)
        {
            var proj = await _projectDatabase.FindOrDefault(id);
            if (proj == null) return NotFound();
            else return ProjectDto.FromProject(proj);
        }

        [HttpGet("{id}/tasks/")]
        public async Task<ActionResult<List<ZwinnyCRUD.Common.Models.Task>>> GetTasks([Required] int id)
        {
            var Project = await _projectDatabase.FindOrDefault(id);
            if (Project == null) return NotFound("Project with this id doesn't exist!");
            return _taskDatabase.FindAll(m => (m.ProjectId.Equals(Project.Id))).ToList();
        }

        [HttpGet("/{id}/files/")]
        public async Task<ActionResult<List<ZwinnyCRUD.Common.Models.File>>> GetFiles([Required] int id)
        {
            var Project = await _projectDatabase.FindOrDefault(id);
            if (Project == null) return NotFound("Project with this id doesn't exist!");
            var File = await _fileDatabase.FindProjectFile(id);
            return _fileDatabase.FindAll(m => (m.ProjectId.Equals(Project.Id))).ToList();
        }

        /// <summary>
        /// Usuwa projekt o podanym ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([Required] int id)
        {
            var deletedProject = await _projectDatabase.Delete(id);
            return deletedProject == null ? (StatusCodeResult)NotFound() : NoContent();
        }

        /// <summary>
        /// Tworzy nowy projekt zwracając Projekt z uzupełnionymi szczegółami.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<ProjectDto>> Create([Required] ProjectDto project)
        {
            var myProj = new Project { Description = project.Description, Title = project.Title, CreationDate = DateTimeOffset.Now };
            await _projectDatabase.Add(myProj);
            return ProjectDto.FromProject(myProj);
        }

        /// <summary>
        /// Aktualizuje projekt o podanym ID zgodnie z przesłanymi parametrami.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
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
