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
        lblTitulo               = new Label();
        tabControl              = new TabControl();
        tabCadetes3             = new TabPage();
        tabCadetes4             = new TabPage();
        tabCadetes5             = new TabPage();
        tabSupervisores         = new TabPage();
        tabCastigos             = new TabPage();

        // Cadetes 3
        pnlTool3                = new Panel();
        btnCargarCsv3           = new Button();
        btnEditarCadetes3       = new Button();
        btnGuardarCadetes3      = new Button();
        btnCancelarCadetes3     = new Button();
        lblEstadoCadetes3       = new Label();
        dgvCadetes3             = new DataGridView();

        // Cadetes 4
        pnlTool4                = new Panel();
        btnCargarCsv4           = new Button();
        btnEditarCadetes4       = new Button();
        btnGuardarCadetes4      = new Button();
        btnCancelarCadetes4     = new Button();
        lblEstadoCadetes4       = new Label();
        dgvCadetes4             = new DataGridView();

        // Cadetes 5
        pnlTool5                = new Panel();
        btnCargarCsv5           = new Button();
        btnEditarCadetes5       = new Button();
        btnGuardarCadetes5      = new Button();
        btnCancelarCadetes5     = new Button();
        lblEstadoCadetes5       = new Label();
        dgvCadetes5             = new DataGridView();

        // Supervisores
        pnlToolSupervisores     = new Panel();
        btnCargarCsvSupervisores = new Button();
        btnEditarSupervisores   = new Button();
        btnGuardarSupervisores  = new Button();
        btnCancelarSupervisores = new Button();
        lblEstadoSupervisores   = new Label();
        dgvSupervisores         = new DataGridView();

        // Castigos
        pnlToolCastigos         = new Panel();
        btnCargarCsvCastigos    = new Button();
        btnEditarCastigos       = new Button();
        btnGuardarCastigos      = new Button();
        btnCancelarCastigos     = new Button();
        lblEstadoCastigos       = new Label();
        dgvCastigos             = new DataGridView();

        tabControl.SuspendLayout();
        foreach (var dgv in new[] { dgvCadetes3, dgvCadetes4, dgvCadetes5, dgvSupervisores, dgvCastigos })
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
        SuspendLayout();

        // Título
        lblTitulo.Text      = "MANTENIMIENTO DE DATOS";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblTitulo.Dock      = DockStyle.Top;
        lblTitulo.Height    = 36;
        lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
        lblTitulo.Padding   = new Padding(8, 0, 0, 0);

        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        tabControl.Controls.AddRange(new TabPage[]
            { tabCadetes3, tabCadetes4, tabCadetes5, tabSupervisores, tabCastigos });

        // Construir tabs cadetes
        ConfigurarTabCadetes(tabCadetes3, "  3° AÑO  ", pnlTool3, dgvCadetes3,
            btnCargarCsv3, "📂 CSV 3°", Color.FromArgb(30, 90, 160),
            btnEditarCadetes3, btnGuardarCadetes3, btnCancelarCadetes3, lblEstadoCadetes3,
            btnCargarCsv3_Click, btnEditarCadetes3_Click,
            btnGuardarCadetes3_Click, btnCancelarCadetes3_Click);

        ConfigurarTabCadetes(tabCadetes4, "  4° AÑO  ", pnlTool4, dgvCadetes4,
            btnCargarCsv4, "📂 CSV 4°", Color.FromArgb(20, 110, 60),
            btnEditarCadetes4, btnGuardarCadetes4, btnCancelarCadetes4, lblEstadoCadetes4,
            btnCargarCsv4_Click, btnEditarCadetes4_Click,
            btnGuardarCadetes4_Click, btnCancelarCadetes4_Click);

        ConfigurarTabCadetes(tabCadetes5, "  5° AÑO  ", pnlTool5, dgvCadetes5,
            btnCargarCsv5, "📂 CSV 5°", Color.FromArgb(140, 70, 10),
            btnEditarCadetes5, btnGuardarCadetes5, btnCancelarCadetes5, lblEstadoCadetes5,
            btnCargarCsv5_Click, btnEditarCadetes5_Click,
            btnGuardarCadetes5_Click, btnCancelarCadetes5_Click);

        // Tab Supervisores
        tabSupervisores.Text    = "  Supervisores  ";
        tabSupervisores.Padding = new Padding(4);
        ConfigurarToolPanel(pnlToolSupervisores,
            btnCargarCsvSupervisores, "📂 CSV Supervisores", Color.FromArgb(30, 60, 120),
            btnEditarSupervisores, btnGuardarSupervisores, btnCancelarSupervisores,
            lblEstadoSupervisores, "Formato: GRADO;APELLIDOS Y NOMBRES;DNI",
            btnCargarCsvSupervisores_Click, btnEditarSupervisores_Click,
            btnGuardarSupervisores_Click, btnCancelarSupervisores_Click);
        ConfigurarDgv(dgvSupervisores);
        dgvSupervisores.Dock = DockStyle.Fill;
        tabSupervisores.Controls.Add(dgvSupervisores);
        tabSupervisores.Controls.Add(pnlToolSupervisores);

        // Tab Castigos
        tabCastigos.Text    = "  Castigos  ";
        tabCastigos.Padding = new Padding(4);
        ConfigurarToolPanel(pnlToolCastigos,
            btnCargarCsvCastigos, "📂 CSV Castigos", Color.FromArgb(30, 60, 120),
            btnEditarCastigos, btnGuardarCastigos, btnCancelarCastigos,
            lblEstadoCastigos, "Formato: CODIGO;CAD III;CAD IV;CAD V;reinc;NOTA;DESCRIPCION",
            btnCargarCsvCastigos_Click, btnEditarCastigos_Click,
            btnGuardarCastigos_Click, btnCancelarCastigos_Click);
        ConfigurarDgv(dgvCastigos);
        dgvCastigos.Dock = DockStyle.Fill;
        tabCastigos.Controls.Add(dgvCastigos);
        tabCastigos.Controls.Add(pnlToolCastigos);

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(1050, 660);
        MinimumSize         = new Size(900, 550);
        Text                = "Mantenimiento — Colegio Militar";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        Load               += FormMantenimiento_Load;

        Controls.Add(tabControl);
        Controls.Add(lblTitulo);

        tabControl.ResumeLayout(false);
        foreach (var dgv in new[] { dgvCadetes3, dgvCadetes4, dgvCadetes5, dgvSupervisores, dgvCastigos })
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private void ConfigurarTabCadetes(TabPage tab, string titulo, Panel pnlTool,
        DataGridView dgv, Button btnCsv, string txtCsv, Color colorCsv,
        Button btnEditar, Button btnGuardar, Button btnCancelar, Label lblEstado,
        EventHandler hCsv, EventHandler hEditar, EventHandler hGuardar, EventHandler hCancelar)
    {
        tab.Text    = titulo;
        tab.Padding = new Padding(4);
        ConfigurarToolPanel(pnlTool, btnCsv, txtCsv, colorCsv,
            btnEditar, btnGuardar, btnCancelar, lblEstado,
            "Formato: N°;DNI;APELLIDOS Y NOMBRES;DIVISION",
            hCsv, hEditar, hGuardar, hCancelar);
        ConfigurarDgv(dgv);
        dgv.Dock = DockStyle.Fill;
        tab.Controls.Add(dgv);
        tab.Controls.Add(pnlTool);
    }

    private static void ConfigurarToolPanel(Panel pnl,
        Button btnCsv, string txtCsv, Color colorCsv,
        Button btnEditar, Button btnGuardar, Button btnCancelar, Label lblEstado,
        string hintText,
        EventHandler hCsv, EventHandler hEditar, EventHandler hGuardar, EventHandler hCancelar)
    {
        pnl.Dock      = DockStyle.Top;
        pnl.Height    = 46;
        pnl.BackColor = Color.FromArgb(240, 243, 250);

        SetupBtn(btnCsv,     txtCsv,        8,   colorCsv,                   140);
        SetupBtn(btnEditar,  "✏️ Editar",   155, Color.FromArgb(60, 90, 160), 90);
        SetupBtn(btnGuardar, "💾 Guardar",  252, Color.FromArgb(30, 120, 60), 90);
        SetupBtn(btnCancelar,"✕ Cancelar",  349, Color.FromArgb(160, 60, 60), 90);

        btnGuardar.Visible  = false;
        btnCancelar.Visible = false;

        lblEstado.Location = new Point(447, 13);
        lblEstado.Size     = new Size(560, 20);
        lblEstado.Font     = new Font("Segoe UI", 8.5f);
        lblEstado.Text     = hintText;
        lblEstado.ForeColor = Color.FromArgb(100, 100, 100);

        btnCsv.Click     += hCsv;
        btnEditar.Click  += hEditar;
        btnGuardar.Click += hGuardar;
        btnCancelar.Click += hCancelar;

        pnl.Controls.AddRange(new Control[] { btnCsv, btnEditar, btnGuardar, btnCancelar, lblEstado });
    }

    private static void SetupBtn(Button btn, string texto, int x, Color color, int ancho)
    {
        btn.Text      = texto;
        btn.Location  = new Point(x, 7);
        btn.Size      = new Size(ancho, 30);
        btn.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);
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

    // Campos
    private Label          lblTitulo;
    private TabControl     tabControl;
    private TabPage        tabCadetes3, tabCadetes4, tabCadetes5;
    private TabPage        tabSupervisores, tabCastigos;
    private Panel          pnlTool3, pnlTool4, pnlTool5;
    private Panel          pnlToolSupervisores, pnlToolCastigos;
    private Button         btnCargarCsv3, btnCargarCsv4, btnCargarCsv5;
    private Button         btnEditarCadetes3, btnGuardarCadetes3, btnCancelarCadetes3;
    private Button         btnEditarCadetes4, btnGuardarCadetes4, btnCancelarCadetes4;
    private Button         btnEditarCadetes5, btnGuardarCadetes5, btnCancelarCadetes5;
    private Button         btnAgregarCadetes3, btnEliminarCadetes3;
    private Button         btnAgregarCadetes4, btnEliminarCadetes4;
    private Button btnAgregarCadetes5, btnEliminarCadetes5;
    private Button btnAgregarSupervisores, btnEliminarSupervisores;
    private Button btnAgregarCastigos, btnEliminarCastigos;
    private Label          lblEstadoCadetes3, lblEstadoCadetes4, lblEstadoCadetes5;
    private DataGridView   dgvCadetes3, dgvCadetes4, dgvCadetes5;
    private Button         btnCargarCsvSupervisores;
    private Button         btnEditarSupervisores, btnGuardarSupervisores, btnCancelarSupervisores;
    private Label          lblEstadoSupervisores;
    private DataGridView   dgvSupervisores;
    private Button         btnCargarCsvCastigos;
    private Button         btnEditarCastigos, btnGuardarCastigos, btnCancelarCastigos;
    private Label          lblEstadoCastigos;
    private DataGridView   dgvCastigos;
}
