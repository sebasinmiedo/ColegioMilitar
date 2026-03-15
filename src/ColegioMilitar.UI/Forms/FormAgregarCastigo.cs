namespace ColegioMilitar.UI.Forms;

public class FormAgregarCastigo : Form
{
    public string Codigo      { get; private set; } = "";
    public string Descripcion { get; private set; } = "";
    public string PuntosIII   { get; private set; } = "0";
    public string PuntosIV    { get; private set; } = "0";
    public string PuntosV     { get; private set; } = "0";
    public int    Reincidencia { get; private set; }
    public string Nota        { get; private set; } = "";

    private TextBox txtCodigo, txtDescripcion, txtIII, txtIV, txtV, txtReinc, txtNota;
    private Button  btnOk, btnCancelar;
    private Label   lblTitulo, lblCodigo, lblDescripcion, lblIII, lblIV, lblV,
                    lblReinc, lblNota, lblHint, lblEstado;

    public FormAgregarCastigo() { InitUI(); }

    private void InitUI()
    {
        // ── Controles ────────────────────────────────────────────────────
        lblTitulo      = new Label   { Text = "AGREGAR CASTIGO", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(30, 60, 120), Location = new Point(15, 12), Size = new Size(420, 26) };

        lblCodigo      = new Label   { Text = "Código:", Location = new Point(15, 52), Size = new Size(55, 20), Font = new Font("Segoe UI", 9) };
        txtCodigo      = new TextBox { Location = new Point(75, 49), Size = new Size(80, 25), Font = new Font("Segoe UI", 10), MaxLength = 10, CharacterCasing = CharacterCasing.Upper };

        lblDescripcion = new Label   { Text = "Descripción del castigo:", Location = new Point(15, 87), Size = new Size(170, 20), Font = new Font("Segoe UI", 9) };
        txtDescripcion = new TextBox { Location = new Point(15, 108), Size = new Size(430, 25), Font = new Font("Segoe UI", 9), MaxLength = 500, CharacterCasing = CharacterCasing.Upper };

        // Puntos por año
        lblHint        = new Label   { Text = "Puntos por año (puede ser número o '1PV'):", Location = new Point(15, 146), Size = new Size(300, 18), Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(80, 80, 80) };

        lblIII         = new Label   { Text = "CAD III:", Location = new Point(15, 170), Size = new Size(55, 20), Font = new Font("Segoe UI", 9) };
        txtIII         = new TextBox { Location = new Point(73, 167), Size = new Size(60, 25), Font = new Font("Segoe UI", 10), MaxLength = 5, CharacterCasing = CharacterCasing.Upper };

        lblIV          = new Label   { Text = "CAD IV:", Location = new Point(148, 170), Size = new Size(55, 20), Font = new Font("Segoe UI", 9) };
        txtIV          = new TextBox { Location = new Point(206, 167), Size = new Size(60, 25), Font = new Font("Segoe UI", 10), MaxLength = 5, CharacterCasing = CharacterCasing.Upper };

        lblV           = new Label   { Text = "CAD V:", Location = new Point(281, 170), Size = new Size(50, 20), Font = new Font("Segoe UI", 9) };
        txtV           = new TextBox { Location = new Point(334, 167), Size = new Size(60, 25), Font = new Font("Segoe UI", 10), MaxLength = 5, CharacterCasing = CharacterCasing.Upper };

        lblReinc       = new Label   { Text = "Reincidencia:", Location = new Point(15, 207), Size = new Size(90, 20), Font = new Font("Segoe UI", 9) };
        txtReinc       = new TextBox { Location = new Point(108, 204), Size = new Size(60, 25), Font = new Font("Segoe UI", 10), MaxLength = 4, Text = "10" };

        lblNota        = new Label   { Text = "Nota:", Location = new Point(185, 207), Size = new Size(38, 20), Font = new Font("Segoe UI", 9) };
        txtNota        = new TextBox { Location = new Point(226, 204), Size = new Size(80, 25), Font = new Font("Segoe UI", 10), MaxLength = 20, CharacterCasing = CharacterCasing.Upper, Text = "DISL" };

        lblEstado      = new Label   { Location = new Point(15, 242), Size = new Size(430, 20), Font = new Font("Segoe UI", 9, FontStyle.Bold) };

        btnOk = new Button
        {
            Text = "✓  Agregar", Location = new Point(15, 270), Size = new Size(130, 34),
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            BackColor = Color.FromArgb(30, 120, 60), ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand
        };
        btnOk.FlatAppearance.BorderSize = 0;

        btnCancelar = new Button
        {
            Text = "Cancelar", Location = new Point(155, 270), Size = new Size(90, 34),
            Font = new Font("Segoe UI", 9), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand
        };

        // ── Valores por defecto ──────────────────────────────────────────
        txtIII.Text  = "7";
        txtIV.Text   = "8";
        txtV.Text    = "10";

        // ── Eventos ──────────────────────────────────────────────────────
        btnOk.Click += (s, e) =>
        {
            var codigo = txtCodigo.Text.Trim();
            var desc   = txtDescripcion.Text.Trim();
            var iii    = txtIII.Text.Trim();
            var iv     = txtIV.Text.Trim();
            var v      = txtV.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
            { lblEstado.ForeColor = Color.Red; lblEstado.Text = "⚠ Ingresa el código."; return; }
            if (string.IsNullOrEmpty(desc))
            { lblEstado.ForeColor = Color.Red; lblEstado.Text = "⚠ Ingresa la descripción."; return; }
            if (string.IsNullOrEmpty(iii) || string.IsNullOrEmpty(iv) || string.IsNullOrEmpty(v))
            { lblEstado.ForeColor = Color.Red; lblEstado.Text = "⚠ Ingresa los puntos para los 3 años."; return; }

            // Validar que sean número o "1PV"
            bool ValidarPunto(string val) =>
                int.TryParse(val, out _) || val.Equals("1PV", StringComparison.OrdinalIgnoreCase);

            if (!ValidarPunto(iii) || !ValidarPunto(iv) || !ValidarPunto(v))
            { lblEstado.ForeColor = Color.Red; lblEstado.Text = "⚠ Los puntos deben ser un número o '1PV'."; return; }

            int.TryParse(txtReinc.Text.Trim(), out int reinc);

            Codigo       = codigo;
            Descripcion  = desc;
            PuntosIII    = iii.ToUpper();
            PuntosIV     = iv.ToUpper();
            PuntosV      = v.ToUpper();
            Reincidencia = reinc;
            Nota         = string.IsNullOrWhiteSpace(txtNota.Text) ? "DISL" : txtNota.Text.Trim();
            DialogResult = DialogResult.OK;
        };

        btnCancelar.Click += (s, e) => DialogResult = DialogResult.Cancel;

        txtDescripcion.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtIII.Focus(); }
        };

        // ── Form ─────────────────────────────────────────────────────────
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode       = AutoScaleMode.Font;
        ClientSize          = new Size(460, 320);
        FormBorderStyle     = FormBorderStyle.FixedDialog;
        MaximizeBox         = false;
        MinimizeBox         = false;
        Text                = "Agregar Castigo";
        Font                = new Font("Segoe UI", 9F);
        StartPosition       = FormStartPosition.CenterParent;
        AcceptButton        = btnOk;
        CancelButton        = btnCancelar;

        Controls.AddRange(new Control[]
        {
            lblTitulo, lblCodigo, txtCodigo, lblDescripcion, txtDescripcion,
            lblHint, lblIII, txtIII, lblIV, txtIV, lblV, txtV,
            lblReinc, txtReinc, lblNota, txtNota,
            lblEstado, btnOk, btnCancelar
        });
    }
}
