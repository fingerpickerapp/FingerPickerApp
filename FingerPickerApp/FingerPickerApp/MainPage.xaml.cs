using System;
using System.Collections.Generic;

using Xamarin.Forms;

using TouchTracking;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.IO;
using FingerPickerApp;

namespace FingerPickerApp
{
    public partial class MainPage : ContentPage
    {
        Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
        List<SKPath> completedPaths = new List<SKPath>();
        List<Finger> fingers = new List<Finger>();

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Blue,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        public MainPage()
        {
            InitializeComponent();
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Run no = 3");
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Run no = 4");

            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    Finger finger = new Finger((int) args.Id,args.LocationX, args.LocationY, "White");
                    fingers.Add(finger);
                    canvasView.InvalidateSurface();
                    Console.WriteLine("fingerX = " + finger.getFingerX() + " fingerY = " + finger.getFingerY());
                    //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< args.id = " + args.Id);
                    break;

                case TouchActionType.Moved:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        //SKPath path = inProgressPaths[args.Id];
                       //path.LineTo(ConvertToPixel(args.Location));
                        //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< location" + args.Location.X);
                        //Console.WriteLine("This is what locations consists of = <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<" + args.Location);

                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    Console.WriteLine(args.Id);
                    
                    fingers.RemoveAt((int) args.Id);
                    for (int i = 0; i < fingers.Count; i++)
                    {
                        Console.WriteLine("This is me = " + fingers[i]);
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKCanvas canvas = args.Surface.Canvas;
            canvas.Clear();

            foreach (Finger finger in fingers)
            {
                Console.WriteLine("fingerX = " + finger.getFingerX() + " fingerY = " + finger.getFingerY());
                canvas.DrawCircle((float) finger.getFingerX(), (float) finger.getFingerY(), 100, paint);
                canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), 85, paint);
                canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), 70, paint);

            }

            foreach (SKPath path in inProgressPaths.Values)
            {
                canvas.DrawPath(path, paint);
            }
        }
        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }
    }
}
