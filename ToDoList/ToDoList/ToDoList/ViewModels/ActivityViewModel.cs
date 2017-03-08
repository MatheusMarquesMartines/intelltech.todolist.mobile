using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ToDoList.Helpers;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.Views;

using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class ActivityViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Activity> Activities { get; set; }
        public Command LoadItemsCommand { get; set; }
        private RestClient client;

        public ActivityViewModel()
        {
            Title = "ToDoList";
            Activities = new ObservableRangeCollection<Activity>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewActivity, Activity>(this, "AddItem", async (obj, activity) =>
            {
                var _activity = activity as Activity;
                using (var data = new AccessDB())
                {
                    _activity.Synchronized = false;
                    _activity.Concluida = false;
                    _activity.IdFake = 0;
                    data.InsertActivity(_activity);
                }

                await DataStore.AddItemAsync(_activity);
                
            });
        }

        public void DeleteActivity(object commandParameter)
        {
            client = new RestClient();
            using (var data = new AccessDB())
            {
                data.DeleteActivity((Activity)commandParameter);
            }

            Activity a = (Activity)commandParameter;
            if (a.Synchronized == true) {
                client.DeleteActivity(a);
            }
            Activities.Remove((Activity)commandParameter);
        }

        internal void Refresh()
        {
            DataStore.InitializeAsync();
        }

        public async Task ExecuteLoadItemsCommand()
        {
           
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Activities.Clear();
                var activities = await DataStore.GetItemsAsync(true);
                Activities.ReplaceRange(activities);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}