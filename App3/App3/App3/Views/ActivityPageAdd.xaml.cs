using System;

using Xamarin.Forms;

namespace todolist
{
    public partial class ActivityPageAdd : ContentPage
    {
        public ActivityPageAdd()
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel();
        }

        public Activity test(Activity a) {
            using (var data = new AccessDB())
            {
                 data.InsertActivity(a);
                return data.GetActivity(a.ID);
            }
             
        }

        protected async void SaveClicked(object sender, EventArgs e)
        {
            if(_title.Text != "" && _content.Text != "") { 
                var a = new Activity
                {
                    Title = this._title.Text,
                    Content = this._content.Text,
                    Date = this._date.Date.ToString(),
                    Hour = this._hour.Time.ToString(),
                    posSituation = 0
                };
                using (var data = new AccessDB())
                {
                    data.InsertActivity(a);
                }

                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.TextColor = Color.Red;
                messageLabel.Text = "Dados Inválidos";
            }
        }
    }
}
