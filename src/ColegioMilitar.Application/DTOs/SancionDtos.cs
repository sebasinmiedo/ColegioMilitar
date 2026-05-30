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

    public Dictionary<int, int> PuntosPorSemana { get; set; } = new();

    public int TotalPuntos => PuntosPorSemana.Values.Sum();
    public decimal PtosDisminucion => Math.Round(TotalPuntos * 0.1m, 2);
    public decimal Nota            => 20m;
    public decimal Conducta        => Nota - PtosDisminucion;
    public decimal ActitudMilitar  { get; set; }
    public decimal NotaFinal       => Math.Round((Conducta + ActitudMilitar) / 2m, 2);
}

public class SemanaInfoDto
{
    public int NroSemana { get; set; }
    public string NombreSemana { get; set; } = string.Empty;
}

public class ConsolidadoBimestreDto
{
    public List<SemanaInfoDto> Semanas { get; set; } = new();
    public List<FilaConsolidadoDto> Filas { get; set; } = new();
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
    public int CantidadPV { get; set; }

    public string PtosDisplay => CantidadPV > 0
         ? $"{TotalPuntos} ({(CantidadPV == 1 ? "1PV" : $"{CantidadPV}PV")})"
         : TotalPuntos.ToString();

    public string Salida => TotalPuntos switch
    {
        >= 20 => "Pierde salida",
        >= 15 => "Sale domingo 07:00 hrs",
        >= 10 => "Sale sábado 07:00 hrs",
        _ => "Completa"
    };
}

public class RacionesAñoDto
{
    public int Vie { get; set; }
    public int Sab { get; set; }
    public int Dom { get; set; }
    public int Total => Vie + Sab + Dom;
}

public class ReporteRacionesDto
{
    public RacionesAñoDto QuintoAño { get; set; } = new();
    public RacionesAñoDto CuartoAño { get; set; } = new();
    public RacionesAñoDto TercerAño { get; set; } = new();
    
    public int TotalVie => QuintoAño.Vie + CuartoAño.Vie + TercerAño.Vie;
    public int TotalSab => QuintoAño.Sab + CuartoAño.Sab + TercerAño.Sab;
    public int TotalDom => QuintoAño.Dom + CuartoAño.Dom + TercerAño.Dom;
    public int TotalGral => TotalVie + TotalSab + TotalDom;
}