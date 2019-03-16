using System;
using System.Collections.Generic;

using Xamarin.Forms;

using TouchTracking;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.IO;
using FingerPickerApp;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace FingerPickerApp
{
    public partial class MainPage : ContentPage
    {
        Random random = new Random();



        const double cycleTime = 1000;     

        Stopwatch stopwatch = new Stopwatch();
        Stopwatch choose = new Stopwatch();

        bool pageIsActive;

        float t;
        float a;

        Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
        List<SKPath> completedPaths = new List<SKPath>();
        List<Finger> fingers = new List<Finger>();

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        public MainPage()
        {
            InitializeComponent();
            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Run no = 3");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;

            //animation timer
            stopwatch.Start();

            //timer starts for finger choosing
            choose.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
            {
                t = (float)(stopwatch.Elapsed.TotalMilliseconds % cycleTime / cycleTime);
                canvasView.InvalidateSurface();

                 a = (float)(choose.Elapsed.TotalSeconds);

                if (!pageIsActive)
                {
                    choose.Stop();
                }
                return pageIsActive;
            });
        }
      

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
        
        //******To Do List******//
        
        //When a finger is removed- remove touch
        //circle moves when finger moves - update x/y positions of finger
        //check id issue

            //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Run no = 4");
            //choose random 3 numbers to assign for colour
            //**If you's can find a better way to assign colours- go for it, I did this in 10mins//**
            int randomNumber = random.Next(70, 255);
            int randomNumber1 = random.Next(70, 255);
            int randomNumber2 = random.Next(70, 255);

            switch (args.Type)
            {

                case TouchActionType.Pressed:
                //**Sometimes same id's are assigned - checked this on Omar's phone and this then messes up when a finger is chosen**//

                    Finger finger = new Finger((int)args.Id, args.LocationX, args.LocationY, randomNumber, randomNumber1, randomNumber2);
                    fingers.Add(finger);
                    Console.WriteLine(args.IsInContact);
                    canvasView.InvalidateSurface();
                    Console.WriteLine(args.Id);
                    //  Console.WriteLine((int)args.Id+"fingerX = " + finger.getFingerX() + " fingerY = " + finger.getFingerY());
                    //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< args.id = " + args.Id);
                    break;

                case TouchActionType.Moved:


                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = inProgressPaths[args.Id];
                        // path.LineTo(ConvertToPixel(args.Location));
                        //   Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< location" + args.Location.X);
                        // Console.WriteLine("This is what locations consists of = <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<" + args.Location);

                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    Console.WriteLine(args.Id);


                    //  fingers.RemoveAt((int)args.Id);
                    for (int i = 0; i < fingers.Count; i++)
                    {
                        //      Console.WriteLine("This is me = " + fingers[i]);
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
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float baseRadius = Math.Min(info.Width, info.Height) / 12;

                foreach (Finger finger in fingers)
                {
                for (int circle = 0; circle < 1; circle++)
                {
                    float radius = baseRadius * (circle + t);

                    paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? t : 1);
                    paint.Color = new SKColor((byte)finger.getFingerColour2(), (byte)finger.getFingerColour(), (byte)finger.getFingerColour1());
                    canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), 89, paint);


                    canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), radius, paint);

                }
            }

         //   Console.WriteLine(t);
         
         //*** This works fine but crashes sometimes when 2 same id's are assigned to a finger**//
            if (a>=5.999999)
            {

                //after 6 seconds clear canvas
                canvas.Clear();
                //randoml choose a finger from list
                int r = random.Next(fingers.Count);

                var finger = fingers[r];
                //get the finger id of the randomly chosen finger
                var finger_id = finger.getFingerId();

                //predicate to remove every finger in the list except from chosen finger
                fingers.RemoveAll(Finger => Finger.getFingerId() != finger_id);

                //loop to draw the chosen circle again 
                for (int circle = 0; circle < 1; circle++)
                {
                    float radius = baseRadius * (circle + t);

                    paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? t : 1);
                    paint.Color = new SKColor((byte)finger.getFingerColour2(), (byte)finger.getFingerColour(), (byte)finger.getFingerColour1());
                    canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), radius, paint);

                    canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), radius, paint);
                }
            }
            }
            }
    }




             
        
    

