using System;
using ToDoList.Models;
using ToDoList.Services;
using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class ActivityDetailViewModel : BaseViewModel
    {
        public Activity Activity { get; set; }
        ActivityViewModel ivm = new ActivityViewModel();
        private AccessDB adb;
        private RestClient client;

        public ActivityDetailViewModel(Activity activity = null)
        {
            Title = activity.Titulo;
            Activity = activity;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }

        public void UpdateActivity(Activity activity)
        {
            adb = new AccessDB();
            adb.UpdateActivity(activity);

            client = new RestClient();
            client.UpdateActivity(activity);
        }
    }
}