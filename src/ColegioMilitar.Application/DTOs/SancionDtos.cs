namespace ColegioMilitar.Application.DTOs;

public class RegistrarSancionDto
{
    public string    CadeteDNI            { get; set; } = string.Empty;
    public string    SupervisorDNI        { get; set; } = string.Empty;
    public string    CastigoCodigo        { get; set; } = string.Empty;
    public DateTime  Fecha                { get; set; } = DateTime.Today;
    public TimeSpan  Hora                 { get; set; } = DateTime.Now.TimeOfDay;
    public string?   Observaciones        { get; set; }
    public int?      SemanaBimestreManual { get; set; }
}

public class FilaConsolidadoDto
{
    public string  CadeteDNI        { get; set; } = string.Empty;
    public string  ApellidosNombres { get; set; } = string.Empty;
    public int     Año              { get; set; }
    public string? Division         { get; set; }

    public int PtosSemana1 { get; set; }
    public int PtosSemana2 { get; set; }
    public int PtosSemana3 { get; set; }
    public int PtosSemana4 { get; set; }
    public int PtosSemana5 { get; set; }

    public int     TotalPuntos    => PtosSemana1 + PtosSemana2 + PtosSemana3 + PtosSemana4 + PtosSemana5;
    public decimal PtosDisminucion => Math.Round(TotalPuntos * 0.1m, 2);
    public decimal Nota            => 20m;
    public decimal Conducta        => Nota - PtosDisminucion;
    public decimal ActitudMilitar  { get; set; }
    public decimal NotaFinal       => Math.Round((Conducta + ActitudMilitar) / 2m, 2);
}

/// <summary>
/// Fila del reporte PTOS SALIDA.
/// La columna Salida considera tanto puntos como el flag 1PV.
/// </summary>
public class FilaPtosSalidaDto
{
    public string CadeteDNI { get; set; } = string.Empty;
    public string ApellidosNombres { get; set; } = string.Empty;
    public int Año { get; set; }
    public int TotalPuntos { get; set; }
    public int CantidadPV { get; set; } // cantidad de sanciones 1PV

    public string PtosDisplay => CantidadPV > 0
        ? (CantidadPV == 1 ? "1PV" : $"{CantidadPV}PV")
        : TotalPuntos.ToString();

    public string Salida => CantidadPV > 0 ? "Pierde salida" : TotalPuntos switch
    {
        >= 20 => "Pierde salida",
        >= 15 => "Sale domingo 07:00 hrs",
        >= 10 => "Sale sábado 07:00 hrs",
        _ => "Completa"
    };
}
