using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using I18NPortable;

namespace bildapp.Pages
{
    public class Recover : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public Recover()
        {
            Title = "Recover_Password".Translate();

            BackgroundColor = Color.White;

            Entry Email = new Entry()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 10, 0, 15),
                Placeholder = "Email".Translate(),
                BackgroundColor = Color.White,
                MaxLength = 72,
                HeightRequest = 40
            };

            Button RecoverPassword = new Button()
            {
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                Text = "Send_Recovery".Translate(),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            RecoverPassword.Clicked += async delegate
            {
                if (Email.Text != null)
                {
                    var webData = await Misc.MakeConnection("http://34.136.168.234/Api/Recover.php",
                                        "?EMAIL=" + Email.Text);

                    Console.WriteLine("webData:" + webData);
                    //if (webData != "0")
                    //{
                        Email.Text = "";
                        await DisplayAlert("Email_Sent".Translate(), "Email_Send_Body".Translate(), "Continue".Translate());
                        await Navigation.PopAsync();
                    //}
                    //else
                    //{
                    //    await DisplayAlert("Email Could Not Be Sent", "There was an issue sending a recovery email!", "Continue");
                    //}
                    
                }
            };

            var MainContent = new StackLayout()
            {
                Children = {
                    Email
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
                        RecoverPassword
                    }
            };
        }
    }
}
