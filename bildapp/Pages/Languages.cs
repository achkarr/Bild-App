using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace bildapp.Pages
{
    public class Languages : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public Languages()
        {
            Title = "Languages";

            BackgroundColor = Color.White;

            Button English = new Button()
            {
                Padding = 10,
                Text = "English",
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.FromRgba(138, 138, 138, 52),
                HeightRequest = 40
            };

            Button Spanish = new Button()
            {
                Padding = 10,
                Text = "Spanish",
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.FromRgba(138, 138, 138, 52),
                HeightRequest = 40
            };


            English.Clicked += async delegate
            {
                await DisplayAlert("Language Swithced", "Your app is now in English", "Continue");
            };

            Spanish.Clicked += async delegate
            {
                await DisplayAlert("Language Swithced", "Your app is now in Spanish", "Continue");
            };

            var MainContent = new StackLayout()
            {
                Children = {
                    English,
                    Spanish
                }
            };
            Content = new StackLayout
            {
                Padding = new Thickness(20, 20, 20, 20),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                    {
                        new ScrollView()
                        {
                            Content = MainContent
                        },
                    }
            };
        }
    }
}
