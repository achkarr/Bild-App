using System;
using Xamarin.Forms;
using Android.Content;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.IO;
using Android.App;

[assembly: Xamarin.Forms.Dependency(typeof(bildapp.Droid.TestClass))]
namespace bildapp.Droid
{
    public class TestClass : IScreenshotManager
    {
        public static Activity Activity { get; set; }

        public async System.Threading.Tasks.Task<byte[]> CaptureAsync(StackLayout st)
        {

            if (Activity == null)
            {
                throw new Exception("You have to set ScreenshotManager.Activity in your Android project");
            }

            var view = st.GetRenderer().View;
            //var view = Activity.Window.DecorView;
            view.DrawingCacheEnabled = true;

            Bitmap bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }
    }
}
