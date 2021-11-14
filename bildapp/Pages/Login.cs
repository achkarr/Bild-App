using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using I18NPortable;

namespace bildapp.Pages
{
    public class Login : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public Login()
        {
            Title = "Sign In";

            BackgroundColor = Color.White;

            Entry Username = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                BackgroundColor = Color.White,
                Placeholder = "Username".Translate(),
                MaxLength = 72,
                HeightRequest = 40
            };

            Entry Password = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Password".Translate(),
                IsPassword = true,
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Button SignInButton = new Button()
            {
                BackgroundColor = Color.FromHex("303F9F"),
                TextColor = Color.White,
                Text = "Login".Translate(),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Button RecoverPassword = new Button()
            {
                BackgroundColor = Color.SteelBlue,
                TextColor = Color.White,
                Text = "Recover".Translate(),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            RecoverPassword.Clicked += async delegate
            {
                await Navigation.PushAsync(new Recover());
            };

            SignInButton.Clicked += async delegate
            {
                if (Username.Text != null && Password.Text != null)
                {
                    if (!Username.Text.Contains(" ") && !Password.Text.Contains(" "))
                    {
                        if (Username.Text.Length > 0 && Password.Text.Length > 0)
                        {
                            string webData = "", LoginToken = "";

                            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);

                            webData = await Misc.MakeConnection("http://34.136.168.234/Api/Login.php",
                                "?USER=" + Username.Text +
                                "&PASS=" + Misc.CreateMD5(Password.Text) +
                                "&INDEX=1");

                            Console.WriteLine("http://34.136.168.234/Api/Login.php" +
                                "?USER=" + Username.Text +
                                "&PASS=" + Misc.CreateMD5(Password.Text) +
                                "&INDEX=1");

                            Console.WriteLine("webData:" + webData);
                            if (webData != "0")
                            {
                                AppSettings.AddOrUpdateValue("token", webData);
                                Application.Current.MainPage = new MainPageCS();
                                //Application.Current.MainPage = new NavigationPage(new MakeImagePage());
                            }
                            else
                                await DisplayAlert("Incorrect_Username_Header".Translate(), "Incorrect_Username_Body".Translate(), "Continue".Translate());

                        }

                    }
                    else
                        await DisplayAlert("Empty_Fields_Header".Translate(), "Empty_Feilds_Body".Translate(), "Continue".Translate());
                }
                else
                    await DisplayAlert("No_Spaces_Header".Translate(), "No_Spaces_Body".Translate(), "Continue".Translate());

            };

            var MainContent = new StackLayout()
            {
                Children = {
                    Username,
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
                        SignInButton,
                        RecoverPassword
                    }
            };
        }
    }
}

