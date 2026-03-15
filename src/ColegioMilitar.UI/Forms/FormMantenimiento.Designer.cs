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
        // Declarar todos los controles
        lblTitulo           = new Label();
        tabControl          = new TabControl();
        tabCadetes3         = new TabPage();
        tabCadetes4         = new TabPage();
        tabCadetes5         = new TabPage();
        tabSupervisores     = new TabPage();
        tabCastigos         = new TabPage();

        pnlTool3            = new Panel();
        btnCargarCsv3       = new Button();
        btnEditarCadetes3   = new Button();
        btnAgregarCadetes3  = new Button();
        btnEliminarCadetes3 = new Button();
        btnGuardarCadetes3  = new Button();
        btnCancelarCadetes3 = new Button();
        lblEstadoCadetes3   = new Label();
        dgvCadetes3         = new DataGridView();

        pnlTool4            = new Panel();
        btnCargarCsv4       = new Button();
        btnEditarCadetes4   = new Button();
        btnAgregarCadetes4  = new Button();
        btnEliminarCadetes4 = new Button();
        btnGuardarCadetes4  = new Button();
        btnCancelarCadetes4 = new Button();
        lblEstadoCadetes4   = new Label();
        dgvCadetes4         = new DataGridView();

        pnlTool5            = new Panel();
        btnCargarCsv5       = new Button();
        btnEditarCadetes5   = new Button();
        btnAgregarCadetes5  = new Button();
        btnEliminarCadetes5 = new Button();
        btnGuardarCadetes5  = new Button();
        btnCancelarCadetes5 = new Button();
        lblEstadoCadetes5   = new Label();
        dgvCadetes5         = new DataGridView();

        pnlToolSupervisores     = new Panel();
        btnCargarCsvSupervisores = new Button();
        btnEditarSupervisores   = new Button();
        btnAgregarSupervisores  = new Button();
        btnEliminarSupervisores = new Button();
        btnGuardarSupervisores  = new Button();
        btnCancelarSupervisores = new Button();
        lblEstadoSupervisores   = new Label();
        dgvSupervisores         = new DataGridView();

        pnlToolCastigos     = new Panel();
        btnCargarCsvCastigos = new Button();
        btnEditarCastigos   = new Button();
        btnAgregarCastigos  = new Button();
        btnEliminarCastigos = new Button();
        btnGuardarCastigos  = new Button();
        btnCancelarCastigos = new Button();
        lblEstadoCastigos   = new Label();
        dgvCastigos         = new DataGridView();

        tabControl.SuspendLayout();
        foreach (var d in new[] { dgvCadetes3, dgvCadetes4, dgvCadetes5, dgvSupervisores, dgvCastigos })
            ((System.ComponentModel.ISupportInitialize)d).BeginInit();
        SuspendLayout();

        // ── Título ────────────────────────────────────────────────────────
        lblTitulo.Text      = "MANTENIMIENTO DE DATOS";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblTitulo.Dock      = DockStyle.Top;
        lblTitulo.Height    = 36;
        lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
        lblTitulo.Padding   = new Padding(8, 0, 0, 0);

        // ── TabControl ────────────────────────────────────────────────────
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        tabControl.Controls.AddRange(new TabPage[]
            { tabCadetes3, tabCadetes4, tabCadetes5, tabSupervisores, tabCastigos });

        // ════════════════════════════════════════════════════════════════
        //  3° AÑO
        // ════════════════════════════════════════════════════════════════
        tabCadetes3.Text    = "  3° AÑO  ";
        tabCadetes3.Padding = new Padding(4);

        ConfigBtn(btnCargarCsv3,       "📂 CSV 3°",    8,   Color.FromArgb(30, 90, 160),  130);
        ConfigBtn(btnEditarCadetes3,   "✏️ Editar",   145, Color.FromArgb(60, 90, 160),   85);
        ConfigBtn(btnAgregarCadetes3,  "➕ Agregar",  236, Color.FromArgb(30, 120, 60),    85);
        ConfigBtn(btnEliminarCadetes3, "🗑 Eliminar", 327, Color.FromArgb(160, 60, 60),    85);
        ConfigBtn(btnGuardarCadetes3,  "💾 Guardar",  418, Color.FromArgb(30, 100, 50),    85);
        ConfigBtn(btnCancelarCadetes3, "✕ Cancelar",  509, Color.FromArgb(120, 60, 60),    85);

        btnAgregarCadetes3.Visible  = false;
        btnEliminarCadetes3.Visible = false;
        btnGuardarCadetes3.Visible  = false;
        btnCancelarCadetes3.Visible = false;

        btnCargarCsv3.Click       += btnCargarCsv3_Click;
        btnEditarCadetes3.Click   += btnEditarCadetes3_Click;
        btnAgregarCadetes3.Click  += btnAgregarCadetes3_Click;
        btnEliminarCadetes3.Click += btnEliminarCadetes3_Click;
        btnGuardarCadetes3.Click  += btnGuardarCadetes3_Click;
        btnCancelarCadetes3.Click += btnCancelarCadetes3_Click;

        ConfigLabel(lblEstadoCadetes3, "Formato: N°;DNI;APELLIDOS Y NOMBRES;DIVISION");

        pnlTool3.Dock      = DockStyle.Top;
        pnlTool3.Height    = 46;
        pnlTool3.BackColor = Color.FromArgb(240, 243, 250);
        pnlTool3.Controls.AddRange(new Control[]
            { btnCargarCsv3, btnEditarCadetes3, btnAgregarCadetes3,
              btnEliminarCadetes3, btnGuardarCadetes3, btnCancelarCadetes3, lblEstadoCadetes3 });

        ConfigurarDgv(dgvCadetes3);
        dgvCadetes3.Dock = DockStyle.Fill;
        tabCadetes3.Controls.Add(dgvCadetes3);
        tabCadetes3.Controls.Add(pnlTool3);

        // ════════════════════════════════════════════════════════════════
        //  4° AÑO
        // ════════════════════════════════════════════════════════════════
        tabCadetes4.Text    = "  4° AÑO  ";
        tabCadetes4.Padding = new Padding(4);

        ConfigBtn(btnCargarCsv4,       "📂 CSV 4°",    8,   Color.FromArgb(20, 110, 60),   130);
        ConfigBtn(btnEditarCadetes4,   "✏️ Editar",   145, Color.FromArgb(60, 90, 160),    85);
        ConfigBtn(btnAgregarCadetes4,  "➕ Agregar",  236, Color.FromArgb(30, 120, 60),     85);
        ConfigBtn(btnEliminarCadetes4, "🗑 Eliminar", 327, Color.FromArgb(160, 60, 60),     85);
        ConfigBtn(btnGuardarCadetes4,  "💾 Guardar",  418, Color.FromArgb(30, 100, 50),     85);
        ConfigBtn(btnCancelarCadetes4, "✕ Cancelar",  509, Color.FromArgb(120, 60, 60),     85);

        btnAgregarCadetes4.Visible  = false;
        btnEliminarCadetes4.Visible = false;
        btnGuardarCadetes4.Visible  = false;
        btnCancelarCadetes4.Visible = false;

        btnCargarCsv4.Click       += btnCargarCsv4_Click;
        btnEditarCadetes4.Click   += btnEditarCadetes4_Click;
        btnAgregarCadetes4.Click  += btnAgregarCadetes4_Click;
        btnEliminarCadetes4.Click += btnEliminarCadetes4_Click;
        btnGuardarCadetes4.Click  += btnGuardarCadetes4_Click;
        btnCancelarCadetes4.Click += btnCancelarCadetes4_Click;

        ConfigLabel(lblEstadoCadetes4, "Formato: N°;DNI;APELLIDOS Y NOMBRES;DIVISION");

        pnlTool4.Dock      = DockStyle.Top;
        pnlTool4.Height    = 46;
        pnlTool4.BackColor = Color.FromArgb(240, 243, 250);
        pnlTool4.Controls.AddRange(new Control[]
            { btnCargarCsv4, btnEditarCadetes4, btnAgregarCadetes4,
              btnEliminarCadetes4, btnGuardarCadetes4, btnCancelarCadetes4, lblEstadoCadetes4 });

        ConfigurarDgv(dgvCadetes4);
        dgvCadetes4.Dock = DockStyle.Fill;
        tabCadetes4.Controls.Add(dgvCadetes4);
        tabCadetes4.Controls.Add(pnlTool4);

        // ════════════════════════════════════════════════════════════════
        //  5° AÑO
        // ════════════════════════════════════════════════════════════════
        tabCadetes5.Text    = "  5° AÑO  ";
        tabCadetes5.Padding = new Padding(4);

        ConfigBtn(btnCargarCsv5,       "📂 CSV 5°",    8,   Color.FromArgb(140, 70, 10),   130);
        ConfigBtn(btnEditarCadetes5,   "✏️ Editar",   145, Color.FromArgb(60, 90, 160),    85);
        ConfigBtn(btnAgregarCadetes5,  "➕ Agregar",  236, Color.FromArgb(30, 120, 60),     85);
        ConfigBtn(btnEliminarCadetes5, "🗑 Eliminar", 327, Color.FromArgb(160, 60, 60),     85);
        ConfigBtn(btnGuardarCadetes5,  "💾 Guardar",  418, Color.FromArgb(30, 100, 50),     85);
        ConfigBtn(btnCancelarCadetes5, "✕ Cancelar",  509, Color.FromArgb(120, 60, 60),     85);

        btnAgregarCadetes5.Visible  = false;
        btnEliminarCadetes5.Visible = false;
        btnGuardarCadetes5.Visible  = false;
        btnCancelarCadetes5.Visible = false;

        btnCargarCsv5.Click       += btnCargarCsv5_Click;
        btnEditarCadetes5.Click   += btnEditarCadetes5_Click;
        btnAgregarCadetes5.Click  += btnAgregarCadetes5_Click;
        btnEliminarCadetes5.Click += btnEliminarCadetes5_Click;
        btnGuardarCadetes5.Click  += btnGuardarCadetes5_Click;
        btnCancelarCadetes5.Click += btnCancelarCadetes5_Click;

        ConfigLabel(lblEstadoCadetes5, "Formato: N°;DNI;APELLIDOS Y NOMBRES;DIVISION");

        pnlTool5.Dock      = DockStyle.Top;
        pnlTool5.Height    = 46;
        pnlTool5.BackColor = Color.FromArgb(240, 243, 250);
        pnlTool5.Controls.AddRange(new Control[]
            { btnCargarCsv5, btnEditarCadetes5, btnAgregarCadetes5,
              btnEliminarCadetes5, btnGuardarCadetes5, btnCancelarCadetes5, lblEstadoCadetes5 });

        ConfigurarDgv(dgvCadetes5);
        dgvCadetes5.Dock = DockStyle.Fill;
        tabCadetes5.Controls.Add(dgvCadetes5);
        tabCadetes5.Controls.Add(pnlTool5);

        // ════════════════════════════════════════════════════════════════
        //  SUPERVISORES
        // ════════════════════════════════════════════════════════════════
        tabSupervisores.Text    = "  Supervisores  ";
        tabSupervisores.Padding = new Padding(4);

        ConfigBtn(btnCargarCsvSupervisores, "📂 CSV Supervisores", 8,   Color.FromArgb(30, 60, 120), 175);
        ConfigBtn(btnEditarSupervisores,    "✏️ Editar",          190, Color.FromArgb(60, 90, 160),  85);
        ConfigBtn(btnAgregarSupervisores,   "➕ Agregar",         281, Color.FromArgb(30, 120, 60),   85);
        ConfigBtn(btnEliminarSupervisores,  "🗑 Eliminar",        372, Color.FromArgb(160, 60, 60),   85);
        ConfigBtn(btnGuardarSupervisores,   "💾 Guardar",         463, Color.FromArgb(30, 100, 50),   85);
        ConfigBtn(btnCancelarSupervisores,  "✕ Cancelar",         554, Color.FromArgb(120, 60, 60),   85);

        btnAgregarSupervisores.Visible  = false;
        btnEliminarSupervisores.Visible = false;
        btnGuardarSupervisores.Visible  = false;
        btnCancelarSupervisores.Visible = false;

        btnCargarCsvSupervisores.Click += btnCargarCsvSupervisores_Click;
        btnEditarSupervisores.Click    += btnEditarSupervisores_Click;
        btnAgregarSupervisores.Click   += btnAgregarSupervisores_Click;
        btnEliminarSupervisores.Click  += btnEliminarSupervisores_Click;
        btnGuardarSupervisores.Click   += btnGuardarSupervisores_Click;
        btnCancelarSupervisores.Click  += btnCancelarSupervisores_Click;

        ConfigLabel(lblEstadoSupervisores, "Formato: GRADO;APELLIDOS Y NOMBRES;DNI");

        pnlToolSupervisores.Dock      = DockStyle.Top;
        pnlToolSupervisores.Height    = 46;
        pnlToolSupervisores.BackColor = Color.FromArgb(240, 243, 250);
        pnlToolSupervisores.Controls.AddRange(new Control[]
            { btnCargarCsvSupervisores, btnEditarSupervisores, btnAgregarSupervisores,
              btnEliminarSupervisores, btnGuardarSupervisores, btnCancelarSupervisores,
              lblEstadoSupervisores });

        ConfigurarDgv(dgvSupervisores);
        dgvSupervisores.Dock = DockStyle.Fill;
        tabSupervisores.Controls.Add(dgvSupervisores);
        tabSupervisores.Controls.Add(pnlToolSupervisores);

        // ════════════════════════════════════════════════════════════════
        //  CASTIGOS
        // ════════════════════════════════════════════════════════════════
        tabCastigos.Text    = "  Castigos  ";
        tabCastigos.Padding = new Padding(4);

        ConfigBtn(btnCargarCsvCastigos, "📂 CSV Castigos", 8,   Color.FromArgb(30, 60, 120), 160);
        ConfigBtn(btnEditarCastigos,    "✏️ Editar",       175, Color.FromArgb(60, 90, 160),  85);
        ConfigBtn(btnAgregarCastigos,   "➕ Agregar",      266, Color.FromArgb(30, 120, 60),   85);
        ConfigBtn(btnEliminarCastigos,  "🗑 Eliminar",     357, Color.FromArgb(160, 60, 60),   85);
        ConfigBtn(btnGuardarCastigos,   "💾 Guardar",      448, Color.FromArgb(30, 100, 50),   85);
        ConfigBtn(btnCancelarCastigos,  "✕ Cancelar",      539, Color.FromArgb(120, 60, 60),   85);

        btnAgregarCastigos.Visible  = false;
        btnEliminarCastigos.Visible = false;
        btnGuardarCastigos.Visible  = false;
        btnCancelarCastigos.Visible = false;

        btnCargarCsvCastigos.Click += btnCargarCsvCastigos_Click;
        btnEditarCastigos.Click    += btnEditarCastigos_Click;
        btnAgregarCastigos.Click   += btnAgregarCastigos_Click;
        btnEliminarCastigos.Click  += btnEliminarCastigos_Click;
        btnGuardarCastigos.Click   += btnGuardarCastigos_Click;
        btnCancelarCastigos.Click  += btnCancelarCastigos_Click;

        ConfigLabel(lblEstadoCastigos, "Formato: CODIGO;CAD III;CAD IV;CAD V;reinc;NOTA;DESCRIPCION");

        pnlToolCastigos.Dock      = DockStyle.Top;
        pnlToolCastigos.Height    = 46;
        pnlToolCastigos.BackColor = Color.FromArgb(240, 243, 250);
        pnlToolCastigos.Controls.AddRange(new Control[]
            { btnCargarCsvCastigos, btnEditarCastigos, btnAgregarCastigos,
              btnEliminarCastigos, btnGuardarCastigos, btnCancelarCastigos,
              lblEstadoCastigos });

        ConfigurarDgv(dgvCastigos);
        dgvCastigos.Dock = DockStyle.Fill;
        tabCastigos.Controls.Add(dgvCastigos);
        tabCastigos.Controls.Add(pnlToolCastigos);

        // ── Form ──────────────────────────────────────────────────────────
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
        foreach (var d in new[] { dgvCadetes3, dgvCadetes4, dgvCadetes5, dgvSupervisores, dgvCastigos })
            ((System.ComponentModel.ISupportInitialize)d).EndInit();
        ResumeLayout(false);
    }

    private static void ConfigBtn(Button btn, string texto, int x, Color color, int ancho)
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

    private static void ConfigLabel(Label lbl, string hint)
    {
        lbl.Location  = new Point(640, 13);
        lbl.Size      = new Size(380, 20);
        lbl.Font      = new Font("Segoe UI", 8.5f);
        lbl.Text      = hint;
        lbl.ForeColor = Color.FromArgb(100, 100, 100);
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
        dgv.BackgroundColor       = Color.White;
        dgv.BorderStyle           = BorderStyle.None;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 255);
        dgv.EnableHeadersVisualStyles                 = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 60, 120);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9, FontStyle.Bold);
        dgv.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.None;
    }

    // ── Campos ────────────────────────────────────────────────────────────
    private Label        lblTitulo;
    private TabControl   tabControl;
    private TabPage      tabCadetes3, tabCadetes4, tabCadetes5, tabSupervisores, tabCastigos;
    private Panel        pnlTool3, pnlTool4, pnlTool5, pnlToolSupervisores, pnlToolCastigos;
    private Button       btnCargarCsv3, btnEditarCadetes3, btnAgregarCadetes3, btnEliminarCadetes3, btnGuardarCadetes3, btnCancelarCadetes3;
    private Button       btnCargarCsv4, btnEditarCadetes4, btnAgregarCadetes4, btnEliminarCadetes4, btnGuardarCadetes4, btnCancelarCadetes4;
    private Button       btnCargarCsv5, btnEditarCadetes5, btnAgregarCadetes5, btnEliminarCadetes5, btnGuardarCadetes5, btnCancelarCadetes5;
    private Label        lblEstadoCadetes3, lblEstadoCadetes4, lblEstadoCadetes5;
    private DataGridView dgvCadetes3, dgvCadetes4, dgvCadetes5;
    private Button       btnCargarCsvSupervisores, btnEditarSupervisores, btnAgregarSupervisores, btnEliminarSupervisores, btnGuardarSupervisores, btnCancelarSupervisores;
    private Label        lblEstadoSupervisores;
    private DataGridView dgvSupervisores;
    private Button       btnCargarCsvCastigos, btnEditarCastigos, btnAgregarCastigos, btnEliminarCastigos, btnGuardarCastigos, btnCancelarCastigos;
    private Label        lblEstadoCastigos;
    private DataGridView dgvCastigos;
}
