﻿using System.Collections.Generic;
using System;

uusing System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
namespace Program;

public abstract class BattleSystem
{
    protected BattleSystem(string id)
    {
        Id = id;
    }
    public string Id;
    public abstract void Set(string id);
    public abstract string Get();

    // Abstract method for OnTick
    public abstract void OnTick();
}

public class Platform : BattleSystem
{
    public Vector Position;
    public double Speed;
    public double Heading;
    public List<Waypoint> Waypoints;
    public List<Sensor> OnboardSensor;
    public List<List<double>> RadarCrossSection;

    public Platform(string id, Vector position, double speed, double heading, List<Waypoint> waypoints, List<Sensor> OnBoardsensor) : base(id)
    {
        Position = position;
        Speed = speed;
        Heading = heading;
        Waypoints = waypoints;
        OnboardSensor = OnBoardsensor;
        RadarCrossSection = CreateRadarCrossSection();
    }

    public void MovePlatform()
    {
        // Implement logic for moving the platform based on speed, heading, and waypoints
        Console.WriteLine("Platform is moving!");
    }

    private List<List<double>> CreateRadarCrossSection()
    {
        List<List<double>> table = new List<List<double>>();

        for (int i = 0; i <= 360; i++)
        {
            List<double> row = new List<double>();

            for (double elevation = 0.5; elevation <= 18.5; elevation += 0.5)
            {
                row.Add(1); // Add 1 to the current row (all values are 1)
            }

            table.Add(row);
        }

        return table;
    }

    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Platform
        Console.WriteLine("Platform is performing OnTick operation");
    }
}

public class Vector
{
    public double X;
    public double Y;
    public Vector(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public class Waypoint
{
    public int Location;

    public Waypoint() { }

    public Waypoint(int location)
    {
        Location = location;
    }
}

public class Sensor : BattleSystem
{
    public Platform HostPlatform;
    public Sensor(string id, Platform platform) : base(id)
    {
        HostPlatform = platform;
    }

    public virtual List<object> Detect()
    {
        // Implement logic for detecting targets based on sensor type (e.g., radar)
        Console.WriteLine("Sensor is detecting...");
        return new List<object>(); // Placeholder, replace with actual detected objects
    }

    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("Sensor is performing OnTick operation");
    }
}

public class Radar : Sensor
{

    public string OperatingMode;
    public string Antenna;
    public string Modulation;
    public double Elevation;
    public double Azimuth;
    public double Frequency;
    public int Pri; // Pulse Repetition Interval
    public int Pwd; // Pulse Width Duration
    public string AntennaScanPattern;
    public List<object> Detected;
    public List<List<double>> Gain_table;

    public Radar(string id, Platform platform, string operatingMode, string antenna, string modulation, double elevation, double azimuth, double frequency, int pri, int pwd, string antennaScanPattern/*, List<List<double>> gain_table*/) : base(id, platform)
    {

        OperatingMode = operatingMode;
        Antenna = antenna;
        Modulation = modulation;
        Elevation = elevation;
        Azimuth = azimuth;
        Frequency = frequency;
        Pri = pri;
        Pwd = pwd;
        AntennaScanPattern = antennaScanPattern;
        Detected = new List<object>();
        Gain_table = CreateGainTable();

    }

    public void Transmit()
    {
        // Implement logic for transmitting a signal from the radar
        Console.WriteLine("Radar is transmitting...");
    }

    public object Receive()
    {
        // Implement logic for receiving a signal by the radar
        Console.WriteLine("Radar is receiving...");
        return new object(); // Placeholder for received signal
    }
    private List<List<double>> CreateGainTable()
    {
        List<List<double>> table = new List<List<double>>();

        for (int i = 0; i <= 360; i++)
        {
            // double azimuth = i;

            List<double> row = new List<double>();

            for (double frequency = 0.5; frequency <= 18.5; frequency += 0.5)
            {
                double gain = Math.Cos(DegreesToRadians(i));
                row.Add(gain);
            }

            table.Add(row);
        }

        return table;
    }
    static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

}


public class Pulsed_radar : Radar
{
    public string id;
    public int Pri; // Pulse Repetition Interval
    public int Pwd;
    public int prf;
    public Pulsed_radar(string id, Platform platform, string operatingMode, string antenna, string modulation, double elevation, double azimuth, double frequency, int pri, int pwd, string antennaScanPattern/* List<List<double>> Gain_table*/) : base(id, platform, operatingMode, antenna, modulation, elevation, azimuth, frequency, pri, pwd, antennaScanPattern /*Gain_table*/)

