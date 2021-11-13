using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using bildapp.Pages;
using System.IO;

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
        public App()
        {
            InitializeComponent();

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
