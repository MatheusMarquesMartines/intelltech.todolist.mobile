using System;
using todolist.Views;
using Xamarin.Forms;

namespace todolist
{
    public partial class ActivityPageList : ContentPage
    {
     
        public ActivityPageList()
        {
            InitializeComponent();       

            using (var data = new AccessDB())
            {
                this.ListActivities.ItemsSource = data.GetActivities();
            }
            
        }

        protected async void addClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActivityPageAdd());
           
        }

        protected async void confEmail(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Configuration());

        }

        private async void Activity_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var activity = (Activity)e.Item;
            //var itemSelecionado2 = (Atividade)ListaAtividades.SelectedItem;
            await Navigation.PushModalAsync(new ModalActivity(activity), true);

            //DisplayAlert("Teste", itemSelecionado1.Title.ToString() + " - " + itemSelecionado2.Title.ToString(), "close");
        }

    }
}