namespace ColegioMilitar.Application.DTOs;

/// <summary>Datos que el formulario envía para registrar una sanción.</summary>
public class RegistrarSancionDto
{
    public string CadeteDNI { get; set; } = string.Empty;
    public string SupervisorDNI { get; set; } = string.Empty;
    public string CastigoCodigo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public TimeSpan Hora { get; set; } = DateTime.Now.TimeOfDay;
    public string? Observaciones { get; set; }
    /// <summary>
    /// Si es null, se calcula automáticamente desde BimestreConfig según la Fecha.
    /// El operador puede sobrescribirla si no hay bimestre configurado.
    /// </summary>
    public int? SemanaBimestreManual { get; set; }
}

/// <summary>Fila del consolidado bimestral (una por cadete).</summary>
public class FilaConsolidadoDto
{
    public string CadeteDNI { get; set; } = string.Empty;
    public string ApellidosNombres { get; set; } = string.Empty;
    public int Año { get; set; }
    public string? Division { get; set; }

    // Puntos por semana (1 a 5)
    public int PtosSemana1 { get; set; }
    public int PtosSemana2 { get; set; }
    public int PtosSemana3 { get; set; }
    public int PtosSemana4 { get; set; }
    public int PtosSemana5 { get; set; }

    public int TotalPuntos => PtosSemana1 + PtosSemana2 + PtosSemana3 + PtosSemana4 + PtosSemana5;

    // Columnas calculadas
    public decimal PtosDisminucion => Math.Round(TotalPuntos * 0.1m, 2);
    public decimal Nota => 20m;
    public decimal Conducta => Nota - PtosDisminucion;

    /// <summary>Ingresado manualmente. Se carga desde ActitudMilitarManual.</summary>
    public decimal ActitudMilitar { get; set; }

    public decimal NotaFinal => Math.Round((Conducta + ActitudMilitar) / 2m, 2);
}

/// <summary>Fila del reporte PTOS SALIDA.</summary>
public class FilaPtosSalidaDto
{
    public string CadeteDNI { get; set; } = string.Empty;
    public string ApellidosNombres { get; set; } = string.Empty;
    public int Año { get; set; }
    public int TotalPuntos { get; set; }

    /// <summary>Calculado en tiempo real, no se almacena en BD.</summary>
    public string Salida => TotalPuntos switch
    {
        < 10 => "Completa",
        < 15 => "Sale sábado 07:00 hrs",
        < 20 => "Sale domingo 07:00 hrs",
        _    => "Pierde salida"
    };
}
