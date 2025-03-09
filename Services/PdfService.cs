using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Vehicle_Maintenance_App.Models;

namespace Vehicle_Maintenance_App.Services
{
    public class PdfService
    {
        public string GenerateMaintenanceReport(Vehicle vehicle)
        {
            using PdfDocument document = new();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);

            graphics.DrawString("Vehicle Report", new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold), PdfBrushes.Black, new Syncfusion.Drawing.PointF(200, 20));
            graphics.DrawString($"Make: {vehicle.Make}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 60));
            graphics.DrawString($"Model: {vehicle.Model}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 90));
            graphics.DrawString($"VIN: {vehicle.Vin}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 120));
            graphics.DrawString($"Year: {vehicle.Year}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 150));
            graphics.DrawString($"Notes: {vehicle.Notes}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 180));

            string filePath = Path.Combine(FileSystem.AppDataDirectory, $"{vehicle.Name}{DateTime.Now:yyyy-MM-ddTHHmmssz}.pdf");
            using FileStream outputStream = new(filePath, FileMode.Create);
            document.Save(outputStream);

            return filePath;
        }

        public async Task Open(string path)
        {
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(path)
            });
        }
    }
}
