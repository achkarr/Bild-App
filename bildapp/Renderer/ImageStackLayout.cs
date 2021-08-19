using System;
using Xamarin.Forms;

namespace bildapp.Renderer
{
    public class MyStackLayout : StackLayout
    {
        public delegate void DrawImageHandler(Action<byte[]> action);
        public DrawImageHandler OnDrawing;
    }
}
