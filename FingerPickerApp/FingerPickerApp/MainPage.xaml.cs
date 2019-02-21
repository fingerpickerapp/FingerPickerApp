using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace FingerPickerApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


       SKPaint redFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Red
        };

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.Black);

            int width = e.Info.Width;
            int height = e.Info.Height;

            canvas.DrawCircle(200,200, 70, redFillPaint);
            
        }
    }
}
