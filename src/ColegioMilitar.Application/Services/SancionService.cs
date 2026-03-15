using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Infrastructure.Repositories;

namespace ColegioMilitar.Application.Services;

public class SancionService
{
    private readonly SancionRepository _sanciones;
    private readonly CadeteRepository _cadetes;
    private readonly CastigoRepository _castigos;
    private readonly SupervisorRepository _supervisores;
    private readonly BimestreRepository _bimestres;

    public SancionService(
        SancionRepository sanciones,
        CadeteRepository cadetes,
        CastigoRepository castigos,
        SupervisorRepository supervisores,
        BimestreRepository bimestres)
    {
        _sanciones   = sanciones;
        _cadetes     = cadetes;
        _castigos    = castigos;
        _supervisores = supervisores;
        _bimestres   = bimestres;
    }

    /// <summary>
    /// Registra una sanción calculando automáticamente puntos y semana.
    /// Lanza excepciones descriptivas si algún dato no existe.
    /// </summary>
    public async Task<Sancion> RegistrarAsync(RegistrarSancionDto dto)
    {
        // 1. Validar que existan cadete, supervisor y castigo
        var cadete = await _cadetes.GetByIdAsync(dto.CadeteDNI)
            ?? throw new InvalidOperationException($"No existe un cadete con DNI '{dto.CadeteDNI}'.");

        var castigo = await _castigos.GetByIdAsync(dto.CastigoCodigo)
            ?? throw new InvalidOperationException($"No existe el código de castigo '{dto.CastigoCodigo}'.");

        _ = await _supervisores.GetByIdAsync(dto.SupervisorDNI)
            ?? throw new InvalidOperationException($"No existe un supervisor con DNI '{dto.SupervisorDNI}'.");

        // 2. Calcular puntos según año del cadete
        int puntosAplicados = castigo.GetPuntosPorAño(cadete.Año);

        // 3. Resolver semana del bimestre
        int semana;
        if (dto.SemanaBimestreManual.HasValue)
        {
            if (dto.SemanaBimestreManual.Value < 1 || dto.SemanaBimestreManual.Value > 5)
                throw new ArgumentException("La semana del bimestre debe estar entre 1 y 5.");
            semana = dto.SemanaBimestreManual.Value;
        }
        else
        {
            var config = await _bimestres.GetSemanaParaFechaAsync(dto.Fecha)
                ?? throw new InvalidOperationException(
                    $"La fecha {dto.Fecha:dd/MM/yyyy} no pertenece a ninguna semana configurada. " +
                    "Configura el bimestre o ingresa la semana manualmente.");
            semana = config.NroSemana;
        }

        // 4. Crear y guardar
        var sancion = new Sancion
        {
            CadeteDNI      = dto.CadeteDNI,
            SupervisorDNI  = dto.SupervisorDNI,
            CastigoCodigo  = dto.CastigoCodigo,
            Fecha          = dto.Fecha.Date,
            Hora           = dto.Hora,
            Observaciones  = dto.Observaciones,
            PuntosAplicados = puntosAplicados,
            SemanaBimestre = semana
        };

        await _sanciones.AddAsync(sancion);
        return sancion;
    }

    public async Task EliminarAsync(int id) =>
        await _sanciones.DeleteAsync(id);

    public async Task<IEnumerable<Sancion>> ListarPorSemanaAsync(int semana) =>
        await _sanciones.GetBySemanaBimestreAsync(semana);

    public async Task<IEnumerable<Sancion>> ListarTodosAsync() =>
        await _sanciones.GetAllAsync();
}
