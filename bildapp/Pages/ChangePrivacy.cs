using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using I18NPortable;

namespace bildapp.Pages
{
    public class ChangePrivacy : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public ChangePrivacy()
        {
            Title = "Change_Privacy".Translate();

            BackgroundColor = Color.White;

            Button DataCollection = new Button()
            {
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                Text = "Disable_Data_Collection".Translate(),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            DataCollection.Clicked += async delegate
            {
                var webData = await Misc.MakeConnection("http://34.136.168.234/Api/Privacy.php",
                                        "?TOKEN=" + Misc.Token + "&PRIVACY=1");

                await DisplayAlert("Data_Collection_Disabled".Translate(), "Data_Collection_Disabled_Body".Translate(), "Continue");
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
