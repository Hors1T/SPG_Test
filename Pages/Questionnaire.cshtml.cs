using Forging_calculation.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forging_calculation.Pages;

public class Questionnaire : PageModel
{
    private readonly CalculationService _calculationService;

    public Questionnaire(CalculationService calculationService)
    {
        _calculationService = calculationService;
    }
    [BindProperty] public double D { get; set; }

    [BindProperty] public double d { get; set; }

    [BindProperty] public double H { get; set; }

    [BindProperty] public double x { get; set; }

    [BindProperty] public double y { get; set; }

    [BindProperty] public double z { get; set; }

    [BindProperty] public double Q { get; set; }

    [BindProperty] public double NapuskTO { get; set; }

    public string Result { get; private set; }

    public void OnPost()
    {
        Result = _calculationService.PerformCalculations(D, d, H, x, y, z, Q, NapuskTO);
    }
}