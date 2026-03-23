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
        ConfigurarBotonesAcciones();
        InicializarTabs();
        lblSubtitulo.Text = $"RELACIÓN DE CASTIGADOS — {DateTime.Today:dd/MM/yyyy}";

        cmbBimestre.Items.Clear();
        for (int i = 1; i <= 4; i++)
            cmbBimestre.Items.Add($"Bimestre {i}");

        cmbBimestre.SelectedIndexChanged -= cmbBimestre_SelectedIndexChanged;
        cmbBimestre.SelectedIndex = 0;
        cmbBimestre.SelectedIndexChanged += cmbBimestre_SelectedIndexChanged;

        await CargarBotonesSemanasAsync();
        PositionarEnSemanaActual();
        await RefrescarTodosAsync();
        CargarReporteBimestralAsync();
    }

    private void CargarReporteBimestralAsync()
    {
        tabReporteBimestral.Controls.Clear();

        var reporte = new FormReporteBimestral(
            _bimestreActivo, _añoAcademico, _semanasActuales);

        reporte.TopLevel = false;
        reporte.FormBorderStyle = FormBorderStyle.None;
        reporte.Dock = DockStyle.Fill;

        tabReporteBimestral.Controls.Add(reporte);
        reporte.Show();

        _ = reporte.CargarDatosAsync();  // ← llama al método público
    }

    private void ConfigurarBotonesAcciones()
    {
        SetupBtn(btnInsertarSancion, "➕  INSERTAR SANCIÓN", 8, Color.FromArgb(30, 120, 60), 168);
        SetupBtn(btnExportarExcel, "📥  EXPORTAR EXCEL", 184, Color.FromArgb(20, 100, 50), 145);
        SetupBtn(btnConfigBimestre, "📅  CONFIG SEMANAS", 337, Color.FromArgb(80, 60, 140), 145);
        SetupBtn(btnMantenimiento, "⚙️  MANTENIMIENTO", 490, Color.FromArgb(80, 80, 80), 145);
    }

    private void InicializarTabs()
    {
        ConfigurarTabAño(tabPage3, "  3° AÑO  ", pnlSemanas3, dgv3, "RELACIÓN DE CASTIGADOS — 3ER AÑO");
        ConfigurarTabAño(tabPage4, "  4° AÑO  ", pnlSemanas4, dgv4, "RELACIÓN DE CASTIGADOS — 4TO AÑO");
        ConfigurarTabAño(tabPage5, "  5° AÑO  ", pnlSemanas5, dgv5, "RELACIÓN DE CASTIGADOS — 5TO AÑO");
        ConfigurarTabSalida();
    }

    private static void SetupBtn(Button btn, string texto, int x, Color color, int ancho)
    {
        btn.Text      = texto;
        btn.Location  = new Point(x, 8);
        btn.Size      = new Size(ancho, 30);
        btn.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        btn.BackColor = color;
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Cursor    = Cursors.Hand;
    }

    private static void ConfigurarTabAño(TabPage tab, string titulo,
        Panel pnlSemanas, DataGridView dgv, string tituloLabel)
    {
        tab.Text    = titulo;
        tab.Padding = new Padding(5);

        var lbl       = new Label();
        lbl.Text      = tituloLabel;
        lbl.Font      = new Font("Segoe UI", 11, FontStyle.Bold);
        lbl.ForeColor = Color.FromArgb(30, 60, 120);
        lbl.Dock      = DockStyle.Top;
        lbl.Height    = 30;
        lbl.TextAlign = ContentAlignment.MiddleLeft;
        lbl.Padding   = new Padding(4, 0, 0, 0);

        pnlSemanas.Dock      = DockStyle.Top;
        pnlSemanas.Height    = 40;
        pnlSemanas.BackColor = Color.FromArgb(235, 240, 250);

        ConfigurarDgv(dgv);
        dgv.Dock = DockStyle.Fill;

        tab.Controls.Add(dgv);
        tab.Controls.Add(pnlSemanas);
        tab.Controls.Add(lbl);
    }

    private void ConfigurarTabSalida()
    {
        tabSalida.Text = "  RELACIÓN DE SALIDA  ";
        tabSalida.Padding = new Padding(5);

        lblSalidaTitulo.Text = "RELACIÓN DE SALIDA — RACIONES";
        lblSalidaTitulo.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblSalidaTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblSalidaTitulo.Dock = DockStyle.Top;
        lblSalidaTitulo.Height = 30;
        lblSalidaTitulo.TextAlign = ContentAlignment.MiddleLeft;
        lblSalidaTitulo.Padding = new Padding(4, 0, 0, 0);

        pnlSemanasSalida.Dock = DockStyle.Top;
        pnlSemanasSalida.Height = 40;
        pnlSemanasSalida.BackColor = Color.FromArgb(235, 240, 250);

        pnlSalidaContent.Dock = DockStyle.Fill;

        // ── Tabla resumen de raciones ────────────────────────────────────────
        tlpResumenSalida.Controls.Clear();
        tlpResumenSalida.ColumnCount = 5;
        tlpResumenSalida.RowCount = 5;
        tlpResumenSalida.AutoSize = true;
        tlpResumenSalida.AutoSizeMode = AutoSizeMode.GrowOnly;
        tlpResumenSalida.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tlpResumenSalida.Dock = DockStyle.None;
        tlpResumenSalida.Location = new Point(14, 68);
        tlpResumenSalida.Margin = new Padding(0);

        tlpResumenSalida.ColumnStyles.Clear();
        tlpResumenSalida.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        for (int i = 0; i < 4; i++)
            tlpResumenSalida.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));

        tlpResumenSalida.RowStyles.Clear();
        for (int i = 0; i < 5; i++)
            tlpResumenSalida.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

        string[] encabezados = { "AÑOS", "VIE", "SAB", "DOM", "TOTAL" };
        for (int i = 0; i < encabezados.Length; i++)
        {
            tlpResumenSalida.Controls.Add(new Label
            {
                Text = encabezados[i],
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(65, 65, 65),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(230, 231, 235),
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
            }, i, 0);
        }

        AgregarFilaResumen(1, "V AÑO", lblResumenVAnoVie, lblResumenVAnoSab, lblResumenVAnoDom, lblResumenVAnoTot,
            Color.FromArgb(255, 225, 190), Color.FromArgb(255, 210, 150), Color.Empty);
        AgregarFilaResumen(2, "IV AÑO", lblResumenIVAnoVie, lblResumenIVAnoSab, lblResumenIVAnoDom, lblResumenIVAnoTot,
            Color.FromArgb(210, 245, 215), Color.FromArgb(200, 230, 190), Color.Empty);
        AgregarFilaResumen(3, "III AÑO", lblResumenIIIAVie, lblResumenIIIASab, lblResumenIIIADom, lblResumenIIIATot,
            Color.FromArgb(210, 220, 255), Color.FromArgb(170, 190, 255), Color.Empty);
        AgregarFilaResumen(4, "TOTAL", lblResumenTotalVie, lblResumenTotalSab, lblResumenTotalDom, lblResumenTotalTot,
            Color.FromArgb(200, 220, 240), Color.FromArgb(180, 200, 230), Color.FromArgb(255, 205, 140));

        // ── Panel contenedor del resumen ─────────────────────────────────────
        pnlResumenSalida.Controls.Clear();
        pnlResumenSalida.BackColor = Color.White;
        pnlResumenSalida.Dock = DockStyle.None;
        pnlResumenSalida.Anchor = AnchorStyles.None;
        pnlResumenSalida.AutoSize = false;
        pnlResumenSalida.Size = new Size(420, 230);
        pnlResumenSalida.Padding = new Padding(0);
        pnlResumenSalida.BorderStyle = BorderStyle.FixedSingle;

        lblResumenTitulo.Text = "RACIONES";
        lblResumenTitulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lblResumenTitulo.ForeColor = Color.FromArgb(40, 80, 140);
        lblResumenTitulo.TextAlign = ContentAlignment.MiddleCenter;
        lblResumenTitulo.Dock = DockStyle.None;
        lblResumenTitulo.Location = new Point(0, 8);
        lblResumenTitulo.Size = new Size(398, 28);

        lblResumenSemana.Text = "Semana --";
        lblResumenSemana.Font = new Font("Segoe UI", 9, FontStyle.Italic);
        lblResumenSemana.ForeColor = Color.FromArgb(90, 90, 90);
        lblResumenSemana.TextAlign = ContentAlignment.MiddleCenter;
        lblResumenSemana.Dock = DockStyle.None;
        lblResumenSemana.Location = new Point(0, 40);
        lblResumenSemana.Size = new Size(398, 18);

        pnlResumenSalida.Controls.Add(lblResumenTitulo);
        pnlResumenSalida.Controls.Add(lblResumenSemana);
        pnlResumenSalida.Controls.Add(tlpResumenSalida);

        // ── Tablas por año (3 columnas) ──────────────────────────────────────
        tlpTablasSalida.Controls.Clear();
        tlpTablasSalida.ColumnCount = 3;
        tlpTablasSalida.RowCount = 1;
        tlpTablasSalida.Dock = DockStyle.Fill;
        tlpTablasSalida.ColumnStyles.Clear();
        for (int i = 0; i < 3; i++)
            tlpTablasSalida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
        tlpTablasSalida.RowStyles.Clear();
        tlpTablasSalida.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpTablasSalida.Margin = new Padding(12, 4, 12, 12);

        BuildSalidaPanel(pnlTablaSalida3, lblTablaSalida3, dgvSalida3, "3ER AÑO", Color.FromArgb(30, 90, 160));
        BuildSalidaPanel(pnlTablaSalida4, lblTablaSalida4, dgvSalida4, "4TO AÑO", Color.FromArgb(20, 110, 60));
        BuildSalidaPanel(pnlTablaSalida5, lblTablaSalida5, dgvSalida5, "5TO AÑO", Color.FromArgb(140, 70, 10));

        tlpTablasSalida.Controls.Add(pnlTablaSalida3, 0, 0);
        tlpTablasSalida.Controls.Add(pnlTablaSalida4, 1, 0);
        tlpTablasSalida.Controls.Add(pnlTablaSalida5, 2, 0);

        // ── Layout principal del tab ─────────────────────────────────────────
        var tablasWrapper = new TableLayoutPanel
        {
            RowCount = 2,
            ColumnCount = 3,
            Dock = DockStyle.Fill
        };
        tablasWrapper.RowStyles.Clear();
        tablasWrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tablasWrapper.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablasWrapper.ColumnStyles.Clear();
        tablasWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablasWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tablasWrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        var resumenHolder = new TableLayoutPanel
        {
            ColumnCount = 3,
            RowCount = 1,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockStyle.None,
            Anchor = AnchorStyles.None,
            Padding = new Padding(0),
            Margin = new Padding(0, 20, 0, 4)
        };
        resumenHolder.ColumnStyles.Clear();
        resumenHolder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        resumenHolder.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        resumenHolder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        resumenHolder.RowStyles.Clear();
        resumenHolder.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        resumenHolder.Controls.Add(pnlResumenSalida, 1, 0);

        pnlSalidaContent.Controls.Clear();
        pnlSalidaContent.Padding = new Padding(12, 10, 12, 10);
        pnlSalidaContent.Controls.Add(tablasWrapper);

        tablasWrapper.Controls.Add(new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) }, 0, 0);
        tablasWrapper.Controls.Add(resumenHolder, 1, 0);
        tablasWrapper.Controls.Add(new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) }, 2, 0);
        tablasWrapper.Controls.Add(tlpTablasSalida, 0, 1);
        tablasWrapper.SetColumnSpan(tlpTablasSalida, 3);

        tabSalida.Controls.Clear();
        tabSalida.Controls.Add(pnlSalidaContent);
        tabSalida.Controls.Add(pnlSemanasSalida);
        tabSalida.Controls.Add(lblSalidaTitulo);
    }

    private void AgregarFilaResumen(int fila, string titulo, Label vie, Label sab, Label dom, Label tot,
        Color colorFila, Color colorTotalesColumn, Color colorTotalRowHighlight)
    {
        var tituloColor = fila == 4
            ? Color.FromArgb(190, 210, 240)
            : Color.FromArgb(245, 245, 245);

        var tituloLabel = new Label
        {
            Text      = titulo,
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock      = DockStyle.Fill,
            BackColor = tituloColor,
            Margin    = new Padding(0)
        };
        tlpResumenSalida.Controls.Add(tituloLabel, 0, fila);

        var highlightColor = fila == 4 && colorTotalRowHighlight != Color.Empty
            ? colorTotalRowHighlight
            : colorFila;

        var valores = new[] { vie, sab, dom, tot };
        for (int col = 0; col < valores.Length; col++)
        {
            var ctrl = valores[col];
            ctrl.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
            ctrl.TextAlign = ContentAlignment.MiddleCenter;
            ctrl.Dock      = DockStyle.Fill;
            ctrl.Margin    = new Padding(0);
            ctrl.Text      = "0";

            if (col == 3)
                ctrl.BackColor = colorTotalesColumn;
            else if (fila == 4)
                ctrl.BackColor = highlightColor;
            else
                ctrl.BackColor = colorFila;

            tlpResumenSalida.Controls.Add(ctrl, col + 1, fila);
        }
    }

    private static void BuildSalidaPanel(Panel panel, Label label, DataGridView dgv,
        string titulo, Color color)
    {
        panel.Dock    = DockStyle.Fill;
        panel.Padding = new Padding(4);
        panel.BackColor = Color.WhiteSmoke;

        label.Text      = $"{titulo}";
        label.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        label.ForeColor = Color.White;
        label.BackColor = color;
        label.Dock      = DockStyle.Top;
        label.Height    = 28;
        label.TextAlign = ContentAlignment.MiddleCenter;

        ConfigurarDgv(dgv);
        dgv.DataBindingComplete += (s, e) => AplicarColoresSalida(dgv);
        dgv.Dock = DockStyle.Fill;

        panel.Controls.Add(dgv);
        panel.Controls.Add(label);
    }

    private static void ConfigurarDgv(DataGridView dgv)
    {
        dgv.ReadOnly              = true;
        dgv.AllowUserToAddRows    = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
        dgv.RowHeadersVisible     = false;
        dgv.Font                  = new Font("Segoe UI", 9);
        dgv.ColumnHeadersHeight   = 28;
        dgv.RowTemplate.Height    = 24;
        dgv.BorderStyle           = BorderStyle.None;
        dgv.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.GridColor             = Color.FromArgb(210, 218, 235);
        dgv.BackgroundColor       = Color.White;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 255);
        dgv.DefaultCellStyle.SelectionBackColor       = Color.FromArgb(180, 200, 240);
        dgv.DefaultCellStyle.SelectionForeColor       = Color.Black;
        dgv.EnableHeadersVisualStyles                 = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 60, 120);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
        dgv.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.None;
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
        CargarReporteBimestralAsync();
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
            .Select(f => new
            {
                f.CadeteDNI,
                f.ApellidosNombres,
                Ptos = f.PtosDisplay,
                f.Salida,
                PtosNum = f.TotalPuntos + (f.CantidadPV > 0 ? 999 : 0)
            })
            .OrderBy(f => f.PtosNum)
            .Select((f, i) => new
            {
                N = i + 1,
                f.CadeteDNI,
                f.ApellidosNombres,
                f.Ptos,
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

        AplicarColoresSalida(dgv);
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
