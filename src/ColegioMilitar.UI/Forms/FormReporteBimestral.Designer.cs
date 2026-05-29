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
        pnlHeader = new Panel();
        lblTitulo = new Label();
        lblSubtitulo = new Label();
        pnlAcciones = new Panel();
        btnGuardar = new Button();
        lblEstado = new Label();
        tabControl = new TabControl();
        tab3 = new TabPage();
        dgv3 = new DataGridView();
        tab4 = new TabPage();
        dgv4 = new DataGridView();
        tab5 = new TabPage();
        dgv5 = new DataGridView();
        pnlHeader.SuspendLayout();
        pnlAcciones.SuspendLayout();
        tabControl.SuspendLayout();
        tab3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv3).BeginInit();
        tab4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv4).BeginInit();
        tab5.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv5).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(30, 60, 120);
        pnlHeader.Controls.Add(lblTitulo);
        pnlHeader.Controls.Add(lblSubtitulo);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1280, 100);
        pnlHeader.TabIndex = 2;
        pnlHeader.Visible = false;
        // 
        // lblTitulo
        // 
        lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(15, 7);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(750, 26);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "REGISTRO DE NOTAS DE CONDUCTA Y ACTITUD MILITAR";
        // 
        // lblSubtitulo
        // 
        lblSubtitulo.Font = new Font("Segoe UI", 9F);
        lblSubtitulo.ForeColor = Color.FromArgb(180, 210, 255);
        lblSubtitulo.Location = new Point(15, 35);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Size = new Size(500, 18);
        lblSubtitulo.TabIndex = 1;
        // 
        // pnlAcciones
        // 
        pnlAcciones.BackColor = Color.FromArgb(240, 243, 250);
        pnlAcciones.Controls.Add(btnGuardar);
        pnlAcciones.Controls.Add(lblEstado);
        pnlAcciones.Dock = DockStyle.Top;
        pnlAcciones.Location = new Point(0, 100);
        pnlAcciones.Name = "pnlAcciones";
        pnlAcciones.Size = new Size(1280, 44);
        pnlAcciones.TabIndex = 1;
        // 
        // btnGuardar
        // 
        btnGuardar.BackColor = Color.FromArgb(30, 100, 50);
        btnGuardar.Cursor = Cursors.Hand;
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnGuardar.ForeColor = Color.White;
        btnGuardar.Location = new Point(8, 7);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(230, 30);
        btnGuardar.TabIndex = 0;
        btnGuardar.Text = "💾  Guardar Actitudes Militares";
        btnGuardar.UseVisualStyleBackColor = false;
        btnGuardar.Click += btnGuardar_Click;
        // 
        // lblEstado
        // 
        lblEstado.Font = new Font("Segoe UI", 9F);
        lblEstado.ForeColor = Color.FromArgb(80, 80, 80);
        lblEstado.Location = new Point(248, 12);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(700, 20);
        lblEstado.TabIndex = 1;
        lblEstado.Text = "💡 Doble clic en ACTITUD MIL para editar — se guarda al salir de la celda. El botón guarda TODAS las actitudes de una vez.";
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tab3);
        tabControl.Controls.Add(tab4);
        tabControl.Controls.Add(tab5);
        tabControl.Dock = DockStyle.Fill;
        tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        tabControl.Location = new Point(0, 144);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1280, 576);
        tabControl.TabIndex = 0;
        // 
        // tab3
        // 
        tab3.Controls.Add(dgv3);
        tab3.Location = new Point(4, 26);
        tab3.Name = "tab3";
        tab3.Padding = new Padding(5);
        tab3.Size = new Size(1272, 546);
        tab3.TabIndex = 0;
        tab3.Text = "  3° AÑO  ";
        // 
        // dgv3
        // 
        dgv3.Dock = DockStyle.Fill;
        dgv3.Location = new Point(5, 5);
        dgv3.Name = "dgv3";
        dgv3.Size = new Size(1262, 536);
        dgv3.TabIndex = 0;
        // 
        // tab4
        // 
        tab4.Controls.Add(dgv4);
        tab4.Location = new Point(4, 26);
        tab4.Name = "tab4";
        tab4.Padding = new Padding(5);
        tab4.Size = new Size(1272, 546);
        tab4.TabIndex = 1;
        tab4.Text = "  4° AÑO  ";
        // 
        // dgv4
        // 
        dgv4.Dock = DockStyle.Fill;
        dgv4.Location = new Point(5, 5);
        dgv4.Name = "dgv4";
        dgv4.Size = new Size(1262, 536);
        dgv4.TabIndex = 0;
        // 
        // tab5
        // 
        tab5.Controls.Add(dgv5);
        tab5.Location = new Point(4, 26);
        tab5.Name = "tab5";
        tab5.Padding = new Padding(5);
        tab5.Size = new Size(1272, 546);
        tab5.TabIndex = 2;
        tab5.Text = "  5° AÑO  ";
        // 
        // dgv5
        // 
        dgv5.Dock = DockStyle.Fill;
        dgv5.Location = new Point(5, 5);
        dgv5.Name = "dgv5";
        dgv5.Size = new Size(1262, 536);
        dgv5.TabIndex = 0;
        // 
        // FormReporteBimestral
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1280, 720);
        Controls.Add(tabControl);
        Controls.Add(pnlAcciones);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(1000, 600);
        Name = "FormReporteBimestral";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Reporte Bimestral — Conducta y Actitud Militar";
        pnlHeader.ResumeLayout(false);
        pnlAcciones.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        tab3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv3).EndInit();
        tab4.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgv4).EndInit();
        tab5.ResumeLayout(false);
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
