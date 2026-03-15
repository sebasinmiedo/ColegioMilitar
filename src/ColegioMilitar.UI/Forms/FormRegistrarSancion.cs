using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI.Forms;

public partial class FormRegistrarSancion : Form
{
    public event Func<Task>? SancionGuardada;

    private Cadete?     _cadeteActual;
    private Castigo?    _castigoActual;
    private Supervisor? _supervisorActual;

    public FormRegistrarSancion()
    {
        InitializeComponent();
        ConfigurarFormulario();
    }

    private void ConfigurarFormulario()
    {
        dtpFecha.Value = DateTime.Today;
        dtpHora.Value  = DateTime.Now;

        txtDNICadete.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarCadeteAsync(); }
        };
        txtCodigoCastigo.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarCastigoAsync(); }
        };
        txtDNISupervisor.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await BuscarSupervisorAsync(); }
        };
        txtObservaciones.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await GuardarAsync(); }
        };

        this.KeyPreview = true;
        this.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5) await GuardarAsync();
        };

        LimpiarCamposPostGuardado();
    }

    private async void FormRegistrarSancion_Load(object sender, EventArgs e)
    {
        await CargarSupervisoresComboAsync();
    }

    // ── Búsquedas ────────────────────────────────────────────────────────────

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
                $"✓ {_cadeteActual.ApellidosNombres}  |  {_cadeteActual.Año}° Año");
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
            MostrarError(lblCastigoInfo, $"✗ No existe el código {codigo}");
        else
        {
            int pts = _cadeteActual is not null
                ? _castigoActual.GetPuntosPorAño(_cadeteActual.Año) : 0;
            MostrarOk(lblCastigoInfo, $"✓ {_castigoActual.Descripcion}  |  {pts} pts");

            if (_supervisorActual is not null) btnGuardar.Focus();
            else txtDNISupervisor.Focus();
        }
    }

    private async Task BuscarSupervisorAsync()
    {
        var dni = txtDNISupervisor.Text.Trim();
        if (string.IsNullOrEmpty(dni)) return;

        _supervisorActual = await Program.Supervisores.GetByIdAsync(dni);

        if (_supervisorActual is null)
            MostrarError(lblSupervisorInfo, $"✗ No existe supervisor con DNI {dni}");
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
        cmbSupervisor.SelectedIndex = -1;

        cmbSupervisor.SelectedIndexChanged += (s, e) =>
        {
            if (cmbSupervisor.SelectedItem is Supervisor sup)
            {
                _supervisorActual     = sup;
                txtDNISupervisor.Text = sup.DNI;
                MostrarOk(lblSupervisorInfo, $"✓ {sup.Grado} {sup.ApellidosNombres}");
            }
        };
    }

    // ── Guardar ──────────────────────────────────────────────────────────────

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
                SemanaBimestreManual = null  // siempre automático
            };

            await Program.SancionService.RegistrarAsync(dto);

            lblEstado.ForeColor = Color.DarkGreen;
            lblEstado.Text      = $"✓ Guardado — {_cadeteActual.ApellidosNombres} | Cod. {_castigoActual.Codigo}";

            // Notificar al Form1 para que refresque la grilla
            if (SancionGuardada is not null)
                await SancionGuardada.Invoke();

            LimpiarCamposPostGuardado();
        }
        catch (Exception ex)
        {
            lblEstado.ForeColor = Color.Red;
            lblEstado.Text      = $"✗ {ex.Message}";
        }
        finally
        {
            btnGuardar.Enabled = true;
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private bool ValidarCampos()
    {
        if (_cadeteActual is null)
        { MostrarError(lblCadeteInfo, "✗ Busca un cadete primero"); txtDNICadete.Focus(); return false; }
        if (_castigoActual is null)
        { MostrarError(lblCastigoInfo, "✗ Ingresa un código de castigo"); txtCodigoCastigo.Focus(); return false; }
        if (_supervisorActual is null)
        { MostrarError(lblSupervisorInfo, "✗ Selecciona un supervisor"); txtDNISupervisor.Focus(); return false; }
        return true;
    }

    private void LimpiarCamposPostGuardado()
    {
        txtDNICadete.Clear();
        txtCodigoCastigo.Clear();
        txtObservaciones.Clear();
        lblCadeteInfo.Text  = string.Empty;
        lblCastigoInfo.Text = string.Empty;
        _cadeteActual       = null;
        _castigoActual      = null;
        txtCodigoCastigo.Enabled = false;
        txtDNICadete.Focus();
    }

    private async void btnLimpiarTodo_Click(object sender, EventArgs e)
    {
        LimpiarCamposPostGuardado();
        _supervisorActual      = null;
        txtDNISupervisor.Text  = string.Empty;
        lblSupervisorInfo.Text = string.Empty;
        lblEstado.Text         = string.Empty;
        cmbSupervisor.SelectedIndex = -1;
        await Task.CompletedTask;
    }

    private static void MostrarOk(Label lbl, string texto)
    { lbl.ForeColor = Color.DarkGreen; lbl.Text = texto; }

    private static void MostrarError(Label lbl, string texto)
    { lbl.ForeColor = Color.Red; lbl.Text = texto; }
}
