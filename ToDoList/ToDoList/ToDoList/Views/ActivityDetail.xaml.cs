
using System;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.ViewModels;

using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class ActivityDetail : ContentPage
    {
        ActivityDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ActivityDetail()
        {
            InitializeComponent();
        }

        public ActivityDetail(ActivityDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            if (_switch.IsToggled == true)
            {
                _status.Text = "Situação: Concluído";
            }
            else
            {
                _status.Text = "Situação: Não Concluído";
            }
        }

         void UpdateItem_Clicked(object sender, EventArgs e)
        {
            viewModel.Activity.Concluida = _switch.IsToggled;
            viewModel.UpdateActivity(viewModel.Activity);
            Navigation.PopAsync();
        }
    }
}
