using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZwinnyCRUD.Mobile.Services
{
    public interface IFilesDataStore<T>
    {
        Task<bool> AddFileAsync(T item);
        Task<bool> DeleteFileAsync(int id);
        Task<T> GetFileAsync(int id);
        Task<IEnumerable<T>> GetFilesAsync(bool forceRefresh = false);
    }
}
