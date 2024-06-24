namespace Forging_calculation.Models;

public class Result
{
    public double Height { get; set; }
    public double HeightWorkpiece { get; set; }
    public double HeightWorkpieceSample { get; set; }
    public double Diameter { get; set; }
    public double HoleDiameter { get; set; }
    public ForgingSize ForgingSizeWorkpiece { get; set; }
    public ForgingSize ForgingSizeWorkpieceSample { get; set; }
    public ForgingMass ForgingMass { get; set; }
    public ForgingMass ForgingMassSample { get; set; }
}