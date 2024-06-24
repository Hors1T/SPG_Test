using Microsoft.AspNetCore.Mvc;

namespace Forging_calculation.Controllers;

public class DrawingController : Controller
{
    private readonly DrawingService _drawingService;

    public DrawingController(DrawingService drawingService)
    {
        _drawingService = drawingService;
    }

    public IActionResult Generate(double holeDiameter, double holeDiameterWorkpiece,
        double diameter, double diameterWorkpiece,
        double height, double heightWorkpiece,
        double nominalMass, double maximalMass,
        double deviation)
    {
        var imageData = _drawingService.GenerateDrawing(holeDiameter, holeDiameterWorkpiece,
            diameter, diameterWorkpiece,
            height, heightWorkpiece,
            nominalMass, maximalMass,
            deviation);
        return File(imageData, "image/png");
    }
}