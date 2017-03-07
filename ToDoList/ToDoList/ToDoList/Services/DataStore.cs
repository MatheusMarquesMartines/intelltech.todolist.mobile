using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToDoList.Services.DataStore))]
namespace ToDoList.Services
{
    public class DataStore : IDataStore<Activity>
    {
        bool isInitialized;
        List<Activity> SortedList;
        List<Activity> activities;
        List<Activity> activities2;

        private AccessDB db;
        private RestClient _client;

        public async Task<bool> AddItemAsync(Activity a)
        {
            await InitializeAsync();
            activities = new List<Activity>();
            activities.Add(a);
            

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Activity a)
        {
            await InitializeAsync();

            var _activity = activities.Where((Activity arg) => arg.Id == a.Id).FirstOrDefault();
            activities.Remove(_activity);
            activities.Add(a);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Activity a)
        {
            await InitializeAsync();

            var _activity = activities.Where((Activity arg) => arg.Id == a.Id).FirstOrDefault();
            activities.Remove(_activity);

            return await Task.FromResult(true);
        }

        public async Task<Activity> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(activities.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Activity>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(SortedList);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            isInitialized = false;
            db = new AccessDB();

            _client = new RestClient();
            if (isInitialized)
                return;

            activities = new List<Activity>();
            activities.Clear();

            var resultLocal = db.GetActivities();
            foreach (var item2 in resultLocal)
            {
               DateTime dt = Convert.ToDateTime(item2.DataHora);
               item2.DataHora = dt.ToString("G");
                activities.Add(item2);
            }

            SortedList = activities.OrderBy(o => o.DataHora).ToList();

            isInitialized = true;
        }

        public async Task<IEnumerable<Activity>> GetItemsAsyncCompare(bool forceRefresh = false)
        {
            db = new AccessDB();
            _client = new RestClient();
            activities2 = new List<Activity>();

            var resultServer = await _client.GetActivities();
            var resultLocal = db.GetActivities();
            bool verifica;
            activities2.Clear();

                for (int i = 0; i < resultLocal.Count; i++)
                {
                    verifica = false;
                    for (int j = 0; j < resultServer.Count(); j++)
                    {
                        if(resultLocal.ElementAt(i).Titulo.Equals(resultServer.ElementAt(j).Titulo) && resultLocal.ElementAt(i).DataHora.Substring(0, 19).Equals(resultServer.ElementAt(j).DataHora.Substring(0, 19)))
                        {
                            verifica = true;
                        }
                    }

                    if (verifica == false)
                    {
                  
                    activities2.Add(resultLocal.ElementAt(i));
                    }
                }

                for (int i = 0; i < resultServer.Count(); i++)
                {
                    verifica = false;
                    for (int j = 0; j < resultLocal.Count; j++)
                    {
                    
                    if (resultServer.ElementAt(i).Titulo.Equals(resultLocal.ElementAt(j).Titulo) && resultServer.ElementAt(i).DataHora.Substring(0, 19).Equals(resultLocal.ElementAt(j).DataHora.Substring(0, 19)))
                        {
                            verifica = true;
                        }
                    }

                    if (verifica == false)
                    {
                  
                    activities2.Add(resultServer.ElementAt(i));
                    }
                }
             formatDate(activities2);
            return await Task.FromResult(activities2);
        }

        private void formatDate(List<Activity> activities2)
        {
            foreach (var item in activities2)
            {
                DateTime dt = Convert.ToDateTime(item.DataHora);
                item.DataHora = dt.ToString("G");
            }
        }

        public async Task Synchronize()
        {
            db = new AccessDB();
            _client = new RestClient();

            var resultServer = await _client.GetActivities();
            var resultLocal = db.GetActivities();
            bool verifica;
            activities2 = new List<Activity>();

            for (int i = 0; i < resultLocal.Count; i++)
            {
                verifica = false;
                for (int j = 0; j < resultServer.Count(); j++)
                {
                    if (resultLocal.ElementAt(i).Titulo.Equals(resultServer.ElementAt(j).Titulo) && resultLocal.ElementAt(i).DataHora.Substring(0, 19).Equals(resultServer.ElementAt(j).DataHora.Substring(0, 19)))
                    {
                        verifica = true;
                    }
                }

                if (verifica == false)
                {
                    if (resultLocal.ElementAt(i).Synchronized != true)
                    {
                        resultLocal.ElementAt(i).Synchronized = true;
                        db.UpdateActivity(resultLocal.ElementAt(i));
                        activities2.Add(resultLocal.ElementAt(i));
                    }
                    else
                    {
                        db.DeleteActivity(resultLocal.ElementAt(i));
                    }

                }
            }

            _client.SendActivity(activities2);

            for (int i = 0; i < resultServer.Count(); i++)
            {
                verifica = false;
                for (int j = 0; j < resultLocal.Count; j++)
                {
                    if (resultServer.ElementAt(i).Titulo.Equals(resultLocal.ElementAt(j).Titulo) && resultServer.ElementAt(i).DataHora.Substring(0, 19).Equals(resultLocal.ElementAt(j).DataHora.Substring(0, 19)))
                    {
                        verifica = true;
                    }
                }

                if (verifica == false)
                {
                    resultServer.ElementAt(i).Synchronized = true;
                    db.InsertActivity(resultServer.ElementAt(i));
                    await AddItemAsync(resultServer.ElementAt(i));
                }
            }
            await InitializeAsync();
        }
    }
}
