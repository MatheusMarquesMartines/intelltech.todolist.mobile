using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncPage : ContentPage
    {
        SyncViewModel viewModel;

        public SyncPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SyncViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Activities.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        public async void Sync_Clicked(object sender, EventArgs e)
        {
            await viewModel.Sync();
        }
    }
}
