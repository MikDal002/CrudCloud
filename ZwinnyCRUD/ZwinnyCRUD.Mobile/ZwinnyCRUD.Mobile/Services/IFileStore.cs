using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ZwinnyCRUD.Mobile.Services
{
    public interface IFileStore<T>
    {
        Task<bool> DeleteFileAsync(int id);
        Task<T> GetFileAsync(string FilePath);
        Task<IEnumerable<T>> GetFilesAsync(int id);
    }
}
