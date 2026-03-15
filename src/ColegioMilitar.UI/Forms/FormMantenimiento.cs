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

        if (btnAgregar is not null) btnAgregar.Visible = activar;
        if (btnEliminar is not null) btnEliminar.Visible = activar;

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

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        var bindingList = new System.ComponentModel.BindingList<CadeteRow>(
            lista.Select((c, i) => new CadeteRow
            {
                N = i + 1,
                DNI = c.DNI,
                ApellidosNombres = c.ApellidosNombres,
                Division = c.Division ?? ""
            }).ToList());

        dgv.DataSource = bindingList;

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
        btnGuardarCadetes3, btnCancelarCadetes3,
        btnAgregarCadetes3, btnEliminarCadetes3, lblEstadoCadetes3);

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
        btnGuardarCadetes4, btnCancelarCadetes4,
        btnAgregarCadetes4, btnEliminarCadetes4, lblEstadoCadetes4);

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
        btnGuardarCadetes5, btnCancelarCadetes5,
        btnAgregarCadetes5, btnEliminarCadetes5, lblEstadoCadetes5);

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
    Button btnAgregar, Button btnEliminar, Label lblEstado)
    {
        int ok = 0, err = 0;

        if (dgv.DataSource is not System.ComponentModel.BindingList<CadeteRow> bindingList)
        { lblEstado.ForeColor = Color.Red; lblEstado.Text = "Error: DataSource inválido."; return; }

        // Forzar commit de la celda activa
        dgv.EndEdit();

        var existentes = (await Program.Cadetes.GetByAñoAsync(año)).ToList();
        var dnisEnLista = bindingList.Select(r => r.DNI.Trim()).ToHashSet();

        // Eliminar los que ya no están
        foreach (var c in existentes.Where(c => !dnisEnLista.Contains(c.DNI)))
            try { await Program.Cadetes.DeleteAsync(c.DNI); } catch { }

        // Agregar o actualizar
        foreach (var row in bindingList)
        {
            var dni = row.DNI.Trim();
            var nombre = row.ApellidosNombres.Trim();
            var div = string.IsNullOrWhiteSpace(row.Division) ? null : row.Division.Trim();
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(nombre)) continue;
            try
            {
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
        dgv.ReadOnly = true;
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.BackgroundColor = Color.White;
        btnEditar.Enabled = true;
        btnGuardar.Visible = false;
        btnCancelar.Visible = false;
        btnAgregar.Visible = false;
        btnEliminar.Visible = false;
        await RefrescarCadetesEnDgv(dgv, año, lblEstado);
    }

    // ════════════════════════════════════════════════════════════════════════
    //  SUPERVISORES
    // ════════════════════════════════════════════════════════════════════════

    private async Task RefrescarSupervisoresAsync()
    {
        var lista = (await Program.Supervisores.GetAllAsync()).ToList();

        dgvSupervisores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        var bindingList = new System.ComponentModel.BindingList<SupervisorRow>(
            lista.Select((s, i) => new SupervisorRow
            {
                N = i + 1,
                DNI = s.DNI,
                Grado = s.Grado,
                ApellidosNombres = s.ApellidosNombres
            }).ToList());

        dgvSupervisores.DataSource = bindingList;

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
        if (dgvSupervisores.DataSource is not System.ComponentModel.BindingList<SupervisorRow> bindingList)
        { lblEstadoSupervisores.ForeColor = Color.Red; lblEstadoSupervisores.Text = "Error: DataSource inválido."; return; }

        dgvSupervisores.EndEdit();

        var existentes = (await Program.Supervisores.GetAllAsync()).ToList();
        var dnisEnLista = bindingList.Select(r => r.DNI.Trim()).ToHashSet();

        foreach (var s in existentes.Where(s => !dnisEnLista.Contains(s.DNI)))
            try { await Program.Supervisores.DeleteAsync(s.DNI); } catch { }

        foreach (var row in bindingList)
        {
            var dni = row.DNI.Trim();
            var grado = row.Grado.Trim();
            var nombre = row.ApellidosNombres.Trim();
            if (string.IsNullOrEmpty(dni)) continue;
            try
            {
                var ex = await Program.Supervisores.GetByIdAsync(dni);
                if (ex is null)
                    await Program.Supervisores.AddAsync(
                        new Supervisor { DNI = dni, Grado = grado, ApellidosNombres = nombre });
                else
                {
                    ex.Grado = grado; ex.ApellidosNombres = nombre;
                    await Program.Supervisores.UpdateAsync(ex);
                }
            }
            catch { }
        }

        ToggleEdicion(dgvSupervisores, btnEditarSupervisores, btnGuardarSupervisores,
            btnCancelarSupervisores, btnAgregarSupervisores, btnEliminarSupervisores,
            ref _modoEdicionSupervisores, false);
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

        var bindingList = new System.ComponentModel.BindingList<CastigoRow>(
            lista.Select((c, i) => new CastigoRow
            {
                N = i + 1,
                Codigo = c.Codigo,
                Descripcion = c.Descripcion,
                III = c.PuntosAño3Raw,
                IV = c.PuntosAño4Raw,
                V = c.PuntosAño5Raw,
                Reinc = c.Reincidencia,
                Nota = c.Nota ?? ""
            }).ToList());

        dgvCastigos.DataSource = bindingList;

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
        if (dgvCastigos.DataSource is not System.ComponentModel.BindingList<CastigoRow> bindingList)
        { lblEstadoCastigos.ForeColor = Color.Red; lblEstadoCastigos.Text = "Error: DataSource inválido."; return; }

        dgvCastigos.EndEdit();

        foreach (var row in bindingList)
        {
            var codigo = row.Codigo.Trim();
            if (string.IsNullOrEmpty(codigo)) continue;
            try
            {
                var ex = await Program.Castigos.GetByIdAsync(codigo);
                if (ex is null)
                    await Program.Castigos.AddAsync(new Domain.Entities.Castigo
                    {
                        Codigo = codigo,
                        Descripcion = row.Descripcion.Trim(),
                        PuntosAño3Raw = row.III.Trim(),
                        PuntosAño4Raw = row.IV.Trim(),
                        PuntosAño5Raw = row.V.Trim(),
                        Reincidencia = row.Reinc,
                        Nota = string.IsNullOrWhiteSpace(row.Nota) ? null : row.Nota.Trim()
                    });
                else
                {
                    ex.Descripcion = row.Descripcion.Trim();
                    ex.PuntosAño3Raw = row.III.Trim();
                    ex.PuntosAño4Raw = row.IV.Trim();
                    ex.PuntosAño5Raw = row.V.Trim();
                    ex.Reincidencia = row.Reinc;
                    ex.Nota = string.IsNullOrWhiteSpace(row.Nota) ? null : row.Nota.Trim();
                    await Program.Castigos.UpdateAsync(ex);
                }
            }
            catch { }
        }

        ToggleEdicion(dgvCastigos, btnEditarCastigos, btnGuardarCastigos,
            btnCancelarCastigos, btnAgregarCastigos, btnEliminarCastigos,
            ref _modoEdicionCastigos, false);
        lblEstadoCastigos.ForeColor = Color.DarkGreen;
        lblEstadoCastigos.Text = "✓ Cambios guardados.";
        await RefrescarCastigosAsync();
    }


    // ════════════════════════════════════════════════════════════════════════
    //  AGREGAR / ELIMINAR FILAS
    // ════════════════════════════════════════════════════════════════════════

    public void AgregarFilaCadetes(int año, Label lblEstado)
    {
        using var form = new FormAgregarCadete(año);
        if (form.ShowDialog() != DialogResult.OK) return;
        Task.Run(async () =>
        {
            try
            {
                var existente = await Program.Cadetes.GetByIdAsync(form.DNI);
                if (existente is not null)
                { Invoke(() => { lblEstado.ForeColor = Color.DarkOrange; lblEstado.Text = $"⚠ Ya existe un cadete con DNI {form.DNI}"; }); return; }
                await Program.Cadetes.AddAsync(new Cadete
                { DNI = form.DNI, ApellidosNombres = form.ApellidosNombres, Año = año, Division = form.Division });
                var dgv = año == 3 ? dgvCadetes3 : año == 4 ? dgvCadetes4 : dgvCadetes5;
                Invoke(async () =>
                {
                    lblEstado.ForeColor = Color.DarkGreen;
                    lblEstado.Text = $"✓ {form.ApellidosNombres} agregado.";
                    await RefrescarCadetesEnDgv(dgv, año, lblEstado);
                });
            }
            catch (Exception ex) { Invoke(() => { lblEstado.ForeColor = Color.Red; lblEstado.Text = $"✗ {ex.Message}"; }); }
        });
    }

    public void EliminarCadeteSeleccionado(DataGridView dgv, int año, Label lblEstado)
    {
        if (dgv.CurrentRow is null) return;
        var dni = dgv.CurrentRow.Cells[1].Value?.ToString() ?? "";
        var nombre = dgv.CurrentRow.Cells[2].Value?.ToString() ?? "";
        if (string.IsNullOrEmpty(dni)) return;

        if (MessageBox.Show($"¿Eliminar a '{nombre}'?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

        Task.Run(async () =>
        {
            try
            {
                await Program.Cadetes.DeleteAsync(dni);
                Invoke(async () =>
                {
                    lblEstado.ForeColor = Color.DarkGreen;
                    lblEstado.Text = $"✓ '{nombre}' eliminado.";
                    await RefrescarCadetesEnDgv(dgv, año, lblEstado);
                });
            }
            catch (Exception ex) { Invoke(() => { lblEstado.ForeColor = Color.Red; lblEstado.Text = $"✗ {ex.Message}"; }); }
        });
    }

    public void AgregarSupervisor(Label lblEstado)
    {
        using var form = new FormAgregarSupervisor();
        if (form.ShowDialog() != DialogResult.OK) return;
        Task.Run(async () =>
        {
            try
            {
                var existente = await Program.Supervisores.GetByIdAsync(form.DNI);
                if (existente is not null)
                { Invoke(() => { lblEstado.ForeColor = Color.DarkOrange; lblEstado.Text = $"⚠ Ya existe supervisor con DNI {form.DNI}"; }); return; }
                await Program.Supervisores.AddAsync(new Supervisor
                { DNI = form.DNI, Grado = form.Grado, ApellidosNombres = form.ApellidosNombres });
                Invoke(async () =>
                {
                    lblEstado.ForeColor = Color.DarkGreen;
                    lblEstado.Text = $"✓ {form.ApellidosNombres} agregado.";
                    await RefrescarSupervisoresAsync();
                });
            }
            catch (Exception ex) { Invoke(() => { lblEstado.ForeColor = Color.Red; lblEstado.Text = $"✗ {ex.Message}"; }); }
        });
    }

    public void EliminarSupervisorSeleccionado(Label lblEstado)
    {
        if (dgvSupervisores.CurrentRow is null) return;
        var dni = dgvSupervisores.CurrentRow.Cells[1].Value?.ToString() ?? "";
        var nombre = dgvSupervisores.CurrentRow.Cells[3].Value?.ToString() ?? "";
        if (string.IsNullOrEmpty(dni)) return;

        if (MessageBox.Show($"¿Eliminar a '{nombre}'?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

        Task.Run(async () =>
        {
            try
            {
                await Program.Supervisores.DeleteAsync(dni);
                Invoke(async () =>
                {
                    lblEstado.ForeColor = Color.DarkGreen;
                    lblEstado.Text = $"✓ '{nombre}' eliminado.";
                    await RefrescarSupervisoresAsync();
                });
            }
            catch (Exception ex) { Invoke(() => { lblEstado.ForeColor = Color.Red; lblEstado.Text = $"✗ {ex.Message}"; }); }
        });
    }


    private void btnAgregarCadetes3_Click(object sender, EventArgs e) => AgregarFilaCadetes(3, lblEstadoCadetes3);
    private void btnEliminarCadetes3_Click(object sender, EventArgs e) => EliminarCadeteSeleccionado(dgvCadetes3, 3, lblEstadoCadetes3);
    private void btnAgregarCadetes4_Click(object sender, EventArgs e) => AgregarFilaCadetes(4, lblEstadoCadetes4);
    private void btnEliminarCadetes4_Click(object sender, EventArgs e) => EliminarCadeteSeleccionado(dgvCadetes4, 4, lblEstadoCadetes4);
    private void btnAgregarCadetes5_Click(object sender, EventArgs e) => AgregarFilaCadetes(5, lblEstadoCadetes5);
    private void btnEliminarCadetes5_Click(object sender, EventArgs e) => EliminarCadeteSeleccionado(dgvCadetes5, 5, lblEstadoCadetes5);
    private void btnAgregarSupervisores_Click(object sender, EventArgs e) => AgregarSupervisor(lblEstadoSupervisores);
    private void btnEliminarSupervisores_Click(object sender, EventArgs e) => EliminarSupervisorSeleccionado(lblEstadoSupervisores);
    private void btnAgregarCastigos_Click(object sender, EventArgs e)
    {
        using var form = new FormAgregarCastigo();
        if (form.ShowDialog() != DialogResult.OK) return;
        Task.Run(async () =>
        {
            try
            {
                var existente = await Program.Castigos.GetByIdAsync(form.Codigo);
                if (existente is not null)
                {
                    Invoke(() => {
                        lblEstadoCastigos.ForeColor = Color.DarkOrange;
                        lblEstadoCastigos.Text = $"⚠ Ya existe el código {form.Codigo}";
                    });
                    return;
                }
                await Program.Castigos.AddAsync(new Domain.Entities.Castigo
                {
                    Codigo = form.Codigo,
                    Descripcion = form.Descripcion,
                    PuntosAño3Raw = form.PuntosIII,
                    PuntosAño4Raw = form.PuntosIV,
                    PuntosAño5Raw = form.PuntosV,
                    Reincidencia = form.Reincidencia,
                    Nota = form.Nota
                });
                Invoke(async () =>
                {
                    lblEstadoCastigos.ForeColor = Color.DarkGreen;
                    lblEstadoCastigos.Text = $"✓ Castigo {form.Codigo} agregado.";
                    await RefrescarCastigosAsync();
                });
            }
            catch (Exception ex)
            {
                Invoke(() => {
                    lblEstadoCastigos.ForeColor = Color.Red;
                    lblEstadoCastigos.Text = $"✗ {ex.Message}";
                });
            }
        });
    }
    private void btnEliminarCastigos_Click(object sender, EventArgs e)
    {
        if (dgvCastigos.CurrentRow is null) return;
        var codigo = dgvCastigos.CurrentRow.Cells[1].Value?.ToString() ?? "";
        var desc = dgvCastigos.CurrentRow.Cells[2].Value?.ToString() ?? "";
        if (string.IsNullOrEmpty(codigo)) return;
        if (MessageBox.Show($"¿Eliminar castigo '{codigo} - {desc}'?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        Task.Run(async () =>
        {
            try
            {
                await Program.Castigos.DeleteAsync(codigo);
                await Invoke(async () => {
                    lblEstadoCastigos.ForeColor = Color.DarkGreen;
                    lblEstadoCastigos.Text = $"✓ Castigo '{codigo}' eliminado.";
                    await RefrescarCastigosAsync();
                });
            }
            catch (Exception ex) { Invoke(() => { lblEstadoCastigos.ForeColor = Color.Red; lblEstadoCastigos.Text = $"✗ {ex.Message}"; }); }
        });
    }

    // ── Clases para binding editable ─────────────────────────────────────
    public class CadeteRow
    {
        public int N { get; set; }
        public string DNI { get; set; } = "";
        public string ApellidosNombres { get; set; } = "";
        public string Division { get; set; } = "";
    }

    public class SupervisorRow
    {
        public int N { get; set; }
        public string DNI { get; set; } = "";
        public string Grado { get; set; } = "";
        public string ApellidosNombres { get; set; } = "";
    }

    public class CastigoRow
    {
        public int N { get; set; }
        public string Codigo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string III { get; set; } = "";
        public string IV { get; set; } = "";
        public string V { get; set; } = "";
        public int Reinc { get; set; }
        public string Nota { get; set; } = "";
    }
}
