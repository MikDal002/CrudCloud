﻿using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Dtos;
using ZwinnyCRUD.Common.Models;
using ZwinnyCRUD.Mobile.Models;

namespace ZwinnyCRUD.Mobile.Services
{
    public interface ZwinnyCrudRestInterface
    {
        [Get("api/v1.0/Project")]
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
    public class MockDataStore : IDataStore<Project>
    {
        private const string BaseUrl = "https://zwinnycrudtest.azurewebsites.net/";
        private readonly ZwinnyCrudRestInterface ApiAccess;
        List<Project> items = new List<Project>();

        public MockDataStore()
        {
            ApiAccess = RestClient.For<ZwinnyCrudRestInterface>(BaseUrl);
        }

        public async Task<bool> AddItemAsync(Project item)
        {
            throw new NotImplementedException();

            // items.Add(item);
            // 
            // return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Project item)
        {
            throw new NotImplementedException();

            // var oldItem = items.Where((Project arg) => arg.Id == item.Id).FirstOrDefault();
            // items.Remove(oldItem);
            // items.Add(item);
            // 
            // return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
            // var oldItem = items.Where((Project arg) => arg.Id == id).FirstOrDefault();
            // items.Remove(oldItem);
            // 
            // return await System.Threading.Tasks.Task.FromResult(true);
        }

        public async Task<Project> GetItemAsync(int id)
        {
            return await System.Threading.Tasks.Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Project>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                items.Clear();
                items = (await ApiAccess.GetAllProjectsAsync())
                    .Where (d => d.Id != null)
                    .Where (d => d.CreationDate != null)
                    .Select(d => new Project()
                    {
                        Id = d!.Id.Value,
                        Description = d.Description,
                        Title = d.Title,
                        CreationDate = d!.CreationDate.Value
                    }).ToList();
            }
            return items;
        }
    }
}