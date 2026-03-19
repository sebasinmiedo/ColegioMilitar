namespace ColegioMilitar.UI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlHeader          = new Panel();
        lblTitulo          = new Label();
        lblSubtitulo       = new Label();
        pnlAcciones        = new Panel();
        btnInsertarSancion = new Button();
        btnExportarExcel   = new Button();
        btnConfigBimestre  = new Button();
        btnMantenimiento   = new Button();
        btnRefrescar       = new Button();
        lblBimestre        = new Label();
        cmbBimestre        = new ComboBox();
        tabControl         = new TabControl();
        tabPage3           = new TabPage();
        tabPage4           = new TabPage();
        tabPage5           = new TabPage();
        tabSalida          = new TabPage();
        pnlSemanas3        = new Panel();
        pnlSemanas4        = new Panel();
        pnlSemanas5        = new Panel();
        pnlSemanasSalida   = new Panel();
        pnlSalidaContent   = new Panel();
        pnlResumenSalida   = new Panel();
        tlpResumenSalida   = new TableLayoutPanel();
        lblResumenTitulo   = new Label();
        lblResumenSemana   = new Label();
        lblResumenVAnoVie   = new Label();
        lblResumenVAnoSab   = new Label();
        lblResumenVAnoDom   = new Label();
        lblResumenVAnoTot   = new Label();
        lblResumenIVAnoVie  = new Label();
        lblResumenIVAnoSab  = new Label();
        lblResumenIVAnoDom  = new Label();
        lblResumenIVAnoTot  = new Label();
        lblResumenIIIAVie   = new Label();
        lblResumenIIIASab   = new Label();
        lblResumenIIIADom   = new Label();
        lblResumenIIIATot   = new Label();
        lblResumenTotalVie  = new Label();
        lblResumenTotalSab  = new Label();
        lblResumenTotalDom  = new Label();
        lblResumenTotalTot  = new Label();
        tlpTablasSalida    = new TableLayoutPanel();
        pnlTablaSalida3    = new Panel();
        pnlTablaSalida4    = new Panel();
        pnlTablaSalida5    = new Panel();
        lblTablaSalida3    = new Label();
        lblTablaSalida4    = new Label();
        lblTablaSalida5    = new Label();
        dgvSalida3         = new DataGridView();
        dgvSalida4         = new DataGridView();
        dgvSalida5         = new DataGridView();
        lblSalidaTitulo    = new Label();
        dgv3               = new DataGridView();
        dgv4               = new DataGridView();
        dgv5               = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlAcciones.SuspendLayout();
        tabControl.SuspendLayout();
        foreach (var d in new[] { dgv3, dgv4, dgv5, dgvSalida3, dgvSalida4, dgvSalida5 })
            ((System.ComponentModel.ISupportInitialize)d).BeginInit();
        SuspendLayout();

        // ── Header ───────────────────────────────────────────────────────
        pnlHeader.Dock      = DockStyle.Top;
        pnlHeader.Height    = 58;
        pnlHeader.BackColor = Color.FromArgb(30, 60, 120);

        lblTitulo.Text      = "IEPM CGAL — DPTO EVAL Y CONTROL";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location  = new Point(15, 7);
        lblTitulo.Size      = new Size(600, 26);

        lblSubtitulo.Text      = $"RELACIÓN DE CASTIGADOS — {DateTime.Today:dd/MM/yyyy}";
        lblSubtitulo.Font      = new Font("Segoe UI", 9);
        lblSubtitulo.ForeColor = Color.FromArgb(180, 210, 255);
        lblSubtitulo.Location  = new Point(15, 35);
        lblSubtitulo.Size      = new Size(500, 18);

        pnlHeader.Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo });

        // ── Panel acciones ────────────────────────────────────────────────
        pnlAcciones.Dock      = DockStyle.Top;
        pnlAcciones.Height    = 46;
        pnlAcciones.BackColor = Color.FromArgb(240, 243, 250);

        SetupBtn(btnInsertarSancion, "➕  INSERTAR SANCIÓN", 8,   Color.FromArgb(30, 120, 60),  168);
        SetupBtn(btnExportarExcel,   "📥  EXPORTAR EXCEL",  184, Color.FromArgb(20, 100, 50),  145);
        SetupBtn(btnConfigBimestre,  "📅  CONFIG SEMANAS",  337, Color.FromArgb(80, 60, 140),  145);
        SetupBtn(btnMantenimiento,   "⚙️  MANTENIMIENTO",   490, Color.FromArgb(80, 80, 80),   145);

        btnRefrescar.Text      = "↺";
        btnRefrescar.Location  = new Point(643, 8);
        btnRefrescar.Size      = new Size(36, 30);
        btnRefrescar.Font      = new Font("Segoe UI", 11);
        btnRefrescar.FlatStyle = FlatStyle.Flat;
        btnRefrescar.Cursor    = Cursors.Hand;

        // Selector de bimestre
        lblBimestre.Text      = "Bimestre:";
        lblBimestre.Location  = new Point(695, 14);
        lblBimestre.Size      = new Size(62, 20);
        lblBimestre.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        lblBimestre.ForeColor = Color.FromArgb(50, 50, 50);

        cmbBimestre.Location      = new Point(760, 10);
        cmbBimestre.Size          = new Size(130, 26);
        cmbBimestre.Font          = new Font("Segoe UI", 9, FontStyle.Bold);
        cmbBimestre.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBimestre.SelectedIndexChanged += cmbBimestre_SelectedIndexChanged;

        btnInsertarSancion.Click += btnInsertarSancion_Click;
        btnExportarExcel.Click   += btnExportarExcel_Click;
        btnConfigBimestre.Click  += btnConfigBimestre_Click;
        btnMantenimiento.Click   += btnMantenimiento_Click;
        btnRefrescar.Click       += btnRefrescar_Click;

        pnlAcciones.Controls.AddRange(new Control[]
        {
            btnInsertarSancion, btnExportarExcel, btnConfigBimestre,
            btnMantenimiento, btnRefrescar, lblBimestre, cmbBimestre
        });

        // ── TabControl ────────────────────────────────────────────────────
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        tabControl.Controls.AddRange(new TabPage[] { tabPage3, tabPage4, tabPage5, tabSalida });

        ConfigurarTabAño(tabPage3, "  3° AÑO  ", pnlSemanas3, dgv3, "RELACIÓN DE CASTIGADOS — 3ER AÑO");
        ConfigurarTabAño(tabPage4, "  4° AÑO  ", pnlSemanas4, dgv4, "RELACIÓN DE CASTIGADOS — 4TO AÑO");
        ConfigurarTabAño(tabPage5, "  5° AÑO  ", pnlSemanas5, dgv5, "RELACIÓN DE CASTIGADOS — 5TO AÑO");
        ConfigurarTabSalida();

        // ── Form ──────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(1280, 720);
        MinimumSize         = new Size(1100, 650);
        Text                = "Sistema de Sanciones — Colegio Militar";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterScreen;
        WindowState         = FormWindowState.Maximized;
        Load               += Form1_Load;

        Controls.Add(tabControl);
        Controls.Add(pnlAcciones);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        pnlAcciones.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        foreach (var d in new[] { dgv3, dgv4, dgv5, dgvSalida3, dgvSalida4, dgvSalida5 })
            ((System.ComponentModel.ISupportInitialize)d).EndInit();
        ResumeLayout(false);
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

        // Panel semanas — se llena dinámicamente desde Form1.cs
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
        tabSalida.Text    = "  RELACIÓN DE SALIDA  ";
        tabSalida.Padding = new Padding(5);

        lblSalidaTitulo.Text      = "RELACIÓN DE SALIDA — RACIONES";
        lblSalidaTitulo.Font      = new Font("Segoe UI", 11, FontStyle.Bold);
        lblSalidaTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblSalidaTitulo.Dock      = DockStyle.Top;
        lblSalidaTitulo.Height    = 30;
        lblSalidaTitulo.TextAlign = ContentAlignment.MiddleLeft;
        lblSalidaTitulo.Padding   = new Padding(4, 0, 0, 0);

        pnlSemanasSalida.Dock      = DockStyle.Top;
        pnlSemanasSalida.Height    = 40;
        pnlSemanasSalida.BackColor = Color.FromArgb(235, 240, 250);

        pnlSalidaContent.Dock = DockStyle.Fill;

        pnlResumenSalida.Dock      = DockStyle.Top;
        pnlResumenSalida.Height    = 230;
        pnlResumenSalida.BackColor = Color.White;
        pnlResumenSalida.Padding   = new Padding(12);
        pnlResumenSalida.BorderStyle = BorderStyle.FixedSingle;

        lblResumenTitulo.Text      = "RACIONES";
        lblResumenTitulo.Font      = new Font("Segoe UI", 12, FontStyle.Bold);
        lblResumenTitulo.ForeColor = Color.FromArgb(40, 80, 140);
        lblResumenTitulo.Dock      = DockStyle.Top;
        lblResumenTitulo.Height    = 30;
        lblResumenTitulo.TextAlign = ContentAlignment.MiddleCenter;

        lblResumenSemana.Text      = "Semana --";
        lblResumenSemana.Font      = new Font("Segoe UI", 9, FontStyle.Italic);
        lblResumenSemana.ForeColor = Color.FromArgb(80, 80, 80);
        lblResumenSemana.Dock      = DockStyle.Top;
        lblResumenSemana.Height    = 18;
        lblResumenSemana.TextAlign = ContentAlignment.MiddleCenter;

        tlpResumenSalida.ColumnCount = 5;
        tlpResumenSalida.RowCount    = 5;
        tlpResumenSalida.AutoSize    = true;
        tlpResumenSalida.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tlpResumenSalida.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        tlpResumenSalida.ColumnStyles.Clear();
        tlpResumenSalida.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        for (int i = 0; i < 4; i++)
            tlpResumenSalida.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
        tlpResumenSalida.RowStyles.Clear();
        for (int i = 0; i < 5; i++)
            tlpResumenSalida.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        tlpResumenSalida.Margin = new Padding(0, 12, 0, 0);

        string[] encabezados = { "AÑOS", "VIE", "SAB", "DOM", "TOTAL" };
        for (int i = 0; i < encabezados.Length; i++)
        {
            tlpResumenSalida.Controls.Add(new Label
            {
                Text      = encabezados[i],
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(240, 240, 240),
                Dock      = DockStyle.Fill,
                Margin    = new Padding(0)
            }, i, 0);
        }

        void AddResumenRow(int row, string titulo, Label vie, Label sab, Label dom, Label tot,
            Color rowColor, Color dayHighlight, Color totalColumnColor)
        {
            var tituloLabel = new Label
            {
                Text      = titulo,
                Font      = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock      = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 245),
                Margin    = new Padding(0)
            };
            tlpResumenSalida.Controls.Add(tituloLabel, 0, row);

            var valores = new[] { vie, sab, dom, tot };
            for (int col = 0; col < valores.Length; col++)
            {
                var ctrl = valores[col];
                ctrl.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
                ctrl.TextAlign = ContentAlignment.MiddleCenter;
                ctrl.Dock      = DockStyle.Fill;
                ctrl.Margin    = new Padding(0);
                ctrl.Text      = "0";
                if (row == 4 && col < 3)
                    ctrl.BackColor = dayHighlight;
                else if (col == 3)
                    ctrl.BackColor = totalColumnColor;
                else
                    ctrl.BackColor = rowColor;

                tlpResumenSalida.Controls.Add(ctrl, col + 1, row);
            }
        }

        AddResumenRow(1, "V AÑO", lblResumenVAnoVie, lblResumenVAnoSab, lblResumenVAnoDom, lblResumenVAnoTot,
            Color.FromArgb(255, 235, 205), Color.FromArgb(255, 210, 150), Color.FromArgb(230, 200, 150));
        AddResumenRow(2, "IV AÑO", lblResumenIVAnoVie, lblResumenIVAnoSab, lblResumenIVAnoDom, lblResumenIVAnoTot,
            Color.FromArgb(210, 245, 215), Color.FromArgb(210, 245, 215), Color.FromArgb(200, 230, 190));
        AddResumenRow(3, "III AÑO", lblResumenIIIAVie, lblResumenIIIASab, lblResumenIIIADom, lblResumenIIIATot,
            Color.FromArgb(210, 220, 255), Color.FromArgb(190, 210, 255), Color.FromArgb(170, 190, 255));
        AddResumenRow(4, "TOTAL", lblResumenTotalVie, lblResumenTotalSab, lblResumenTotalDom, lblResumenTotalTot,
            Color.FromArgb(200, 220, 240), Color.FromArgb(255, 205, 140), Color.FromArgb(180, 200, 230));


        tlpTablasSalida.ColumnCount = 3;
        tlpTablasSalida.RowCount    = 1;
        tlpTablasSalida.Dock        = DockStyle.Fill;
        tlpTablasSalida.ColumnStyles.Clear();
        for (int i = 0; i < 3; i++)
            tlpTablasSalida.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
        tlpTablasSalida.RowStyles.Clear();
        tlpTablasSalida.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        BuildSalidaPanel(pnlTablaSalida3, lblTablaSalida3, dgvSalida3, "3ER AÑO", Color.FromArgb(30, 90, 160));
        BuildSalidaPanel(pnlTablaSalida4, lblTablaSalida4, dgvSalida4, "4TO AÑO", Color.FromArgb(20, 110, 60));
        BuildSalidaPanel(pnlTablaSalida5, lblTablaSalida5, dgvSalida5, "5TO AÑO", Color.FromArgb(140, 70, 10));

        tlpTablasSalida.Controls.Add(pnlTablaSalida3, 0, 0);
        tlpTablasSalida.Controls.Add(pnlTablaSalida4, 1, 0);
        tlpTablasSalida.Controls.Add(pnlTablaSalida5, 2, 0);

        pnlSalidaContent.Controls.Add(tlpTablasSalida);
        pnlSalidaContent.Controls.Add(pnlResumenSalida);
        pnlResumenSalida.Controls.Add(lblResumenTitulo);
        pnlResumenSalida.Controls.Add(lblResumenSemana);
        pnlResumenSalida.Controls.Add(tlpResumenSalida);
        tlpResumenSalida.Location = new Point(0, lblResumenSemana.Bottom + 14);
        pnlResumenSalida.SizeChanged += (s, e) =>
        {
            tlpResumenSalida.Left = Math.Max((pnlResumenSalida.ClientSize.Width - tlpResumenSalida.Width) / 2, 0);
            tlpResumenSalida.Top = lblResumenSemana.Bottom + 14;
        };

        tabSalida.Controls.Add(pnlSalidaContent);
        tabSalida.Controls.Add(pnlSemanasSalida);
        tabSalida.Controls.Add(lblSalidaTitulo);
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

    private Panel             pnlHeader;
    private Label             lblTitulo, lblSubtitulo;
    private Panel             pnlAcciones;
    private Button            btnInsertarSancion, btnExportarExcel;
    private Button            btnConfigBimestre, btnMantenimiento, btnRefrescar;
    private Label             lblBimestre;
    private ComboBox          cmbBimestre;
    private TabControl        tabControl;
    private TabPage           tabPage3, tabPage4, tabPage5, tabSalida;
    private Panel             pnlSemanas3, pnlSemanas4, pnlSemanas5, pnlSemanasSalida;
    private DataGridView      dgv3, dgv4, dgv5;
    private Panel             pnlSalidaContent, pnlResumenSalida;
    private TableLayoutPanel  tlpResumenSalida, tlpTablasSalida;
    private Label             lblResumenTitulo, lblResumenSemana;
    private Label             lblResumenVAnoVie, lblResumenVAnoSab, lblResumenVAnoDom, lblResumenVAnoTot;
    private Label             lblResumenIVAnoVie, lblResumenIVAnoSab, lblResumenIVAnoDom, lblResumenIVAnoTot;
    private Label             lblResumenIIIAVie, lblResumenIIIASab, lblResumenIIIADom, lblResumenIIIATot;
    private Label             lblResumenTotalVie, lblResumenTotalSab, lblResumenTotalDom, lblResumenTotalTot;
    private Label             lblSalidaTitulo;
    private Panel             pnlTablaSalida3, pnlTablaSalida4, pnlTablaSalida5;
    private Label             lblTablaSalida3, lblTablaSalida4, lblTablaSalida5;
    private DataGridView      dgvSalida3, dgvSalida4, dgvSalida5;
}
