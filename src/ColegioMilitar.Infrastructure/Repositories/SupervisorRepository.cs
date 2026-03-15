using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Repositories;

public class SupervisorRepository : IRepository<Supervisor, string>
{
    private readonly ColegioMilitarContext _ctx;

    public SupervisorRepository(ColegioMilitarContext ctx) => _ctx = ctx;

    public async Task<Supervisor?> GetByIdAsync(string dni) =>
        await _ctx.Supervisores.FindAsync(dni);

    public async Task<IEnumerable<Supervisor>> GetAllAsync() =>
        await _ctx.Supervisores
            .OrderBy(s => s.Grado)
            .ThenBy(s => s.ApellidosNombres)
            .ToListAsync();

    /// <summary>Devuelve "GRADO APELLIDOS NOMBRES" para mostrar en combos.</summary>
    public async Task<IEnumerable<Supervisor>> BuscarAsync(string texto) =>
        await _ctx.Supervisores
            .Where(s => s.ApellidosNombres.Contains(texto)
                     || s.DNI.Contains(texto)
                     || s.Grado.Contains(texto))
            .OrderBy(s => s.ApellidosNombres)
            .ToListAsync();

    public async Task AddAsync(Supervisor supervisor)
    {
        await _ctx.Supervisores.AddAsync(supervisor);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Supervisor supervisor)
    {
        _ctx.Supervisores.Update(supervisor);
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(string dni)
    {
        var sup = await GetByIdAsync(dni);
        if (sup is not null)
        {
            _ctx.Supervisores.Remove(sup);
            await _ctx.SaveChangesAsync();
        }
    }
}
