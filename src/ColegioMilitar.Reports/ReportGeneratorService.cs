using ClosedXML.Excel;
using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;
using System.Diagnostics;

namespace ColegioMilitar.Reports;

public class ReportGeneratorService
{
    public void GenerarReporteSemanal(
        string templatePath,
        string outputPath,
        int semanaSeleccionada,
        string nombreSemana,
        IEnumerable<Sancion> sanciones,
        IEnumerable<FilaPtosSalidaDto> salidaQuinto,
        IEnumerable<FilaPtosSalidaDto> salidaCuarto,
        IEnumerable<FilaPtosSalidaDto> salidaTercer,
        ReporteRacionesDto raciones,
        IEnumerable<Cadete> todosLosCadetes)
    {
        // Hacer una copia de la plantilla en el output y trabajar sobre ella
        File.Copy(templatePath, outputPath, true);

        using (var wb = new XLWorkbook(outputPath))
        {
            // ── PROCESAR TERCER AÑO ─────────────────────────────────────────
            ProcesarSancionesSemanalesPorAño(wb.Worksheet("TBL-SANCIONES 3er"), 3, sanciones, todosLosCadetes);
            ProcesarTablaSalidaPorAño(wb.Worksheet("SALIDA 3er"), salidaTercer, "III AÑO", raciones.TercerAño);

            // ── PROCESAR CUARTO AÑO ─────────────────────────────────────────
            ProcesarSancionesSemanalesPorAño(wb.Worksheet("TBL-SANCIONES 4to"), 4, sanciones, todosLosCadetes);
            ProcesarTablaSalidaPorAño(wb.Worksheet("SALIDA 4to"), salidaCuarto, "IV AÑO", raciones.CuartoAño);

            // ── PROCESAR QUINTO AÑO ─────────────────────────────────────────
            ProcesarSancionesSemanalesPorAño(wb.Worksheet("TBL-SANCIONES 5to"), 5, sanciones, todosLosCadetes);
            ProcesarTablaSalidaPorAño(wb.Worksheet("SALIDA 5to"), salidaQuinto, "V AÑO", raciones.QuintoAño);

            // ── TAB: RACIONES ───────────────────────────────────────────────
            var wsRaciones = wb.Worksheet("RACIONES");
            
            // Fecha actual
            wsRaciones.Cell("B3").Value = DateTime.Today.ToString("dd/MM/yyyy");

            // V AÑO
            wsRaciones.Cell("C6").Value = raciones.QuintoAño.Vie;
            wsRaciones.Cell("D6").Value = raciones.QuintoAño.Sab;
            wsRaciones.Cell("E6").Value = raciones.QuintoAño.Dom;
            wsRaciones.Cell("F6").Value = raciones.QuintoAño.Total;

            // IV AÑO
            wsRaciones.Cell("C7").Value = raciones.CuartoAño.Vie;
            wsRaciones.Cell("D7").Value = raciones.CuartoAño.Sab;
            wsRaciones.Cell("E7").Value = raciones.CuartoAño.Dom;
            wsRaciones.Cell("F7").Value = raciones.CuartoAño.Total;

            // III AÑO
            wsRaciones.Cell("C8").Value = raciones.TercerAño.Vie;
            wsRaciones.Cell("D8").Value = raciones.TercerAño.Sab;
            wsRaciones.Cell("E8").Value = raciones.TercerAño.Dom;
            wsRaciones.Cell("F8").Value = raciones.TercerAño.Total;

            // TOTAL
            wsRaciones.Cell("C9").Value = raciones.TotalVie;
            wsRaciones.Cell("D9").Value = raciones.TotalSab;
            wsRaciones.Cell("E9").Value = raciones.TotalDom;
            wsRaciones.Cell("F9").Value = raciones.TotalGral;

            // Limpiar AutoFilters y remover definiciones de Tablas para evitar excepciones
            foreach (var w in wb.Worksheets)
            {
                w.AutoFilter.Clear();
                var tableNames = w.Tables.Select(t => t.Name).ToList();
                foreach (var name in tableNames)
                {
                    w.Tables.Remove(name);
                }
            }

            wb.Save();
        }
    }

