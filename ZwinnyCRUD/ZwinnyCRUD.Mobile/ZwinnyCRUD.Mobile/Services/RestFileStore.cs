using Microsoft.AspNetCore.Http;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwinnyCRUD.Common.Dtos;
using ZwinnyCRUD.Common.Models;

namespace ZwinnyCRUD.Mobile.Services
{
    public interface ZwinnyCrudRestFileInterface
    {
        [Get("api/v1.0/Project/{project_id}/files")]
        Task<IEnumerable<FileDto>> GetFilesAsync([Path] int project_id);

        /*[Get("api/v1.0/File/")]
        Task<FileDto> GetFileAsync([Path] string FilePath);

        [Delete("api/v1.0/File/")]
        Task<FileDto> DeleteFileAsync(int id);*/
    }
    public class RestFileStore : IFileStore<Common.Models.File>
    {
        private const string BaseUrl = "https://zwinnycrudtest.azurewebsites.net/";
        private readonly ZwinnyCrudRestFileInterface ApiAccess;

        public RestFileStore()
        {
            ApiAccess = RestClient.For<ZwinnyCrudRestFileInterface>(BaseUrl);
        }
        public async Task<IEnumerable<Common.Models.File>> GetFilesAsync(int id)
        {                
            var _files = (await ApiAccess.GetFilesAsync(id))
                .Where(d => d.Id != null)
                .Select(d => new Common.Models.File()
                {
                    Id = d.Id.Value,
                    Name = d.Name,
                }).ToList();
            
            return _files;
        }

        /*public Task<bool> DeleteFileAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<File> GetFileAsync(int id)
        {
            throw new NotImplementedException();
        }*/
    }
}