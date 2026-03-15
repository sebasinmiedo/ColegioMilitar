namespace ColegioMilitar.UI.Forms;

public class FormAgregarCadete : Form
{
    public string DNI              { get; private set; } = "";
    public string ApellidosNombres { get; private set; } = "";
    public string? Division        { get; private set; }

    private readonly int _año;
    private TextBox txtDNI, txtNombre, txtDivision;
    private Button btnOk, btnCancelar;
    private Label lblTitulo, lblDNI, lblNombre, lblDivision, lblEstado;

    public FormAgregarCadete(int año)
    {
        _año = año;
        InitUI();
    }

    private void InitUI()
    {
        lblTitulo    = new Label   { Text = $"AGREGAR CADETE — {_año}° AÑO", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(30,60,120), Location = new Point(15,12), Size = new Size(340,26) };
        lblDNI       = new Label   { Text = "DNI:", Location = new Point(15,52), Size = new Size(35,20), Font = new Font("Segoe UI",9) };
        txtDNI       = new TextBox { Location = new Point(55,49), Size = new Size(150,25), Font = new Font("Segoe UI",10), MaxLength = 20 };
        lblNombre    = new Label   { Text = "Apellidos y Nombres:", Location = new Point(15,87), Size = new Size(150,20), Font = new Font("Segoe UI",9) };
        txtNombre    = new TextBox { Location = new Point(15,108), Size = new Size(350,25), Font = new Font("Segoe UI",10), MaxLength = 200, CharacterCasing = CharacterCasing.Upper };
        lblDivision  = new Label   { Text = "División (opcional):", Location = new Point(15,143), Size = new Size(140,20), Font = new Font("Segoe UI",9) };
        txtDivision  = new TextBox { Location = new Point(15,163), Size = new Size(60,25), Font = new Font("Segoe UI",10), MaxLength = 5 };
        lblEstado    = new Label   { Location = new Point(15,198), Size = new Size(350,20), Font = new Font("Segoe UI",9,FontStyle.Bold) };

        btnOk = new Button
        {
            Text = "✓  Agregar", Location = new Point(15,225), Size = new Size(120,34),
            Font = new Font("Segoe UI",9,FontStyle.Bold),
            BackColor = Color.FromArgb(30,120,60), ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand
        };
        btnOk.FlatAppearance.BorderSize = 0;

        btnCancelar = new Button
        {
            Text = "Cancelar", Location = new Point(145,225), Size = new Size(90,34),
            Font = new Font("Segoe UI",9), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand
        };

        btnOk.Click += (s, e) =>
        {
            var dni    = txtDNI.Text.Trim();
            var nombre = txtNombre.Text.Trim();
            if (string.IsNullOrEmpty(dni))   { lblEstado.ForeColor = Color.Red; lblEstado.Text = "⚠ Ingresa el DNI."; return; }
            if (string.IsNullOrEmpty(nombre)){ lblEstado.ForeColor = Color.Red; lblEstado.Text = "⚠ Ingresa el nombre."; return; }
            DNI              = dni;
            ApellidosNombres = nombre;
            Division         = string.IsNullOrWhiteSpace(txtDivision.Text) ? null : txtDivision.Text.Trim();
            DialogResult     = DialogResult.OK;
        };

        btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; };

        // Enter en nombre → aceptar
        txtNombre.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnOk.PerformClick(); } };

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(380, 275);
        FormBorderStyle     = FormBorderStyle.FixedDialog;
        MaximizeBox         = false; MinimizeBox = false;
        Text                = $"Agregar Cadete {_año}° Año";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        AcceptButton        = btnOk;
        CancelButton        = btnCancelar;

        Controls.AddRange(new Control[] { lblTitulo, lblDNI, txtDNI, lblNombre, txtNombre, lblDivision, txtDivision, lblEstado, btnOk, btnCancelar });
    }
}
