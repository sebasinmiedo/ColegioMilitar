using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Repositories;

public class BimestreRepository
{
    private readonly ColegioMilitarContext _ctx;

    public BimestreRepository(ColegioMilitarContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<BimestreConfig>> GetAllAsync() =>
        await _ctx.BimestresConfig
            .OrderBy(b => b.Año).ThenBy(b => b.Bimestre).ThenBy(b => b.NroSemana)
            .ToListAsync();

    public async Task<IEnumerable<BimestreConfig>> GetByBimestreAsync(int bimestre, int año) =>
        await _ctx.BimestresConfig
            .Where(b => b.Bimestre == bimestre && b.Año == año)
            .OrderBy(b => b.NroSemana)
            .ToListAsync();

    /// <summary>
    /// Dado una fecha, devuelve la semana del bimestre a la que pertenece.
    /// Retorna null si la fecha no cae en ninguna semana configurada.
    /// </summary>
    public async Task<BimestreConfig?> GetSemanaParaFechaAsync(DateTime fecha) =>
        await _ctx.BimestresConfig
            .Where(b => b.FechaInicio <= fecha && fecha <= b.FechaFin && !b.Cerrada)
            .FirstOrDefaultAsync();

    public async Task AddAsync(BimestreConfig config)
    {
        await _ctx.BimestresConfig.AddAsync(config);
        await _ctx.SaveChangesAsync();
    }

    /// <summary>Marca una semana como cerrada (ya no acepta sanciones nuevas en esa semana).</summary>
    public async Task CerrarSemanaAsync(int bimestre, int año, int nroSemana)
    {
        var semana = await _ctx.BimestresConfig
            .FirstOrDefaultAsync(b => b.Bimestre == bimestre
                                   && b.Año == año
                                   && b.NroSemana == nroSemana);
        if (semana is not null)
        {
            semana.Cerrada = true;
            await _ctx.SaveChangesAsync();
        }
    }
}
