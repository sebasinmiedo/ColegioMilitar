using ColegioMilitar.UI.Forms;
using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI;

public partial class Form1 : Form
{
    private int _bimestreActivo    = 1;
    private int _añoAcademico      = DateTime.Today.Year;
    private int _semanaActiva3     = 0; // 0 = todas
    private int _semanaActiva4     = 0;
    private int _semanaActiva5     = 0;
    private int _semanaActivaSalida = 0;

    private List<BimestreConfig> _semanasActuales = new();

    public Form1() { InitializeComponent(); }

    private async void Form1_Load(object sender, EventArgs e)
    {
        cmbBimestre.Items.Clear();
        for (int i = 1; i <= 4; i++)
            cmbBimestre.Items.Add($"Bimestre {i}");

        cmbBimestre.SelectedIndexChanged -= cmbBimestre_SelectedIndexChanged;
        cmbBimestre.SelectedIndex = 0;
        cmbBimestre.SelectedIndexChanged += cmbBimestre_SelectedIndexChanged;

        await CargarBotonesSemanasAsync();
        PositionarEnSemanaActual();
        await RefrescarTodosAsync();
    }


    private async Task CargarBotonesSemanasAsync()
    {
        _semanasActuales = (await Program.Bimestres
            .GetByBimestreAsync(_bimestreActivo, _añoAcademico))
            .OrderBy(s => s.NroSemana)
            .ToList();

        RebuildSemanaButtons(pnlSemanas3, new EventHandler(BtnSemana3_Click));
        RebuildSemanaButtons(pnlSemanas4, new EventHandler(BtnSemana4_Click));
        RebuildSemanaButtons(pnlSemanas5, new EventHandler(BtnSemana5_Click));
        RebuildSemanaButtons(pnlSemanasSalida, new EventHandler(BtnSemanaSalida_Click), incluirTodas: false);
    }

    private void PositionarEnSemanaActual()
    {
        if (!_semanasActuales.Any()) return;

        var hoy = DateTime.Today;

        // Buscar semana que contenga hoy
        var semanaHoy = _semanasActuales
            .FirstOrDefault(s => s.FechaInicio <= hoy && hoy <= s.FechaFin);

        // Si es fin de semana o fuera de rango, tomar la más reciente anterior
        if (semanaHoy is null)
            semanaHoy = _semanasActuales
                .Where(s => s.FechaFin < hoy)
                .OrderByDescending(s => s.FechaFin)
                .FirstOrDefault();

        // Si aún es null (antes de todas las semanas), tomar la primera
        semanaHoy ??= _semanasActuales.First();

        int nro = semanaHoy.NroSemana;
        _semanaActiva3 = _semanaActiva4 = _semanaActiva5 = nro;
        _semanaActivaSalida = nro;

        ResaltarBotonActivo(pnlSemanas3, nro);
        ResaltarBotonActivo(pnlSemanas4, nro);
        ResaltarBotonActivo(pnlSemanas5, nro);
        ResaltarBotonActivo(pnlSemanasSalida, nro);
    }

    // ── Selector de bimestre ─────────────────────────────────────────────

    private async void cmbBimestre_SelectedIndexChanged(object sender, EventArgs e)
    {
        _bimestreActivo = cmbBimestre.SelectedIndex + 1;
        _semanaActiva3 = _semanaActiva4 = _semanaActiva5 = _semanaActivaSalida = 0;
        await CargarBotonesSemanasAsync();
        PositionarEnSemanaActual();
        await RefrescarTodosAsync();
    }

    private void RebuildSemanaButtons(Panel pnl, EventHandler handler, bool incluirTodas = true)
    {
        pnl.Controls.Clear();
        int x = 6;

        if (!_semanasActuales.Any())
        {
            var lbl = new Label();
            lbl.Text      = "⚠ No hay semanas configuradas. Usa '📅 Config Semanas'.";
            lbl.Font      = new Font("Segoe UI", 9, FontStyle.Italic);
            lbl.ForeColor = Color.DarkOrange;
            lbl.Location  = new Point(x, 10);
            lbl.AutoSize  = true;
            pnl.Controls.Add(lbl);
            return;
        }

        if (incluirTodas)
        {
            var btnTodas = CrearBotonSemana("Todas", null, true);
            btnTodas.Location = new Point(x, 5);
            btnTodas.Click   += handler;
            pnl.Controls.Add(btnTodas);
            x += btnTodas.Width + 5;
        }

        foreach (var semana in _semanasActuales)
        {
            var btn = CrearBotonSemana(semana.NombreSemana, semana.NroSemana, false);
            btn.Location = new Point(x, 5);
            btn.Click   += handler;
            pnl.Controls.Add(btn);
            x += btn.Width + 5;
        }

        // Marcar el primero como activo
        ResaltarBotonActivo(pnl, incluirTodas ? 0 : (_semanasActuales.FirstOrDefault()?.NroSemana ?? 1));
    }

