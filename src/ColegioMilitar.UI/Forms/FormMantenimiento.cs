using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI.Forms;

public partial class FormMantenimiento : Form
{
    public FormMantenimiento() { InitializeComponent(); }

    private async void FormMantenimiento_Load(object sender, EventArgs e)
    {
        await RefrescarCadetesAsync();
        await RefrescarSupervisoresAsync();
        await RefrescarCastigosAsync();
    }

    // ── Cadetes ───────────────────────────────────────────────────────────

    private async Task RefrescarCadetesAsync()
    {
        var lista = await Program.Cadetes.GetAllAsync();
        dgvCadetes.DataSource = lista.Select((c, i) => new
        {
            N = i + 1, c.DNI, c.ApellidosNombres, Año = $"{c.Año}°", Division = c.Division ?? "-"
        }).ToList();
    }

    private async void btnCargarCsv3_Click(object sender, EventArgs e) =>
        await CargarCsvCadetesAsync(3, lblEstadoCadetes);

    private async void btnCargarCsv4_Click(object sender, EventArgs e) =>
        await CargarCsvCadetesAsync(4, lblEstadoCadetes);

    private async void btnCargarCsv5_Click(object sender, EventArgs e) =>
        await CargarCsvCadetesAsync(5, lblEstadoCadetes);

    private async Task CargarCsvCadetesAsync(int año, Label lblEstado)
    {
        using var dlg = new OpenFileDialog
        {
            Title  = $"CSV de Cadetes — {año}° Año",
            Filter = "CSV|*.csv|Todos|*.*"
        };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int ok = 0, skip = 0, err = 0;
        var lineas = await File.ReadAllLinesAsync(dlg.FileName);

        foreach (var linea in lineas.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var cols = linea.Split(new char[] { ',', ';' }, StringSplitOptions.None);
            if (cols.Length < 3) continue;
            try
            {
                // Formato: N°, CODIGO(DNI), APELLIDOS Y NOMBRES, [DIVISION]
                var dni    = cols[1].Trim().Trim('"');
                var nombre = cols[2].Trim().Trim('"');
                var div    = cols.Length > 3 ? cols[3].Trim().Trim('"') : null;

                if (string.IsNullOrEmpty(dni)) continue;

                var existente = await Program.Cadetes.GetByIdAsync(dni);
                if (existente is null)
                {
                    await Program.Cadetes.AddAsync(new Cadete
                        { DNI = dni, ApellidosNombres = nombre, Año = año, Division = div });
                    ok++;
                }
                else skip++;
            }
            catch { err++; }
        }

        lblEstado.ForeColor = ok > 0 ? Color.DarkGreen : Color.DarkOrange;
        lblEstado.Text = $"✓ {ok} agregados  |  {skip} ya existían  |  {err} errores  (año {año}°)";
        await RefrescarCadetesAsync();
    }

    // ── Supervisores ──────────────────────────────────────────────────────

    private async Task RefrescarSupervisoresAsync()
    {
        var lista = await Program.Supervisores.GetAllAsync();
        dgvSupervisores.DataSource = lista.Select((s, i) => new
        {
            N = i + 1, s.DNI, s.Grado, s.ApellidosNombres
        }).ToList();
    }

    private async void btnCargarCsvSupervisores_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog { Title = "CSV de Supervisores", Filter = "CSV|*.csv|Todos|*.*" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int ok = 0, skip = 0, err = 0;
        var lineas = await File.ReadAllLinesAsync(dlg.FileName);

        foreach (var linea in lineas.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var cols = linea.Split(new char[] { ',', ';' }, StringSplitOptions.None);
            if (cols.Length < 3) continue;
            try
            {
                // Formato: GRADO, APELLIDOS Y NOMBRES, DNI
                var grado  = cols[0].Trim().Trim('"');
                var nombre = cols[1].Trim().Trim('"');
                var dni    = cols[2].Trim().Trim('"');
                if (string.IsNullOrEmpty(dni)) continue;

                var existente = await Program.Supervisores.GetByIdAsync(dni);
                if (existente is null)
                {
                    await Program.Supervisores.AddAsync(new Supervisor
                        { DNI = dni, Grado = grado, ApellidosNombres = nombre });
                    ok++;
                }
                else skip++;
            }
            catch { err++; }
        }

        lblEstadoSupervisores.ForeColor = ok > 0 ? Color.DarkGreen : Color.DarkOrange;
        lblEstadoSupervisores.Text = $"✓ {ok} agregados  |  {skip} ya existían  |  {err} errores";
        await RefrescarSupervisoresAsync();
    }

    // ── Castigos ──────────────────────────────────────────────────────────

    private async Task RefrescarCastigosAsync()
    {
        var lista = await Program.Castigos.GetAllAsync();
        dgvCastigos.DataSource = lista.Select((c, i) => new
        {
            N = i + 1, c.Codigo, c.Descripcion,
            III = c.PuntosAño3Raw, IV = c.PuntosAño4Raw, V = c.PuntosAño5Raw,
            Reinc = c.Reincidencia, c.Nota
        }).ToList();
    }

    private async void btnCargarCsvCastigos_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog { Title = "CSV de Castigos", Filter = "CSV|*.csv|Todos|*.*" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int ok = 0, skip = 0, err = 0;
        var lineas = await File.ReadAllLinesAsync(dlg.FileName);

        foreach (var linea in lineas.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var cols = linea.Split(new char[] { ',', ';' }, StringSplitOptions.None);
            if (cols.Length < 6) continue;
            try
            {
                // Formato: CODIGO, CAD III, CAD IV, CAD V, reinc, NOTA, DESCRIPCION
                var codigo = cols[0].Trim().Trim('"');
                var p3     = cols[1].Trim().Trim('"');
                var p4     = cols[2].Trim().Trim('"');
                var p5     = cols[3].Trim().Trim('"');
                int.TryParse(cols[4].Trim(), out int reinc);
                var nota   = cols[5].Trim().Trim('"');
                // Descripción puede contener comas — unir columnas restantes
                var desc   = cols.Length > 6
                    ? string.Join(",", cols.Skip(6)).Trim().Trim('"')
                    : "";

                if (string.IsNullOrEmpty(codigo)) continue;

                var existente = await Program.Castigos.GetByIdAsync(codigo);
                if (existente is null)
                {
                    await Program.Castigos.AddAsync(new Castigo
                    {
                        Codigo        = codigo,
                        Descripcion   = desc,
                        PuntosAño3Raw = p3,
                        PuntosAño4Raw = p4,
                        PuntosAño5Raw = p5,
                        Reincidencia  = reinc,
                        Nota          = nota
                    });
                    ok++;
                }
                else skip++;
            }
            catch { err++; }
        }

        lblEstadoCastigos.ForeColor = ok > 0 ? Color.DarkGreen : Color.DarkOrange;
        lblEstadoCastigos.Text = $"✓ {ok} agregados  |  {skip} ya existían  |  {err} errores";
        await RefrescarCastigosAsync();
    }
}
