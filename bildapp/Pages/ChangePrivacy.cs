using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace bildapp.Pages
{
    public class ChangePrivacy : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public ChangePrivacy()
        {
            Title = "Change Privacy";

            BackgroundColor = Color.White;

            Button DataCollection = new Button()
            {
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                Text = "Disable Data Collection",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            DataCollection.Clicked += async delegate
            {
                var webData = await Misc.MakeConnection("http://34.136.168.234/Api/Privacy.php",
                                        "?TOKEN=" + Misc.Token + "&PRIVACY=1");

                await DisplayAlert("Data Collection Disabled", "We turned off data collection for your account!", "Continue");
            };

            Content = new StackLayout
            {
                Padding = new Thickness(20, 20, 20, 20),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                    {
                        DataCollection
                    }
            };
        }
    }
}
