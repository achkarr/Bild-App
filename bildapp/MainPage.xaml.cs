using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using bildapp.Pages;

namespace bildapp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

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
    }
}
