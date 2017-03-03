using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class MailViewModel : BaseViewModel
    {
        public MailViewModel()
        {
            Title = "ToDoList";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
