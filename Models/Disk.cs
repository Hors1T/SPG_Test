namespace Forging_calculation.Models;

public class Disk
{
    private double _volume;
    private double _mass;
    public double Diameter { get; set; }
    public double Radius { get; set; }
    public double Height { get; set; }

    public double Volume
    {
        get { return _volume; }
        set { _volume = Math.Round(value, 1); }
    }

    public double Mass
    {
        get { return _mass; }
        set { _mass = Math.Round(value, 3); }
    }
}