using Forging_calculation.Models;
using Forging_calculation.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forging_calculation.Pages;

public class ResultModel : PageModel
{
    public Result Result { get; set; }
    private readonly CalculationService _calculationService;
    public ResultModel(CalculationService calculationService)
    {
        _calculationService = calculationService;
    }
    
    public void OnGet(double diameter, double holeDiameter, double H, double x, double y, double z, double Q, double NapuskTO)
    {
        Result = _calculationService.PerformCalculations(diameter, holeDiameter, H, x, y, z, Q, NapuskTO);
    }
}