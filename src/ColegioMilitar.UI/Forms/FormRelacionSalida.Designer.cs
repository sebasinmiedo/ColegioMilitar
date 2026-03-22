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
        pnlHeader = new Panel();
        lblTitulo = new Label();
        lblFecha = new Label();
        tabControl = new TabControl();
        tab3 = new TabPage();
        dgv3 = new DataGridView();
        pnlRaciones3 = new Panel();
        tab4 = new TabPage();
        dgv4 = new DataGridView();
        pnlRaciones4 = new Panel();
        tab5 = new TabPage();
        dgv5 = new DataGridView();
        pnlRaciones5 = new Panel();
        tabResumen = new TabPage();
        dgvResumen = new DataGridView();
        lblResumenTitulo = new Label();
        pnlHeader.SuspendLayout();
        tabControl.SuspendLayout();
        tab3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv3).BeginInit();
        tab4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv4).BeginInit();
        tab5.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv5).BeginInit();
        tabResumen.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvResumen).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(30, 60, 120);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Controls.Add(lblFecha);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1100, 55);
        pnlHeader.TabIndex = 1;
        // 
        // lblTitulo
        // 
        lblTitulo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(15, 7);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(500, 26);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "RACIONES — RELACIÓN DE SALIDA";
        // 
        // lblFecha
        // 
        lblFecha.Font = new Font("Segoe UI", 9F);
        lblFecha.ForeColor = Color.FromArgb(180, 210, 255);
        lblFecha.Location = new Point(15, 35);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(200, 18);
        lblFecha.TabIndex = 1;
        lblFecha.Text = "--/--/----";
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tab3);
        tabControl.Controls.Add(tab4);
        tabControl.Controls.Add(tab5);
        tabControl.Controls.Add(tabResumen);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        tabControl.Location = new Point(0, 55);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1100, 645);
        tabControl.TabIndex = 0;
        // 
        // tab3
        // 
        tab3.Controls.Add(dgv3);
        tab3.Controls.Add(pnlRaciones3);
        tab3.Location = new Point(4, 26);
        tab3.Name = "tab3";
        tab3.Padding = new Padding(5);
        tab3.Size = new Size(1092, 615);
        tab3.TabIndex = 0;
        tab3.Text = "  3° AÑO  ";
        // 
        // dgv3
        // 
        dgv3.Dock = DockStyle.Fill;
        dgv3.Location = new Point(5, 5);
        dgv3.Name = "dgv3";
        dgv3.Size = new Size(1082, 605);
        dgv3.TabIndex = 0;
        // 
        // pnlRaciones3
        // 
        pnlRaciones3.Location = new Point(0, 0);
        pnlRaciones3.Name = "pnlRaciones3";
        pnlRaciones3.Size = new Size(200, 100);
        pnlRaciones3.TabIndex = 1;
        // 
        // tab4
        // 
        tab4.Controls.Add(dgv4);
        tab4.Controls.Add(pnlRaciones4);
        tab4.Location = new Point(4, 26);
        tab4.Name = "tab4";
        tab4.Padding = new Padding(5);
        tab4.Size = new Size(192, 70);
        tab4.TabIndex = 1;
        tab4.Text = "  4° AÑO  ";
        // 
        // dgv4
        // 
        dgv4.Dock = DockStyle.Fill;
        dgv4.Location = new Point(5, 5);
        dgv4.Name = "dgv4";
        dgv4.Size = new Size(182, 60);
        dgv4.TabIndex = 0;
        // 
        // pnlRaciones4
        // 
        pnlRaciones4.Location = new Point(0, 0);
        pnlRaciones4.Name = "pnlRaciones4";
        pnlRaciones4.Size = new Size(200, 100);
        pnlRaciones4.TabIndex = 1;
        // 
        // tab5
        // 
        tab5.Controls.Add(dgv5);
        tab5.Controls.Add(pnlRaciones5);
        tab5.Location = new Point(4, 26);
        tab5.Name = "tab5";
        tab5.Padding = new Padding(5);
        tab5.Size = new Size(192, 70);
        tab5.TabIndex = 2;
        tab5.Text = "  5° AÑO  ";
        // 
        // dgv5
        // 
        dgv5.Dock = DockStyle.Fill;
        dgv5.Location = new Point(5, 5);
        dgv5.Name = "dgv5";
        dgv5.Size = new Size(182, 60);
        dgv5.TabIndex = 0;
        // 
        // pnlRaciones5
        // 
        pnlRaciones5.Location = new Point(0, 0);
        pnlRaciones5.Name = "pnlRaciones5";
        pnlRaciones5.Size = new Size(200, 100);
        pnlRaciones5.TabIndex = 1;
        // 
        // tabResumen
        // 
        tabResumen.Controls.Add(dgvResumen);
        tabResumen.Controls.Add(lblResumenTitulo);
        tabResumen.Location = new Point(4, 26);
        tabResumen.Name = "tabResumen";
        tabResumen.Padding = new Padding(5);
        tabResumen.Size = new Size(1092, 615);
        tabResumen.TabIndex = 3;
        tabResumen.Text = "  RESUMEN RACIONES  ";
        // 
        // dgvResumen
        // 
        dgvResumen.Dock = DockStyle.Fill;
        dgvResumen.Location = new Point(5, 5);
        dgvResumen.Name = "dgvResumen";
        dgvResumen.Size = new Size(1082, 605);
        dgvResumen.TabIndex = 0;
        // 
        // lblResumenTitulo
        // 
        lblResumenTitulo.Location = new Point(0, 0);
        lblResumenTitulo.Name = "lblResumenTitulo";
        lblResumenTitulo.Size = new Size(100, 23);
        lblResumenTitulo.TabIndex = 1;
        // 
        // FormRelacionSalida
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 700);
        Controls.Add(tabControl);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(900, 600);
        Name = "FormRelacionSalida";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Relación de Salida — Raciones";
        WindowState = FormWindowState.Maximized;
        Load += FormRelacionSalida_Load;
        pnlHeader.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        tab3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv3).EndInit();
        tab4.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv4).EndInit();
        tab5.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv5).EndInit();
        tabResumen.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvResumen).EndInit();
        ResumeLayout(false);
    }

    private void BuildRacionesPanel(Panel pnl, string titulo, Color color)
    {
        pnl.Dock      = DockStyle.Top;
        pnl.Height    = 90;
        pnl.BackColor = Color.White;
        pnl.Padding   = new Padding(5);

        // Título de la tabla
        var lblTit = new Label
        {
            Text      = $"RACIONES — {titulo}",
            Font      = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = color,
            Location  = new Point(5, 5),
            Size      = new Size(500, 22),
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Cabeceras
        string[] headers = { "AÑOS", "VIERNES", "SÁBADO", "DOMINGO", "TOTAL" };
        int[] widths = { 80, 80, 80, 80, 80 };
        int x = 5;

        for (int i = 0; i < headers.Length; i++)
        {
            var lbl = new Label
            {
                Text      = headers[i],
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = color,
                Location  = new Point(x, 30),
                Size      = new Size(widths[i], 22),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnl.Controls.Add(lbl);
            x += widths[i];
        }

        // Fila de valores
        var lblAño = new Label
        {
            Text      = titulo.Split(' ')[0] + " AÑO",
            Font      = new Font("Segoe UI", 8.5f),
            Location  = new Point(5, 55),
            Size      = new Size(80, 22),
            TextAlign = ContentAlignment.MiddleCenter,
            BorderStyle = BorderStyle.FixedSingle
        };

        var lblVie = new Label { Location = new Point(85,  55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), Text = "0" };
        var lblSab = new Label { Location = new Point(165, 55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), Text = "0" };
        var lblDom = new Label { Location = new Point(245, 55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), Text = "0" };
        var lblTot = new Label { Location = new Point(325, 55), Size = new Size(80, 22), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.FromArgb(200, 230, 200), Text = "0" };

        // Guardar referencias para actualización
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
        dgv.EnableHeadersVisualStyles                      = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor        = Color.FromArgb(30, 60, 120);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor        = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font             = new Font("Segoe UI", 9, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.Alignment        = DataGridViewContentAlignment.MiddleCenter;
        dgv.ColumnHeadersBorderStyle                       = DataGridViewHeaderBorderStyle.None;
        dgv.DefaultCellStyle.Alignment                     = DataGridViewContentAlignment.MiddleCenter;
    }

    private Panel        pnlHeader;
    private Label        lblTitulo, lblFecha;
    private TabControl   tabControl;
    private TabPage      tab3, tab4, tab5, tabResumen;
    private Panel        pnlRaciones3, pnlRaciones4, pnlRaciones5;
    private DataGridView dgv3, dgv4, dgv5, dgvResumen;
    private Label lblResumenTitulo;
}
