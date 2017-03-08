using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T activity);
        Task<bool> UpdateItemAsync(T activity);
        Task<bool> DeleteItemAsync(T activity);
        Task<T> GetItemAsync(long id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsyncCompare(bool forceRefresh = false);

        Task InitializeAsync();
        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();
        Task Synchronize();
    }
}
