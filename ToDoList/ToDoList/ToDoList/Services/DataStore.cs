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
        List<Activity> SortedList, activities;
        private AccessDB db;
        private RestClient _client;

        private void formatDate(List<Activity> activities)
        {
            foreach (var item in activities)
            {
                DateTime dt = Convert.ToDateTime(item.DataHora);
                item.DataHora = dt.ToString("G");
            }
        }

        public async Task<bool> AddItemAsync(Activity a)
        {
            //await InitializeAsync();
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

        public async Task<Activity> GetItemAsync(long id)
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
            if (isInitialized) { return; }

            db = new AccessDB();
            _client = new RestClient();
            var fake = new List<Activity>();
            activities = new List<Activity>();
            activities.Clear();

            var result = db.GetActivities();

            foreach (var item in result)
            {
                if (item.IdFake != 0 && item.Synchronized == true)
                {
                    DateTime dt = Convert.ToDateTime(item.DataHora);
                    item.DataHora = dt.ToString("G");
                    activities.Add(item);
                }
                else
                {
                    fake.Add(item);
                }
            }

            if (fake.Count > 0)
            {
                bool verifica = false;
                synchronizeIdFake(fake);
                foreach (var item in fake)
                {
                    foreach (var item2 in activities)
                    {
                        if (item.GUID.Equals(item2.GUID))
                        {
                            verifica = true;
                        }
                    }
                    if (verifica != true)
                    {
                        activities.Add(item);
                    }
                    verifica = false;
                }
            }

            SortedList = activities.OrderBy(o => o.DataHora).ToList();
            isInitialized = true;
        }

        public async Task<IEnumerable<Activity>> GetItemsAsyncCompare(bool forceRefresh = false)
        {
            db = new AccessDB();
            _client = new RestClient();
            activities = new List<Activity>();
            activities.Clear();

            var resultServer = await _client.GetActivities();
            var resultLocal = db.GetActivities();
            bool verifica = false;


            foreach (var server in resultServer) {
                foreach (var local in resultLocal) {
                    if (server.GUID.Equals(local.GUID)) {
                        verifica = true;
                    }
                }
                if (verifica != true) {
                    activities.Add(server);
                }
                verifica = false;
            }

            foreach (var local in resultLocal)
            {
                foreach (var server in resultServer)
                {
                    if (local.GUID.Equals(server.GUID))
                    {
                        verifica = true;
                    }
                }
                if (verifica != true)
                {
                    var verifica2 = false;
                    foreach (var item2 in activities)
                    {
                        if (local.GUID.Equals(item2.GUID))
                        {
                            verifica2 = true;
                        }
                    }
                    if (verifica2 != true)
                    {
                        activities.Add(local);
                    }
                }
                verifica = false;
            }

            formatDate(activities);
            return await Task.FromResult(activities);
        }

        public async Task Synchronize()
        {
            db = new AccessDB();
            _client = new RestClient();
            activities = new List<Activity>();
            activities.Clear();

            var resultServer = await _client.GetActivities();
            var resultLocal = db.GetActivities();
            bool verifica;
            

            for (int i = 0; i < resultLocal.Count; i++){
                verifica = false;
                for (int j = 0; j < resultServer.Count(); j++){
                    if (resultLocal.ElementAt(i).GUID.Equals(resultServer.ElementAt(j).GUID)){
                        verifica = true;
                    }
                }

                if (verifica == false){
                    if (resultLocal.ElementAt(i).Synchronized != true) {
                        resultLocal.ElementAt(i).Synchronized = true;
                        db.UpdateActivity(resultLocal.ElementAt(i));
                        activities.Add(resultLocal.ElementAt(i));
                    }else{
                        db.DeleteActivity(resultLocal.ElementAt(i));
                    }

                }
            }
 
            _client.SendActivity(activities);
            synchronizeIdFake(activities);

            for (int i = 0; i < resultServer.Count(); i++){
                verifica = false;
                for (int j = 0; j < resultLocal.Count; j++){
                    if (resultServer.ElementAt(i).GUID.Equals(resultLocal.ElementAt(j).GUID)){
                        verifica = true;
                    }
                }

                if (verifica == false){
                    resultServer.ElementAt(i).Synchronized = true;
                    resultServer.ElementAt(i).IdFake = resultServer.ElementAt(i).Id;
                    db.InsertActivity(resultServer.ElementAt(i));
                    await AddItemAsync(resultServer.ElementAt(i));
                }
            }
            await InitializeAsync();
        }

        private async void synchronizeIdFake(List<Activity> activities){
            var server = await _client.GetActivities();

            foreach (var a in activities) {
                foreach (var s in server) {
                    if (a.GUID.Equals(s.GUID)) {
                        a.IdFake = s.Id;
                        db.UpdateActivity(a);
                    }
                }
            }
        }
    }
}