    private void ProcesarSancionesSemanalesPorAño(
        IXLWorksheet wsSanciones,
        int año,
        IEnumerable<Sancion> sanciones,
        IEnumerable<Cadete> todosLosCadetes)
    {
        int filaActual = 7;
        
        // Filtrar cadetes y sanciones por el año académico correspondiente
        var cadetesAño = todosLosCadetes.Where(c => c.Año == año).OrderBy(c => c.ApellidosNombres).ToList();
        var sancionesAño = sanciones.Where(s => s.Cadete.Año == año).ToList();
        
        // Diccionario para búsqueda rápida de sanciones por DNI
        var sancionesPorCadete = sancionesAño
            .GroupBy(s => s.Cadete.DNI)
            .ToDictionary(g => g.Key, g => g.OrderBy(s => s.Fecha).ThenBy(s => s.Hora).ToList());

        int nro = 1;
        foreach (var cadete in cadetesAño)
        {
            if (sancionesPorCadete.TryGetValue(cadete.DNI, out var cadeteSanciones) && cadeteSanciones.Any())
            {
                bool esPrimeraSancion = true;
                foreach (var sancion in cadeteSanciones)
                {
                    wsSanciones.Row(filaActual).InsertRowsBelow(1);
                    var row = wsSanciones.Row(filaActual);
                    
                    if (esPrimeraSancion)
                    {
                        row.Cell("A").Value = nro++;
                        row.Cell("B").Value = cadete.DNI;
                        row.Cell("C").Value = cadete.ApellidosNombres;
                    }

                    row.Cell("D").Value = sancion.Castigo.Descripcion;
                    row.Cell("E").Value = sancion.EsPierdeSalida ? "1PV" : sancion.PuntosAplicados.ToString();
                    row.Cell("F").Value = sancion.Castigo.Reincidencia;
                    row.Cell("G").Value = sancion.Hora.ToString(@"hh\:mm");
                    row.Cell("H").Value = sancion.Fecha.ToString("dd/MM/yyyy");
                    row.Cell("I").Value = $"{sancion.Supervisor.Grado} {sancion.Supervisor.ApellidosNombres}";

                    if (sancion.Perdonada)
                    {
                        row.Style.Font.Strikethrough = true;
                        row.Style.Font.FontColor = XLColor.Gray;
                    }

                    filaActual++;
                    esPrimeraSancion = false;
                }
            }
            else
            {
                // No tiene sanciones, solo imprimir el nombre en blanco
                wsSanciones.Row(filaActual).InsertRowsBelow(1);
                var row = wsSanciones.Row(filaActual);
                
                row.Cell("A").Value = nro++;
                row.Cell("B").Value = cadete.DNI;
                row.Cell("C").Value = cadete.ApellidosNombres;
                
                filaActual++;
            }
        }

        // Eliminar fila vacía de plantilla desplazada
        wsSanciones.Row(filaActual).Delete();
    }

    private void ProcesarTablaSalidaPorAño(
        IXLWorksheet wsSalida,
        IEnumerable<FilaPtosSalidaDto> lista,
        string añoLabel,
        RacionesAñoDto racionesAño)
    {
        int filaSalida = 4;

        int nroSalida = 1;
        foreach (var cadete in lista)
        {
            wsSalida.Range(filaSalida, 1, filaSalida, 5).InsertRowsBelow(1);
            var row = wsSalida.Row(filaSalida);
            
            row.Cell("A").Value = nroSalida++;
            row.Cell("B").Value = cadete.CadeteDNI;
            row.Cell("C").Value = cadete.ApellidosNombres;
            row.Cell("D").Value = cadete.PtosDisplay;
            row.Cell("E").Value = cadete.Salida;

            if (cadete.Salida.Contains("Pierde salida"))
            {
                var color = XLColor.FromArgb(255, 200, 200);
                for (int col = 1; col <= 5; col++)
                {
                    row.Cell(col).Style.Fill.BackgroundColor = color;
                }
            }
            else if (cadete.Salida.Contains("domingo"))
            {
                var color = XLColor.FromArgb(255, 230, 180);
                for (int col = 1; col <= 5; col++)
                {
                    row.Cell(col).Style.Fill.BackgroundColor = color;
                }
            }

            filaSalida++;
        }
        
        // Fila en blanco como separador
        wsSalida.Range(filaSalida, 1, filaSalida, 5).InsertRowsBelow(1);
        filaSalida++;

        wsSalida.Range(filaSalida, 1, filaSalida, 5).Delete(XLShiftDeletedCells.ShiftCellsUp); // Borrar fila base desplazada

        // Escribir raciones del año en la tabla lateral
        wsSalida.Cell("G4").Value = añoLabel;
        wsSalida.Cell("H4").Value = racionesAño.Vie;
        wsSalida.Cell("I4").Value = racionesAño.Sab;
        wsSalida.Cell("J4").Value = racionesAño.Dom;
        wsSalida.Cell("K4").Value = racionesAño.Total;
    }

