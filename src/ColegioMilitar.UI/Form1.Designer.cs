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
        tabReporteBimestral = new TabPage();
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
        ((System.ComponentModel.ISupportInitialize)dgv3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgv4).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgv5).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvSalida3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvSalida4).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvSalida5).BeginInit();
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

        lblSubtitulo.Text      = "RELACIÓN DE CASTIGADOS — --";
        lblSubtitulo.Font      = new Font("Segoe UI", 9);
        lblSubtitulo.ForeColor = Color.FromArgb(180, 210, 255);
        lblSubtitulo.Location  = new Point(15, 35);
        lblSubtitulo.Size      = new Size(500, 18);

        pnlHeader.Controls.AddRange(new Control[] { lblTitulo, lblSubtitulo });

        // ── Panel acciones ────────────────────────────────────────────────
        pnlAcciones.Dock      = DockStyle.Top;
        pnlAcciones.Height    = 46;
        pnlAcciones.BackColor = Color.FromArgb(240, 243, 250);

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
        tabControl.Controls.AddRange(new TabPage[] { tabPage3, tabPage4, tabPage5, tabSalida, tabReporteBimestral });

        tabReporteBimestral.Text = "  REPORTE BIMESTRAL  ";
        tabReporteBimestral.Padding = new Padding(5);

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
        ((System.ComponentModel.ISupportInitialize)dgv3).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgv4).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgv5).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvSalida3).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvSalida4).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvSalida5).EndInit();
        ResumeLayout(false);
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
    private TabPage           tabReporteBimestral;
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
