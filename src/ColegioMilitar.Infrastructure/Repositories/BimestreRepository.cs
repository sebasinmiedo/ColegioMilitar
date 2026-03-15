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

    public async Task<BimestreConfig?> GetSemanaParaFechaAsync(DateTime fecha) =>
        await _ctx.BimestresConfig
            .Where(b => b.FechaInicio <= fecha && fecha <= b.FechaFin && !b.Cerrada)
            .FirstOrDefaultAsync();

    public async Task AddAsync(BimestreConfig config)
    {
        await _ctx.BimestresConfig.AddAsync(config);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(BimestreConfig config)
    {
        _ctx.BimestresConfig.Update(config);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _ctx.BimestresConfig.FindAsync(id);
        if (item is not null)
        {
            _ctx.BimestresConfig.Remove(item);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task CerrarSemanaAsync(int id)
    {
        var semana = await _ctx.BimestresConfig.FindAsync(id);
        if (semana is not null) { semana.Cerrada = true; await _ctx.SaveChangesAsync(); }
    }

    public async Task ReabrirSemanaAsync(int id)
    {
        var semana = await _ctx.BimestresConfig.FindAsync(id);
        if (semana is not null) { semana.Cerrada = false; await _ctx.SaveChangesAsync(); }
    }
}
