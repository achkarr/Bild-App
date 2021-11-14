using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using bildapp.Pages;
using Newtonsoft.Json.Linq;

namespace bildapp
{
    public partial class MainPage : ContentPage
    {
        public static async void LoadImageList()
        {
            string Return = "";
            Return = await Misc.MakeConnection("http://34.136.168.234/Api/LoadImages.php", "");
            try
            {
                JObject o = JObject.Parse(Return);
                var Data = (JArray)o["data"];

                foreach (var item in Data)
                {
                    Misc.BackgroundImageArray.Add((string)item);
                }

            }
            catch(Exception e)
            {

            }
        }

        public static async void LoadSavedConfigurations()
        {
            string Return = "";
            Return = await Misc.MakeConnection("http://34.136.168.234/Api/SavedConfigurations.php", "");
            try
            {
                JObject o = JObject.Parse(Return);
                var Data = (JArray)o["data"];

                foreach (var item in Data)
                {
                    SavedConfigurations.SavedConfigs.Add((JObject)item["item"]);
                }

            }
            catch (Exception e)
            {

            }
        }

        public MainPage()
        {
            InitializeComponent();

            LoadImageList();
            LoadSavedConfigurations();
            if (Misc.Token != null)
            {
                if (Misc.Token.Length > 0)
                {
                    string Return = "";
                    Task.Factory.StartNew(async () => {
                        Return = await Misc.MakeConnection("http://34.136.168.234/Api/Login.php?DATA=" + Misc.Token, "&INDEX=2");
                        Console.WriteLine("Return: " + Return);
                        if (Return.Contains("1"))
                        {
                            await Task.Delay(1000);
                            Console.WriteLine("GO: " + Return);
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                Application.Current.MainPage = new MainPageCS();
                            });
                        }
                    });
                }
            }

            
        }

        void Signup(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        void Login(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        void Skip(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPageCS();
        }
    }
}
