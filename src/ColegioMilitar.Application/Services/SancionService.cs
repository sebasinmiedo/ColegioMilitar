using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Repositories;

namespace ColegioMilitar.Application.Services;

public class SancionService
{
    private readonly SancionRepository    _sanciones;
    private readonly CadeteRepository     _cadetes;
    private readonly CastigoRepository    _castigos;
    private readonly SupervisorRepository _supervisores;
    private readonly BimestreRepository   _bimestres;

    public SancionService(
        SancionRepository sanciones, CadeteRepository cadetes,
        CastigoRepository castigos, SupervisorRepository supervisores,
        BimestreRepository bimestres)
    {
        _sanciones    = sanciones;
        _cadetes      = cadetes;
        _castigos     = castigos;
        _supervisores = supervisores;
        _bimestres    = bimestres;
    }

    public async Task<Sancion> RegistrarAsync(RegistrarSancionDto dto)
    {
        var cadete = await _cadetes.GetByIdAsync(dto.CadeteDNI)
            ?? throw new InvalidOperationException($"No existe cadete con DNI '{dto.CadeteDNI}'.");

        var castigo = await _castigos.GetByIdAsync(dto.CastigoCodigo)
            ?? throw new InvalidOperationException($"No existe el código '{dto.CastigoCodigo}'.");

        _ = await _supervisores.GetByIdAsync(dto.SupervisorDNI)
            ?? throw new InvalidOperationException($"No existe supervisor con DNI '{dto.SupervisorDNI}'.");

        // Detectar 1PV antes de calcular puntos
        bool esPierdeSalida = castigo.EsPierdeSalida(cadete.Año);
        int  puntosAplicados = castigo.GetPuntosPorAño(cadete.Año); // 0 si es 1PV

        // Resolver semana automáticamente
        int semana;
        if (dto.SemanaBimestreManual.HasValue)
        {
            if (dto.SemanaBimestreManual.Value < 1 || dto.SemanaBimestreManual.Value > 5)
                throw new ArgumentException("La semana debe estar entre 1 y 5.");
            semana = dto.SemanaBimestreManual.Value;
        }
        else
        {
            var config = await _bimestres.GetSemanaParaFechaAsync(dto.Fecha)
                ?? throw new InvalidOperationException(
                    $"La semana del {dto.Fecha:dd/MM/yyyy} está cerrada o no existe. " +
                    "Ve a '📅 Config Semanas' para verificar el estado de la semana.");
            semana = config.NroSemana;
        }

        var sancion = new Sancion
        {
            CadeteDNI       = dto.CadeteDNI,
            SupervisorDNI   = dto.SupervisorDNI,
            CastigoCodigo   = dto.CastigoCodigo,
            Fecha           = dto.Fecha.Date,
            Hora            = dto.Hora,
            Observaciones   = dto.Observaciones,
            PuntosAplicados = puntosAplicados,
            EsPierdeSalida  = esPierdeSalida,
            SemanaBimestre  = semana
        };

        await _sanciones.AddAsync(sancion);
        return sancion;
    }

    public async Task EliminarAsync(int id) => await _sanciones.DeleteAsync(id);

    public async Task<IEnumerable<Sancion>> ListarPorSemanaAsync(int semana) =>
        await _sanciones.GetBySemanaBimestreAsync(semana);

    public async Task<IEnumerable<Sancion>> ListarTodosAsync() =>
        await _sanciones.GetAllAsync();
}
