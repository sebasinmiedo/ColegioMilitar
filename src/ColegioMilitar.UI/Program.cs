using ColegioMilitar.Application.Services;
using ColegioMilitar.Infrastructure.Data;
using ColegioMilitar.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.UI;

internal static class Program
{
    // Servicios disponibles globalmente en la app (poor-man DI para WinForms)
    public static SancionService    SancionService    { get; private set; } = null!;
    public static ConsolidadoService ConsolidadoService { get; private set; } = null!;
    public static CadeteRepository  Cadetes           { get; private set; } = null!;
    public static CastigoRepository Castigos          { get; private set; } = null!;
    public static SupervisorRepository Supervisores   { get; private set; } = null!;
    public static BimestreRepository Bimestres        { get; private set; } = null!;

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            InicializarBaseDatos();
            InicializarServicios();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error al inicializar la base de datos:\n\n{ex.Message}",
                "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Application.Run(new Form1());
    }

    private static void InicializarBaseDatos()
    {
        DbFactory.Initialize(); // ruta por defecto: carpeta del .exe
        DbFactory.EnsureCreated();

        // Activar WAL mode para soportar múltiples usuarios simultáneos
        using var ctx = DbFactory.Create();
        ctx.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");
        ctx.Database.ExecuteSqlRaw("PRAGMA foreign_keys=ON;");
    }

    private static void InicializarServicios()
    {
        // Cada operación crea su propio contexto (patrón Unit of Work por formulario)
        Cadetes      = new CadeteRepository(DbFactory.Create());
        Castigos     = new CastigoRepository(DbFactory.Create());
        Supervisores = new SupervisorRepository(DbFactory.Create());
        Bimestres    = new BimestreRepository(DbFactory.Create());

        var sancionRepo = new SancionRepository(DbFactory.Create());

        SancionService = new SancionService(
            sancionRepo, Cadetes, Castigos, Supervisores, Bimestres);

        ConsolidadoService = new ConsolidadoService(
            sancionRepo, Cadetes, DbFactory.Create());
    }
}
