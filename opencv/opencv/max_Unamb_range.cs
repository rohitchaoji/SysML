﻿using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using radar;         // here im using radar namespace of other ( claseses) file 

class max_Unamb_range
{
    static void Main(string[] args)
    {

        double initial_time = 0;

        //Create an empty list of pulses,radarbase,aircraft
       // List<Pulse> pulselist = new List<Pulse>();
        List<RadarBase> radarbaselist = new List<RadarBase>();
        List<Aircraft> aircraftlist = new List<Aircraft>();

        //Dictionary of the pulse

        Dictionary<int, Pulse> pulse_dictionary = new Dictionary<int, Pulse>();

        // radAR BASE class initialsization

        RadarBase radarbase = new RadarBase("27", new Vector(100, 400), 56, 89, [], []);

        //radAR  class initialsization
        Radar radar = new Radar("0",radarbase, "sat", "ant", "none", 0, 0, 1.5,500,1.5,"asp",100.0,200.0,1.0,1.0,100.0);
        radarbase.onboardSensor.Add(radar);// assigning the onboardsensor to a radar
        radarbaselist.Add(radarbase);

        //aircraft class initialsization
        Aircraft aircraft = new Aircraft("5", new Vector(100, 400), 56, 89, [], []);
        aircraftlist.Add(aircraft);                      //here set aircraft pos as same as radar pos bz max unamb range is =500 so that aircraft pos is radarpos+unamgious rangee
   

        int tick = 0;
        int pul_index = 0;  
        int latest_radar_transmit_tick = 0;
        int current_pulse_id = 0;



        while (true)
        {
            Mat image = new Mat(800, 1000, Emgu.CV.CvEnum.DepthType.Cv8U, 3);
            image.SetTo(new Bgr(0, 0, 0).MCvScalar);

            for (int i = aircraftlist.Count - 1; i >= 0; i--)
            {
                //Green Aircraft
                CvInvoke.Circle(image, new Point((int)aircraftlist[i].position.X+500, (int)aircraftlist[i].position.Y), 3, new MCvScalar(0, 255, 0), -1);
            }                                                                //umambrange=c/(2f)  =>c is pulse velocity where pulsevelocity is vector quantity sotake magnitude of it (sqrt(velx square)+(vel.y square))   so we get only one value for range beacuse rngeis scalar quantity
                                                                               //f is prf which is 1/pri   ==> 500/(2*0.001)==500
            
            static double DegreesToRadians(double degrees)// converting degree into radians (azimuth input)
            {
                return degrees * (Math.PI / 180);
            }


            for (int i = radarbaselist.Count - 1; i >= 0; i--)
            {
                //Blue Radar
                CvInvoke.Circle(image, new Point((int)radarbaselist[i].position.X, (int)radarbaselist[i].position.Y), 3, new MCvScalar(255, 0, 0), -1);

                if (tick % radar.pri == 0)
                //if (tick == 0)
                {
                    //Pulse position should be equal to radar base
                    //create a pulse
                    pul_index += 1;
                    double vel_x = Math.Cos(DegreesToRadians(((Radar)radarbase.onboardSensor[0]).azimuth));
                    double vel_y = Math.Sin(DegreesToRadians(((Radar)radarbase.onboardSensor[0]).azimuth));         
            
                    //creating pulse
                    Pulse pulse = new Pulse(current_pulse_id, new Vector(radarbaselist[i].position.X , radarbaselist[i].position.Y), new Vector(vel_x, vel_y),55,"sou",85,75,22,58,10,0,0);//pulse position should be rdar position so it achieve by .x and .y individually 
                    initial_time = tick;
                    //// add that pulse in list of pulses
                    pulse_dictionary.Add(pul_index, pulse);

                }

            }

            //for each pulse in list of pulses{
            for (int i = pulse_dictionary.Count - 1; i >= 0; i--)
            {
                int currentKey = pulse_dictionary.Keys.ElementAt(i);
                Pulse pulse = pulse_dictionary[currentKey];
                pulse.Move();


                Bgr pulse_pixelValue = GetPixelBgr(image, (int)pulse.position.X, (int)pulse.position.Y);

                if (pulse_pixelValue.Green == 255)
                {
                    pulse.reverse();
                }

                if (pulse_pixelValue.Green == 0 && pulse_pixelValue.Blue == 255 && pulse_pixelValue.Red == 0)
                {
                    double time_diff = tick-initial_time;
                    double pul_vel = 0;
                    if (pulse.velocity.X> 0){
                        pul_vel = pulse.velocity.X;
                    }
                    else {
                        pul_vel= -pulse.velocity.X;
                    }
                    double distance = (pul_vel * time_diff) / 2;     // c is the pulse velocity

                    Console.WriteLine($"time_diff: {time_diff}, Distance: {distance+7}, id: {pulse.Id}");
                    
                    pulse_dictionary.Remove(i);
                }

                //   draw red circle for each pulse
                CvInvoke.Circle(image, new Point((int)pulse.position.X, (int)pulse.position.Y), 3, new MCvScalar(0, 0, 255), -1);
                
            }


            tick += 1;
            
            CvInvoke.Imshow("Visualise", image);
            int key = CvInvoke.WaitKey(10);
           
            

            if (key == 113 || key == 81)
            {
                break;

            }
            

        }
        CvInvoke.DestroyAllWindows();
    }
        static Bgr GetPixelValue(Mat image, int x, int y)
        {
            // Get the data pointer for the image
            IntPtr ptr = image.DataPointer;

            // Calculate the byte index corresponding to the pixel at (x, y)
            int pixelSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Bgr));
            int step = image.Step;
            int byteIndex = y * step + x * pixelSize;

            // Read the BGR values from the image data
            byte blue = System.Runtime.InteropServices.Marshal.ReadByte(ptr, byteIndex);
            byte green = System.Runtime.InteropServices.Marshal.ReadByte(ptr, byteIndex + 1);
            byte red = System.Runtime.InteropServices.Marshal.ReadByte(ptr, byteIndex + 2);

            // Create a Bgr structure with the pixel values
            return new Bgr(blue, green, red);
        }
        static Bgr GetPixelBgr(Mat image, int x, int y)
        {
            // Access the BGR values of the pixel at the specified coordinates
            Image<Bgr, byte> img = image.ToImage<Bgr, byte>();
            return img[y, x];
        }
    static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

}


