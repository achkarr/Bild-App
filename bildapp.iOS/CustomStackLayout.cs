using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.IO;
using UIKit;

[assembly: ExportRenderer(typeof(bildapp.Renderer.MyStackLayout), typeof(bildapp.iOS.CustomViewRenderer))]
namespace bildapp.iOS
{
    public class CustomViewRenderer : ViewRenderer<StackLayout, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                bildapp.Renderer.MyStackLayout layout = e.NewElement as bildapp.Renderer.MyStackLayout;
                layout.OnDrawing += NewElement_OnDrawing;
            }
        }

        private void NewElement_OnDrawing(Action<byte[]> action)
        {
            UIGraphics.BeginImageContext(NativeView.Frame.Size);
            NativeView.DrawViewHierarchy(NativeView.Frame, true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            using (var imageData = image.AsPNG())
            {
                var bytes = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                action(bytes);
            }
        }
    }
}