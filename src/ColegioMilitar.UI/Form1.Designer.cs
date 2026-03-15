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
        lblTitulo            = new Label();
        lblSubtitulo         = new Label();
        pnlMenu              = new Panel();
        btnRegistrarSanciones = new Button();
        lblVersion           = new Label();

        pnlMenu.SuspendLayout();
        SuspendLayout();

        // ── Título ───────────────────────────────────────────────────────────
        lblTitulo.Text      = "SISTEMA DE SANCIONES";
        lblTitulo.Font      = new Font("Segoe UI", 20, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
        lblTitulo.Dock      = DockStyle.Top;
        lblTitulo.Height    = 60;
        lblTitulo.Padding   = new Padding(0, 15, 0, 0);

        lblSubtitulo.Text      = "Colegio Militar";
        lblSubtitulo.Font      = new Font("Segoe UI", 11);
        lblSubtitulo.ForeColor = Color.FromArgb(200, 220, 255);
        lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
        lblSubtitulo.Dock      = DockStyle.Top;
        lblSubtitulo.Height    = 30;

        // ── Panel de botones ─────────────────────────────────────────────────
        pnlMenu.Location  = new Point(60, 130);
        pnlMenu.Size      = new Size(280, 300);
        pnlMenu.BackColor = Color.Transparent;

        // Botón 1: Registrar Sanciones
        btnRegistrarSanciones.Text      = "📋  Registrar Sanciones";
        btnRegistrarSanciones.Location  = new Point(0, 0);
        btnRegistrarSanciones.Size      = new Size(280, 55);
        btnRegistrarSanciones.Font      = new Font("Segoe UI", 11, FontStyle.Bold);
        btnRegistrarSanciones.BackColor = Color.FromArgb(30, 120, 60);
        btnRegistrarSanciones.ForeColor = Color.White;
        btnRegistrarSanciones.FlatStyle = FlatStyle.Flat;
        btnRegistrarSanciones.FlatAppearance.BorderSize = 0;
        btnRegistrarSanciones.Cursor    = Cursors.Hand;
        btnRegistrarSanciones.Click    += btnRegistrarSanciones_Click;

        pnlMenu.Controls.Add(btnRegistrarSanciones);

        // ── Versión ──────────────────────────────────────────────────────────
        lblVersion.Text      = "v1.0 — 2026";
        lblVersion.Font      = new Font("Segoe UI", 8);
        lblVersion.ForeColor = Color.FromArgb(150, 180, 220);
        lblVersion.TextAlign = ContentAlignment.MiddleCenter;
        lblVersion.Dock      = DockStyle.Bottom;
        lblVersion.Height    = 25;

        // ── Form ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(400, 500);
        FormBorderStyle     = FormBorderStyle.FixedSingle;
        MaximizeBox         = false;
        Text                = "Colegio Militar — Sistema de Sanciones";
        BackColor           = Color.FromArgb(30, 60, 120);
        StartPosition       = FormStartPosition.CenterScreen;
        Font                = new Font("Segoe UI", 9F);

        Controls.Add(lblTitulo);
        Controls.Add(lblSubtitulo);
        Controls.Add(pnlMenu);
        Controls.Add(lblVersion);

        pnlMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label   lblTitulo;
    private Label   lblSubtitulo;
    private Panel   pnlMenu;
    private Button  btnRegistrarSanciones;
    private Label   lblVersion;
}
