using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Data;
using ColegioMilitar.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Application.Services;

public class ConsolidadoService
{
    private readonly SancionRepository _sanciones;
    private readonly CadeteRepository _cadetes;
    private readonly ColegioMilitarContext _ctx;

    public ConsolidadoService(
        SancionRepository sanciones,
        CadeteRepository cadetes,
        ColegioMilitarContext ctx)
    {
        _sanciones = sanciones;
        _cadetes   = cadetes;
        _ctx       = ctx;
    }

    /// <summary>
    /// Genera el consolidado bimestral para un año de cadetes (3, 4 o 5).
    /// Una fila por cadete con puntos desagregados por semana y totales calculados.
    /// </summary>
    public async Task<IEnumerable<FilaConsolidadoDto>> GenerarConsolidadoAsync(
        int añoCadete, int bimestre, int añoAcademico)
    {
        // Todos los cadetes del año, ordenados alfabéticamente
        var cadetes = await _cadetes.GetByAñoAsync(añoCadete);

        // Todas las sanciones del bimestre para ese año
        var sanciones = (await _sanciones.GetParaConsolidadoAsync(
            añoCadete, bimestre, añoAcademico)).ToList();

        // Notas de Actitud Militar ingresadas manualmente
        var actitudes = await _ctx.ActitudesMilitares
            .Where(a => a.Bimestre == bimestre && a.AñoAcademico == añoAcademico)
            .ToListAsync();

        var filas = new List<FilaConsolidadoDto>();

        foreach (var cadete in cadetes)
        {
            var sancionesCadete = sanciones
                .Where(s => s.CadeteDNI == cadete.DNI)
                .ToList();

            int PtosSemana(int semana) => sancionesCadete
                .Where(s => s.SemanaBimestre == semana)
                .Sum(s => s.PuntosAplicados);

            var actitud = actitudes
                .FirstOrDefault(a => a.CadeteDNI == cadete.DNI);

            filas.Add(new FilaConsolidadoDto
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
            });
        }

        return filas;
    }

    /// <summary>
    /// Genera el reporte PTOS SALIDA para una semana específica.
    /// Solo considera sanciones de esa semana (no el bimestre completo).
    /// </summary>
    public async Task<IEnumerable<FilaPtosSalidaDto>> GenerarPtosSalidaAsync(int semana)
    {
        var sanciones = (await _sanciones.GetBySemanaBimestreAsync(semana)).ToList();
        var cadetes   = await _cadetes.GetAllAsync();

        return cadetes
            .Select(c => new FilaPtosSalidaDto
            {
                CadeteDNI        = c.DNI,
                ApellidosNombres = c.ApellidosNombres,
                Año              = c.Año,
                TotalPuntos      = sanciones
                    .Where(s => s.CadeteDNI == c.DNI)
                    .Sum(s => s.PuntosAplicados)
            })
            .OrderBy(f => f.Año)
            .ThenBy(f => f.ApellidosNombres);
    }

    /// <summary>Guarda o actualiza la nota de Actitud Militar de un cadete.</summary>
    public async Task GuardarActitudMilitarAsync(
        string cadeteDni, int bimestre, int añoAcademico, decimal nota)
    {
        var existente = await _ctx.ActitudesMilitares
            .FirstOrDefaultAsync(a => a.CadeteDNI  == cadeteDni
                                   && a.Bimestre    == bimestre
                                   && a.AñoAcademico == añoAcademico);
        if (existente is null)
        {
            _ctx.ActitudesMilitares.Add(new ActitudMilitarManual
            {
                CadeteDNI    = cadeteDni,
                Bimestre     = bimestre,
                AñoAcademico = añoAcademico,
                NotaActitud  = nota
            });
        }
        else
        {
            existente.NotaActitud = nota;
        }

        await _ctx.SaveChangesAsync();
    }
}
