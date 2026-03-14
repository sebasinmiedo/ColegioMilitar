namespace ColegioMilitar.Domain.Entities;

public class Sancion
{
    public int Id { get; set; }

    public string CadeteDNI { get; set; } = string.Empty;
    public string SupervisorDNI { get; set; } = string.Empty;
    public string CastigoCodigo { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public string? Observaciones { get; set; }

    /// <summary>
    /// Calculado al insertar según el año del cadete. No se modifica después.
    /// </summary>
    public int PuntosAplicados { get; set; }

    /// <summary>
    /// Semana dentro del bimestre: 1 a 5. Se asigna según BimestreConfig o manualmente.
    /// </summary>
    public int SemanaBimestre { get; set; }

    // Navegación
    public Cadete Cadete { get; set; } = null!;
    public Supervisor Supervisor { get; set; } = null!;
    public Castigo Castigo { get; set; } = null!;
}
