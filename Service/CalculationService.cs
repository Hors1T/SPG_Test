using Forging_calculation.Models;
using Microsoft.EntityFrameworkCore;

namespace Forging_calculation.Service;

public class CalculationService
{
    private ForgingSize forgingSizeWorkpiece { get; set; }
    private ForgingSize forgingSizeWorkpieceSample { get; set; }
    private ForgingMass forgingMass { get; set; }
    private ForgingMass forgingMassSample { get; set; }
    
    private readonly ApplicationDbContext _context;
    public Result Result { get; set; }
    public CalculationService(ApplicationDbContext context)
    {
        _context = context;
        Result  = new Result();
        forgingSizeWorkpiece = new ForgingSize();
        forgingSizeWorkpieceSample = new ForgingSize();
        forgingMass = new ForgingMass();
        forgingMassSample = new ForgingMass();
    }
    public Result PerformCalculations(double D, double d, double H, double X, double Y, double Z, double Q,
        double NapuskTO)
    {
        CalculateHeights(H, X, Y, Z, Q, NapuskTO);
        CalculateSizeForging(Result.HeightWorkpiece,Result.HeightWorkpieceSample,D,d,NapuskTO);
        CalculateWeightWorkpieceForging();
        CalculateWeightWorkpieceSampleForging();

        Result.Height = H;
        Result.ForgingSizeWorkpiece = forgingSizeWorkpiece;
        Result.ForgingSizeWorkpieceSample = forgingSizeWorkpieceSample;
        Result.ForgingMass = forgingMass;
        Result.ForgingMassSample = forgingMassSample;
        
        return Result;
    }

    public void CalculateHeights(double H, double X, double Y, double Z, double Q,
        double NapuskTO)
    {
        Result.HeightWorkpiece = (H * X) + (Y * Z)+ NapuskTO;
        Result.HeightWorkpieceSample = (H * X) + (Y * Z) + Q + NapuskTO;
    }

    public void CalculateSizeForging(double HeightWorkpiece, double HeightWorkpieceSample, double D, double d, double NapuskTO)
    {
        var holeDiameter = d-NapuskTO;
        var diameter = D+NapuskTO;
        Result.Diameter = diameter;
        Result.HoleDiameter = holeDiameter;
        
        forgingSizeWorkpiece.Allowance = GetAllowance(HeightWorkpiece,D).Result;
        forgingSizeWorkpiece.Deviation = GetDeviation(HeightWorkpiece,D).Result;
        forgingSizeWorkpiece.Height = (HeightWorkpiece+forgingSizeWorkpiece.Allowance);
        forgingSizeWorkpiece.Diameter = (D + forgingSizeWorkpiece.Allowance) + NapuskTO;
        forgingSizeWorkpiece.HoleDiameter = (holeDiameter - forgingSizeWorkpiece.Allowance);
       
        forgingSizeWorkpieceSample.Allowance = GetAllowance(HeightWorkpieceSample,D).Result;
        forgingSizeWorkpieceSample.Deviation = GetDeviation(HeightWorkpieceSample,D).Result;
        forgingSizeWorkpieceSample.Height = (HeightWorkpieceSample+forgingSizeWorkpieceSample.Allowance);
        forgingSizeWorkpieceSample.Diameter = (D + forgingSizeWorkpieceSample.Allowance) + NapuskTO;
        forgingSizeWorkpieceSample.HoleDiameter  = (holeDiameter-forgingSizeWorkpieceSample.Allowance);
        
    }

