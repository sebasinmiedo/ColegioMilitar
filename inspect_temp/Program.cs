using ClosedXML.Excel;
using System;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;

class Program
{
    static void Main()
    {
        var paths = new[]
        {
            @"C:\Users\sabes\Documents\proyectoSistemaSanciones\ColegioMilitar\src\ColegioMilitar.UI\Resources\Templates\Plantilla_Registro_Sanciones.xlsx",
            @"C:\Users\sabes\Documents\proyectoSistemaSanciones\ColegioMilitar\src\ColegioMilitar.UI\bin\Debug\net10.0-windows\Resources\Templates\Plantilla_Registro_Sanciones.xlsx"
        };

        foreach (var path in paths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Template not found: {path} (skipping)");
                continue;
            }
            Console.WriteLine($"Processing template: {path}");

            // 1. Write cell values and formatting using ClosedXML
            using (var wb = new XLWorkbook(path))
            {
                var sheets = wb.Worksheets.ToList();
                foreach (var ws in sheets)
                {
                    ws.AutoFilter.Clear();
                    var tableNames = ws.Tables.Select(t => t.Name).ToList();
                    foreach (var name in tableNames)
                    {
                        ws.Tables.Remove(name);
                    }

                    if (ws.Name.StartsWith("TBL-SANCIONES"))
                    {
                        Console.WriteLine($"  Writing signatures to cells in sheet '{ws.Name}'");
                        // Clear any old text in these target cells just in case
                        for (int r = 9; r <= 13; r++)
                        {
                            ws.Cell(r, 3).Clear();
                            ws.Cell(r, 5).Clear();
                            ws.Cell(r, 9).Clear();
                        }

                        ws.Cell(9, 3).Value = "_______________________";
                        ws.Cell(9, 5).Value = "_______________________";
                        ws.Cell(9, 9).Value = "_______________________";

                        ws.Cell(10, 3).Value = "O 118527000 B+";
                        ws.Cell(10, 5).Value = "O-123060300-O+";
                        ws.Cell(10, 9).Value = "O-128555500-O+";

                        ws.Cell(11, 3).Value = "EDGARDO SULLCA LLAMOCCA";
                        ws.Cell(11, 5).Value = "CARLOS A. TAMARIZ RODRIGUEZ";
                        ws.Cell(11, 9).Value = "GERARDO D. TINEO VELITA";

                        ws.Cell(12, 3).Value = "CRL EP";
                        ws.Cell(12, 5).Value = "MY CAB";
                        ws.Cell(12, 9).Value = "ALFZ CAB";

                        ws.Cell(13, 3).Value = "DIRECTOR DE LA IEPM \"CRL GAL\"";
                        ws.Cell(13, 5).Value = "Sub Director IEPM Crl GAL";
                        ws.Cell(13, 9).Value = "JEFE DPTO EVALUACION";

                        for (int r = 9; r <= 13; r++)
                        {
                            ws.Cell(r, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(r, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(r, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            ws.Cell(r, 3).Style.Font.FontSize = 9;
                            ws.Cell(r, 5).Style.Font.FontSize = 9;
                            ws.Cell(r, 9).Style.Font.FontSize = 9;

                            ws.Cell(r, 3).Style.Font.Bold = (r == 11);
                            ws.Cell(r, 5).Style.Font.Bold = (r == 11);
                            ws.Cell(r, 9).Style.Font.Bold = (r == 11);
                        }
                    }
                    else if (ws.Name.StartsWith("SALIDA"))
                    {
                        Console.WriteLine($"  Writing signature to cells in sheet '{ws.Name}'");
                        for (int r = 7; r <= 11; r++)
                        {
                            ws.Cell(r, 3).Clear();
                        }

                        ws.Cell(7, 3).Value = "_______________________";
                        ws.Cell(8, 3).Value = "O 118527000 B+";
                        ws.Cell(9, 3).Value = "EDGARDO SULLCA LLAMOCCA";
                        ws.Cell(10, 3).Value = "CRL EP";
                        ws.Cell(11, 3).Value = "DIRECTOR DE LA IEPM \"CRL GAL\"";

                        for (int r = 7; r <= 11; r++)
                        {
                            ws.Cell(r, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Cell(r, 3).Style.Font.FontSize = 9;
                            ws.Cell(r, 3).Style.Font.Bold = (r == 9);
                        }
                    }
                }
                wb.Save();
            }

            // 2. Open using OpenXML SDK and delete the Textboxes/Shapes
            using (var doc = SpreadsheetDocument.Open(path, true))
            {
                var workbookPart = doc.WorkbookPart;
                if (workbookPart == null) continue;

                foreach (var sheet in workbookPart.Workbook.Sheets.Cast<Sheet>())
                {
                    if (sheet.Name == null || (!sheet.Name.Value.StartsWith("TBL-SANCIONES") && !sheet.Name.Value.StartsWith("SALIDA")))
                        continue;

                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id!);
                    var drawingsPart = worksheetPart.DrawingsPart;
                    if (drawingsPart == null) continue;

                    var worksheetDrawing = drawingsPart.WorksheetDrawing;
                    if (worksheetDrawing == null) continue;

                    int removedCount = 0;
                    foreach (var element in worksheetDrawing.ChildElements.ToList())
                    {
                        var pic = element.Descendants<DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture>().FirstOrDefault();
                        if (pic == null)
                        {
                            element.Remove();
                            removedCount++;
                        }
                    }
                    if (removedCount > 0)
                    {
                        Console.WriteLine($"  Removed {removedCount} shapes/textboxes from sheet '{sheet.Name}'");
                        worksheetDrawing.Save();
                    }
                }
            }
            Console.WriteLine($"Done with {path}\n");
        }
    }
}
