using System;
using Android.Content;
using Android.Preferences;

using Xamarin.Forms;

namespace todolist
{
    public partial class Configuration : ContentPage
    {
        public Context c;

        public Configuration()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("email"))
            {
                string id = Application.Current.Properties["email"].ToString();
                _email.Text = id;
            }

            if (Application.Current.Properties.ContainsKey("hour"))
            {
                string hour = Application.Current.Properties["hour"].ToString();
                _tHour.Text = "Hora de recebimento: " + hour;
            }
        }

        protected async void SaveClicked(object sender, EventArgs e)
        {
            Application.Current.Properties["hour"] = _hour.Time.ToString().Trim();
            await Navigation.PopAsync();
        }

      
    }
}