    public void CalculateWeightWorkpieceForging()
    {
        var diskRadiusNominal = forgingSizeWorkpiece.Diameter / 2;
        var diskVolumeNominal = Math.PI*(Math.Pow(diskRadiusNominal,2))*forgingSizeWorkpiece.Height;
        forgingMass.DiskNominal = new Disk()
        {
            Diameter = forgingSizeWorkpiece.Diameter,
            Height = forgingSizeWorkpiece.Height,
            Radius = diskRadiusNominal,
            Volume = diskVolumeNominal,
            Mass = (diskVolumeNominal * 0.78) / 100000000
        };
        
        var diskRadiusMaximal = (forgingSizeWorkpiece.Diameter + forgingSizeWorkpiece.Deviation) / 2;
        var diskVolumeMaximal = Math.PI*(Math.Pow(diskRadiusMaximal,2))*forgingSizeWorkpiece.Height + forgingSizeWorkpiece.Deviation;
        forgingMass.DiskMaximal = new Disk()
        {
            Diameter = forgingSizeWorkpiece.Diameter + forgingSizeWorkpiece.Deviation,
            Height = forgingSizeWorkpiece.Height + forgingSizeWorkpiece.Deviation,
            Radius = diskRadiusMaximal,
            Volume = diskVolumeMaximal,
            Mass = (diskVolumeMaximal * 0.78) / 100000000
        };
        
        var holeRadiusNominal = forgingSizeWorkpiece.HoleDiameter / 2;
        var holeVolumeNominal = Math.PI*(Math.Pow(holeRadiusNominal,2))*forgingSizeWorkpiece.Height;
        forgingMass.HoleNominal = new Hole()
        {
            Diameter = forgingSizeWorkpiece.HoleDiameter,
            Height = forgingSizeWorkpiece.Height,
            Radius = holeRadiusNominal,
            Volume = holeVolumeNominal,
            Mass = (holeVolumeNominal * 0.78) / 100000000
        };
        
        var holeRadiusMaximal = (forgingSizeWorkpiece.HoleDiameter - 3*forgingSizeWorkpiece.Deviation) / 2;
        var holeVolumeMaximal = Math.PI*(Math.Pow(holeRadiusMaximal,2))*forgingSizeWorkpiece.Height;
        forgingMass.HoleMaximal = new Hole()
        {
            Diameter = forgingSizeWorkpiece.HoleDiameter - 3*forgingSizeWorkpiece.Deviation,
            Height = forgingSizeWorkpiece.Height + forgingSizeWorkpiece.Deviation,
            Radius = holeRadiusMaximal,
            Volume = holeVolumeMaximal,
            Mass = (holeVolumeMaximal * 0.78) / 100000000
        };

        forgingMass.MassNominal = forgingMass.DiskNominal.Mass - forgingMass.HoleNominal.Mass;
        forgingMass.MassMaximal = forgingMass.DiskMaximal.Mass - forgingMass.HoleMaximal.Mass;
    }
    public void CalculateWeightWorkpieceSampleForging()
    {
        var diskRadiusNominal = forgingSizeWorkpieceSample.Diameter / 2;
        var diskVolumeNominal = Math.PI*(Math.Pow(diskRadiusNominal,2))*forgingSizeWorkpieceSample.Height;
        forgingMassSample.DiskNominal = new Disk()
        {
            Diameter = forgingSizeWorkpieceSample.Diameter,
            Height = forgingSizeWorkpieceSample.Height,
            Radius = diskRadiusNominal,
            Volume = diskVolumeNominal,
            Mass = (diskVolumeNominal * 0.78) / 100000000
        };
        
        var diskRadiusMaximal = (forgingSizeWorkpieceSample.Diameter + forgingSizeWorkpieceSample.Deviation) / 2;
        var diskVolumeMaximal = Math.PI*(Math.Pow(diskRadiusMaximal,2))*forgingSizeWorkpieceSample.Height + forgingSizeWorkpieceSample.Deviation;
        forgingMassSample.DiskMaximal = new Disk()
        {
            Diameter = forgingSizeWorkpieceSample.Diameter + forgingSizeWorkpieceSample.Deviation,
            Height = forgingSizeWorkpieceSample.Height + forgingSizeWorkpieceSample.Deviation,
            Radius = diskRadiusMaximal,
            Volume = diskVolumeMaximal,
            Mass = (diskVolumeMaximal * 0.78) / 100000000
        };
        
        var holeRadiusNominal = forgingSizeWorkpieceSample.HoleDiameter / 2;
        var holeVolumeNominal = Math.PI*(Math.Pow(holeRadiusNominal,2))*forgingSizeWorkpieceSample.Height;
        forgingMassSample.HoleNominal = new Hole()
        {
            Diameter = forgingSizeWorkpieceSample.HoleDiameter,
            Height = forgingSizeWorkpieceSample.Height,
            Radius = holeRadiusNominal,
            Volume = holeVolumeNominal,
            Mass = (holeVolumeNominal * 0.78) / 100000000
        };
        
        var holeRadiusMaximal = (forgingSizeWorkpieceSample.HoleDiameter - 3*forgingSizeWorkpieceSample.Deviation) / 2;
        var holeVolumeMaximal = Math.PI*(Math.Pow(holeRadiusMaximal,2))*forgingSizeWorkpieceSample.Height;
        forgingMassSample.HoleMaximal = new Hole()
        {
            Diameter = forgingSizeWorkpieceSample.HoleDiameter - 3*forgingSizeWorkpieceSample.Deviation,
            Height = forgingSizeWorkpieceSample.Height + forgingSizeWorkpieceSample.Deviation,
            Radius = holeRadiusMaximal,
            Volume = holeVolumeMaximal,
            Mass = (holeVolumeMaximal * 0.78) / 100000000
        };

        forgingMassSample.MassNominal = forgingMassSample.DiskNominal.Mass - forgingMassSample.HoleNominal.Mass;
        forgingMassSample.MassMaximal = forgingMassSample.DiskMaximal.Mass - forgingMassSample.HoleMaximal.Mass;
    }
    private async Task<double> GetAllowance(double Height, double Diameter)
    {
             var allowance = await _context.Allowances
            .Where(t => Height >= t.H_min && Height <= t.H_max && Diameter >= t.D_min && Diameter <= t.D_max)
            .Select(t => t.allowance)
            .FirstOrDefaultAsync();
             if (allowance != null)
             {
                 return allowance;
             }

             throw new Exception("No matching allowance found.");
    }
    private async Task<double> GetDeviation(double Height, double Diameter)
    {
        var deviation = await _context.Allowances
            .Where(t => Height >= t.H_min && Height <= t.H_max && Diameter >= t.D_min && Diameter <= t.D_max)
            .Select(t => t.Deviation)
            .FirstOrDefaultAsync();
        if (deviation != null)
        {
            return deviation;
        }

        throw new Exception("No matching deviation found.");
    }
}