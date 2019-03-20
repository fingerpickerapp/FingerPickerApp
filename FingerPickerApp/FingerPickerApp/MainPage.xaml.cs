using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Essentials;

using TouchTracking;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;
using Plugin.MediaManager;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;

namespace FingerPickerApp
{
    public partial class MainPage : ContentPage
    {
        // instantiate a random object
        Random random = new Random();

        // store value intothe cycletime
        const double cycleTime = 1000;

        // create a stopwatch instance
        Stopwatch stopwatch = new Stopwatch();

        // create another stopwatch instance
        Stopwatch choose = new Stopwatch();

        // cleate a boolean variable
        bool pageIsActive;

        // floats declared
        float t;
        float a;
        //int index;

        int randomNumber;
        int randomNumber1;
        int randomNumber2;

        // a list of finger objects declared
        List<Finger> fingers = new List<Finger>();

        // painting declared with red colour
        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        // painting declared with black fill colour

        SKPaint black = new SKPaint
        {
            //Style = SKPaintStyle.Stroke,
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black,
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
            //choose.Start();

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

            randomNumber = random.Next(70, 255);
            randomNumber1 = random.Next(70, 255);
            randomNumber2 = random.Next(70, 255);




            switch (args.Type)
            {

                case TouchActionType.Pressed:
                    choose.Reset();
                    
                    Console.WriteLine("I am running");
                    Console.Write("secs=="+a);
                    Finger finger = new Finger((int)args.Id, args.LocationX, args.LocationY, randomNumber, randomNumber1, randomNumber2);
                    fingers.Add(finger);

                    playSound(false);

                    if (fingers.Count >= 2)
                    {
                        choose.Start();
                    }

                      
                    canvasView.InvalidateSurface();
                  
                    break;

                case TouchActionType.Moved:


                    Console.Write("secs==" + a);

                    int index = fingers.FindIndex(Finger => Finger.getFingerId() == args.Id);

                    if (index == args.Id)
                    {
                        fingers[index].setFingerX(args.LocationX);
                        fingers[index].setFingerY(args.LocationY);
                    }

                    break;

                case TouchActionType.Released:
                    Console.WriteLine(args.Id);

                    fingers.RemoveAll(Finger => Finger.getFingerId() == args.Id);

                    break;

                case TouchActionType.Cancelled:

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
                if (finger != null)
                {
                    for (int circle = 0; circle < 1; circle++)
                    {
                        float radius = (baseRadius * (circle + t))*2;

                        paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? t : 1);
                        paint.Color = new SKColor((byte)finger.getFingerColour2(), (byte)finger.getFingerColour(), (byte)finger.getFingerColour1());
                        canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), 89, paint);


                        canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), radius, paint);

                    }
                }
            }

            if (a >= 3)
            {
                //var duration = TimeSpan.FromSeconds(0.5);
                Vibration.Vibrate();
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);

                //after 6 seconds clear canvas
                canvas.Clear();
                //randoml choose a finger from list
                if (fingers.Count >= 1) {
                    int r = random.Next(fingers.Count);
                    var finger = fingers[r];
                    Console.WriteLine(finger);
                    //get the finger id of the randomly chosen finger
                    if (finger != null)
                    {
                        var finger_id = finger.getFingerId();
                        //predicate to remove every finger in the list except from chosen finger
                        fingers.RemoveAll(Finger => Finger.getFingerId() != finger_id);
                        playSound(true);
                    }
                    
                    //loop to draw the chosen circle again 
                    for (int circle = 0; circle < 1; circle++)
                    {
                        if (finger != null)
                        {
                            float radius = (baseRadius * (circle + t))*2;
                            //change the background colour
                            canvas.DrawColor(new SKColor((byte)finger.getFingerColour2(), (byte)finger.getFingerColour(), (byte)finger.getFingerColour1()));
                            black.Color = new SKColor((byte)0, (byte)0, (byte)0);
                            canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), 200, black);
                            Vibration.Cancel();
                            paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? t : 1);
                            paint.Color = new SKColor((byte)finger.getFingerColour2(), (byte)finger.getFingerColour(), (byte)finger.getFingerColour1());
                            canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), radius, paint);
                            canvas.DrawCircle((float)finger.getFingerX(), (float)finger.getFingerY(), radius, paint);
                        }
                     


                    }

                }

            }

        }
        public void playSound(bool chosen)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream;
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

            if(chosen)
            {
                audioStream = assembly.GetManifestResourceStream("FingerPickerApp." + "chosen.mp3");
            }
            else
            {
                audioStream = assembly.GetManifestResourceStream("FingerPickerApp." + "pianonote.mp3");
            }
            player.Load(audioStream);
            player.Play();
        }
    }
    }





             
        
    

