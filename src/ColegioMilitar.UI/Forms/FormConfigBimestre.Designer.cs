namespace ColegioMilitar.UI.Forms;

partial class FormConfigBimestre
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo    = new Label();
        grpFiltro    = new GroupBox();
        lblBimestre  = new Label();
        nudBimestre  = new NumericUpDown();
        lblAño       = new Label();
        nudAño       = new NumericUpDown();
        grpNueva     = new GroupBox();
        lblNombre    = new Label();
        txtNombre    = new TextBox();
        lblInicio    = new Label();
        dtpInicio    = new DateTimePicker();
        lblFin       = new Label();
        dtpFin       = new DateTimePicker();
        btnAgregar   = new Button();
        dgvSemanas   = new DataGridView();
        pnlBotones   = new Panel();
        btnEliminar  = new Button();
        btnCerrar    = new Button();
        lblEstado    = new Label();

        grpFiltro.SuspendLayout();
        grpNueva.SuspendLayout();
        pnlBotones.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSemanas).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudBimestre).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAño).BeginInit();
        SuspendLayout();

        // ── Título ────────────────────────────────────────────────────────
        lblTitulo.Text      = "CONFIGURACIÓN DE SEMANAS";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblTitulo.Dock      = DockStyle.Top;
        lblTitulo.Height    = 36;
        lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
        lblTitulo.Padding   = new Padding(8, 0, 0, 0);

        // ── Filtro bimestre/año ───────────────────────────────────────────
        grpFiltro.Text     = "Bimestre";
        grpFiltro.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpFiltro.Location = new Point(10, 44);
        grpFiltro.Size     = new Size(340, 55);

        lblBimestre.Text     = "Bimestre:";
        lblBimestre.Location = new Point(10, 22);
        lblBimestre.Size     = new Size(65, 20);
        lblBimestre.Font     = new Font("Segoe UI", 9);

        nudBimestre.Location = new Point(78, 19);
        nudBimestre.Size     = new Size(55, 25);
        nudBimestre.Minimum  = 1;
        nudBimestre.Maximum  = 6;
        nudBimestre.ValueChanged += nudBimestre_ValueChanged;

        lblAño.Text     = "Año:";
        lblAño.Location = new Point(150, 22);
        lblAño.Size     = new Size(35, 20);
        lblAño.Font     = new Font("Segoe UI", 9);

        nudAño.Location = new Point(188, 19);
        nudAño.Size     = new Size(80, 25);
        nudAño.Minimum  = 2020;
        nudAño.Maximum  = 2040;
        nudAño.ValueChanged += nudAño_ValueChanged;

        grpFiltro.Controls.AddRange(new Control[]
            { lblBimestre, nudBimestre, lblAño, nudAño });

        // ── Agregar nueva semana ──────────────────────────────────────────
        grpNueva.Text     = "Agregar semana";
        grpNueva.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpNueva.Location = new Point(10, 108);
        grpNueva.Size     = new Size(560, 80);

        lblNombre.Text     = "Nombre (fecha viernes):";
        lblNombre.Location = new Point(10, 24);
        lblNombre.Size     = new Size(160, 20);
        lblNombre.Font     = new Font("Segoe UI", 9);

        txtNombre.Location    = new Point(173, 21);
        txtNombre.Size        = new Size(100, 25);
        txtNombre.Font        = new Font("Segoe UI", 10);
        txtNombre.MaxLength   = 20;
        txtNombre.CharacterCasing = CharacterCasing.Upper;

        lblInicio.Text     = "Inicio:";
        lblInicio.Location = new Point(283, 24);
        lblInicio.Size     = new Size(45, 20);
        lblInicio.Font     = new Font("Segoe UI", 9);

        dtpInicio.Location = new Point(330, 21);
        dtpInicio.Size     = new Size(105, 25);
        dtpInicio.Format   = DateTimePickerFormat.Short;

        lblFin.Text     = "Fin:";
        lblFin.Location = new Point(445, 24);
        lblFin.Size     = new Size(30, 20);
        lblFin.Font     = new Font("Segoe UI", 9);

        dtpFin.Location = new Point(478, 21);
        dtpFin.Size     = new Size(75, 25);
        dtpFin.Format   = DateTimePickerFormat.Short;

        btnAgregar.Text      = "➕ Agregar";
        btnAgregar.Location  = new Point(10, 48);
        btnAgregar.Size      = new Size(100, 28);
        btnAgregar.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        btnAgregar.BackColor = Color.FromArgb(30, 120, 60);
        btnAgregar.ForeColor = Color.White;
        btnAgregar.FlatStyle = FlatStyle.Flat;
        btnAgregar.FlatAppearance.BorderSize = 0;
        btnAgregar.Cursor    = Cursors.Hand;
        btnAgregar.Click    += btnAgregar_Click;

        grpNueva.Controls.AddRange(new Control[]
            { lblNombre, txtNombre, lblInicio, dtpInicio, lblFin, dtpFin, btnAgregar });

        // ── Grilla ────────────────────────────────────────────────────────
        dgvSemanas.Location              = new Point(10, 200);
        dgvSemanas.Size                  = new Size(560, 260);
        dgvSemanas.Anchor                = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dgvSemanas.ReadOnly              = true;
        dgvSemanas.AllowUserToAddRows    = false;
        dgvSemanas.AllowUserToDeleteRows = false;
        dgvSemanas.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
        dgvSemanas.RowHeadersVisible     = false;
        dgvSemanas.Font                  = new Font("Segoe UI", 9);
        dgvSemanas.ColumnHeadersHeight   = 28;
        dgvSemanas.RowTemplate.Height    = 26;
        dgvSemanas.BackgroundColor       = Color.White;
        dgvSemanas.BorderStyle           = BorderStyle.FixedSingle;
        dgvSemanas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 255);
        dgvSemanas.EnableHeadersVisualStyles                 = false;
        dgvSemanas.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 60, 120);
        dgvSemanas.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        dgvSemanas.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9, FontStyle.Bold);
        dgvSemanas.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.None;

        // ── Panel botones de acción sobre grilla ─────────────────────────
        pnlBotones.Location  = new Point(10, 468);
        pnlBotones.Size      = new Size(560, 38);
        pnlBotones.Anchor    = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        btnEliminar.Text      = "🗑  Eliminar seleccionada";
        btnEliminar.Location  = new Point(0, 4);
        btnEliminar.Size      = new Size(190, 30);
        btnEliminar.Font      = new Font("Segoe UI", 9);
        btnEliminar.BackColor = Color.FromArgb(180, 40, 40);
        btnEliminar.ForeColor = Color.White;
        btnEliminar.FlatStyle = FlatStyle.Flat;
        btnEliminar.FlatAppearance.BorderSize = 0;
        btnEliminar.Cursor    = Cursors.Hand;
        btnEliminar.Click    += btnEliminar_Click;

        btnCerrar.Text      = "🔒  Cerrar semana";
        btnCerrar.Location  = new Point(198, 4);
        btnCerrar.Size      = new Size(160, 30);
        btnCerrar.Font      = new Font("Segoe UI", 9);
        btnCerrar.BackColor = Color.FromArgb(80, 80, 80);
        btnCerrar.ForeColor = Color.White;
        btnCerrar.FlatStyle = FlatStyle.Flat;
        btnCerrar.FlatAppearance.BorderSize = 0;
        btnCerrar.Cursor    = Cursors.Hand;
        btnCerrar.Click    += btnCerrar_Click;

        pnlBotones.Controls.AddRange(new Control[] { btnEliminar, btnCerrar });

        lblEstado.Location  = new Point(10, 512);
        lblEstado.Size      = new Size(560, 20);
        lblEstado.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        lblEstado.Anchor    = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        // ── Form ──────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(580, 540);
        MinimumSize         = new Size(580, 540);
        FormBorderStyle     = FormBorderStyle.FixedSingle;
        MaximizeBox         = false;
        Text                = "Configurar Semanas del Bimestre";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        Load               += FormConfigBimestre_Load;

        Controls.AddRange(new Control[]
        {
            lblTitulo, grpFiltro, grpNueva, dgvSemanas, pnlBotones, lblEstado
        });

        grpFiltro.ResumeLayout(false);
        grpNueva.ResumeLayout(false);
        pnlBotones.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSemanas).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudBimestre).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAño).EndInit();
        ResumeLayout(false);
    }

    private Label            lblTitulo;
    private GroupBox         grpFiltro;
    private Label            lblBimestre;
    private NumericUpDown    nudBimestre;
    private Label            lblAño;
    private NumericUpDown    nudAño;
    private GroupBox         grpNueva;
    private Label            lblNombre;
    private TextBox          txtNombre;
    private Label            lblInicio;
    private DateTimePicker   dtpInicio;
    private Label            lblFin;
    private DateTimePicker   dtpFin;
    private Button           btnAgregar;
    private DataGridView     dgvSemanas;
    private Panel            pnlBotones;
    private Button           btnEliminar;
    private Button           btnCerrar;
    private Label            lblEstado;
}
