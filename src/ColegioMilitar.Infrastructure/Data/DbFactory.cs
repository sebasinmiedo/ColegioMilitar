using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Data;

/// <summary>
/// Fábrica estática para obtener instancias del contexto en la app WinForms.
/// La ruta de la base de datos se resuelve automáticamente junto al ejecutable.
/// </summary>
public static class DbFactory
{
    private static string? _connectionString;

    public static void Initialize(string? dbPath = null)
    {
        var path = dbPath ?? Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "ColegioMilitar.db");

        _connectionString = $"Data Source={path}";
    }

    public static ColegioMilitarContext Create()
    {
        if (_connectionString is null)
            Initialize();

        var options = new DbContextOptionsBuilder<ColegioMilitarContext>()
            .UseSqlite(_connectionString!)
            .Options;

        return new ColegioMilitarContext(options);
    }

    /// <summary>
    /// Aplica migraciones pendientes y crea la base si no existe.
    /// Llamar una vez al arrancar la app.
    /// </summary>
    public static void EnsureCreated()
    {
        using var ctx = Create();
        ctx.Database.Migrate();
    }
}
