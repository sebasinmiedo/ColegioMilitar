using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Repositories;

public class SancionRepository : IRepository<Sancion, int>
{
    private readonly ColegioMilitarContext _ctx;

    public SancionRepository(ColegioMilitarContext ctx) => _ctx = ctx;

    public async Task<Sancion?> GetByIdAsync(int id) =>
        await _ctx.Sanciones
            .Include(s => s.Cadete)
            .Include(s => s.Supervisor)
            .Include(s => s.Castigo)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<IEnumerable<Sancion>> GetAllAsync() =>
    await _ctx.Sanciones
        .Include(s => s.Cadete)
        .Include(s => s.Supervisor)
        .Include(s => s.Castigo)
        .OrderByDescending(s => s.Fecha)
        .ToListAsync();

    /// <summary>Todas las sanciones de una semana específica.</summary>
    public async Task<IEnumerable<Sancion>> GetBySemanaBimestreAsync(int semana) =>
        await _ctx.Sanciones
            .Include(s => s.Cadete)
            .Include(s => s.Supervisor)
            .Include(s => s.Castigo)
            .Where(s => s.SemanaBimestre == semana)
            .OrderBy(s => s.Cadete.ApellidosNombres)
            .ToListAsync();

    /// <summary>Sanciones de un cadete en un bimestre completo (semanas 1-5).</summary>
    public async Task<IEnumerable<Sancion>> GetByCadeteYBimestreAsync(
        string cadeteDni, int bimestre, int añoAcademico)
    {
        // Filtramos por año académico y bimestre usando las fechas de BimestreConfig
        var fechasBimestre = await _ctx.BimestresConfig
            .Where(b => b.Bimestre == bimestre && b.Año == añoAcademico)
            .ToListAsync();

        if (!fechasBimestre.Any())
            return Enumerable.Empty<Sancion>();

        var fechaMin = fechasBimestre.Min(b => b.FechaInicio);
        var fechaMax = fechasBimestre.Max(b => b.FechaFin);

        return await _ctx.Sanciones
            .Include(s => s.Castigo)
            .Where(s => s.CadeteDNI == cadeteDni
                     && s.Fecha >= fechaMin
                     && s.Fecha <= fechaMax)
            .OrderBy(s => s.SemanaBimestre)
            .ToListAsync();
    }

    /// <summary>
    /// Todas las sanciones agrupadas por cadete para el consolidado bimestral.
    /// Filtra por año del cadete (3, 4 o 5) y rango de fechas del bimestre.
    /// </summary>
    public async Task<IEnumerable<Sancion>> GetParaConsolidadoAsync(
        int añoCadete, int bimestre, int añoAcademico)
    {
        var fechasBimestre = await _ctx.BimestresConfig
            .Where(b => b.Bimestre == bimestre && b.Año == añoAcademico)
            .ToListAsync();

        if (!fechasBimestre.Any())
            return Enumerable.Empty<Sancion>();

        var fechaMin = fechasBimestre.Min(b => b.FechaInicio);
        var fechaMax = fechasBimestre.Max(b => b.FechaFin);

        return await _ctx.Sanciones
            .Include(s => s.Cadete)
            .Include(s => s.Castigo)
            .Where(s => s.Cadete.Año == añoCadete
                     && s.Fecha >= fechaMin
                     && s.Fecha <= fechaMax)
            .OrderBy(s => s.Cadete.ApellidosNombres)
            .ThenBy(s => s.SemanaBimestre)
            .ToListAsync();
    }

    public async Task AddAsync(Sancion sancion)
    {
        await _ctx.Sanciones.AddAsync(sancion);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sancion sancion)
    {
        _ctx.Sanciones.Update(sancion);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sancion = await _ctx.Sanciones.FindAsync(id);
        if (sancion is not null)
        {
            _ctx.Sanciones.Remove(sancion);
            await _ctx.SaveChangesAsync();
        }
    }
}
