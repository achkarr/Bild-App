using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

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
                Placeholder = "Username",
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Entry Password = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Password",
                IsPassword = true,
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Button SignUpButton = new Button()
            {
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                Text = "Login",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            SignUpButton.Clicked += async delegate
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
                                        Application.Current.MainPage = new NavigationPage(new MakeImagePage());
                                    }
                                    else
                                        await DisplayAlert("Incorrect Username Or Password", "The Username Or Password You Have Entered Is Incorrect!", "Continue");

                                }

                    }
                    else
                        await DisplayAlert("Empty Fields", "Please Do Not Leave Any Fields Empty.", "Continue");
                }
                else
                    await DisplayAlert("No Spaces Allowed", "Whitespaces Are Not Allowed In The Username Or Password.", "Continue");

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
                        SignUpButton
                    }
            };
        }
    }
}

