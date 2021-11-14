using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using bildapp.Pages;
using System.IO;
using I18NPortable;
using System.Diagnostics;
using System.Reflection;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace bildapp
{

    public interface IFileService
    {
        string SavePicture(string name, Stream data, string location = "temp");
    }
    public interface IScreenshotManager
    {
        Task<byte[]> CaptureAsync(StackLayout st);
    }
    public partial class App : Application
    {
        public static ISettings AppSettings => CrossSettings.Current;

        public App()
        {
            InitializeComponent();

            var currentAssembly = GetType().GetTypeInfo().Assembly;

            I18N.Current
                .SetLogger(text => Debug.WriteLine(text))
                .SetNotFoundSymbol("⛔")
                .SetFallbackLocale("ea")
                .Init(currentAssembly);

            var lang = AppSettings.GetValueOrDefault("Language", "");

            if(lang.Length > 0)
            {
                I18N.Current.Locale = lang;
            }

            MainPage = new NavigationPage(new MainPage());//new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
