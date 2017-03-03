using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Helpers;
using ToDoList.Models;
using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    class SyncViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Activity> Activities { get; set; }
        public Command LoadItemsCommand { get; set; }

        public SyncViewModel()
        {
            Title = "ToDoList";
            Activities = new ObservableRangeCollection<Activity>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Activities.Clear();
                var activities = await DataStore.GetItemsAsyncCompare(true);
                Activities.ReplaceRange(activities);
            }
            catch (Exception ex)
            {
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

        public async Task Sync()
        {
            try
            {
                Activities.Clear();
                await DataStore.Synchronize();
                var activities = await DataStore.GetItemsAsyncCompare(true);
                Activities.ReplaceRange(activities);
            }
            catch (Exception ex)
            {
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
