using Xamarin.Forms;

namespace todolist
{
    public class App : Application
    {
        public App()
        {
            // MainPage = new AtividadePageList();
            if (Application.Current.Properties.ContainsKey("email"))
            {
                MainPage = new NavigationPage(new ActivityPageList());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
