using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI.Forms;

public partial class FormMantenimiento : Form
{
    private bool _modoEdicionCadetes3 = false;
    private bool _modoEdicionCadetes4 = false;
    private bool _modoEdicionCadetes5 = false;
    private bool _modoEdicionSupervisores = false;
    private bool _modoEdicionCastigos = false;

    public FormMantenimiento() { InitializeComponent(); }

    private async void FormMantenimiento_Load(object sender, EventArgs e)
    {
        await RefrescarCadetes3Async();
        await RefrescarCadetes4Async();
        await RefrescarCadetes5Async();
        await RefrescarSupervisoresAsync();
        await RefrescarCastigosAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  HELPERS GENÉRICOS
    // ════════════════════════════════════════════════════════════════════════

    private static void ToggleEdicion(DataGridView dgv, Button btnEditar,
    Button btnGuardar, Button btnCancelar,
    Button btnAgregar, Button btnEliminar,
    ref bool modoEdicion, bool activar)
    {
        modoEdicion = activar;
        dgv.ReadOnly = !activar;
        dgv.AllowUserToAddRows = activar;
        dgv.AllowUserToDeleteRows = activar;
        btnEditar.Enabled = !activar;
        btnGuardar.Visible = activar;
        btnCancelar.Visible = activar;
        btnAgregar.Visible = activar;
        btnEliminar.Visible = activar;
        dgv.BackgroundColor = activar
            ? Color.FromArgb(255, 255, 220)
            : Color.White;
    }

    // ════════════════════════════════════════════════════════════════════════
    //  CADETES 3° AÑO
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarCadetes3Async() =>
        await RefrescarCadetesEnDgv(dgvCadetes3, 3, lblEstadoCadetes3);

    private async Task RefrescarCadetesEnDgv(DataGridView dgv, int año, Label lbl)
    {
        var lista = (await Program.Cadetes.GetByAñoAsync(año)).ToList();

        // Desactivar AutoSize global antes de asignar datos
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        dgv.DataSource = lista.Select((c, i) => new
        {
            N = i + 1,
            c.DNI,
            ApellidosNombres = c.ApellidosNombres,
            Division = c.Division ?? ""
        }).ToList();

        // Ahora sí existen las columnas — configurar por índice
        if (dgv.Columns.Count < 4) return;

        dgv.Columns[0].Width = 40;
        dgv.Columns[0].ReadOnly = true;
        dgv.Columns[0].HeaderText = "N°";

        dgv.Columns[1].Width = 90;
        dgv.Columns[1].HeaderText = "DNI";

        dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dgv.Columns[2].HeaderText = "APELLIDOS Y NOMBRES";

        dgv.Columns[3].Width = 70;
        dgv.Columns[3].HeaderText = "DIVISIÓN";
    }

    private async void btnCargarCsv3_Click(object sender, EventArgs e) =>
        await CargarCsvCadetesAsync(3, lblEstadoCadetes3);

    private void btnEditarCadetes3_Click(object sender, EventArgs e) =>
    ToggleEdicion(dgvCadetes3, btnEditarCadetes3, btnGuardarCadetes3,
        btnCancelarCadetes3, btnAgregarCadetes3, btnEliminarCadetes3,
        ref _modoEdicionCadetes3, true);
    private async void btnGuardarCadetes3_Click(object sender, EventArgs e) =>
        await GuardarCambiosCadetesAsync(dgvCadetes3, 3, btnEditarCadetes3,
            btnGuardarCadetes3, btnCancelarCadetes3, lblEstadoCadetes3);

    private async void btnCancelarCadetes3_Click(object sender, EventArgs e)
    {
        ToggleEdicion(dgvCadetes3, btnEditarCadetes3, btnGuardarCadetes3,
            btnCancelarCadetes3, btnAgregarCadetes3, btnEliminarCadetes3,
            ref _modoEdicionCadetes3, false);
        await RefrescarCadetes3Async();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  CADETES 4° AÑO
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarCadetes4Async() =>
        await RefrescarCadetesEnDgv(dgvCadetes4, 4, lblEstadoCadetes4);

    private async void btnCargarCsv4_Click(object sender, EventArgs e) =>
        await CargarCsvCadetesAsync(4, lblEstadoCadetes4);

    private void btnEditarCadetes4_Click(object sender, EventArgs e) =>
    ToggleEdicion(dgvCadetes4, btnEditarCadetes4, btnGuardarCadetes4,
        btnCancelarCadetes4, btnAgregarCadetes4, btnEliminarCadetes4,
        ref _modoEdicionCadetes4, true);

    private async void btnGuardarCadetes4_Click(object sender, EventArgs e) =>
        await GuardarCambiosCadetesAsync(dgvCadetes4, 4, btnEditarCadetes4,
            btnGuardarCadetes4, btnCancelarCadetes4, lblEstadoCadetes4);

    private async void btnCancelarCadetes4_Click(object sender, EventArgs e)
    {
        ToggleEdicion(dgvCadetes4, btnEditarCadetes4, btnGuardarCadetes4,
            btnCancelarCadetes4, btnAgregarCadetes4, btnEliminarCadetes4,
            ref _modoEdicionCadetes4, false);
        await RefrescarCadetes4Async();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  CADETES 5° AÑO
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarCadetes5Async() =>
        await RefrescarCadetesEnDgv(dgvCadetes5, 5, lblEstadoCadetes5);

    private async void btnCargarCsv5_Click(object sender, EventArgs e) =>
        await CargarCsvCadetesAsync(5, lblEstadoCadetes5);

    private void btnEditarCadetes5_Click(object sender, EventArgs e) =>
    ToggleEdicion(dgvCadetes5, btnEditarCadetes5, btnGuardarCadetes5,
        btnCancelarCadetes5, btnAgregarCadetes5, btnEliminarCadetes5,
        ref _modoEdicionCadetes5, true);

    private async void btnGuardarCadetes5_Click(object sender, EventArgs e) =>
        await GuardarCambiosCadetesAsync(dgvCadetes5, 5, btnEditarCadetes5,
            btnGuardarCadetes5, btnCancelarCadetes5, lblEstadoCadetes5);

    private async void btnCancelarCadetes5_Click(object sender, EventArgs e)
    {
        ToggleEdicion(dgvCadetes5, btnEditarCadetes5, btnGuardarCadetes5,
            btnCancelarCadetes5, btnAgregarCadetes5, btnEliminarCadetes5,
            ref _modoEdicionCadetes5, false);
        await RefrescarCadetes5Async();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  LÓGICA COMPARTIDA CADETES
    // ════════════════════════════════════════════════════════════════════════

    private async Task CargarCsvCadetesAsync(int año, Label lblEstado)
    {
        using var dlg = new OpenFileDialog
            { Title = $"CSV Cadetes {año}° Año", Filter = "CSV|*.csv|Todos|*.*" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int ok = 0, skip = 0, err = 0;
        foreach (var linea in (await File.ReadAllLinesAsync(dlg.FileName)).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var cols = linea.Split(new[] { ';', ',' });
            if (cols.Length < 3) continue;
            try
            {
                var dni    = cols[1].Trim().Trim('"');
                var nombre = cols[2].Trim().Trim('"');
                var div    = cols.Length > 3 ? cols[3].Trim().Trim('"') : null;
                if (string.IsNullOrEmpty(dni)) continue;

                if (await Program.Cadetes.GetByIdAsync(dni) is null)
                {
                    await Program.Cadetes.AddAsync(
                        new Cadete { DNI = dni, ApellidosNombres = nombre, Año = año, Division = div });
                    ok++;
                }
                else skip++;
            }
            catch { err++; }
        }

        lblEstado.ForeColor = ok > 0 ? Color.DarkGreen : Color.DarkOrange;
        lblEstado.Text = $"✓ {ok} agregados  |  {skip} ya existían  |  {err} errores";
        await RefrescarCadetesEnDgv(
            año == 3 ? dgvCadetes3 : año == 4 ? dgvCadetes4 : dgvCadetes5, año, lblEstado);
    }

    private async Task GuardarCambiosCadetesAsync(DataGridView dgv, int año,
    Button btnEditar, Button btnGuardar, Button btnCancelar,
    Label lblEstado)
    {
        int ok = 0, err = 0;
        var existentes = (await Program.Cadetes.GetByAñoAsync(año)).ToList();

        var dnisEnGrilla = new HashSet<string>();
        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow) continue;
            var dni = row.Cells["DNI"].Value?.ToString()?.Trim();
            if (!string.IsNullOrEmpty(dni)) dnisEnGrilla.Add(dni);
        }
        foreach (var c in existentes.Where(c => !dnisEnGrilla.Contains(c.DNI)))
            try { await Program.Cadetes.DeleteAsync(c.DNI); } catch { }

        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow) continue;
            try
            {
                var dni = row.Cells["DNI"].Value?.ToString()?.Trim() ?? "";
                var nombre = row.Cells["ApellidosNombres"].Value?.ToString()?.Trim() ?? "";
                var div = row.Cells["Division"].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(nombre)) continue;

                var existente = await Program.Cadetes.GetByIdAsync(dni);
                if (existente is null)
                    await Program.Cadetes.AddAsync(
                        new Cadete { DNI = dni, ApellidosNombres = nombre, Año = año, Division = div });
                else
                {
                    existente.ApellidosNombres = nombre;
                    existente.Division = div;
                    await Program.Cadetes.UpdateAsync(existente);
                }
                ok++;
            }
            catch { err++; }
        }

        lblEstado.ForeColor = Color.DarkGreen;
        lblEstado.Text = $"✓ {ok} guardados.{(err > 0 ? $" {err} errores." : "")}";
        // Desactivar modo edición manualmente
        dgv.ReadOnly = true;
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.BackgroundColor = Color.White;
        btnEditar.Enabled = true;
        btnGuardar.Visible = false;
        btnCancelar.Visible = false;
        await RefrescarCadetesEnDgv(dgv, año, lblEstado);
    }

