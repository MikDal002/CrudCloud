using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZwinnyCRUD.Mobile.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddProjectAsync(T item);
        Task<bool> UpdateProjectAsync(T item);
        Task<bool> DeleteProjectAsync(int id);
        Task<T> GetProjectAsync(int id);
        Task<IEnumerable<T>> GetProjectsAsync(bool forceRefresh = false);
    }
}
