using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace bildapp.Pages
{
    public class Settings : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public Settings()
        {
            Title = "Settings";

            BackgroundColor = Color.White;

            Button DeleteAccount = new Button()
            {
                Padding = 10,
                Text = "Delete Account",
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.FromRgba(138, 138, 138, 52),
                HeightRequest = 40
            };

            Button ChangePassword = new Button()
            {
                Padding = 10,
                Text = "Change Password",
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.FromRgba(138, 138, 138, 52),
                HeightRequest = 40
            };

            Button PrivacySettings = new Button()
            {
                Padding = 10,
                Text = "Privacy Settings",
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.FromRgba(138, 138, 138, 52),
                HeightRequest = 40
            };

            Button LanguageSettings = new Button()
            {
                Padding = 10,
                Text = "Language Settings",
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.FromRgba(138, 138, 138, 52),
                HeightRequest = 40
            };

            PrivacySettings.Clicked += async delegate
            {
                await Navigation.PushAsync(new ChangePrivacy());
            };

            ChangePassword.Clicked += async delegate
            {
                await Navigation.PushAsync(new ChangePassword());
            };

            LanguageSettings.Clicked += async delegate
            {
                await Navigation.PushAsync(new Languages());
            };

            DeleteAccount.Clicked += async delegate
            {
                await Navigation.PushAsync(new DeleteAccount());
            };

            var MainContent = new StackLayout()
            {
                Children = {
                    DeleteAccount,
                    ChangePassword,
                    PrivacySettings,
                    LanguageSettings
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
