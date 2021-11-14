using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using I18NPortable;

namespace bildapp.Pages
{
    public class ChangePassword : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public ChangePassword()
        {
            Title = "Change_Password".Translate();

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

            Entry RePassword = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Re-enter Password",
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Button ChangePassword = new Button()
            {
                BackgroundColor = Color.MidnightBlue,
                TextColor = Color.White,
                Text = "Change_Password".Translate(),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Entry NewPassword = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "New_Password".Translate(),
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            ChangePassword.Clicked += async delegate
            {
                if (Password.Text != null && RePassword.Text != null && NewPassword.Text != null)
                {
                    if (RePassword.Text == Password.Text)
                    {
                        if (NewPassword.Text != null)
                        {
                            if (NewPassword.Text != RePassword.Text)
                            {
                                var webData = await Misc.MakeConnection("http://34.136.168.234/Api/Change.php",
                                        "?TOKEN=" + Misc.Token +
                                        "&PASS=" + Misc.CreateMD5(Password.Text) +
                                        "&NEW=" + Misc.CreateMD5(NewPassword.Text));

                                Console.WriteLine("webData:" + webData);
                                if (webData != "0")
                                {
                                    Password.Text = "";
                                    RePassword.Text = "";
                                    NewPassword.Text = "";
                                    await DisplayAlert("Password_Changed_Header".Translate(), "Password_Changed_Body".Translate(), "Continue".Translate());
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Password_Change_Error".Translate(), "Password_Change_Error_Body".Translate(), "Continue".Translate());
                                }
                            }
                            else
                            {
                                await DisplayAlert("Password_Match_Error".Translate(), "Password_Match_Error_Body".Translate(), "Continue".Translate());
                            }
                        }
                    }
                    else
                    {
                        await DisplayAlert("Password_Match_Error".Translate(), "Password_Match_Error_Body".Translate(), "Continue".Translate());
                    }
                }
            };

            var MainContent = new StackLayout()
            {
                Children = {
                    Password,
                    RePassword,
                    NewPassword
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

