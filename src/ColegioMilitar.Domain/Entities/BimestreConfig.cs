namespace ColegioMilitar.Domain.Entities;

/// <summary>
/// Define el rango de fechas de cada semana dentro de un bimestre.
/// Permite asignar SemanaBimestre automáticamente al registrar una sanción.
/// </summary>
public class BimestreConfig
{
    public int Id { get; set; }
    public int Bimestre { get; set; }       // 1, 2, 3, 4...
    public int Año { get; set; }            // Año académico, ej. 2026
    public int NroSemana { get; set; }      // 1 a 5
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public bool Cerrada { get; set; }       // true = semana ya cerrada (solo lectura)
}

/// <summary>
/// Nota de Actitud Militar ingresada manualmente por bimestre y cadete.
/// No se calcula; se edita directamente en la pantalla de consolidado.
/// </summary>
public class ActitudMilitarManual
{
    public int Id { get; set; }
    public string CadeteDNI { get; set; } = string.Empty;
    public int Bimestre { get; set; }
    public int AñoAcademico { get; set; }
    public decimal NotaActitud { get; set; }  // Ej: 18.5

    // Navegación
    public Cadete Cadete { get; set; } = null!;
}
