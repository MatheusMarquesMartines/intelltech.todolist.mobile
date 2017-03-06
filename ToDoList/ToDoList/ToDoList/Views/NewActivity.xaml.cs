using System;

using ToDoList.Models;

using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class NewActivity : ContentPage
    {
        public Activity Activity { get; set; }

        public NewActivity()
        {
            InitializeComponent();
            date.MinimumDate = DateTime.Now;
            Activity = new Activity
            {
                Titulo = "",
                Descricao = "",
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (!Activity.Titulo.Equals("") && !Activity.Descricao.Equals(""))
            {
                DateTime dt = Convert.ToDateTime(date.Date.ToString().Substring(0, 10) + " " + hour.Time.ToString());
                dt.GetDateTimeFormats();
                Activity.DataHora = dt.ToString("o").Substring(0, 19);
                await DisplayAlert("Sucesso", "Atividade inserida com sucesso.", "Ok");
                MessagingCenter.Send(this, "AddItem", Activity);
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("Erro", "Dados inválidos ou em branco, por favor verifique.", "Ok");

            }
        }
    }
}