using ToDoList.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToDoList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ActivityPage())
                    {
                        Title = "Activities"
                       
                    },
                    new NavigationPage(new MailPage())
                    {
                        Title = "Email"
                    },
                     new NavigationPage(new SyncPage())
                    {
                        Title = "Sync"
                    },
                }
            };
        }
    }
}
