using ColegioMilitar.UI.Forms;
using ColegioMilitar.Domain.Entities;
using ColegioMilitar.Application.DTOs;

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
    private List<FilaPtosSalidaDto> _datosSalida = new();

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
        _semanaActivaSalida = btn.Tag is int s ? s : 1;
        ResaltarBotonActivo(pnlSemanasSalida, _semanaActivaSalida);
        await RefrescarSalidaAsync(_semanaActivaSalida);
    }

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
                        IdSancion = s.Id,          // ← agregar
                        EsPerdonada = s.Perdonada,   // ← agregar
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
                            IdSancion = 0,
                            EsPerdonada = false,
                            N = n++.ToString(),
                            Codigo = cadete.DNI,
                            ApellidosNombres = cadete.ApellidosNombres,
                            Motivo = "",
                            Ptos = "",
                            Reinc = "",
                            Hora = "",
                            Fecha = "",
                            Superior = ""
                        });
                    }
                    else
                    {
                        bool esPrimera = true;
                        foreach (var s in sc)
                        {
                            filas.Add(new FilaSancionRow
                            {
                                IdSancion = s.Id,
                                EsPerdonada = s.Perdonada,
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

            if (dgv.Columns.Count < 11) return;

            // Ocultar columnas internas
            dgv.Columns[0].Visible = false;  // IdSancion
            dgv.Columns[10].Visible = false; // EsPerdonada

            // Configurar columnas visibles
            dgv.Columns[1].Width = 40; dgv.Columns[1].HeaderText = "N°";
            dgv.Columns[2].Width = 85; dgv.Columns[2].HeaderText = "CÓDIGO";
            dgv.Columns[3].Width = 230; dgv.Columns[3].HeaderText = "APELLIDOS Y NOMBRES";
            dgv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[4].HeaderText = "MOTIVO";
            dgv.Columns[5].Width = 50; dgv.Columns[5].HeaderText = "PTOS";
            dgv.Columns[6].Width = 50; dgv.Columns[6].HeaderText = "REINC";
            dgv.Columns[7].Width = 60; dgv.Columns[7].HeaderText = "HORA";
            dgv.Columns[8].Width = 85; dgv.Columns[8].HeaderText = "FECHA";
            dgv.Columns[9].Width = 180; dgv.Columns[9].HeaderText = "SUPERIOR QUE CASTIGA";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Error en RefrescarTabAsync");
        }

        // Colorear perdonadas en gris claro
        dgv.Columns["IdSancion"]!.Visible = false;
        dgv.Columns["EsPerdonada"]!.Visible = false;

        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.DataBoundItem is FilaSancionRow fila && fila.EsPerdonada)
            {
                // Solo tachar columnas de sanción (Motivo, Ptos, Reinc, Hora, Fecha, Superior)
                var coloreadas = new[] { 3, 4, 5, 6, 7, 8 };
                foreach (var col in coloreadas)
                {
                    if (col < row.Cells.Count)
                    {
                        row.Cells[col].Style.ForeColor = Color.Silver;
                        row.Cells[col].Style.Font = new Font("Segoe UI", 9, FontStyle.Strikeout);
                    }
                }
            }
        }

        // Menú contextual
        var menu = new ContextMenuStrip();
        var itemModificar = new ToolStripMenuItem("✏️  Modificar sanción");
        var itemPerdonar = new ToolStripMenuItem("🕊️  Perdonar sanción");
        var itemEliminar = new ToolStripMenuItem("🗑️  Eliminar sanción");

        itemModificar.Click += async (s, e) => await AccionSancion(dgv, "modificar");
        itemPerdonar.Click += async (s, e) => await AccionSancion(dgv, "perdonar");
        itemEliminar.Click += async (s, e) => await AccionSancion(dgv, "eliminar");

        // Cambiar texto dinámicamente según si ya está perdonada
        menu.Opening += (s, e) =>
        {
            if (dgv.CurrentRow?.DataBoundItem is FilaSancionRow f && f.EsPerdonada)
                itemPerdonar.Text = "↩️  Revertir perdón";
            else
                itemPerdonar.Text = "🕊️  Perdonar sanción";
        };

        menu.Items.AddRange(new ToolStripItem[] { itemModificar, itemPerdonar, itemEliminar });
        dgv.ContextMenuStrip = menu;

    }

    private async Task AccionSancion(DataGridView dgv, string accion)
    {
        if (dgv.CurrentRow?.DataBoundItem is not FilaSancionRow fila) return;
        if (fila.IdSancion == 0) return;

        // Buscar el nombre — puede estar en la fila actual o en la fila anterior
        string nombre = fila.ApellidosNombres;
        if (string.IsNullOrEmpty(nombre))
        {
            // Buscar hacia arriba hasta encontrar el nombre
            int idx = dgv.CurrentRow.Index - 1;
            while (idx >= 0)
            {
                if (dgv.Rows[idx].DataBoundItem is FilaSancionRow anterior
                    && !string.IsNullOrEmpty(anterior.ApellidosNombres))
                {
                    nombre = anterior.ApellidosNombres;
                    break;
                }
                idx--;
            }
        }

        switch (accion)
        {
            case "perdonar":
                if (fila.EsPerdonada)
                {
                    // Ya está perdonada — ofrecer desperdonar
                    if (MessageBox.Show(
                        $"Esta sanción de '{nombre}' ya está perdonada.\n¿Deseas revertir el perdón?",
                        "Revertir perdón", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        != DialogResult.Yes) return;
                    await Program.SancionService.DesperdonarAsync(fila.IdSancion);
                }
                else
                {
                    if (MessageBox.Show(
                        $"¿Perdonar la sanción de '{nombre}'?\n" +
                        "No contará para puntos pero seguirá visible.",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        != DialogResult.Yes) return;
                    await Program.SancionService.PerdonarAsync(fila.IdSancion);
                }
                break;

            case "eliminar":
                if (MessageBox.Show(
                    $"¿Eliminar permanentemente la sanción de '{nombre}'?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    != DialogResult.Yes) return;
                await Program.SancionService.EliminarAsync(fila.IdSancion);
                break;

            case "modificar":
                MessageBox.Show("Modificar sanción — próximamente.",
                    "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
        }

        await RefrescarTodosAsync();
    }

    private async Task RefrescarSalidaAsync(int semana)
    {
        try
        {
            _datosSalida = (await Program.ConsolidadoService
                .GenerarPtosSalidaAsync(semana)).ToList();

            var semanaConfig = _semanasActuales.FirstOrDefault(s => s.NroSemana == semana);
            lblResumenSemana.Text = semanaConfig is not null
                ? $"{semanaConfig.NombreSemana} — Semana {semanaConfig.NroSemana}"
                : $"Semana {semana}";

            ActualizarResumenSalida();
            RefrescarTablaSalida(3, dgvSalida3);
            RefrescarTablaSalida(4, dgvSalida4);
            RefrescarTablaSalida(5, dgvSalida5);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Relación de Salida", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ActualizarResumenSalida()
    {
        var rowV = CalcularFilaResumen(5, lblResumenVAnoVie, lblResumenVAnoSab, lblResumenVAnoDom, lblResumenVAnoTot);
        var rowIV = CalcularFilaResumen(4, lblResumenIVAnoVie, lblResumenIVAnoSab, lblResumenIVAnoDom, lblResumenIVAnoTot);
        var rowIII = CalcularFilaResumen(3, lblResumenIIIAVie, lblResumenIIIASab, lblResumenIIIADom, lblResumenIIIATot);

        lblResumenTotalVie.Text = (rowV.Vie + rowIV.Vie + rowIII.Vie).ToString();
        lblResumenTotalSab.Text = (rowV.Sab + rowIV.Sab + rowIII.Sab).ToString();
        lblResumenTotalDom.Text = (rowV.Dom + rowIV.Dom + rowIII.Dom).ToString();
        lblResumenTotalTot.Text = (rowV.Vie + rowV.Sab + rowV.Dom
                                 + rowIV.Vie + rowIV.Sab + rowIV.Dom
                                 + rowIII.Vie + rowIII.Sab + rowIII.Dom).ToString();
    }

    private (int Vie, int Sab, int Dom) CalcularFilaResumen(
        int año, Label lblVie, Label lblSab, Label lblDom, Label lblTot)
    {
        var datosAño = _datosSalida.Where(f => f.Año == año).ToList();
        int ptosMayorIg10 = datosAño.Count(f => f.TotalPuntos >= 10 || f.CantidadPV > 0);
        int ptosMenor15 = datosAño.Count(f => f.TotalPuntos < 15 && f.CantidadPV == 0);
        int ptosMenor10 = datosAño.Count(f => f.TotalPuntos < 10 && f.CantidadPV == 0);
        int ptosMayorIg20 = datosAño.Count(f => f.TotalPuntos >= 20 || f.CantidadPV > 0);

        int viernes = ptosMayorIg10;
        int sabado = viernes - (ptosMenor15 - ptosMenor10);
        int domingo = ptosMayorIg20;
        sabado = Math.Max(0, sabado);

        lblVie.Text = viernes.ToString();
        lblSab.Text = sabado.ToString();
        lblDom.Text = domingo.ToString();
        lblTot.Text = (viernes + sabado + domingo).ToString();

        return (viernes, sabado, domingo);
    }

    private void RefrescarTablaSalida(int año, DataGridView dgv)
    {
        var filas = _datosSalida
            .Where(f => f.Año == año)
            .Select((f, i) => new
            {
                N = i + 1,
                f.CadeteDNI,
                f.ApellidosNombres,
                Ptos = f.PtosDisplay,
                f.Salida
            })
            .ToList();

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgv.DataSource = filas;

        if (dgv.Columns.Count >= 5)
        {
            dgv.Columns[0].Width = 40;
            dgv.Columns[0].HeaderText = "N°";
            dgv.Columns[1].Width = 90;
            dgv.Columns[1].HeaderText = "DNI";
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[2].HeaderText = "APELLIDOS Y NOMBRES";
            dgv.Columns[3].Width = 60;
            dgv.Columns[3].HeaderText = "PTOS";
            dgv.Columns[4].Width = 200;
            dgv.Columns[4].HeaderText = "SALIDA";
        }
    }

    private static void AplicarColoresSalida(DataGridView dgv)
    {
        foreach (DataGridViewRow row in dgv.Rows)
        {
            var salida = row.Cells[4].Value?.ToString() ?? "";
            var color = salida switch
            {
                "Pierde salida"           => Color.FromArgb(255, 200, 200),
                "Sale domingo 07:00 hrs" => Color.FromArgb(255, 220, 180),
                "Sale sábado 07:00 hrs"   => Color.FromArgb(255, 240, 200),
                _                         => Color.FromArgb(200, 240, 200)
            };
            row.DefaultCellStyle.BackColor = color;
            row.DefaultCellStyle.ForeColor = Color.Black;
            row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 200, 240);
            row.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        dgv.ClearSelection();
        dgv.Refresh();
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
        public int IdSancion { get; set; }  // col 0 — oculta
        public string N { get; set; } = "";  // col 1
        public string Codigo { get; set; } = "";  // col 2
        public string ApellidosNombres { get; set; } = "";  // col 3
        public string Motivo { get; set; } = "";  // col 4
        public string Ptos { get; set; } = "";  // col 5
        public string Reinc { get; set; } = "";  // col 6
        public string Hora { get; set; } = "";  // col 7
        public string Fecha { get; set; } = "";  // col 8
        public string Superior { get; set; } = "";  // col 9
        public bool EsPerdonada { get; set; }        // col 10 — oculta
    }
}
