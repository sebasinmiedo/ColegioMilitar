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
        // ── Controles ────────────────────────────────────────────────────────
        lblTitulo          = new Label();

        // Cadete
        grpCadete          = new GroupBox();
        lblDNICadete       = new Label();
        txtDNICadete       = new TextBox();
        lblCadeteInfo      = new Label();

        // Castigo
        grpCastigo         = new GroupBox();
        lblCodigoCastigo   = new Label();
        txtCodigoCastigo   = new TextBox();
        lblCastigoInfo     = new Label();

        // Supervisor
        grpSupervisor      = new GroupBox();
        lblDNISupervisor   = new Label();
        txtDNISupervisor   = new TextBox();
        cmbSupervisor      = new ComboBox();
        lblSupervisorInfo  = new Label();

        // Fecha/Hora/Semana
        grpFecha           = new GroupBox();
        lblFecha           = new Label();
        dtpFecha           = new DateTimePicker();
        lblHora            = new Label();
        dtpHora            = new DateTimePicker();
        chkSemanaManual    = new CheckBox();
        pnlSemanaManual    = new Panel();
        lblSemana          = new Label();
        nudSemana          = new NumericUpDown();

        // Observaciones
        grpObs             = new GroupBox();
        // lblObservaciones   = new Label();
        txtObservaciones   = new TextBox();

        // Botones y estado
        btnGuardar         = new Button();
        btnLimpiarTodo     = new Button();
        btnRefrescar       = new Button();
        lblEstado          = new Label();

        // Grilla recientes
        grpRecientes       = new GroupBox();
        dgvRecientes       = new DataGridView();

        // ── SuspendLayout ───────────────────────────────────────────────────
        grpCadete.SuspendLayout();
        grpCastigo.SuspendLayout();
        grpSupervisor.SuspendLayout();
        grpFecha.SuspendLayout();
        pnlSemanaManual.SuspendLayout();
        grpObs.SuspendLayout();
        grpRecientes.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvRecientes).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSemana).BeginInit();
        SuspendLayout();

        // ════════════════════════════════════════════════════════════════════
        //  TÍTULO
        // ════════════════════════════════════════════════════════════════════
        lblTitulo.Text      = "REGISTRO DE SANCIONES";
        lblTitulo.Font      = new Font("Segoe UI", 14, FontStyle.Bold);
        lblTitulo.Location  = new Point(12, 12);
        lblTitulo.Size      = new Size(400, 30);
        lblTitulo.ForeColor = Color.FromArgb(30, 60, 120);

        // ════════════════════════════════════════════════════════════════════
        //  GRUPO CADETE
        // ════════════════════════════════════════════════════════════════════
        grpCadete.Text     = "1. Cadete";
        grpCadete.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpCadete.Location = new Point(12, 50);
        grpCadete.Size     = new Size(420, 75);

        lblDNICadete.Text     = "DNI:";
        lblDNICadete.Location = new Point(10, 25);
        lblDNICadete.Size     = new Size(35, 20);
        lblDNICadete.Font     = new Font("Segoe UI", 9);

        txtDNICadete.Location  = new Point(48, 22);
        txtDNICadete.Size      = new Size(130, 25);
        txtDNICadete.Font      = new Font("Segoe UI", 10);
        txtDNICadete.MaxLength = 20;
        ToolTip tt = new(); tt.SetToolTip(txtDNICadete, "Ingresa el DNI y presiona Enter");

        lblCadeteInfo.Location  = new Point(10, 50);
        lblCadeteInfo.Size      = new Size(400, 20);
        lblCadeteInfo.Font      = new Font("Segoe UI", 8.5f);

        grpCadete.Controls.AddRange(new Control[] { lblDNICadete, txtDNICadete, lblCadeteInfo });

        // ════════════════════════════════════════════════════════════════════
        //  GRUPO CASTIGO
        // ════════════════════════════════════════════════════════════════════
        grpCastigo.Text     = "2. Código de Castigo";
        grpCastigo.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpCastigo.Location = new Point(12, 135);
        grpCastigo.Size     = new Size(420, 75);

        lblCodigoCastigo.Text     = "Código:";
        lblCodigoCastigo.Location = new Point(10, 25);
        lblCodigoCastigo.Size     = new Size(55, 20);
        lblCodigoCastigo.Font     = new Font("Segoe UI", 9);

        txtCodigoCastigo.Location  = new Point(68, 22);
        txtCodigoCastigo.Size      = new Size(100, 25);
        txtCodigoCastigo.Font      = new Font("Segoe UI", 10);
        txtCodigoCastigo.MaxLength = 10;
        txtCodigoCastigo.Enabled   = false;
        ToolTip tt2 = new(); tt2.SetToolTip(txtCodigoCastigo, "Ingresa el código y presiona Enter");

        lblCastigoInfo.Location = new Point(10, 50);
        lblCastigoInfo.Size     = new Size(400, 20);
        lblCastigoInfo.Font     = new Font("Segoe UI", 8.5f);

        grpCastigo.Controls.AddRange(new Control[] { lblCodigoCastigo, txtCodigoCastigo, lblCastigoInfo });

        // ════════════════════════════════════════════════════════════════════
        //  GRUPO SUPERVISOR
        // ════════════════════════════════════════════════════════════════════
        grpSupervisor.Text     = "3. Supervisor";
        grpSupervisor.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpSupervisor.Location = new Point(12, 220);
        grpSupervisor.Size     = new Size(420, 90);

        lblDNISupervisor.Text     = "DNI:";
        lblDNISupervisor.Location = new Point(10, 25);
        lblDNISupervisor.Size     = new Size(35, 20);
        lblDNISupervisor.Font     = new Font("Segoe UI", 9);

        txtDNISupervisor.Location  = new Point(48, 22);
        txtDNISupervisor.Size      = new Size(110, 25);
        txtDNISupervisor.Font      = new Font("Segoe UI", 10);
        txtDNISupervisor.MaxLength = 20;

        cmbSupervisor.Location         = new Point(170, 22);
        cmbSupervisor.Size             = new Size(235, 25);
        cmbSupervisor.Font             = new Font("Segoe UI", 9);
        cmbSupervisor.DropDownStyle    = ComboBoxStyle.DropDownList;
        cmbSupervisor.DropDownWidth    = 400;
        ToolTip tt3 = new(); tt3.SetToolTip(cmbSupervisor, "O selecciona desde el combo");

        lblSupervisorInfo.Location = new Point(10, 55);
        lblSupervisorInfo.Size     = new Size(400, 20);
        lblSupervisorInfo.Font     = new Font("Segoe UI", 8.5f);

        grpSupervisor.Controls.AddRange(new Control[]
            { lblDNISupervisor, txtDNISupervisor, cmbSupervisor, lblSupervisorInfo });

        // ════════════════════════════════════════════════════════════════════
        //  GRUPO FECHA / HORA / SEMANA
        // ════════════════════════════════════════════════════════════════════
        grpFecha.Text     = "4. Fecha y Semana";
        grpFecha.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpFecha.Location = new Point(12, 320);
        grpFecha.Size     = new Size(420, 95);

        lblFecha.Text     = "Fecha:";
        lblFecha.Location = new Point(10, 25);
        lblFecha.Size     = new Size(45, 20);
        lblFecha.Font     = new Font("Segoe UI", 9);

        dtpFecha.Location = new Point(58, 22);
        dtpFecha.Size     = new Size(130, 25);
        dtpFecha.Format   = DateTimePickerFormat.Short;

        lblHora.Text     = "Hora:";
        lblHora.Location = new Point(200, 25);
        lblHora.Size     = new Size(38, 20);
        lblHora.Font     = new Font("Segoe UI", 9);

        dtpHora.Location   = new Point(240, 22);
        dtpHora.Size       = new Size(100, 25);
        dtpHora.Format     = DateTimePickerFormat.Time;
        dtpHora.ShowUpDown = true;

        chkSemanaManual.Text     = "Asignar semana manualmente";
        chkSemanaManual.Location = new Point(10, 55);
        chkSemanaManual.Size     = new Size(220, 22);
        chkSemanaManual.Font     = new Font("Segoe UI", 9);

        // Panel semana manual (oculto por defecto)
        pnlSemanaManual.Location = new Point(235, 50);
        pnlSemanaManual.Size     = new Size(170, 28);
        pnlSemanaManual.Visible  = false;

        lblSemana.Text     = "Semana:";
        lblSemana.Location = new Point(0, 5);
        lblSemana.Size     = new Size(60, 20);
        lblSemana.Font     = new Font("Segoe UI", 9);

        nudSemana.Location = new Point(65, 2);
        nudSemana.Size     = new Size(50, 25);

        pnlSemanaManual.Controls.AddRange(new Control[] { lblSemana, nudSemana });

        grpFecha.Controls.AddRange(new Control[]
            { lblFecha, dtpFecha, lblHora, dtpHora, chkSemanaManual, pnlSemanaManual });

        // ════════════════════════════════════════════════════════════════════
        //  GRUPO OBSERVACIONES
        // ════════════════════════════════════════════════════════════════════
        grpObs.Text     = "5. Observaciones (opcional — Enter para guardar)";
        grpObs.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpObs.Location = new Point(12, 425);
        grpObs.Size     = new Size(420, 55);

        txtObservaciones.Location  = new Point(10, 22);
        txtObservaciones.Size      = new Size(395, 25);
        txtObservaciones.Font      = new Font("Segoe UI", 9);
        txtObservaciones.MaxLength = 300;

        grpObs.Controls.Add(txtObservaciones);

        // ════════════════════════════════════════════════════════════════════
        //  BOTONES Y ESTADO
        // ════════════════════════════════════════════════════════════════════
        btnGuardar.Text      = "💾  GUARDAR  (F5)";
        btnGuardar.Location  = new Point(12, 490);
        btnGuardar.Size      = new Size(180, 38);
        btnGuardar.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
        btnGuardar.BackColor = Color.FromArgb(30, 120, 60);
        btnGuardar.ForeColor = Color.White;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.Click    += btnGuardar_Click;

        btnLimpiarTodo.Text      = "🗑  Limpiar todo";
        btnLimpiarTodo.Location  = new Point(205, 490);
        btnLimpiarTodo.Size      = new Size(130, 38);
        btnLimpiarTodo.Font      = new Font("Segoe UI", 9);
        btnLimpiarTodo.FlatStyle = FlatStyle.Flat;
        btnLimpiarTodo.Click    += btnLimpiarTodo_Click;

        btnRefrescar.Text      = "↺  Refrescar";
        btnRefrescar.Location  = new Point(348, 490);
        btnRefrescar.Size      = new Size(90, 38);
        btnRefrescar.Font      = new Font("Segoe UI", 9);
        btnRefrescar.FlatStyle = FlatStyle.Flat;
        btnRefrescar.Click    += btnRefrescar_Click;

        lblEstado.Location  = new Point(12, 535);
        lblEstado.Size      = new Size(420, 22);
        lblEstado.Font      = new Font("Segoe UI", 9, FontStyle.Bold);

        // ════════════════════════════════════════════════════════════════════
        //  GRILLA REGISTROS RECIENTES
        // ════════════════════════════════════════════════════════════════════
        grpRecientes.Text     = "Últimas sanciones registradas";
        grpRecientes.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
        grpRecientes.Location = new Point(445, 50);
        grpRecientes.Size     = new Size(720, 510);

        dgvRecientes.Location              = new Point(8, 22);
        dgvRecientes.Size                  = new Size(702, 480);
        dgvRecientes.ReadOnly              = true;
        dgvRecientes.AllowUserToAddRows    = false;
        dgvRecientes.AllowUserToDeleteRows = false;
        dgvRecientes.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRecientes.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
        dgvRecientes.RowHeadersVisible     = false;
        dgvRecientes.Font                  = new Font("Segoe UI", 8.5f);
        dgvRecientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);
        dgvRecientes.EnableHeadersVisualStyles = false;
        dgvRecientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 60, 120);
        dgvRecientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvRecientes.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);

        grpRecientes.Controls.Add(dgvRecientes);

        // ════════════════════════════════════════════════════════════════════
        //  FORM PRINCIPAL
        // ════════════════════════════════════════════════════════════════════
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(1180, 570);
        MinimumSize         = new Size(1180, 610);
        Text                = "Registro de Sanciones — Colegio Militar";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterScreen;
        Load               += FormRegistrarSancion_Load;

        Controls.AddRange(new Control[]
        {
            lblTitulo,
            grpCadete, grpCastigo, grpSupervisor, grpFecha, grpObs,
            btnGuardar, btnLimpiarTodo, btnRefrescar, lblEstado,
            grpRecientes
        });

        // ── ResumeLayout ────────────────────────────────────────────────────
        grpCadete.ResumeLayout(false);
        grpCastigo.ResumeLayout(false);
        grpSupervisor.ResumeLayout(false);
        grpFecha.ResumeLayout(false);
        pnlSemanaManual.ResumeLayout(false);
        grpObs.ResumeLayout(false);
        grpRecientes.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvRecientes).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSemana).EndInit();
        ResumeLayout(false);
    }

    // ── Campos del Designer ─────────────────────────────────────────────────
    private Label       lblTitulo;
    private GroupBox    grpCadete;
    private Label       lblDNICadete;
    private TextBox     txtDNICadete;
    private Label       lblCadeteInfo;
    private GroupBox    grpCastigo;
    private Label       lblCodigoCastigo;
    private TextBox     txtCodigoCastigo;
    private Label       lblCastigoInfo;
    private GroupBox    grpSupervisor;
    private Label       lblDNISupervisor;
    private TextBox     txtDNISupervisor;
    private ComboBox    cmbSupervisor;
    private Label       lblSupervisorInfo;
    private GroupBox    grpFecha;
    private Label       lblFecha;
    private DateTimePicker dtpFecha;
    private Label       lblHora;
    private DateTimePicker dtpHora;
    private CheckBox    chkSemanaManual;
    private Panel       pnlSemanaManual;
    private Label       lblSemana;
    private NumericUpDown nudSemana;
    private GroupBox    grpObs;
    private TextBox     txtObservaciones;
    private Button      btnGuardar;
    private Button      btnLimpiarTodo;
    private Button      btnRefrescar;
    private Label       lblEstado;
    private GroupBox    grpRecientes;
    private DataGridView dgvRecientes;
}
