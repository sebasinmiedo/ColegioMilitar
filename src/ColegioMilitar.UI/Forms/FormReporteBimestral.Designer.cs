namespace ColegioMilitar.UI.Forms;

partial class FormReporteBimestral
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlHeader    = new Panel();
        lblTitulo    = new Label();
        lblSubtitulo = new Label();
        pnlAcciones  = new Panel();
        btnGuardar   = new Button();
        lblEstado    = new Label();
        tabControl   = new TabControl();
        tab3         = new TabPage();
        tab4         = new TabPage();
        tab5         = new TabPage();
        dgv3         = new DataGridView();
        dgv4         = new DataGridView();
        dgv5         = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlAcciones.SuspendLayout();
        tabControl.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgv4).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgv5).BeginInit();
        SuspendLayout();

        // Header
        pnlHeader.Dock      = DockStyle.Top;
        //pnlHeader.Height    = 58;
        pnlHeader.Visible = false;
        pnlHeader.BackColor = Color.FromArgb(30, 60, 120);

        lblTitulo.Text      = "REGISTRO DE NOTAS DE CONDUCTA Y ACTITUD MILITAR";
        lblTitulo.Font      = new Font("Segoe UI", 12, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location  = new Point(15, 7);
        lblTitulo.Size      = new Size(750, 26);

        lblSubtitulo.Font      = new Font("Segoe UI", 9);
        lblSubtitulo.ForeColor = Color.FromArgb(180, 210, 255);
        lblSubtitulo.Location  = new Point(15, 35);
        lblSubtitulo.Size      = new Size(500, 18);

        pnlHeader.Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo });

        // Panel acciones
        pnlAcciones.Dock      = DockStyle.Top;
        pnlAcciones.Height    = 44;
        pnlAcciones.BackColor = Color.FromArgb(240, 243, 250);

        btnGuardar.Text      = "💾  Guardar Actitudes Militares";
        btnGuardar.Location  = new Point(8, 7);
        btnGuardar.Size      = new Size(230, 30);
        btnGuardar.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
        btnGuardar.BackColor = Color.FromArgb(30, 100, 50);
        btnGuardar.ForeColor = Color.White;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.Cursor    = Cursors.Hand;
        btnGuardar.Click    += btnGuardar_Click;

        lblEstado.Location  = new Point(248, 12);
        lblEstado.Size      = new Size(700, 20);
        lblEstado.Font      = new Font("Segoe UI", 9);
        lblEstado.Text      = "💡 Doble clic en ACTITUD MIL para editar — se guarda al salir de la celda. El botón guarda TODAS las actitudes de una vez.";
        lblEstado.ForeColor = Color.FromArgb(80, 80, 80);

        pnlAcciones.Controls.AddRange(new Control[] { btnGuardar, lblEstado });

        // TabControl
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        tabControl.Controls.AddRange(new TabPage[] { tab3, tab4, tab5 });

        tab3.Text    = "  3° AÑO  ";
        tab3.Padding = new Padding(5);
        ConfigurarDgv(dgv3);
        dgv3.Dock = DockStyle.Fill;
        tab3.Controls.Add(dgv3);

        tab4.Text    = "  4° AÑO  ";
        tab4.Padding = new Padding(5);
        ConfigurarDgv(dgv4);
        dgv4.Dock = DockStyle.Fill;
        tab4.Controls.Add(dgv4);

        tab5.Text    = "  5° AÑO  ";
        tab5.Padding = new Padding(5);
        ConfigurarDgv(dgv5);
        dgv5.Dock = DockStyle.Fill;
        tab5.Controls.Add(dgv5);

        // Form — SIN Load event
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(1280, 720);
        MinimumSize         = new Size(1000, 600);
        Text                = "Reporte Bimestral — Conducta y Actitud Militar";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;

        Controls.Add(tabControl);
        Controls.Add(pnlAcciones);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        pnlAcciones.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv3).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgv4).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgv5).EndInit();
        ResumeLayout(false);
    }

    private static void ConfigurarDgv(DataGridView dgv)
    {
        dgv.ReadOnly              = false;
        dgv.AllowUserToAddRows    = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.SelectionMode         = DataGridViewSelectionMode.CellSelect;
        dgv.RowHeadersVisible     = false;
        dgv.Font                  = new Font("Segoe UI", 9);
        dgv.ColumnHeadersHeight   = 50;
        dgv.RowTemplate.Height    = 24;
        dgv.BackgroundColor       = Color.White;
        dgv.BorderStyle           = BorderStyle.None;
        dgv.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.GridColor             = Color.FromArgb(210, 218, 235);
        dgv.AutoGenerateColumns   = false;
        dgv.MultiSelect           = false;
        dgv.EnableHeadersVisualStyles                 = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 60, 120);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
        dgv.ColumnHeadersDefaultCellStyle.WrapMode    = DataGridViewTriState.True;
        dgv.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.Single;
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 210, 240);
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
    }

    private Panel        pnlHeader;
    private Label        lblTitulo, lblSubtitulo;
    private Panel        pnlAcciones;
    private Button       btnGuardar;
    private Label        lblEstado;
    private TabControl   tabControl;
    private TabPage      tab3, tab4, tab5;
    private DataGridView dgv3, dgv4, dgv5;
}
