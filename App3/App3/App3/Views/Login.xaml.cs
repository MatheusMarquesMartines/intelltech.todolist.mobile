using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace todolist
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("email"))
            {
                Navigation.PushAsync(new ActivityPageList());
            }
        }

        protected async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (_email.Text != "" && _password.Text != "")
            {
                Application.Current.Properties["email"] = _email.Text.Trim();
                Application.Current.Properties["password"] = _password.Text.Trim();
                await Navigation.PushAsync(new ActivityPageList());
            }
        }
    }
}
