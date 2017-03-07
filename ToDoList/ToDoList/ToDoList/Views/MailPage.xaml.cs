
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using ToDoList.Services;
using ToDoList.Models;
using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class MailPage : ContentPage
    {
        RestClient _client;

        public MailPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Email"))
            {
                _email.Text = Application.Current.Properties["Email"].ToString();
            }

            if (Application.Current.Properties.ContainsKey("Hour"))
            {
                DateTime dateTime = DateTime.ParseExact(Application.Current.Properties["Hour"].ToString(), "HH:mm:ss",
                                         CultureInfo.InvariantCulture);

                _hour.Time = dateTime.TimeOfDay;
            }

            BindingContext = this;
        }

         public void Save_Clicked(object sender, EventArgs e)
        {
            Email email = new Email();
            _client = new RestClient();
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (!_email.Text.Trim().Equals("") && !_hour.Time.ToString().Trim().Equals(""))
            {
                if (rg.IsMatch(_email.Text.Trim()))
                {
                    Application.Current.Properties["Hour"] = _hour.Time.ToString().Trim();
                    Application.Current.Properties["Email"] = _email.Text.Trim();
                    email.StrEmail = _email.Text.Trim();
                    email.DataHora = _hour.Time.ToString().Trim();

                    _client.UpdateEmail(email);
                    DisplayAlert("Sucesso", "Dados salvos com sucesso", "Ok");
                }
                else
                {
                    DisplayAlert("Erro", "E-mail inválido, por favor corrija.", "Ok");
                }
            }
            else
            {
                DisplayAlert("Erro", "Dados inválidos ou em branco, por favor verifique.", "Ok");
            }
        }
    }
}
