using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using ColegioMilitar.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Application.Services;

public class ConsolidadoService
{
    private readonly SancionRepository _sanciones;
    private readonly CadeteRepository  _cadetes;
    private readonly ColegioMilitarContext _ctx;

    public ConsolidadoService(SancionRepository sanciones,
        CadeteRepository cadetes, ColegioMilitarContext ctx)
    {
        _sanciones = sanciones;
        _cadetes   = cadetes;
        _ctx       = ctx;
    }

    public async Task<IEnumerable<FilaConsolidadoDto>> GenerarConsolidadoAsync(
        int añoCadete, int bimestre, int añoAcademico)
    {
        var cadetes   = await _cadetes.GetByAñoAsync(añoCadete);
        var sanciones = (await _sanciones.GetParaConsolidadoAsync(
            añoCadete, bimestre, añoAcademico)).ToList();

        var actitudes = await _ctx.ActitudesMilitares
            .Where(a => a.Bimestre == bimestre && a.AñoAcademico == añoAcademico)
            .ToListAsync();

        return cadetes.Select(cadete =>
        {
            var sc = sanciones.Where(s => s.CadeteDNI == cadete.DNI).ToList();
            int PtosSemana(int sem) => sc.Where(s => s.SemanaBimestre == sem).Sum(s => s.PuntosAplicados);
            var actitud = actitudes.FirstOrDefault(a => a.CadeteDNI == cadete.DNI);

            return new FilaConsolidadoDto
            {
                CadeteDNI        = cadete.DNI,
                ApellidosNombres = cadete.ApellidosNombres,
                Año              = cadete.Año,
                Division         = cadete.Division,
                PtosSemana1      = PtosSemana(1),
                PtosSemana2      = PtosSemana(2),
                PtosSemana3      = PtosSemana(3),
                PtosSemana4      = PtosSemana(4),
                PtosSemana5      = PtosSemana(5),
                ActitudMilitar   = actitud?.NotaActitud ?? 0m
            };
        });
    }

    /// <summary>
    /// Genera el reporte PTOS SALIDA para una semana.
    /// Incluye todos los cadetes del año indicado (0 = todos los años).
    /// </summary>
    public async Task<IEnumerable<FilaPtosSalidaDto>> GenerarPtosSalidaAsync(
        int semana, int añoCadete = 0)
    {
        var sanciones = (await _sanciones.GetBySemanaBimestreAsync(semana)).ToList();
        var cadetes   = añoCadete == 0
            ? await _cadetes.GetAllAsync()
            : await _cadetes.GetByAñoAsync(añoCadete);

        return cadetes
            .Select(c => new FilaPtosSalidaDto
            {
                CadeteDNI        = c.DNI,
                ApellidosNombres = c.ApellidosNombres,
                Año              = c.Año,
                TotalPuntos      = sanciones
                    .Where(s => s.CadeteDNI == c.DNI)
                    .Sum(s => s.PuntosAplicados),
                TienePierdeSalida = sanciones
                    .Any(s => s.CadeteDNI == c.DNI && s.EsPierdeSalida)
            })
            .OrderBy(f => f.Año)
            .ThenBy(f => f.ApellidosNombres);
    }

    public async Task GuardarActitudMilitarAsync(
        string cadeteDni, int bimestre, int añoAcademico, decimal nota)
    {
        var existente = await _ctx.ActitudesMilitares
            .FirstOrDefaultAsync(a => a.CadeteDNI   == cadeteDni
                                   && a.Bimestre     == bimestre
                                   && a.AñoAcademico == añoAcademico);
        if (existente is null)
            _ctx.ActitudesMilitares.Add(new ActitudMilitarManual
                { CadeteDNI = cadeteDni, Bimestre = bimestre,
                  AñoAcademico = añoAcademico, NotaActitud = nota });
        else
            existente.NotaActitud = nota;

        await _ctx.SaveChangesAsync();
    }
}
