using System;

using ToDoList.Models;
using ToDoList.ViewModels;

using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class ActivityPage : ContentPage
    {
        ActivityViewModel viewModel;

        public ActivityPage()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new ActivityViewModel();

            validate();

        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            viewModel.DeleteActivity(mi.CommandParameter);
            validate();

            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }


        async void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            var activity = args.Item as Activity;
            if (activity == null)
                return;

            await Navigation.PushAsync(new ActivityDetail(new ActivityDetailViewModel(activity)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
            
            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewActivity());
        }

       protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Activities.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }

            validate();
            
        }

        private void validate() {
            if (viewModel.Activities.Count != 0)
            {
                aviso.IsVisible = false;
            }
            else
            {
                aviso.IsVisible = true;
            }
        }
    }
}