    public void GenerarReporteBimestral(
        string templatePath,
        string outputPath,
        int bimestre,
        string nombreBimestre,
        ConsolidadoBimestreDto dataTercerA,
        ConsolidadoBimestreDto dataCuartoA,
        ConsolidadoBimestreDto dataQuintoA)
    {
        File.Copy(templatePath, outputPath, true);

        using (var wb = new XLWorkbook(outputPath))
        {
            ProcesarTabBimestral(wb.Worksheet("TERCER AÑO"), dataTercerA, $"REGISTRO DE NOTAS DE CONDUCTA Y ACTITUD MILITAR - {nombreBimestre} BIMESTRE 3ER AÑO");
            ProcesarTabBimestral(wb.Worksheet("CUARTO AÑO"), dataCuartoA, $"REGISTRO DE NOTAS DE CONDUCTA Y ACTITUD MILITAR - {nombreBimestre} BIMESTRE 4TO AÑO");
            ProcesarTabBimestral(wb.Worksheet("QUINTO AÑO"), dataQuintoA, $"REGISTRO DE NOTAS DE CONDUCTA Y ACTITUD MILITAR - {nombreBimestre} BIMESTRE 5TO AÑO");
            
            // Limpiar AutoFilters y remover definiciones de Tablas
            foreach (var w in wb.Worksheets)
            {
                w.AutoFilter.Clear();
                var tableNames = w.Tables.Select(t => t.Name).ToList();
                foreach (var name in tableNames)
                {
                    w.Tables.Remove(name);
                }
            }

            wb.Save();
        }
    }

    private void ProcesarTabBimestral(IXLWorksheet ws, ConsolidadoBimestreDto data, string titulo)
    {
        ws.Cell("A1").Value = titulo;

        int nSemanas = data.Semanas.Count;
        
        // Descombinar rango de cabecera original en la plantilla (columnas D a H)
        ws.Range(2, 4, 2, 8).Unmerge();

        // Ajustar columnas dinámicamente según el número de semanas configuradas
        if (nSemanas > 5)
        {
            ws.Column(9).InsertColumnsBefore(nSemanas - 5);
        }
        else if (nSemanas < 5)
        {
            ws.Columns(4 + nSemanas, 8).Delete();
        }

        // Headers dinámicos
        for (int i = 0; i < nSemanas; i++)
        {
            ws.Cell(3, 4 + i).Value = data.Semanas[i].NombreSemana;
        }
        
        // Escribir headers finales desplazados
        int offsetColumn = 4 + nSemanas;
        ws.Cell(3, offsetColumn).Value     = "PUNTOS";
        ws.Cell(3, offsetColumn + 1).Value = "PTOS DISM";
        ws.Cell(3, offsetColumn + 2).Value = "NOTA";
        ws.Cell(3, offsetColumn + 3).Value = "CONDUCTA";
        ws.Cell(3, offsetColumn + 4).Value = "ACTITUD MIL";
        ws.Cell(3, offsetColumn + 5).Value = "NOTA FINAL";

        // Establecer el estilo de las cabeceras finales
        for (int c = offsetColumn; c <= offsetColumn + 5; c++)
        {
            ws.Cell(3, c).Style.Font.Bold = true;
            ws.Cell(3, c).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(3, c).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        int filaActual = 4;
        int nro = 1;

        foreach (var cadete in data.Filas.OrderBy(f => f.ApellidosNombres))
        {
            ws.Row(filaActual).InsertRowsBelow(1);
            var row = ws.Row(filaActual);

            row.Cell(1).Value = nro++;
            row.Cell(2).Value = cadete.CadeteDNI;
            row.Cell(3).Value = cadete.ApellidosNombres;

            // Valores semanales
            for (int i = 0; i < nSemanas; i++)
            {
                int semNro = data.Semanas[i].NroSemana;
                int pts = cadete.PuntosPorSemana.TryGetValue(semNro, out var p) ? p : 0;
                row.Cell(4 + i).Value = pts;
                row.Cell(4 + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            row.Cell(offsetColumn).Value = cadete.TotalPuntos;
            row.Cell(offsetColumn + 1).Value = cadete.PtosDisminucion;
            row.Cell(offsetColumn + 2).Value = cadete.Nota;
            row.Cell(offsetColumn + 3).Value = cadete.Conducta;
            row.Cell(offsetColumn + 4).Value = cadete.ActitudMilitar;
            row.Cell(offsetColumn + 5).Value = cadete.NotaFinal;

            // Formatear decimales
            row.Cell(offsetColumn + 1).Style.NumberFormat.Format = "0.00";
            row.Cell(offsetColumn + 2).Style.NumberFormat.Format = "0.00";
            row.Cell(offsetColumn + 3).Style.NumberFormat.Format = "0.00";
            row.Cell(offsetColumn + 4).Style.NumberFormat.Format = "0.00";
            row.Cell(offsetColumn + 5).Style.NumberFormat.Format = "0.00";

            filaActual++;
        }

        ws.Row(filaActual).Delete(); // Borrar fila base desplazada
        
        // Ajustar el header "FECHA CASTIGOS" para que haga un merge correcto
        if (nSemanas > 0)
        {
            ws.Range(2, 4, 2, 3 + nSemanas).Merge();
        }
    }
}
