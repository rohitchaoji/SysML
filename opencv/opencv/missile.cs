﻿using radar;
using System.Collections.Generic;
using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Emgu.CV.Dnn;
namespace missile;


public class Missiles : Wepons
{

    public Vector position;
    public double speed;
    public double heading;
    public List<Waypoint> waypoints;
    public bool released;
    public Vector target_coordinates;
    public Missiles(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, bool released, Vector target_coardinates)
        : base(id, platform)
    {
        this.position = position;
        this.speed = speed;
        this.heading = heading;
        this.waypoints = waypoints;
        this.released = released;
        this.target_coordinates = target_coardinates;
    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("Sensor is performing OnTick operation");
    }
}
public class Radar_guided : Missiles
{


    public Radar_guided(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {

    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}
public class Active_guider : Missiles
{
    double onboard_radar;
    public Active_guider(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, double onboard_radar, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {
        this.onboard_radar = onboard_radar;
    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}
public class Image_guidence : Missiles
{
    double onboard__image_sensor;
    public Image_guidence(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, double onboard_image_sensor, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {
        this.onboard__image_sensor = onboard_image_sensor;
    }
    public override void Set(int id)
    {
        this.Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}
public class Gps_guidence : Missiles
{

    public Gps_guidence(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {

    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}
public class Semiacive_guidence : Missiles
{
    double onboard_receiver;
    public Semiacive_guidence(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, double onboard_receiver, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {
        this.onboard_receiver = onboard_receiver;
    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}
public class Passive_guidence : Missiles
{

    public Passive_guidence(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {

    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}
public class Terrian_following_guidence : Missiles
{

    public Terrian_following_guidence(int id, Platform platform, Vector position, double speed, double heading, List<Waypoint> waypoints, bool released, Vector target_coardinates) : base(id, platform, position, speed, heading, waypoints, released, target_coardinates)

    {


    }
    public override void Set(int id)
    {
        Id = id;
    }

    public override int Get()
    {
        return Id;
    }

    public override void OnTick()
    {
        // Implement OnTick logic specific to Sensor
        Console.WriteLine("radar guided is performing OnTick operation");
    }
}