using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Dtos;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Mobile.Services
{
    public interface ZwinnyCrudRestInterface
    {
        [Get("api/v1.0/Project")]
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();

        [Post("api/v1.0/Project/")]
        System.Threading.Tasks.Task<ProjectDto> AddNewProject([Body] ProjectDto project);
    }
    public class RestDataStore : IDataStore<Project>
    {
        private const string BaseUrl = "https://zwinnycrudtest.azurewebsites.net/";
        // private const string BaseUrl = "http://192.168.0.227:45455/";
        private readonly ZwinnyCrudRestInterface ApiAccess;
        List<Project> _projects = new List<Project>();

        public RestDataStore()
        {
            ApiAccess = RestClient.For<ZwinnyCrudRestInterface>(BaseUrl);
        }

        public async Task<bool> AddProjectAsync(Project project)
        {
            var proj = await ApiAccess.AddNewProject(ProjectDto.FromProject(project));
            if (proj.Id == null || proj.CreationDate == null)
            {
                return false;
            }
            project.Id = proj.Id.Value;
            project.CreationDate = proj.CreationDate.Value;
            return true;
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            throw new NotImplementedException();

            // var oldItem = items.Where((Project arg) => arg.Id == item.Id).FirstOrDefault();
            // items.Remove(oldItem);
            // items.Add(item);
            // 
            // return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            throw new NotImplementedException();
            // var oldItem = items.Where((Project arg) => arg.Id == id).FirstOrDefault();
            // items.Remove(oldItem);
            // 
            // return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await System.Threading.Tasks.Task.FromResult(_projects.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                _projects.Clear();
                _projects = (await ApiAccess.GetAllProjectsAsync())
                    .Where(d => d.Id != null)
                    .Where(d => d.CreationDate != null)
                    .Select(d => new Project()
                    {
                        Id = d.Id.Value,
                        Description = d.Description,
                        Title = d.Title,
                        CreationDate = d.CreationDate.Value
                    }).ToList();
            }
            return _projects;
        }
    }
}