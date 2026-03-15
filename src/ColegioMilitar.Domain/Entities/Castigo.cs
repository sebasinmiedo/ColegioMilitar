namespace ColegioMilitar.Domain.Entities;

public class Castigo
{
    public string Codigo      { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    // Almacenados como string porque pueden ser "7", "10", "1PV"
    public string PuntosAño3Raw { get; set; } = "0";
    public string PuntosAño4Raw { get; set; } = "0";
    public string PuntosAño5Raw { get; set; } = "0";

    public int    Reincidencia  { get; set; }
    public string? Nota         { get; set; }

    // Navegación
    public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();

    // ── Constante para castigo que implica pérdida de salida automática ──
    public const string PIERDE_SALIDA = "1PV";

    /// <summary>
    /// Devuelve el raw string del castigo para el año dado (puede ser "7" o "1PV").
    /// </summary>
    public string GetRawPorAño(int año) => año switch
    {
        3 => PuntosAño3Raw,
        4 => PuntosAño4Raw,
        5 => PuntosAño5Raw,
        _ => throw new ArgumentOutOfRangeException(nameof(año), "El año debe ser 3, 4 o 5.")
    };

    /// <summary>
    /// True si el castigo para ese año es "1PV" (pierde salida directamente).
    /// </summary>
    public bool EsPierdeSalida(int año) =>
        GetRawPorAño(año).Trim().Equals(PIERDE_SALIDA, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Devuelve los puntos numéricos. Si es "1PV" devuelve 0 (la lógica
    /// de salida lo maneja por separado con EsPierdeSalida).
    /// </summary>
    public int GetPuntosPorAño(int año)
    {
        var raw = GetRawPorAño(año).Trim();
        return int.TryParse(raw, out int pts) ? pts : 0;
    }
}
