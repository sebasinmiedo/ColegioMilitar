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
        dgv3               = new DataGridView();
        dgv4               = new DataGridView();
        dgv5               = new DataGridView();
        dgvSalida          = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlAcciones.SuspendLayout();
        tabControl.SuspendLayout();
        foreach (var d in new[] { dgv3, dgv4, dgv5, dgvSalida })
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
        ConfigurarTabAño(tabSalida, "  RELACIÓN DE SALIDA  ", pnlSemanasSalida, dgvSalida, "RELACIÓN DE SALIDA");

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
        foreach (var d in new[] { dgv3, dgv4, dgv5, dgvSalida })
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

    private Panel        pnlHeader;
    private Label        lblTitulo, lblSubtitulo;
    private Panel        pnlAcciones;
    private Button       btnInsertarSancion, btnExportarExcel;
    private Button       btnConfigBimestre, btnMantenimiento, btnRefrescar;
    private Label        lblBimestre;
    private ComboBox     cmbBimestre;
    private TabControl   tabControl;
    private TabPage      tabPage3, tabPage4, tabPage5, tabSalida;
    private Panel        pnlSemanas3, pnlSemanas4, pnlSemanas5, pnlSemanasSalida;
    private DataGridView dgv3, dgv4, dgv5, dgvSalida;
}
