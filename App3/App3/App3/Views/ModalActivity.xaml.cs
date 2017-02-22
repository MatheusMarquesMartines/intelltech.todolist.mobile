using System;

using Xamarin.Forms;

namespace todolist.Views
{
    public partial class ModalActivity : ContentPage
    {

        Activity activity;

        public ModalActivity(Activity a)
        {
            InitializeComponent();
            activity = a;
            a.setSituation(a.posSituation);

            Label header = new Label
            {
                Text = a.Title,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Label date = new Label
            {
                Text = a.Date.Substring(0, 10) + " " + a.Hour.Substring(0, 5),
                HorizontalOptions = LayoutOptions.Center
            };

            Label body = new Label
            {
                Text = a.Content,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };


            Label switcherText = new Label
            {
                Text = "Situação da Atividade: " + a.getSituation(),
                HorizontalOptions = LayoutOptions.Center
                //VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Switch switcher;

            if (a.posSituation == 1)
            {
                switcher = new Switch
                {
                    IsToggled = true,
                    HorizontalOptions = LayoutOptions.Center
                };
            }
            else
            {
                switcher = new Switch
                {
                    IsToggled = false,
                    HorizontalOptions = LayoutOptions.Center
                };
            }
            switcher.Toggled += switcher_Toggled;

            Button button = new Button
            {
                Text = "Fechar",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += ButtonCloseModal_OnClicked;


   

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    date,
                    body,
                    switcherText,
                    switcher,
                    button
                }
            };

        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            if (activity.posSituation == 0)
            {
                activity.posSituation = 1;
            }
            else
            {
                activity.posSituation = 0;
            }

            activity.setSituation(activity.posSituation);

            using (var data = new AccessDB())
            {
                data.UpdateActivity(activity);
            }
        }

        private async void ButtonCloseModal_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
