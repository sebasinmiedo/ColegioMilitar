namespace ColegioMilitar.Domain.Entities;

public class Supervisor
{
    public string DNI { get; set; } = string.Empty;
    public string Grado { get; set; } = string.Empty;   // CRL, MY, CAP, TTE, ALFZ, TCOJS…
    public string ApellidosNombres { get; set; } = string.Empty;

    // Navegación
    public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();
}
