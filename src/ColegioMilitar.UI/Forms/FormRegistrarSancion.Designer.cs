namespace ColegioMilitar.UI.Forms;

partial class FormRegistrarSancion
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitulo         = new Label();
        grpCadete         = new GroupBox();
        lblDNICadete      = new Label();
        txtDNICadete      = new TextBox();
        lblCadeteInfo     = new Label();
        grpCastigo        = new GroupBox();
        lblCodigoCastigo  = new Label();
        txtCodigoCastigo  = new TextBox();
        lblCastigoInfo    = new Label();
        grpSupervisor     = new GroupBox();
        lblDNISupervisor  = new Label();
        txtDNISupervisor  = new TextBox();
        cmbSupervisor     = new ComboBox();
        lblSupervisorInfo = new Label();
        grpFecha          = new GroupBox();
        lblFecha          = new Label();
        dtpFecha          = new DateTimePicker();
        lblHora           = new Label();
        dtpHora           = new DateTimePicker();
        grpObs            = new GroupBox();
        txtObservaciones  = new TextBox();
        btnGuardar        = new Button();
        btnLimpiarTodo    = new Button();
        lblEstado         = new Label();

        grpCadete.SuspendLayout();
        grpCastigo.SuspendLayout();
        grpSupervisor.SuspendLayout();
        grpFecha.SuspendLayout();
        grpObs.SuspendLayout();
        SuspendLayout();

        // ── Título ───────────────────────────────────────────────────────────
        lblTitulo.Text      = "INSERTAR SANCIÓN";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.FromArgb(30, 60, 120);
        lblTitulo.Location  = new Point(15, 12);
        lblTitulo.Size      = new Size(370, 28);

        // ── Cadete ───────────────────────────────────────────────────────────
        grpCadete.Text     = "1. Cadete";
        grpCadete.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpCadete.Location = new Point(15, 48);
        grpCadete.Size     = new Size(390, 68);

        lblDNICadete.Text     = "DNI:";
        lblDNICadete.Location = new Point(10, 24);
        lblDNICadete.Size     = new Size(33, 20);
        lblDNICadete.Font     = new Font("Segoe UI", 9);

        txtDNICadete.Location  = new Point(46, 21);
        txtDNICadete.Size      = new Size(140, 25);
        txtDNICadete.Font      = new Font("Segoe UI", 10);
        txtDNICadete.MaxLength = 20;

        lblCadeteInfo.Location = new Point(10, 48);
        lblCadeteInfo.Size     = new Size(370, 16);
        lblCadeteInfo.Font     = new Font("Segoe UI", 8.5f);

        grpCadete.Controls.AddRange(new Control[] { lblDNICadete, txtDNICadete, lblCadeteInfo });

        // ── Castigo ──────────────────────────────────────────────────────────
        grpCastigo.Text     = "2. Código de Castigo  (Enter para buscar)";
        grpCastigo.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpCastigo.Location = new Point(15, 125);
        grpCastigo.Size     = new Size(390, 68);

        lblCodigoCastigo.Text     = "Código:";
        lblCodigoCastigo.Location = new Point(10, 24);
        lblCodigoCastigo.Size     = new Size(52, 20);
        lblCodigoCastigo.Font     = new Font("Segoe UI", 9);

        txtCodigoCastigo.Location  = new Point(65, 21);
        txtCodigoCastigo.Size      = new Size(110, 25);
        txtCodigoCastigo.Font      = new Font("Segoe UI", 10);
        txtCodigoCastigo.MaxLength = 10;
        txtCodigoCastigo.Enabled   = false;

        lblCastigoInfo.Location = new Point(10, 48);
        lblCastigoInfo.Size     = new Size(370, 16);
        lblCastigoInfo.Font     = new Font("Segoe UI", 8.5f);

        grpCastigo.Controls.AddRange(new Control[] { lblCodigoCastigo, txtCodigoCastigo, lblCastigoInfo });

        // ── Supervisor ───────────────────────────────────────────────────────
        grpSupervisor.Text     = "3. Supervisor";
        grpSupervisor.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpSupervisor.Location = new Point(15, 202);
        grpSupervisor.Size     = new Size(390, 82);

        lblDNISupervisor.Text     = "DNI:";
        lblDNISupervisor.Location = new Point(10, 24);
        lblDNISupervisor.Size     = new Size(33, 20);
        lblDNISupervisor.Font     = new Font("Segoe UI", 9);

        txtDNISupervisor.Location  = new Point(46, 21);
        txtDNISupervisor.Size      = new Size(110, 25);
        txtDNISupervisor.Font      = new Font("Segoe UI", 10);
        txtDNISupervisor.MaxLength = 20;

        cmbSupervisor.Location      = new Point(165, 21);
        cmbSupervisor.Size          = new Size(215, 25);
        cmbSupervisor.Font          = new Font("Segoe UI", 9);
        cmbSupervisor.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbSupervisor.DropDownWidth = 380;

        lblSupervisorInfo.Location = new Point(10, 54);
        lblSupervisorInfo.Size     = new Size(370, 16);
        lblSupervisorInfo.Font     = new Font("Segoe UI", 8.5f);

        grpSupervisor.Controls.AddRange(new Control[]
            { lblDNISupervisor, txtDNISupervisor, cmbSupervisor, lblSupervisorInfo });

        // ── Fecha / Hora ─────────────────────────────────────────────────────
        grpFecha.Text     = "4. Fecha y Hora";
        grpFecha.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpFecha.Location = new Point(15, 293);
        grpFecha.Size     = new Size(390, 55);

        lblFecha.Text     = "Fecha:";
        lblFecha.Location = new Point(10, 24);
        lblFecha.Size     = new Size(44, 20);
        lblFecha.Font     = new Font("Segoe UI", 9);

        dtpFecha.Location = new Point(57, 21);
        dtpFecha.Size     = new Size(130, 25);
        dtpFecha.Format   = DateTimePickerFormat.Short;

        lblHora.Text     = "Hora:";
        lblHora.Location = new Point(202, 24);
        lblHora.Size     = new Size(36, 20);
        lblHora.Font     = new Font("Segoe UI", 9);

        dtpHora.Location   = new Point(241, 21);
        dtpHora.Size       = new Size(100, 25);
        dtpHora.Format     = DateTimePickerFormat.Time;
        dtpHora.ShowUpDown = true;

        grpFecha.Controls.AddRange(new Control[] { lblFecha, dtpFecha, lblHora, dtpHora });

        // ── Observaciones ────────────────────────────────────────────────────
        grpObs.Text     = "5. Observaciones  (Enter para guardar)";
        grpObs.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpObs.Location = new Point(15, 358);
        grpObs.Size     = new Size(390, 52);

        txtObservaciones.Location  = new Point(10, 22);
        txtObservaciones.Size      = new Size(370, 25);
        txtObservaciones.Font      = new Font("Segoe UI", 9);
        txtObservaciones.MaxLength = 300;

        grpObs.Controls.Add(txtObservaciones);

        // ── Botones ──────────────────────────────────────────────────────────
        btnGuardar.Text      = "💾  GUARDAR  (F5)";
        btnGuardar.Location  = new Point(15, 422);
        btnGuardar.Size      = new Size(200, 40);
        btnGuardar.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
        btnGuardar.BackColor = Color.FromArgb(30, 120, 60);
        btnGuardar.ForeColor = Color.White;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.Cursor    = Cursors.Hand;
        btnGuardar.Click    += btnGuardar_Click;

        btnLimpiarTodo.Text      = "🗑  Limpiar todo";
        btnLimpiarTodo.Location  = new Point(225, 422);
        btnLimpiarTodo.Size      = new Size(130, 40);
        btnLimpiarTodo.Font      = new Font("Segoe UI", 9);
        btnLimpiarTodo.FlatStyle = FlatStyle.Flat;
        btnLimpiarTodo.Cursor    = Cursors.Hand;
        btnLimpiarTodo.Click    += btnLimpiarTodo_Click;

        lblEstado.Location  = new Point(15, 470);
        lblEstado.Size      = new Size(390, 20);
        lblEstado.Font      = new Font("Segoe UI", 9, FontStyle.Bold);

        // ── Form ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(420, 500);
        FormBorderStyle     = FormBorderStyle.FixedSingle;
        MaximizeBox         = false;
        Text                = "Insertar Sanción";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        TopMost             = true;
        Load               += FormRegistrarSancion_Load;

        Controls.AddRange(new Control[]
        {
            lblTitulo, grpCadete, grpCastigo, grpSupervisor,
            grpFecha, grpObs, btnGuardar, btnLimpiarTodo, lblEstado
        });

        grpCadete.ResumeLayout(false);
        grpCastigo.ResumeLayout(false);
        grpSupervisor.ResumeLayout(false);
        grpFecha.ResumeLayout(false);
        grpObs.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label          lblTitulo;
    private GroupBox       grpCadete;
    private Label          lblDNICadete;
    private TextBox        txtDNICadete;
    private Label          lblCadeteInfo;
    private GroupBox       grpCastigo;
    private Label          lblCodigoCastigo;
    private TextBox        txtCodigoCastigo;
    private Label          lblCastigoInfo;
    private GroupBox       grpSupervisor;
    private Label          lblDNISupervisor;
    private TextBox        txtDNISupervisor;
    private ComboBox       cmbSupervisor;
    private Label          lblSupervisorInfo;
    private GroupBox       grpFecha;
    private Label          lblFecha;
    private DateTimePicker dtpFecha;
    private Label          lblHora;
    private DateTimePicker dtpHora;
    private GroupBox       grpObs;
    private TextBox        txtObservaciones;
    private Button         btnGuardar;
    private Button         btnLimpiarTodo;
    private Label          lblEstado;
}
