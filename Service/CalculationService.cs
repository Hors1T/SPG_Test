namespace Forging_calculation.Service;

public class CalculationService
{
    public string PerformCalculations(double D, double d, double H, double X, double Y, double Z, double Q,
        double NapuskTO)
    {
        return String.Empty;
        
    }

    public void CalculateHeights(double H, double X, double Y, double Z, double Q,
        double NapuskTO)
    {
        var H1 = (H * X) + (Y * Z)+ NapuskTO;
        var H2 = (H * X) + (Y * Z) + Q + NapuskTO;
    }

    public void CalculateSizeForging(double H1, double H2, double D, double d, double NapuskTO)
    {
        var AllowanceWorkpiece = GetAllowance(H1,D);
        var DeviationWorkpiece = GetDeviation(H1,D);
        var AllowanceWorkpieceSample = GetAllowance(H2,D);
        var DeviationWorkpieceSample = GetDeviation(H2,D);
        var HeightWorkpiece = (H1+AllowanceWorkpiece)+-DeviationWorkpiece;
        var HeightWorkpieceSample = (H2+AllowanceWorkpieceSample)+-DeviationWorkpieceSample;
        var DiameterDetail = D+NapuskTO;
        var DiameterWorkpiece = (D+AllowanceWorkpiece)+-DeviationWorkpiece+NapuskTO;
        var DiameterWorkpieceSample= (D+AllowanceWorkpieceSample)+-DeviationWorkpieceSample+NapuskTO;
        var HoleDiameterDetail = d-NapuskTO;
        var HoleDiameterWorkpiece =(HoleDiameterDetail-AllowanceWorkpiece)+-3*DeviationWorkpiece;
        var HoleDiameterWorkpieceSample =(HoleDiameterDetail-AllowanceWorkpieceSample)+-3*DeviationWorkpieceSample;

    }

    public void CalculateWeightForging()
    {
        
    }
    public double GetAllowance(double Height, double Diameter)
    {
        return 41;
    }
    public double GetDeviation(double Height, double Diameter)
    {
        return 41;
    }
}