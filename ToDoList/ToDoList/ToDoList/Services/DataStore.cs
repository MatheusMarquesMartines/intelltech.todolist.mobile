using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        bool verifica;
        List<Activity> activities;
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

            return await Task.FromResult(activities);
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
            db = new AccessDB();

            _client = new RestClient();
            if (isInitialized)
                return;

            activities = new List<Activity>();
            var resultServer = await _client.GetActivities();
            activities.Clear();

            var resultLocal = db.GetActivities();
            foreach (var item2 in resultLocal)
            {
                activities.Add(item2);
            }

            
            foreach (var item in resultServer)
            {
                verifica = false;

                for (int i=0; i< resultLocal.Count; i++)
                {
                    if (resultLocal.ElementAt(i).titulo.Equals(item.titulo) && resultLocal.ElementAt(i).dataHora.Equals(item.dataHora))
                    {
                        verifica = true;
                    }
                }

                if (verifica == false)
                {
                    activities.Add(item);
                }
                
            }
            

            isInitialized = true;
        }

        public async Task<IEnumerable<Activity>> GetItemsAsyncCompare(bool forceRefresh = false)
        {
            db = new AccessDB();
            _client = new RestClient();
            activities = new List<Activity>();

            var resultServer = await _client.GetActivities();
            var resultLocal = db.GetActivities();
            bool verifica;
            activities.Clear();

                for (int i = 0; i < resultLocal.Count; i++)
                {
                    verifica = false;
                    for (int j = 0; j < resultServer.Count(); j++)
                    {
                        if(resultLocal.ElementAt(i).titulo.Equals(resultServer.ElementAt(j).titulo) && resultLocal.ElementAt(i).titulo.Equals(resultServer.ElementAt(j).titulo))
                        {
                            verifica = true;
                        }
                    }

                    if (verifica == false)
                    {
                        activities.Add(resultLocal.ElementAt(i));
                    }
                }

                for (int i = 0; i < resultServer.Count(); i++)
                {
                    verifica = false;
                    for (int j = 0; j < resultLocal.Count; j++)
                    {
                        if (resultServer.ElementAt(i).titulo.Equals(resultLocal.ElementAt(j).titulo) && resultServer.ElementAt(i).titulo.Equals(resultLocal.ElementAt(j).titulo))
                        {
                            verifica = true;
                        }
                    }

                    if (verifica == false)
                    {
                        activities.Add(resultServer.ElementAt(i));
                    }
                }

            return await Task.FromResult(activities);
        }

        public async Task Synchronize()
        {
            db = new AccessDB();
            _client = new RestClient();

            var resultServer = await _client.GetActivities();
            var resultLocal = db.GetActivities();
            bool verifica;
            activities = new List<Activity>();

            for (int i = 0; i < resultLocal.Count; i++)
            {
                verifica = false;
                for (int j = 0; j < resultServer.Count(); j++)
                {
                    if (resultLocal.ElementAt(i).titulo.Equals(resultServer.ElementAt(j).titulo) && resultLocal.ElementAt(i).dataHora.Equals(resultServer.ElementAt(j).dataHora))
                    {
                        verifica = true;
                    }
                }

                if (verifica == false)
                {
                    activities.Add(resultLocal.ElementAt(i));
                }
            }

            _client.SendActivity(activities);

            for (int i = 0; i < resultServer.Count(); i++)
            {
                verifica = false;
                for (int j = 0; j < resultLocal.Count; j++)
                {
                    if (resultServer.ElementAt(i).titulo.Equals(resultLocal.ElementAt(j).titulo) && resultServer.ElementAt(i).dataHora.Equals(resultLocal.ElementAt(j).dataHora))
                    {
                        verifica = true;
                    }
                }

                if (verifica == false)
                {
                    db.InsertActivity(resultServer.ElementAt(i));
                }
            }
        }
    }
}
