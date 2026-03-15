using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Repositories;

public class CadeteRepository : IRepository<Cadete, string>
{
    private readonly ColegioMilitarContext _ctx;

    public CadeteRepository(ColegioMilitarContext ctx) => _ctx = ctx;

    public async Task<Cadete?> GetByIdAsync(string dni) =>
        await _ctx.Cadetes.FindAsync(dni);

    public async Task<IEnumerable<Cadete>> GetAllAsync() =>
        await _ctx.Cadetes.OrderBy(c => c.ApellidosNombres).ToListAsync();

    /// <summary>Filtra por año (3, 4 o 5) y ordena alfabéticamente.</summary>
    public async Task<IEnumerable<Cadete>> GetByAñoAsync(int año) =>
        await _ctx.Cadetes
            .Where(c => c.Año == año)
            .OrderBy(c => c.ApellidosNombres)
            .ToListAsync();

    /// <summary>Búsqueda parcial por apellidos/nombres (para autocomplete).</summary>
    public async Task<IEnumerable<Cadete>> BuscarAsync(string texto) =>
        await _ctx.Cadetes
            .Where(c => c.ApellidosNombres.Contains(texto) || c.DNI.Contains(texto))
            .OrderBy(c => c.ApellidosNombres)
            .ToListAsync();

    public async Task AddAsync(Cadete cadete)
    {
        await _ctx.Cadetes.AddAsync(cadete);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cadete cadete)
    {
        _ctx.Cadetes.Update(cadete);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(string dni)
    {
        var cadete = await GetByIdAsync(dni);
        if (cadete is not null)
        {
            _ctx.Cadetes.Remove(cadete);
            await _ctx.SaveChangesAsync();
        }
    }
}
