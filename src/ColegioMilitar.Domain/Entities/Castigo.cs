namespace ColegioMilitar.Domain.Entities;

public class Castigo
{
    public string Codigo { get; set; } = string.Empty;       // "1001", "1002"...
    public string Descripcion { get; set; } = string.Empty;  // "ABUSO DE CONFIANZA..."
    public int PuntosAño3 { get; set; }
    public int PuntosAño4 { get; set; }
    public int PuntosAño5 { get; set; }
    public int Reincidencia { get; set; }
    public string? Nota { get; set; }  // "DISL", etc.

    // Navegación
    public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();

    /// <summary>
    /// Devuelve los puntos correspondientes según el año del cadete (3, 4 o 5).
    /// </summary>
    public int GetPuntosPorAño(int año) => año switch
    {
        3 => PuntosAño3,
        4 => PuntosAño4,
        5 => PuntosAño5,
        _ => throw new ArgumentOutOfRangeException(nameof(año), "El año debe ser 3, 4 o 5.")
    };
}
