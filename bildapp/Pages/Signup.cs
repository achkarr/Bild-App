using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Rg.Plugins.Popup.Services;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;
using I18NPortable;

namespace bildapp.Pages
{
    public class SignUpPage : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        void OnItemClicked(object sender, EventArgs e)
        {
        }
        public async Task ShowMessage(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }
        public SignUpPage()
        {
            Title = "Sign Up";

            BackgroundColor = Color.White;

            Entry Username = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Username".Translate(),
                BackgroundColor = Color.White,
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
            Entry PasswordReentry = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Password_Confirmation".Translate(),
                IsPassword = true,
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Entry Email = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Email",
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Button SignUpButton = new Button()
            {
                BackgroundColor = Color.MidnightBlue,
                TextColor = Color.White,
                Text = "Signup",
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            SignUpButton.Clicked += async delegate
            {
                if (Username.Text != null && PasswordReentry.Text != null && Password.Text != null && Email.Text != null)
                {
                    if (!Username.Text.Contains(" ") && !Password.Text.Contains(" ") && !PasswordReentry.Text.Contains(" "))
                    {
                        if (Username.Text.Length > 0 && Password.Text.Length > 0 && PasswordReentry.Text.Length > 0 && Email.Text.Length > 0)
                        {
                            if (Password.Text == PasswordReentry.Text)
                            {
                                if (IsValidEmail(Email.Text))
                                {
                                        string webData = "", LoginToken = "";

                                        var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);

                                        webData = await Misc.MakeConnection("http://34.136.168.234/Api/Register.php" + "?USER=" + Username.Text +
                                            "&PASS=" + Misc.CreateMD5(Password.Text) +
                                            "&EMAIL=" + Email.Text, "");

                                        webData = Regex.Replace(webData, @"\s+", "");

                                        if (webData != "0")
                                        {
                                            await ShowMessage("Signup_Completed_Header".Translate(), "Signup_Completed_Body".Translate(), "Continue".Translate(), async () =>
                                            {
                                                Username.Text = "";
                                                Password.Text = "";
                                                PasswordReentry.Text = "";
                                                AppSettings.AddOrUpdateValue("token", webData);
                                                Application.Current.MainPage = new MainPageCS();
                                                //Application.Current.MainPage = new NavigationPage(new MakeImagePage());

                                            });
                                        }
                                        else
                                            await DisplayAlert("Username_Or_Email_Unavailable".Translate(), "Username_Or_Email_Unavailable_Body".Translate(), "Continue".Translate());

                                    }
                                }
                                else
                                    await DisplayAlert("Invalid_Email_Header".Translate(), "Invalid_Email_Body".Translate(), "Continue".Translate());
                            }
                            else
                                await DisplayAlert("Do_Not_Match_Header".Translate(), "Do_Not_Match_Body".Translate(), "Continue".Translate());

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
                    PasswordReentry,
                    Email,
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

