namespace Forging_calculation.Models;

public class ForgingMass
{
    private double _massMaximal;
    private double _massNomimal;
    public Disk DiskNominal { get; set; }
    public Disk DiskMaximal { get; set; }
    public Hole HoleNominal { get; set; }
    public Hole HoleMaximal { get; set; }

    public double MassMaximal
    {
        get { return _massMaximal; }
        set { _massMaximal = Math.Round(value, 3); }
    }

    public double MassNominal
    {
        get { return _massNomimal; }
        set { _massNomimal = Math.Round(value, 3); }
    }
}