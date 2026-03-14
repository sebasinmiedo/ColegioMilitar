namespace ColegioMilitar.Domain.Entities;

public class Cadete
{
    public string DNI { get; set; } = string.Empty;
    public string ApellidosNombres { get; set; } = string.Empty;
    public int Año { get; set; }  // 3, 4 o 5
    public string? Division { get; set; }

    // Navegación
    public ICollection<Sancion> Sanciones { get; set; } = new List<Sancion>();
    public ICollection<ActitudMilitarManual> ActitudesMilitares { get; set; } = new List<ActitudMilitarManual>();
}