    {
        Pri = pri; // Pulse Repetition Interval
        Pwd = pwd;
        prf = prf;
    }
    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("pulse radar is performing OnTick operation");
    }

}
public class Continous_wave : Radar
{
    public double transmitted_frequency;
    public double Received_frequency;

    public Continous_wave(string id, Platform platform, double transmitted_frequency, double Received_frequency, string operatingMode, string antenna, string modulation, double elevation, double azimuth, double frequency, int pri, int pwd, string antennaScanPattern/* List<List<double>> Gain_table*/) : base(id, platform, operatingMode, antenna, modulation, elevation, azimuth, frequency, pri, pwd, antennaScanPattern/* Gain_table*/)

    {
        transmitted_frequency = transmitted_frequency; // Pulse Repetition Interval
        Received_frequency = Received_frequency;

    }
    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("continious wave is performing OnTick operation");
    }

}

public class Pulse_Doppler : Radar
{

    public Pulse_Doppler(string id, Platform platform, string operatingMode, string antenna, string modulation, double elevation, double azimuth, double frequency, int pri, int pwd, string antennaScanPattern /*List<List<double>> Gain_table*/) : base(id, platform, operatingMode, antenna, modulation, elevation, azimuth, frequency, pri, pwd, antennaScanPattern/*, Gain_table*/)

    {

    }
    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("pulse-dopler is performing OnTick operation");
    }

}



public class Aircraft : Platform
{
    private string id;

    public override void Set(string id)
    {
        this.id = id;
    }

    public override string Get()
    {
        return id;
    }
    public Aircraft(string id, Vector position, double Speed, double Heading, List<Waypoint> Waypoints, List<Sensor> OnboardSensor /*,double radar_cross_section*/) : base(id, position, Speed, Heading, Waypoints, OnboardSensor)
    {


    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Aircraft
        Console.WriteLine("Aircraft is performing OnTick operation");
    }
}
public class RadarBase : Platform
{
    public string id;


    public override void Set(string id)
    {
        this.id = id;
    }

    public override string Get()
    {
        return id;
    }
    public RadarBase(string id, Vector position, double Speed, double Heading, List<Waypoint> Waypoints, List<Sensor> OnboardSensor) : base(id, position, Speed, Heading, Waypoints, OnboardSensor)
    {

    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to RadarBase
        Console.WriteLine("RadarBase is performing OnTick operation");
    }
}
public class Pulse
{
    public int Id;
    public Vector Position;
    public Vector velocity;
    public double energy;
    public string source;
    public double pwd;
    public double frequency;
    public double beam_width;
    public double beam_width_vel;



    public Pulse(int id, Vector position, Vector Velocity, double pwd, double frequency, double beam_width, double beam_width_vel)
    {
        Id = id;
        Position = position;
        velocity = Velocity;
        pwd = pwd;
        frequency = frequency;
        beam_width = beam_width;
        beam_width_vel = beam_width_vel;
    }
    public void Move()
    {
        Position.X += velocity.X;
        Position.Y += velocity.Y;
    }
    public void reverse()
    {
        velocity.X = -velocity.X;
        velocity.Y = -velocity.Y;
    }

}

public class Wepons : BattleSystem
{
    public Platform HostPlatform;
    public Wepons(string id, Platform platform) : base(id)
    {
        HostPlatform = platform;
    }


    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("wepons is performing OnTick operation");
    }
}
public class Guns : Wepons
{
    public string Id;
    public double elivation;
    public double azimuth;
    public Guns(string id, Platform platform, double Elivation, double Azimuth) : base(id, platform)
    {
        Id = id;
        elivation = Elivation;
        azimuth = Azimuth;
    }
}

public class Missiles : Wepons
{

    public Vector Position;
    public double Speed;
    public double Heading;
    public List<Waypoint> Waypoints;
    public Missiles(string id, Platform platform, Vector Position, double Speed, double heading, List<Waypoint> waypoints) : base(id, platform)
    {
        Position = Position;
        Speed = Speed;
        Heading = heading;
        Waypoints = waypoints;
    }
    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("Sensor is performing OnTick operation");
    }
}
public class AAA : Guns
{
    public double shell_fuses_delay;
    public AAA(string id, Platform platform, double shell_fuses_delay, double Elivation, double Azimuth) : base(id, platform, Elivation, Azimuth)
    {
        shell_fuses_delay = shell_fuses_delay;
    }
    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("aaa is performing OnTick operation");
    }
}
public class Radar_guided : Missiles
{

    public Radar_guided(string id, Platform platform, Vector Position, double Speed, double heading, List<Waypoint> waypoints) : base(id, platform, Position, Speed, heading, waypoints)

    {

    }
    public override void Set(string id)
    {
        Id = id;
    }

    public override string Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}





            