    // ════════════════════════════════════════════════════════════════════════
    //  SUPERVISORES
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarSupervisoresAsync()
    {
        var lista = (await Program.Supervisores.GetAllAsync()).ToList();

        dgvSupervisores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        dgvSupervisores.DataSource = lista.Select((s, i) => new
        {
            N = i + 1,
            s.DNI,
            s.Grado,
            s.ApellidosNombres
        }).ToList();

        if (dgvSupervisores.Columns.Count < 4) return;

        dgvSupervisores.Columns[0].Width = 40;
        dgvSupervisores.Columns[0].ReadOnly = true;
        dgvSupervisores.Columns[0].HeaderText = "N°";

        dgvSupervisores.Columns[1].Width = 90;
        dgvSupervisores.Columns[1].HeaderText = "DNI";

        dgvSupervisores.Columns[2].Width = 90;
        dgvSupervisores.Columns[2].HeaderText = "GRADO";

        dgvSupervisores.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dgvSupervisores.Columns[3].HeaderText = "APELLIDOS Y NOMBRES";
    }

    private async void btnCargarCsvSupervisores_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog { Title = "CSV Supervisores", Filter = "CSV|*.csv|Todos|*.*" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int ok = 0, skip = 0, err = 0;
        foreach (var linea in (await File.ReadAllLinesAsync(dlg.FileName)).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var cols = linea.Split(new[] { ';', ',' });
            if (cols.Length < 3) continue;
            try
            {
                var grado  = cols[0].Trim().Trim('"');
                var nombre = cols[1].Trim().Trim('"');
                var dni    = cols[2].Trim().Trim('"');
                if (string.IsNullOrEmpty(dni)) continue;

                if (await Program.Supervisores.GetByIdAsync(dni) is null)
                {
                    await Program.Supervisores.AddAsync(
                        new Supervisor { DNI = dni, Grado = grado, ApellidosNombres = nombre });
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

    private void btnEditarSupervisores_Click(object sender, EventArgs e) =>
    ToggleEdicion(dgvSupervisores, btnEditarSupervisores, btnGuardarSupervisores,
        btnCancelarSupervisores, btnAgregarSupervisores, btnEliminarSupervisores,
        ref _modoEdicionSupervisores, true);

    private async void btnGuardarSupervisores_Click(object sender, EventArgs e)
    {
        var existentes = (await Program.Supervisores.GetAllAsync()).ToList();
        var dnisEnGrilla = new HashSet<string>();
        foreach (DataGridViewRow row in dgvSupervisores.Rows)
        {
            if (row.IsNewRow) continue;
            var d = row.Cells["DNI"].Value?.ToString()?.Trim();
            if (!string.IsNullOrEmpty(d)) dnisEnGrilla.Add(d);
        }
        foreach (var s in existentes.Where(s => !dnisEnGrilla.Contains(s.DNI)))
            try { await Program.Supervisores.DeleteAsync(s.DNI); } catch { }

        foreach (DataGridViewRow row in dgvSupervisores.Rows)
        {
            if (row.IsNewRow) continue;
            try
            {
                var dni    = row.Cells["DNI"].Value?.ToString()?.Trim() ?? "";
                var grado  = row.Cells["Grado"].Value?.ToString()?.Trim() ?? "";
                var nombre = row.Cells["ApellidosNombres"].Value?.ToString()?.Trim() ?? "";
                if (string.IsNullOrEmpty(dni)) continue;

                var ex = await Program.Supervisores.GetByIdAsync(dni);
                if (ex is null)
                    await Program.Supervisores.AddAsync(
                        new Supervisor { DNI = dni, Grado = grado, ApellidosNombres = nombre });
                else { ex.Grado = grado; ex.ApellidosNombres = nombre;
                       await Program.Supervisores.UpdateAsync(ex); }
            }
            catch { }
        }
        ToggleEdicion(dgvSupervisores, btnEditarSupervisores, btnGuardarSupervisores,
            btnCancelarSupervisores, ref _modoEdicionSupervisores, false);
        lblEstadoSupervisores.ForeColor = Color.DarkGreen;
        lblEstadoSupervisores.Text = "✓ Cambios guardados.";
        await RefrescarSupervisoresAsync();
    }

    private async void btnCancelarSupervisores_Click(object sender, EventArgs e)
    {
        ToggleEdicion(dgvSupervisores, btnEditarSupervisores, btnGuardarSupervisores,
            btnCancelarSupervisores, btnAgregarSupervisores, btnEliminarSupervisores,
            ref _modoEdicionSupervisores, false);
        await RefrescarSupervisoresAsync();
    }

    // ════════════════════════════════════════════════════════════════════════
    //  CASTIGOS
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarCastigosAsync()
    {
        var lista = (await Program.Castigos.GetAllAsync()).ToList();

        dgvCastigos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        dgvCastigos.DataSource = lista.Select((c, i) => new
        {
            N = i + 1,
            c.Codigo,
            c.Descripcion,
            III = c.PuntosAño3Raw,
            IV = c.PuntosAño4Raw,
            V = c.PuntosAño5Raw,
            Reinc = c.Reincidencia,
            c.Nota
        }).ToList();

        if (dgvCastigos.Columns.Count < 8) return;

        dgvCastigos.Columns[0].Width = 40;
        dgvCastigos.Columns[0].ReadOnly = true;
        dgvCastigos.Columns[0].HeaderText = "N°";

        dgvCastigos.Columns[1].Width = 70;
        dgvCastigos.Columns[1].HeaderText = "CÓDIGO";

        dgvCastigos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dgvCastigos.Columns[2].HeaderText = "DESCRIPCIÓN";

        dgvCastigos.Columns[3].Width = 55;
        dgvCastigos.Columns[3].HeaderText = "CAD III";

        dgvCastigos.Columns[4].Width = 55;
        dgvCastigos.Columns[4].HeaderText = "CAD IV";

        dgvCastigos.Columns[5].Width = 55;
        dgvCastigos.Columns[5].HeaderText = "CAD V";

        dgvCastigos.Columns[6].Width = 55;
        dgvCastigos.Columns[6].HeaderText = "REINC";

        dgvCastigos.Columns[7].Width = 55;
        dgvCastigos.Columns[7].HeaderText = "NOTA";
    }

    private async void btnCargarCsvCastigos_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog { Title = "CSV Castigos", Filter = "CSV|*.csv|Todos|*.*" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        int ok = 0, skip = 0, err = 0;
        foreach (var linea in (await File.ReadAllLinesAsync(dlg.FileName)).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;
            var cols = linea.Split(new[] { ';', ',' });
            if (cols.Length < 6) continue;
            try
            {
                var codigo = cols[0].Trim().Trim('"');
                var p3 = cols[1].Trim().Trim('"');
                var p4 = cols[2].Trim().Trim('"');
                var p5 = cols[3].Trim().Trim('"');
                int.TryParse(cols[4].Trim(), out int reinc);
                var nota = cols[5].Trim().Trim('"');
                var desc = cols.Length > 6
                    ? string.Join(",", cols.Skip(6)).Trim().Trim('"') : "";
                if (string.IsNullOrEmpty(codigo)) continue;

                if (await Program.Castigos.GetByIdAsync(codigo) is null)
                {
                    await Program.Castigos.AddAsync(new Domain.Entities.Castigo
                    {
                        Codigo = codigo, Descripcion = desc,
                        PuntosAño3Raw = p3, PuntosAño4Raw = p4, PuntosAño5Raw = p5,
                        Reincidencia = reinc, Nota = nota
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

    private void btnEditarCastigos_Click(object sender, EventArgs e) =>
    ToggleEdicion(dgvCastigos, btnEditarCastigos, btnGuardarCastigos,
        btnCancelarCastigos, btnAgregarCastigos, btnEliminarCastigos,
        ref _modoEdicionCastigos, true);

    private async void btnCancelarCastigos_Click(object sender, EventArgs e)
    {
        ToggleEdicion(dgvCastigos, btnEditarCastigos, btnGuardarCastigos,
            btnCancelarCastigos, btnAgregarCastigos, btnEliminarCastigos,
            ref _modoEdicionCastigos, false);
        await RefrescarCastigosAsync();
    }

    private async void btnGuardarCastigos_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in dgvCastigos.Rows)
        {
            if (row.IsNewRow) continue;
            try
            {
                var codigo = row.Cells["Codigo"].Value?.ToString()?.Trim() ?? "";
                if (string.IsNullOrEmpty(codigo)) continue;
                var desc  = row.Cells["Descripcion"].Value?.ToString()?.Trim() ?? "";
                var p3    = row.Cells["III"].Value?.ToString()?.Trim() ?? "0";
                var p4    = row.Cells["IV"].Value?.ToString()?.Trim() ?? "0";
                var p5    = row.Cells["V"].Value?.ToString()?.Trim() ?? "0";
                int.TryParse(row.Cells["Reinc"].Value?.ToString(), out int reinc);
                var nota  = row.Cells["Nota"].Value?.ToString()?.Trim();

                var ex = await Program.Castigos.GetByIdAsync(codigo);
                if (ex is null)
                    await Program.Castigos.AddAsync(new Domain.Entities.Castigo
                    {
                        Codigo = codigo, Descripcion = desc,
                        PuntosAño3Raw = p3, PuntosAño4Raw = p4, PuntosAño5Raw = p5,
                        Reincidencia = reinc, Nota = nota
                    });
                else
                {
                    ex.Descripcion = desc; ex.PuntosAño3Raw = p3;
                    ex.PuntosAño4Raw = p4; ex.PuntosAño5Raw = p5;
                    ex.Reincidencia = reinc; ex.Nota = nota;
                    await Program.Castigos.UpdateAsync(ex);
                }
            }
            catch { }
        }
        ToggleEdicion(dgvCastigos, btnEditarCastigos, btnGuardarCastigos,
            btnCancelarCastigos, ref _modoEdicionCastigos, false);
        lblEstadoCastigos.ForeColor = Color.DarkGreen;
        lblEstadoCastigos.Text = "✓ Cambios guardados.";
        await RefrescarCastigosAsync();
    }
}
