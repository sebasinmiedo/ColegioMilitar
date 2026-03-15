using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI.Forms;

public partial class FormRegistrarSancion : Form
{
    // ── Estado interno ───────────────────────────────────────────────────────
    private Cadete?    _cadeteActual;
    private Castigo?   _castigoActual;
    private Supervisor? _supervisorActual;

    public FormRegistrarSancion()
    {
        InitializeComponent();
        ConfigurarFormulario();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  INICIALIZACIÓN
    // ════════════════════════════════════════════════════════════════════════

    private void ConfigurarFormulario()
    {
        // Fecha/hora por defecto = ahora
        dtpFecha.Value = DateTime.Today;
        dtpHora.Value  = DateTime.Now;

        // Semana manual: 1-5, oculto por defecto (se resuelve automático)
        nudSemana.Minimum = 1;
        nudSemana.Maximum = 5;
        nudSemana.Value   = 1;
        pnlSemanaManual.Visible = false;

        chkSemanaManual.CheckedChanged += (s, e) =>
            pnlSemanaManual.Visible = chkSemanaManual.Checked;

        // Enter en DNI cadete → buscar
        txtDNICadete.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarCadeteAsync(); }
        };

        // Enter en código castigo → buscar
        txtCodigoCastigo.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarCastigoAsync(); }
        };

        // Enter en DNI supervisor → buscar
        txtDNISupervisor.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarSupervisorAsync(); }
        };

        // Enter en observaciones → guardar
        txtObservaciones.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await GuardarAsync(); }
        };

        // F5 = guardar desde cualquier campo
        this.KeyPreview = true;
        this.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5) await GuardarAsync();
        };

        LimpiarCamposPostGuardado();
    }

    private async void FormRegistrarSancion_Load(object sender, EventArgs e)
    {
        // Pre-cargar supervisores en el combo (cambian poco)
        await CargarSupervisoresComboAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  BÚSQUEDAS
    // ════════════════════════════════════════════════════════════════════════

    private async Task BuscarCadeteAsync()
    {
        var dni = txtDNICadete.Text.Trim();
        if (string.IsNullOrEmpty(dni)) return;

        _cadeteActual = await Program.Cadetes.GetByIdAsync(dni);

        if (_cadeteActual is null)
        {
            MostrarError(lblCadeteInfo, $"✗ No existe cadete con DNI {dni}");
            txtCodigoCastigo.Enabled = false;
        }
        else
        {
            MostrarOk(lblCadeteInfo,
                $"✓ {_cadeteActual.ApellidosNombres}  |  {_cadeteActual.Año}° Año  |  Div. {_cadeteActual.Division ?? "-"}");
            txtCodigoCastigo.Enabled = true;
            txtCodigoCastigo.Focus();
        }
    }

    private async Task BuscarCastigoAsync()
    {
        var codigo = txtCodigoCastigo.Text.Trim();
        if (string.IsNullOrEmpty(codigo)) return;

        _castigoActual = await Program.Castigos.GetByIdAsync(codigo);

        if (_castigoActual is null)
        {
            MostrarError(lblCastigoInfo, $"✗ No existe el código {codigo}");
        }
        else
        {
            int pts = _cadeteActual is not null
                ? _castigoActual.GetPuntosPorAño(_cadeteActual.Año)
                : 0;

            MostrarOk(lblCastigoInfo,
                $"✓ {_castigoActual.Descripcion}  |  {pts} pts");

            // Foco al supervisor solo si ya está seleccionado; si no, ir al campo
            if (_supervisorActual is not null)
                btnGuardar.Focus();
            else
                txtDNISupervisor.Focus();
        }
    }

    private async Task BuscarSupervisorAsync()
    {
        var dni = txtDNISupervisor.Text.Trim();
        if (string.IsNullOrEmpty(dni)) return;

        _supervisorActual = await Program.Supervisores.GetByIdAsync(dni);

        if (_supervisorActual is null)
        {
            MostrarError(lblSupervisorInfo, $"✗ No existe supervisor con DNI {dni}");
        }
        else
        {
            MostrarOk(lblSupervisorInfo,
                $"✓ {_supervisorActual.Grado} {_supervisorActual.ApellidosNombres}");
            txtObservaciones.Focus();
        }
    }

    private async Task CargarSupervisoresComboAsync()
    {
        var supervisores = await Program.Supervisores.GetAllAsync();
        cmbSupervisor.DataSource    = supervisores.ToList();
        cmbSupervisor.DisplayMember = "ApellidosNombres";
        cmbSupervisor.ValueMember   = "DNI";

        cmbSupervisor.SelectedIndexChanged += (s, e) =>
        {
            if (cmbSupervisor.SelectedItem is Supervisor sup)
            {
                _supervisorActual = sup;
                txtDNISupervisor.Text = sup.DNI;
                MostrarOk(lblSupervisorInfo, $"✓ {sup.Grado} {sup.ApellidosNombres}");
            }
        };
    }

    // ════════════════════════════════════════════════════════════════════════
    //  GUARDAR
    // ════════════════════════════════════════════════════════════════════════

    private async void btnGuardar_Click(object sender, EventArgs e) => await GuardarAsync();

    private async Task GuardarAsync()
    {
        if (!ValidarCampos()) return;

        btnGuardar.Enabled = false;
        lblEstado.Text     = "Guardando...";

        try
        {
            var dto = new RegistrarSancionDto
            {
                CadeteDNI     = _cadeteActual!.DNI,
                SupervisorDNI = _supervisorActual!.DNI,
                CastigoCodigo = _castigoActual!.Codigo,
                Fecha         = dtpFecha.Value.Date,
                Hora          = dtpHora.Value.TimeOfDay,
                Observaciones = string.IsNullOrWhiteSpace(txtObservaciones.Text)
                                    ? null : txtObservaciones.Text.Trim(),
                SemanaBimestreManual = chkSemanaManual.Checked
                                    ? (int)nudSemana.Value : null
            };

            await Program.SancionService.RegistrarAsync(dto);

            // Mostrar confirmación en la grilla de registros recientes
            await RefrescarGrillaAsync();

            lblEstado.ForeColor = Color.Green;
            lblEstado.Text      = $"✓ Sanción guardada — {_cadeteActual.ApellidosNombres} | {_castigoActual.Codigo}";

            LimpiarCamposPostGuardado();
        }
        catch (Exception ex)
        {
            lblEstado.ForeColor = Color.Red;
            lblEstado.Text      = $"✗ Error: {ex.Message}";
        }
        finally
        {
            btnGuardar.Enabled = true;
        }
    }

    // ════════════════════════════════════════════════════════════════════════
    //  GRILLA DE REGISTROS RECIENTES
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarGrillaAsync()
    {
        var sanciones = (await Program.SancionService.ListarTodosAsync())
            .Take(50)   // últimas 50 para no sobrecargar
            .Select(s => new
            {
                s.Id,
                Cadete     = s.Cadete.ApellidosNombres,
                Año        = $"{s.Cadete.Año}°",
                Codigo     = s.CastigoCodigo,
                Descripcion = s.Castigo.Descripcion,
                Puntos     = s.PuntosAplicados,
                Semana     = s.SemanaBimestre,
                Fecha      = s.Fecha.ToString("dd/MM/yyyy"),
                Supervisor = $"{s.Supervisor.Grado} {s.Supervisor.ApellidosNombres}"
            });

        dgvRecientes.DataSource = sanciones.ToList();

        // Ajustar columnas solo la primera vez
        if (dgvRecientes.Columns.Count > 0 && dgvRecientes.Columns["Id"] is not null)
            dgvRecientes.Columns["Id"]!.Visible = false;
    }

    // ════════════════════════════════════════════════════════════════════════
    //  HELPERS
    // ════════════════════════════════════════════════════════════════════════

    private bool ValidarCampos()
    {
        if (_cadeteActual is null)
        { MostrarError(lblCadeteInfo, "✗ Busca un cadete primero (Enter en el DNI)"); txtDNICadete.Focus(); return false; }

        if (_castigoActual is null)
        { MostrarError(lblCastigoInfo, "✗ Busca un código de castigo (Enter en el código)"); txtCodigoCastigo.Focus(); return false; }

        if (_supervisorActual is null)
        { MostrarError(lblSupervisorInfo, "✗ Selecciona un supervisor"); txtDNISupervisor.Focus(); return false; }

        return true;
    }

    /// <summary>Limpia solo los campos que cambian por sanción. DNI supervisor se mantiene.</summary>
    private void LimpiarCamposPostGuardado()
    {
        txtDNICadete.Clear();
        txtCodigoCastigo.Clear();
        txtObservaciones.Clear();

        lblCadeteInfo.Text   = string.Empty;
        lblCastigoInfo.Text  = string.Empty;

        _cadeteActual  = null;
        _castigoActual = null;

        txtCodigoCastigo.Enabled = false;
        txtDNICadete.Focus();
    }

    private static void MostrarOk(Label lbl, string texto)
    {
        lbl.ForeColor = Color.DarkGreen;
        lbl.Text      = texto;
    }

    private static void MostrarError(Label lbl, string texto)
    {
        lbl.ForeColor = Color.Red;
        lbl.Text      = texto;
    }

    private async void btnLimpiarTodo_Click(object sender, EventArgs e)
    {
        LimpiarCamposPostGuardado();
        _supervisorActual = null;
        txtDNISupervisor.Clear();
        lblSupervisorInfo.Text = string.Empty;
        lblEstado.Text         = string.Empty;
        await Task.CompletedTask;
    }

    private async void btnRefrescar_Click(object sender, EventArgs e) =>
        await RefrescarGrillaAsync();
}
