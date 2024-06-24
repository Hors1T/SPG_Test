using Forging_calculation.Models;
using Forging_calculation.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forging_calculation.Pages;

public class Index : PageModel
{
    [BindProperty] public double D { get; set; }

    [BindProperty] public double hd { get; set; }

    [BindProperty] public double H { get; set; }

    [BindProperty] public double x { get; set; }

    [BindProperty] public double y { get; set; }

    [BindProperty] public double z { get; set; }

    [BindProperty] public double Q { get; set; }

    [BindProperty] public double NapuskTO { get; set; }

    public Result Result { get; private set; }

    public IActionResult OnPost()
    {
        return RedirectToPage("/Result", new { diameter = D, holeDiameter = hd, H = H, x = x, y = y, z = z, Q = Q, NapuskTO = NapuskTO });
    }
}