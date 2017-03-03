namespace ToDoList.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new ToDoList.App());
        }
    }
}