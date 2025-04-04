using System.Data;
using System.Text.Json;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Vehicle_Maintenance_App.Configuration;
using Vehicle_Maintenance_App.Data;
using Vehicle_Maintenance_App.Models;

namespace Vehicle_Maintenance_App.Services
{
    public class PdfService
    {
        private DatabaseService _databaseService;
        private string PdfDirectoryPath(int vehicleId) => Path.Combine(FileSystem.AppDataDirectory, vehicleId.ToString());

        public PdfService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<string> GenerateMaintenanceReport(Vehicle vehicle)
        {
            using PdfDocument document = new();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfGrid pdfGrid = new PdfGrid();
            PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);

            var events = await _databaseService.GetMaintenanceEventsByVehicleIdAsync(vehicle.Id);

            graphics.DrawString("Maintenance Report", new PdfStandardFont(PdfFontFamily.TimesRoman, 14, PdfFontStyle.Bold), PdfBrushes.Black, new Syncfusion.Drawing.PointF(200, 20));
            graphics.DrawString($"Make: {vehicle.Make}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 50));
            graphics.DrawString($"Model: {vehicle.Model}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 70));
            graphics.DrawString($"VIN: {vehicle.Vin}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 90));
            graphics.DrawString($"Year: {vehicle.Year}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 110));
            graphics.DrawString($"Notes: {vehicle.Notes}", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(20, 130));

            // Create a DataTable to hold MaintenanceEvent data
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Event Type");
            dataTable.Columns.Add("Parts");
            dataTable.Columns.Add("Labor Cost");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Performed By");
            dataTable.Columns.Add("Date");

            foreach (MaintenanceEvent e in events)
            {
                dataTable.Rows.Add(
                    e.EventType,
                    e.PartsUsed == null ? Constants.NotApplicable : PartsJsonToString(e.PartsUsed),
                    e.LaborCost.HasValue ? e.LaborCost.Value.ToString("C") : Constants.NotApplicable,
                    e.Description ?? Constants.NotApplicable,
                    e.PerformedBy,
                    e.PerformedTimestamp.ToString("yyyy-MM-dd HH:mm")
                );
            }

            pdfGrid.DataSource = dataTable;

            // Draw the grid on the page
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 150));

            ClearPdfDirectory(vehicle.Id);
            string filePath = Path.Combine(GetPdfDirectory(vehicle.Id), $"{vehicle.Name}{DateTime.Now:yyyy-MM-ddTHHmmssz}.pdf");
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

        public string GetPdfDirectory(int vehicleId)
        {
            string path = PdfDirectoryPath(vehicleId);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public void ClearPdfDirectory(int vehicleId)
        {
            string path = PdfDirectoryPath(vehicleId);

            if (!Directory.Exists(path)) return;

            foreach (string file in Directory.GetFiles(path))
                File.Delete(file);
        }

        private string PartsJsonToString(string partsJsonList)
        {
            var obj = JsonSerializer.Deserialize<List<Part>>(partsJsonList);
            if (obj == null) return Constants.NotApplicable;

            string partString = string.Empty;
            foreach (Part p in obj)
            {
                partString += $"({p.Quantity}) {p.Name}\n" 
                    + (p.UnitCost == 0 ? string.Empty : $" @ {p.UnitCost:c2}\n")
                    + (string.IsNullOrEmpty(p.ModelNumber) ? string.Empty : $" (Model#: {p.ModelNumber})\n")
                    + "\n";
            }
            return partString;
        }
    }
}
