using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.IO;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(bildapp.iOS.TestClass))]
namespace bildapp.iOS
{
    public class TestClass : IScreenshotManager
    {
        public async System.Threading.Tasks.Task<byte[]> CaptureAsync(StackLayout st)
        {
            var view = st.GetRenderer().NativeView;//UIApplication.SharedApplication.KeyWindow.RootViewController.View;

            UIGraphics.BeginImageContext(view.Frame.Size);
            view.DrawViewHierarchy(view.Frame, true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            using (var imageData = image.AsPNG())
            {
                var bytes = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                return bytes;
            }
        }
    }
}
