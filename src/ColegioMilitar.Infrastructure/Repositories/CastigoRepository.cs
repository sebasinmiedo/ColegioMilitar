using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Repositories;

public class CastigoRepository : IRepository<Castigo, string>
{
    private readonly ColegioMilitarContext _ctx;

    public CastigoRepository(ColegioMilitarContext ctx) => _ctx = ctx;

    public async Task<Castigo?> GetByIdAsync(string codigo) =>
        await _ctx.Castigos.FindAsync(codigo);

    public async Task<IEnumerable<Castigo>> GetAllAsync() =>
        await _ctx.Castigos.OrderBy(c => c.Codigo).ToListAsync();

    /// <summary>Busca por código o descripción (para autocomplete en el formulario).</summary>
    public async Task<IEnumerable<Castigo>> BuscarAsync(string texto) =>
        await _ctx.Castigos
            .Where(c => c.Codigo.Contains(texto) || c.Descripcion.Contains(texto))
            .OrderBy(c => c.Codigo)
            .ToListAsync();

    public async Task AddAsync(Castigo castigo)
    {
        await _ctx.Castigos.AddAsync(castigo);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Castigo castigo)
    {
        _ctx.Castigos.Update(castigo);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(string codigo)
    {
        var castigo = await GetByIdAsync(codigo);
        if (castigo is not null)
        {
            _ctx.Castigos.Remove(castigo);
            await _ctx.SaveChangesAsync();
        }
    }
}
