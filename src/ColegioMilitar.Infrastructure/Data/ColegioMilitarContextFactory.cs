using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ColegioMilitar.Infrastructure.Data;

/// <summary>
/// Permite que EF Core Tools (dotnet ef migrations add) encuentre el DbContext
/// sin necesidad de tener la UI corriendo. Solo se usa en tiempo de diseño.
/// </summary>
public class ColegioMilitarContextFactory : IDesignTimeDbContextFactory<ColegioMilitarContext>
{
    public ColegioMilitarContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ColegioMilitarContext>()
            .UseSqlite("Data Source=ColegioMilitar_dev.db")
            .Options;

        return new ColegioMilitarContext(options);
    }
}
