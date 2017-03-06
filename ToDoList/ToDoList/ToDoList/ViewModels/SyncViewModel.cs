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
        public ObservableRangeCollection<Activity> ActivitiesSync { get; set; }
        public Command LoadItemsCommandSync { get; set; }

        public SyncViewModel()
        {
            Title = "ToDoList";
            ActivitiesSync = new ObservableRangeCollection<Activity>();
            LoadItemsCommandSync = new Command(async () => await ExecuteLoadItemsCommandSync());
        }

        async Task ExecuteLoadItemsCommandSync()
        {
            if (IsBusySync)
                return;

            IsBusySync = true;

            try
            {
                ActivitiesSync.Clear();
                var activities = await DataStore.GetItemsAsyncCompare(true);
                ActivitiesSync.ReplaceRange(activities);
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
                IsBusySync = false;
            }
        }

        public async Task Sync()
        {
            try
            {
                ActivitiesSync.Clear();
                await DataStore.Synchronize();
                var activities = await DataStore.GetItemsAsyncCompare(true);
                ActivitiesSync.ReplaceRange(activities);
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
                IsBusySync = false;
            }
        }
    }
}
