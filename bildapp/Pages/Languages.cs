using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using I18NPortable;

namespace bildapp.Pages
{
    public class Languages : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public Languages()
        {
            Title = "Languages".Translate();

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
                AppSettings.AddOrUpdateValue("Language", "en-US");
                await DisplayAlert("Language_Switched".Translate(), "Language_Switched_Body".Translate(), "Continue".Translate());
            };

            Spanish.Clicked += async delegate
            {
                AppSettings.AddOrUpdateValue("Language", "es-ES");
                await DisplayAlert("Language_Switched".Translate(), "Language_Switched_Body".Translate(), "Continue".Translate());
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
