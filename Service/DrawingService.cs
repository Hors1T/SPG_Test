using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

public class DrawingService
{
    public byte[] GenerateDrawing(double holeDiameter, double holeDiameterWorkpiece, double diameter,
        double diameterWorkpiece, double height, double heightWorkpiece, double nominalMass, double maximalMass,
        double deviation)
    {
        var bitmapW = 800;
        var bitmapH = 400;

        using (var bitmap = new Bitmap(bitmapW, bitmapH))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                // Clear background
                graphics.Clear(Color.White);

                // Define pens and brushes
                var pen = new Pen(Color.Black, 2);
                var smallPen = new Pen(Color.Black, 1);
                var bluePen = new Pen(Color.Blue, 2);
                var orangePen = new Pen(Color.Orange, 2);
                var font = new Font("Arial", 10, FontStyle.Italic);
                var brush = new SolidBrush(Color.Black);

                // Draw the main rectangle
                graphics.DrawRectangle(pen, 10, 70, 750, 300);

                // Draw small rectangles 
                graphics.DrawRectangle(pen, 240, 170, 250, 110);
                graphics.DrawRectangle(bluePen, 50, 170, 190, 110);
                graphics.DrawRectangle(bluePen, 490, 170, 190, 110);

                // Draw smallest rectangles 
                graphics.DrawRectangle(pen, 235, 175, 260, 100);
                graphics.DrawRectangle(pen, 55, 175, 180, 100);
                graphics.DrawRectangle(pen, 495, 175, 180, 100);

                //Draw Hatch
                HatchRectangle(graphics, smallPen, 50, 170, 190, 110, 10);
                HatchRectangle(graphics, smallPen, 490, 170, 190, 110, 10);

                // Draw the orange line
                graphics.DrawLine(orangePen, 370, 160, 370, 290);

                //Draw snoski
                graphics.DrawLine(smallPen, 49, 280, 49, 330);
                graphics.DrawLine(smallPen, 680, 280, 680, 330);

                graphics.DrawLine(smallPen, 240, 170, 240, 120);
                graphics.DrawLine(smallPen, 489, 170, 489, 120);

                graphics.DrawLine(smallPen, 680, 169, 730, 169);
                graphics.DrawLine(smallPen, 680, 280, 730, 280);

                //Draw size
                graphics.DrawLine(smallPen, 49, 310, 680, 310);
                graphics.DrawLine(smallPen, 49, 310, 59, 307);
                graphics.DrawLine(smallPen, 49, 310, 59, 313);
                graphics.DrawLine(smallPen, 670, 307, 680, 310);
                graphics.DrawLine(smallPen, 670, 313, 680, 310);

                graphics.DrawLine(smallPen, 240, 140, 489, 140);
                graphics.DrawLine(smallPen, 240, 140, 250, 137);
                graphics.DrawLine(smallPen, 240, 140, 250, 143);
                graphics.DrawLine(smallPen, 479, 137, 489, 140);
                graphics.DrawLine(smallPen, 479, 143, 489, 140);

                graphics.DrawLine(smallPen, 710, 169, 710, 280);
                graphics.DrawLine(smallPen, 710, 169, 713, 179);
                graphics.DrawLine(smallPen, 710, 169, 707, 179);
                graphics.DrawLine(smallPen, 710, 280, 713, 270);
                graphics.DrawLine(smallPen, 710, 280, 707, 270);

                // Draw text
                graphics.DrawString($"Ø{holeDiameter}", font, brush, 350, 125);
                graphics.DrawString($"{holeDiameterWorkpiece}+-{deviation * 3}", font, brush, 340, 143);

                graphics.DrawString($"Ø{diameter}", font, brush, 340, 313);
                graphics.DrawString($"{diameterWorkpiece}+-{deviation}", font, brush, 335, 295);

                var state = graphics.Save();
                graphics.TranslateTransform(710, 210);
                graphics.RotateTransform(-90);
                graphics.DrawString($"{height}", font, brush, -27, -14);
                graphics.DrawString($"{heightWorkpiece}", font, brush, -27, 3);
                graphics.Restore(state);

                graphics.DrawString($"Масса номинал {nominalMass.ToString(CultureInfo.GetCultureInfo("ru-RU"))} тонны",
                    font, brush, 500, 30);
                graphics.DrawString($"Масса максимал {maximalMass.ToString(CultureInfo.GetCultureInfo("ru-RU"))} тонны", font, brush, 500, 50);
                graphics.DrawString($"Выше стрелки - размеры детали", font, brush, 10, 30);
                graphics.DrawString($"Ниже стрелки - размеры поковки", font, brush, 10, 50);
            }

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    private void HatchRectangle(Graphics g, Pen pen, int x, int y, int width, int height, int hatchSpacing)
    {
        for (var i = 0; i < width + height; i += hatchSpacing)
        {
            var startX = x + i;
            var startY = y;
            var endX = x + i - height;
            var endY = y + height;

            if (startX > x + width)
            {
                startX = x + width;
                startY = y + (i - width);
            }

            if (endX < x)
            {
                endX = x;
                endY = y + (i);
            }

            g.DrawLine(pen, startX, startY, endX, endY);
        }
    }
}