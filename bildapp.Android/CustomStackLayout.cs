using System;
using Xamarin.Forms;
using Android.Content;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.IO;

[assembly: ExportRenderer(typeof(bildapp.Renderer.MyStackLayout), typeof(bildapp.Droid.CustomViewRenderer))]
namespace bildapp.Droid
{
    public class CustomViewRenderer : ViewRenderer<bildapp.Renderer.MyStackLayout, Android.Views.View>
    {
        public CustomViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<bildapp.Renderer.MyStackLayout> e)
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
            if (this.ViewGroup != null)
            {
                int width = ViewGroup.Width;
                int height = ViewGroup.Height;

                //create and draw the bitmap
                Bitmap bmp = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                Canvas c = new Canvas(bmp);
                ViewGroup.Draw(c);

                MemoryStream stream = new MemoryStream();
                bmp.Compress(Bitmap.CompressFormat.Png, 100, stream);
                byte[] byteArray = stream.ToArray();
                action(byteArray);
            }
        }
    }
}
