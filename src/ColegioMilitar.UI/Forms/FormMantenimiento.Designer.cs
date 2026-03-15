namespace ColegioMilitar.UI.Forms;

partial class FormMantenimiento
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo             = new Label();
        tabControl            = new TabControl();
        tabCadetes            = new TabPage();
        tabSupervisores       = new TabPage();
        tabCastigos           = new TabPage();
        pnlToolCadetes        = new Panel();
        btnCargarCsv3         = new Button();
        btnCargarCsv4         = new Button();
        btnCargarCsv5         = new Button();
        lblEstadoCadetes      = new Label();
        dgvCadetes            = new DataGridView();
        pnlToolSupervisores   = new Panel();
        btnCargarCsvSupervisores = new Button();
        lblEstadoSupervisores = new Label();
        dgvSupervisores       = new DataGridView();
        pnlToolCastigos       = new Panel();
        btnCargarCsvCastigos  = new Button();
        lblEstadoCastigos     = new Label();
        dgvCastigos           = new DataGridView();

        tabControl.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCadetes).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvSupervisores).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvCastigos).BeginInit();
        SuspendLayout();

        // Título
        lblTitulo.Text      = "MANTENIMIENTO DE DATOS";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblTitulo.Dock      = DockStyle.Top;
        lblTitulo.Height    = 36;
        lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
        lblTitulo.Padding   = new Padding(8, 0, 0, 0);

        // TabControl
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        tabControl.Controls.AddRange(new TabPage[] { tabCadetes, tabSupervisores, tabCastigos });

        // ── Tab Cadetes ──────────────────────────────────────────────────
        tabCadetes.Text    = "  Cadetes  ";
        tabCadetes.Padding = new Padding(4);

        pnlToolCadetes.Dock      = DockStyle.Top;
        pnlToolCadetes.Height    = 82;
        pnlToolCadetes.BackColor = Color.FromArgb(240, 243, 250);

        var lblInfo = new Label();
        lblInfo.Text      = "Carga un CSV por año. Formato: N°, DNI, APELLIDOS Y NOMBRES, [DIVISION]";
        lblInfo.Font      = new Font("Segoe UI", 8.5f);
        lblInfo.ForeColor = Color.FromArgb(80, 80, 80);
        lblInfo.Location  = new Point(8, 5);
        lblInfo.Size      = new Size(700, 18);

        CrearBtnCsv(btnCargarCsv3, "📂  CSV 3° Año", 8,   Color.FromArgb(30, 90, 160));
        CrearBtnCsv(btnCargarCsv4, "📂  CSV 4° Año", 168, Color.FromArgb(20, 110, 60));
        CrearBtnCsv(btnCargarCsv5, "📂  CSV 5° Año", 328, Color.FromArgb(140, 70, 10));

        btnCargarCsv3.Click += btnCargarCsv3_Click;
        btnCargarCsv4.Click += btnCargarCsv4_Click;
        btnCargarCsv5.Click += btnCargarCsv5_Click;

        lblEstadoCadetes.Location  = new Point(8, 58);
        lblEstadoCadetes.Size      = new Size(800, 18);
        lblEstadoCadetes.Font      = new Font("Segoe UI", 9);

        pnlToolCadetes.Controls.AddRange(new Control[]
            { lblInfo, btnCargarCsv3, btnCargarCsv4, btnCargarCsv5, lblEstadoCadetes });

        ConfigurarDgv(dgvCadetes);
        dgvCadetes.Dock = DockStyle.Fill;
        tabCadetes.Controls.Add(dgvCadetes);
        tabCadetes.Controls.Add(pnlToolCadetes);

        // ── Tab Supervisores ─────────────────────────────────────────────
        tabSupervisores.Text    = "  Supervisores  ";
        tabSupervisores.Padding = new Padding(4);

        pnlToolSupervisores.Dock      = DockStyle.Top;
        pnlToolSupervisores.Height    = 46;
        pnlToolSupervisores.BackColor = Color.FromArgb(240, 243, 250);

        btnCargarCsvSupervisores.Text      = "📂  Cargar CSV de Supervisores";
        btnCargarCsvSupervisores.Location  = new Point(8, 7);
        btnCargarCsvSupervisores.Size      = new Size(240, 30);
        btnCargarCsvSupervisores.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        btnCargarCsvSupervisores.BackColor = Color.FromArgb(30, 60, 120);
        btnCargarCsvSupervisores.ForeColor = Color.White;
        btnCargarCsvSupervisores.FlatStyle = FlatStyle.Flat;
        btnCargarCsvSupervisores.FlatAppearance.BorderSize = 0;
        btnCargarCsvSupervisores.Cursor    = Cursors.Hand;
        btnCargarCsvSupervisores.Click    += btnCargarCsvSupervisores_Click;

        lblEstadoSupervisores.Location = new Point(258, 12);
        lblEstadoSupervisores.Size     = new Size(500, 20);
        lblEstadoSupervisores.Font     = new Font("Segoe UI", 9);
        lblEstadoSupervisores.Text     = "Formato: GRADO, APELLIDOS Y NOMBRES, DNI";

        pnlToolSupervisores.Controls.AddRange(new Control[]
            { btnCargarCsvSupervisores, lblEstadoSupervisores });

        ConfigurarDgv(dgvSupervisores);
        dgvSupervisores.Dock = DockStyle.Fill;
        tabSupervisores.Controls.Add(dgvSupervisores);
        tabSupervisores.Controls.Add(pnlToolSupervisores);

        // ── Tab Castigos ─────────────────────────────────────────────────
        tabCastigos.Text    = "  Castigos  ";
        tabCastigos.Padding = new Padding(4);

        pnlToolCastigos.Dock      = DockStyle.Top;
        pnlToolCastigos.Height    = 46;
        pnlToolCastigos.BackColor = Color.FromArgb(240, 243, 250);

        btnCargarCsvCastigos.Text      = "📂  Cargar CSV de Castigos";
        btnCargarCsvCastigos.Location  = new Point(8, 7);
        btnCargarCsvCastigos.Size      = new Size(215, 30);
        btnCargarCsvCastigos.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        btnCargarCsvCastigos.BackColor = Color.FromArgb(30, 60, 120);
        btnCargarCsvCastigos.ForeColor = Color.White;
        btnCargarCsvCastigos.FlatStyle = FlatStyle.Flat;
        btnCargarCsvCastigos.FlatAppearance.BorderSize = 0;
        btnCargarCsvCastigos.Cursor    = Cursors.Hand;
        btnCargarCsvCastigos.Click    += btnCargarCsvCastigos_Click;

        lblEstadoCastigos.Location = new Point(232, 12);
        lblEstadoCastigos.Size     = new Size(650, 20);
        lblEstadoCastigos.Font     = new Font("Segoe UI", 9);
        lblEstadoCastigos.Text     = "Formato: CODIGO, CAD III, CAD IV, CAD V, reinc, NOTA, DESCRIPCION";

        pnlToolCastigos.Controls.AddRange(new Control[]
            { btnCargarCsvCastigos, lblEstadoCastigos });

        ConfigurarDgv(dgvCastigos);
        dgvCastigos.Dock = DockStyle.Fill;
        tabCastigos.Controls.Add(dgvCastigos);
        tabCastigos.Controls.Add(pnlToolCastigos);

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(1000, 620);
        MinimumSize         = new Size(800, 500);
        Text                = "Mantenimiento — Colegio Militar";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        Load               += FormMantenimiento_Load;

        Controls.Add(tabControl);
        Controls.Add(lblTitulo);

        tabControl.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCadetes).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvSupervisores).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvCastigos).EndInit();
        ResumeLayout(false);
    }

    private static void CrearBtnCsv(Button btn, string texto, int x, Color color)
    {
        btn.Text      = texto;
        btn.Location  = new Point(x, 26);
        btn.Size      = new Size(155, 28);
        btn.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        btn.BackColor = color;
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Cursor    = Cursors.Hand;
    }

    private static void ConfigurarDgv(DataGridView dgv)
    {
        dgv.ReadOnly              = true;
        dgv.AllowUserToAddRows    = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
        dgv.RowHeadersVisible     = false;
        dgv.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.Font                  = new Font("Segoe UI", 9);
        dgv.ColumnHeadersHeight   = 28;
        dgv.RowTemplate.Height    = 24;
        dgv.BackgroundColor       = Color.White;
        dgv.BorderStyle           = BorderStyle.None;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 255);
        dgv.EnableHeadersVisualStyles                 = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 60, 120);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9, FontStyle.Bold);
        dgv.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.None;
    }

    private Label          lblTitulo;
    private TabControl     tabControl;
    private TabPage        tabCadetes;
    private TabPage        tabSupervisores;
    private TabPage        tabCastigos;
    private Panel          pnlToolCadetes;
    private Button         btnCargarCsv3;
    private Button         btnCargarCsv4;
    private Button         btnCargarCsv5;
    private Label          lblEstadoCadetes;
    private DataGridView   dgvCadetes;
    private Panel          pnlToolSupervisores;
    private Button         btnCargarCsvSupervisores;
    private Label          lblEstadoSupervisores;
    private DataGridView   dgvSupervisores;
    private Panel          pnlToolCastigos;
    private Button         btnCargarCsvCastigos;
    private Label          lblEstadoCastigos;
    private DataGridView   dgvCastigos;
}
