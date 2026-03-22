namespace ColegioMilitar.UI.Forms;

partial class FormRelacionSalida
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
        lblFecha     = new Label();
        tabControl   = new TabControl();
        tab3         = new TabPage();
        tab4         = new TabPage();
        tab5         = new TabPage();
        tabResumen   = new TabPage();
        pnlRaciones3 = new Panel();
        pnlRaciones4 = new Panel();
        pnlRaciones5 = new Panel();
        dgv3         = new DataGridView();
        dgv4         = new DataGridView();
        dgv5         = new DataGridView();
        dgvResumen   = new DataGridView();

        pnlHeader.SuspendLayout();
        tabControl.SuspendLayout();
        foreach (var d in new[] { dgv3, dgv4, dgv5, dgvResumen })
            ((System.ComponentModel.ISupportInitialize)d).BeginInit();
        SuspendLayout();

        // ── Header ───────────────────────────────────────────────────────
        pnlHeader.Dock      = DockStyle.Top;
        pnlHeader.Height    = 55;
        pnlHeader.BackColor = Color.FromArgb(30, 60, 120);

        lblTitulo.Text      = "RACIONES — RELACIÓN DE SALIDA";
        lblTitulo.Font      = new Font("Segoe UI", 13, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location  = new Point(15, 7);
        lblTitulo.Size      = new Size(600, 26);

        lblFecha.Text      = DateTime.Today.ToString("dd/MM/yyyy");
        lblFecha.Font      = new Font("Segoe UI", 9);
        lblFecha.ForeColor = Color.FromArgb(180, 210, 255);
        lblFecha.Location  = new Point(15, 35);
        lblFecha.Size      = new Size(200, 18);

        pnlHeader.Controls.AddRange(new Control[] { lblTitulo, lblFecha });

        // ── TabControl ────────────────────────────────────────────────────
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        tabControl.Controls.AddRange(new TabPage[] { tab3, tab4, tab5, tabResumen });

        // ── Tab 3° AÑO ───────────────────────────────────────────────────
        tab3.Text    = "  3° AÑO  ";
        tab3.Padding = new Padding(5);
        BuildRacionesPanel(pnlRaciones3, "3ER AÑO", Color.FromArgb(30, 90, 160));
        ConfigurarDgv(dgv3);
        dgv3.Dock = DockStyle.Fill;
        tab3.Controls.Add(dgv3);
        tab3.Controls.Add(pnlRaciones3);

        // ── Tab 4° AÑO ───────────────────────────────────────────────────
        tab4.Text    = "  4° AÑO  ";
        tab4.Padding = new Padding(5);
        BuildRacionesPanel(pnlRaciones4, "4TO AÑO", Color.FromArgb(20, 110, 60));
        ConfigurarDgv(dgv4);
        dgv4.Dock = DockStyle.Fill;
        tab4.Controls.Add(dgv4);
        tab4.Controls.Add(pnlRaciones4);

        // ── Tab 5° AÑO ───────────────────────────────────────────────────
        tab5.Text    = "  5° AÑO  ";
        tab5.Padding = new Padding(5);
        BuildRacionesPanel(pnlRaciones5, "5TO AÑO", Color.FromArgb(140, 70, 10));
        ConfigurarDgv(dgv5);
        dgv5.Dock = DockStyle.Fill;
        tab5.Controls.Add(dgv5);
        tab5.Controls.Add(pnlRaciones5);

        // ── Tab RESUMEN ──────────────────────────────────────────────────
        tabResumen.Text    = "  RESUMEN RACIONES  ";
        tabResumen.Padding = new Padding(5);

        var lblResumenTitulo = new Label
        {
            Text      = "RACIONES (DÍAS VIE, SAB Y DOM)",
            Font      = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.FromArgb(60, 130, 60),
            Dock      = DockStyle.Top,
            Height    = 32,
            TextAlign = ContentAlignment.MiddleCenter
        };

        ConfigurarDgv(dgvResumen);
        dgvResumen.Dock = DockStyle.Fill;
        tabResumen.Controls.Add(dgvResumen);
        tabResumen.Controls.Add(lblResumenTitulo);

        // ── Form ──────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(1100, 700);
        MinimumSize         = new Size(900, 600);
        Text                = "Relación de Salida — Raciones";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        WindowState         = FormWindowState.Maximized;
        Load               += FormRelacionSalida_Load;

        Controls.Add(tabControl);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        foreach (var d in new[] { dgv3, dgv4, dgv5, dgvResumen })
            ((System.ComponentModel.ISupportInitialize)d).EndInit();
        ResumeLayout(false);
    }

    private void BuildRacionesPanel(Panel pnl, string titulo, Color color)
    {
        pnl.Dock      = DockStyle.Top;
        pnl.Height    = 90;
        pnl.BackColor = Color.White;
        pnl.Padding   = new Padding(0);

        var lblTit = new Label
        {
            Text      = $"RACIONES — {titulo}",
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = color,
            Location  = new Point(5, 5),
            Size      = new Size(420, 22),
            TextAlign = ContentAlignment.MiddleCenter
        };

        string[] headers = { "AÑOS", "VIERNES", "SÁBADO", "DOMINGO", "TOTAL" };
        int[] widths = { 80, 80, 80, 80, 80 };
        int x = 5;
        for (int i = 0; i < headers.Length; i++)
        {
            pnl.Controls.Add(new Label
            {
                Text        = headers[i],
                Font        = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor   = Color.White,
                BackColor   = color,
                Location    = new Point(x, 30),
                Size        = new Size(widths[i], 22),
                TextAlign   = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            });
            x += widths[i];
        }

        var lblAño = new Label
        {
            Text        = titulo,
            Font        = new Font("Segoe UI", 8.5f),
            Location    = new Point(5, 55),
            Size        = new Size(80, 22),
            TextAlign   = ContentAlignment.MiddleCenter,
            BorderStyle = BorderStyle.FixedSingle
        };

        var lblVie = new Label { Location = new Point(85,  55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), Text = "0" };
        var lblSab = new Label { Location = new Point(165, 55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), Text = "0" };
        var lblDom = new Label { Location = new Point(245, 55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), Text = "0" };
        var lblTot = new Label { Location = new Point(325, 55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(200, 230, 200), Text = "0" };

        pnl.Tag = new FormRelacionSalida.RacionesLabels
        {
            LblViernes = lblVie,
            LblSabado  = lblSab,
            LblDomingo = lblDom,
            LblTotal   = lblTot
        };

        pnl.Controls.AddRange(new Control[] { lblTit, lblAño, lblVie, lblSab, lblDom, lblTot });
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
        dgv.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.GridColor             = Color.FromArgb(210, 218, 235);
        dgv.EnableHeadersVisualStyles                 = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 60, 120);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
        dgv.ColumnHeadersBorderStyle                  = DataGridViewHeaderBorderStyle.None;
        dgv.DefaultCellStyle.Alignment                = DataGridViewContentAlignment.MiddleCenter;
    }

    private Panel        pnlHeader;
    private Label        lblTitulo, lblFecha;
    private TabControl   tabControl;
    private TabPage      tab3, tab4, tab5, tabResumen;
    private Panel        pnlRaciones3, pnlRaciones4, pnlRaciones5;
    private DataGridView dgv3, dgv4, dgv5, dgvResumen;
}
