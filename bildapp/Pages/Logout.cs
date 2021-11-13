using System;

using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace bildapp.Pages
{
    public class Logout : ContentPage
    {
        public static ISettings AppSettings => CrossSettings.Current;
        public Logout()
        {
            Misc.Token = null;
            AppSettings.AddOrUpdateValue("token", "");
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}

