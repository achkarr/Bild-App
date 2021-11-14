using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using I18NPortable;

namespace bildapp.Pages
{
    public class DeleteAccount : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public DeleteAccount()
        {
            Title = "Delete_Account".Translate();

            BackgroundColor = Color.White;

            Entry Password = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Password".Translate(),
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Button ChangePassword = new Button()
            {
                BackgroundColor = Color.IndianRed,
                TextColor = Color.White,
                Text = "Delete_Account".Translate(),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ChangePassword.Clicked += async delegate
            {

                if (Password.Text != null)
                {
                    var webData = await Misc.MakeConnection("http://34.136.168.234/Api/Delete.php",
                            "?TOKEN=" + Misc.Token +
                            "&PASS=" + Misc.CreateMD5(Password.Text));

                    Console.WriteLine("webData:" + webData);

                    Console.WriteLine("URL:" + "http://34.136.168.234/Api/Delete.php"+
                            "?TOKEN=" + Misc.Token +
                            "&PASS=" + Misc.CreateMD5(Password.Text));

                    if (webData.Contains("1"))
                    {
                        Password.Text = "";
                        await DisplayAlert("Account_Deleted_Header".Translate(), "Account_Deleted_Body".Translate(), "Continue".Translate());
                        Misc.Token = null;
                        AppSettings.AddOrUpdateValue("token", "");
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Account_Deletion_Failure_Header".Translate(), "Account_Deletion_Failure_Body".Translate(), "Continue".Translate());
                    }   
                }
            };

            var MainContent = new StackLayout()
            {
                Children = {
                    Password,
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
                        ChangePassword
                    }
            };
        }
    }
}