    private static Button CrearBotonSemana(string texto, int? tag, bool esActivo)
    {
        var btn       = new Button();
        btn.Text      = texto;
        btn.Tag       = tag.HasValue ? (object)tag.Value : null;
        btn.Size      = new Size(texto.Length > 6 ? 95 : 65, 28);
        btn.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Cursor    = Cursors.Hand;
        btn.BackColor = esActivo ? Color.FromArgb(30, 60, 120) : Color.FromArgb(200, 210, 230);
        btn.ForeColor = esActivo ? Color.White : Color.FromArgb(30, 60, 120);
        return btn;
    }

    // ── Handlers de semana ───────────────────────────────────────────────

    private async void BtnSemana3_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActiva3 = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanas3, _semanaActiva3);
        await RefrescarTabAsync(3, dgv3, _semanaActiva3);
    }

    private async void BtnSemana4_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActiva4 = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanas4, _semanaActiva4);
        await RefrescarTabAsync(4, dgv4, _semanaActiva4);
    }

    private async void BtnSemana5_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActiva5 = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanas5, _semanaActiva5);
        await RefrescarTabAsync(5, dgv5, _semanaActiva5);
    }

    private async void BtnSemanaSalida_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActivaSalida = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanasSalida, _semanaActivaSalida);
        await RefrescarSalidaAsync(_semanaActivaSalida);
    }

    // mantener compatibilidad con Designer
    private async void BtnSemana3_Salida_Click(object sender, EventArgs e) =>
        await Task.CompletedTask;

    private static void ResaltarBotonActivo(Panel pnl, int semana)
    {
        foreach (Control c in pnl.Controls)
        {
            if (c is not Button b) continue;
            bool activo = b.Tag is int s ? s == semana : semana == 0;
            b.BackColor = activo ? Color.FromArgb(30, 60, 120) : Color.FromArgb(200, 210, 230);
            b.ForeColor = activo ? Color.White : Color.FromArgb(30, 60, 120);
        }
    }

    // ── Carga de datos ───────────────────────────────────────────────────

    private async Task RefrescarTodosAsync()
    {
        await RefrescarTabAsync(3, dgv3, _semanaActiva3);
        await RefrescarTabAsync(4, dgv4, _semanaActiva4);
        await RefrescarTabAsync(5, dgv5, _semanaActiva5);
        await RefrescarSalidaAsync(_semanaActivaSalida);
    }

    private async Task RefrescarTabAsync(int añoCadete, DataGridView dgv, int semana)
    {
        try
        {
            var cadetes = (await Program.Cadetes.GetByAñoAsync(añoCadete)).ToList();
            var sanciones = (await Program.SancionService.ListarTodosAsync())
                .Where(s => s.Cadete.Año == añoCadete)
                .Where(s => semana == 0 || s.SemanaBimestre == semana)
                .ToList();

            var filas = new List<FilaSancionRow>();
            int n = 1;

            if (semana == 0)
            {
                // Todas — orden cronológico en memoria (no en BD para evitar error TimeSpan)
                foreach (var s in sanciones
                    .AsEnumerable()
                    .OrderByDescending(s => s.Fecha)
                    .ThenByDescending(s => s.Hora.Ticks))
                {
                    filas.Add(new FilaSancionRow
                    {
                        N = n++.ToString(),
                        Codigo = s.CadeteDNI,
                        ApellidosNombres = s.Cadete.ApellidosNombres,
                        Motivo = s.Castigo.Descripcion,
                        Ptos = s.EsPierdeSalida ? "1PV" : s.PuntosAplicados.ToString(),
                        Reinc = s.Castigo.Reincidencia.ToString(),
                        Hora = s.Hora.ToString(@"hh\:mm"),
                        Fecha = s.Fecha.ToString("dd/MM/yyyy"),
                        Superior = $"{s.Supervisor.Grado} {s.Supervisor.ApellidosNombres}"
                    });
                }
            }
            else
            {
                // Semana específica — todos los cadetes (formato Excel)
                foreach (var cadete in cadetes)
                {
                    var sc = sanciones.Where(s => s.CadeteDNI == cadete.DNI).ToList();
                    if (!sc.Any())
                    {
                        filas.Add(new FilaSancionRow
                        {
                            N = n++.ToString(),
                            Codigo = cadete.DNI,
                            ApellidosNombres = cadete.ApellidosNombres
                        });
                    }
                    else
                    {
                        bool esPrimera = true;
                        foreach (var s in sc)
                        {
                            filas.Add(new FilaSancionRow
                            {
                                N = esPrimera ? n++.ToString() : "",
                                Codigo = esPrimera ? cadete.DNI : "",
                                ApellidosNombres = esPrimera ? cadete.ApellidosNombres : "",
                                Motivo = s.Castigo.Descripcion,
                                Ptos = s.EsPierdeSalida ? "1PV" : s.PuntosAplicados.ToString(),
                                Reinc = s.Castigo.Reincidencia.ToString(),
                                Hora = s.Hora.ToString(@"hh\:mm"),
                                Fecha = s.Fecha.ToString("dd/MM/yyyy"),
                                Superior = $"{s.Supervisor.Grado} {s.Supervisor.ApellidosNombres}"
                            });
                            esPrimera = false;
                        }
                    }
                }
            }

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.DataSource = filas;

            if (dgv.Columns.Count < 9) return;
            dgv.Columns[0].Width = 40; dgv.Columns[0].HeaderText = "N°";
            dgv.Columns[1].Width = 85; dgv.Columns[1].HeaderText = "CÓDIGO";
            dgv.Columns[2].Width = 230; dgv.Columns[2].HeaderText = "APELLIDOS Y NOMBRES";
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[3].HeaderText = "MOTIVO";
            dgv.Columns[4].Width = 50; dgv.Columns[4].HeaderText = "PTOS";
            dgv.Columns[5].Width = 50; dgv.Columns[5].HeaderText = "REINC";
            dgv.Columns[6].Width = 60; dgv.Columns[6].HeaderText = "HORA";
            dgv.Columns[7].Width = 85; dgv.Columns[7].HeaderText = "FECHA";
            dgv.Columns[8].Width = 180; dgv.Columns[8].HeaderText = "SUPERIOR QUE CASTIGA";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Error en RefrescarTabAsync");
        }
    }

    private async Task RefrescarSalidaAsync(int semana)
    {
        try
        {
            var filas = (await Program.ConsolidadoService.GenerarPtosSalidaAsync(semana))
                .Select((f, i) => new {
                    N = i + 1, f.CadeteDNI, f.ApellidosNombres,
                    Año = $"{f.Año}°", f.TotalPuntos, Salida = f.Salida
                }).ToList();

            dgvSalida.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvSalida.DataSource = filas;

            if (dgvSalida.Columns.Count < 6) return;
            dgvSalida.Columns[0].Width = 40;  dgvSalida.Columns[0].HeaderText = "N°";
            dgvSalida.Columns[1].Width = 90;  dgvSalida.Columns[1].HeaderText = "DNI";
            dgvSalida.Columns[2].Width = 280; dgvSalida.Columns[2].HeaderText = "APELLIDOS Y NOMBRES";
            dgvSalida.Columns[3].Width = 50;  dgvSalida.Columns[3].HeaderText = "AÑO";
            dgvSalida.Columns[4].Width = 60;  dgvSalida.Columns[4].HeaderText = "PTOS";
            dgvSalida.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                                dgvSalida.Columns[5].HeaderText = "SALIDA";

            foreach (DataGridViewRow row in dgvSalida.Rows)
            {
                var salida = row.Cells[5].Value?.ToString() ?? "";
                row.DefaultCellStyle.ForeColor = salida switch
                {
                    "Pierde salida"          => Color.Red,
                    "Sale domingo 07:00 hrs" => Color.OrangeRed,
                    "Sale sábado 07:00 hrs"  => Color.DarkOrange,
                    _                        => Color.DarkGreen
                };
                row.DefaultCellStyle.Font = salida == "Pierde salida"
                    ? new Font("Segoe UI", 9, FontStyle.Bold)
                    : new Font("Segoe UI", 9);
            }
        }
        catch { }
    }

    // ── Botones de acción ────────────────────────────────────────────────

    private void btnInsertarSancion_Click(object sender, EventArgs e)
    {
        var form = new FormRegistrarSancion();
        form.SancionGuardada += async () => await RefrescarTodosAsync();
        form.Show(this);
    }

    private void btnExportarExcel_Click(object sender, EventArgs e) =>
        MessageBox.Show("Exportación a Excel — próximamente.",
            "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void btnMantenimiento_Click(object sender, EventArgs e) =>
        new FormMantenimiento().Show(this);

    private async void btnConfigBimestre_Click(object sender, EventArgs e)
    {
        new FormConfigBimestre().ShowDialog(this);
        await CargarBotonesSemanasAsync();
        await RefrescarTodosAsync();
    }

    private async void btnRefrescar_Click(object sender, EventArgs e)
    {
        await CargarBotonesSemanasAsync();
        await RefrescarTodosAsync();
    }

    private class FilaSancionRow
    {
        public string N { get; set; } = "";
        public string Codigo { get; set; } = "";
        public string ApellidosNombres { get; set; } = "";
        public string Motivo { get; set; } = "";
        public string Ptos { get; set; } = "";
        public string Reinc { get; set; } = "";
        public string Hora { get; set; } = "";
        public string Fecha { get; set; } = "";
        public string Superior { get; set; } = "";
    }
}
