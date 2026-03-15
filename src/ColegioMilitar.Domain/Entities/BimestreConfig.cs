namespace ColegioMilitar.Domain.Entities;

/// <summary>
/// Define cada semana dentro de un bimestre.
/// El nombre es la fecha del viernes de esa semana (ej: "20 MARZO").
/// No hay límite fijo de semanas por bimestre.
/// </summary>
public class BimestreConfig
{
    public int    Id          { get; set; }
    public int    Bimestre    { get; set; }       // 1, 2, 3, 4...
    public int    Año         { get; set; }        // Año académico ej. 2026
    public int    NroSemana   { get; set; }        // 1, 2, 3... sin límite fijo
    /// <summary>Fecha del viernes de la semana. Ej: "20 MARZO"</summary>
    public string NombreSemana { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin    { get; set; }
    public bool   Cerrada      { get; set; }
}

/// <summary>
/// Nota de Actitud Militar ingresada manualmente por bimestre y cadete.
/// </summary>
public class ActitudMilitarManual
{
    public int Id { get; set; }
    public string CadeteDNI { get; set; } = string.Empty;
    public int Bimestre { get; set; }
    public int AñoAcademico { get; set; }
    public decimal NotaActitud { get; set; }

    // Navegación
    public Cadete Cadete { get; set; } = null!;
}