#r "nuget: ClosedXML, 0.105.0"
using ClosedXML.Excel;
using System;

var wb = new XLWorkbook(@"C:\Users\sabes\Documents\proyectoSistemaSanciones\ColegioMilitar\src\ColegioMilitar.UI\Resources\Templates\Plantilla_Registro_Sanciones.xlsx");
var ws = wb.Worksheet("TBL-SANCIONES");
Console.WriteLine("Pictures count: " + ws.Pictures.Count);
foreach (var p in ws.Pictures) {
    Console.WriteLine($"Pic: {p.Name}, TopLeft: {p.TopLeftCell.Address}");
}
