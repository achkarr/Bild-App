using Android.App;
using Android.OS;
using Android.Content;
using Android.Util;
using System.Threading.Tasks;

namespace bildapp.Droid
{
    [Activity(Theme = "@style/BildApp.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashPage : Activity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            //AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(1000); // Simulate a bit of startup work.
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
