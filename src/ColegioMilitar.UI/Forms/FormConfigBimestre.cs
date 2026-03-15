using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI.Forms;

public partial class FormConfigBimestre : Form
{
    public FormConfigBimestre()
    {
        InitializeComponent();
    }

    private async void FormConfigBimestre_Load(object sender, EventArgs e)
    {
        nudBimestre.Value = 1;
        nudAño.Value      = DateTime.Today.Year;
        await RefrescarGrillaAsync();
    }

    // ── Refrescar grilla ─────────────────────────────────────────────────

    private async Task RefrescarGrillaAsync()
    {
        var semanas = await Program.Bimestres.GetByBimestreAsync(
            (int)nudBimestre.Value, (int)nudAño.Value);

        dgvSemanas.DataSource = semanas
            .OrderBy(s => s.NroSemana)
            .Select((s, i) => new
            {
                s.Id,
                Nro       = i + 1,
                s.NombreSemana,
                Inicio    = s.FechaInicio.ToString("dd/MM/yyyy"),
                Fin       = s.FechaFin.ToString("dd/MM/yyyy"),
                Cerrada   = s.Cerrada ? "✓ Cerrada" : "Abierta"
            }).ToList();

        if (dgvSemanas.Columns.Count > 0)
        {
            dgvSemanas.Columns["Id"]!.Visible       = false;
            dgvSemanas.Columns["Nro"]!.Width        = 40;
            dgvSemanas.Columns["NombreSemana"]!.Width = 120;
            dgvSemanas.Columns["Inicio"]!.Width     = 100;
            dgvSemanas.Columns["Fin"]!.Width        = 100;
            dgvSemanas.Columns["Cerrada"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvSemanas.Columns["Nro"]!.HeaderText         = "N°";
            dgvSemanas.Columns["NombreSemana"]!.HeaderText = "NOMBRE (fecha viernes)";
            dgvSemanas.Columns["Inicio"]!.HeaderText      = "INICIO";
            dgvSemanas.Columns["Fin"]!.HeaderText         = "FIN";
            dgvSemanas.Columns["Cerrada"]!.HeaderText     = "ESTADO";
        }
    }

    // ── Agregar semana ───────────────────────────────────────────────────

    private async void btnAgregar_Click(object sender, EventArgs e)
    {
        var nombre = txtNombre.Text.Trim().ToUpper();
        if (string.IsNullOrEmpty(nombre))
        { MostrarError("Ingresa el nombre de la semana (ej: 20 MARZO)"); return; }

        if (dtpInicio.Value.Date > dtpFin.Value.Date)
        { MostrarError("La fecha de inicio debe ser anterior al fin."); return; }

        // Calcular número de semana automáticamente
        var existentes = (await Program.Bimestres.GetByBimestreAsync(
            (int)nudBimestre.Value, (int)nudAño.Value)).ToList();
        int nroSemana = existentes.Count + 1;

        await Program.Bimestres.AddAsync(new BimestreConfig
        {
            Bimestre    = (int)nudBimestre.Value,
            Año         = (int)nudAño.Value,
            NroSemana   = nroSemana,
            NombreSemana = nombre,
            FechaInicio = dtpInicio.Value.Date,
            FechaFin    = dtpFin.Value.Date,
            Cerrada     = false
        });

        MostrarOk($"✓ Semana {nroSemana} '{nombre}' agregada.");
        LimpiarFormulario();
        await RefrescarGrillaAsync();
    }

    // ── Eliminar semana ──────────────────────────────────────────────────

    private async void btnEliminar_Click(object sender, EventArgs e)
    {
        if (dgvSemanas.CurrentRow is null) return;
        var id = (int)dgvSemanas.CurrentRow.Cells["Id"].Value;
        var nombre = dgvSemanas.CurrentRow.Cells["NombreSemana"].Value?.ToString();

        var confirm = MessageBox.Show(
            $"¿Eliminar la semana '{nombre}'?\nSi tiene sanciones asociadas, no se eliminará.",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes) return;

        try
        {
            await Program.Bimestres.DeleteAsync(id);
            // Renumerar semanas restantes
            await RenumerarSemanasAsync();
            MostrarOk("✓ Semana eliminada y numeración actualizada.");
            await RefrescarGrillaAsync();
        }
        catch (Exception ex)
        {
            MostrarError($"No se pudo eliminar: {ex.Message}");
        }
    }

    // ── Cerrar semana ────────────────────────────────────────────────────

    private async void btnCerrar_Click(object sender, EventArgs e)
    {
        if (dgvSemanas.CurrentRow is null) return;
        var id     = (int)dgvSemanas.CurrentRow.Cells["Id"].Value;
        var nombre = dgvSemanas.CurrentRow.Cells["NombreSemana"].Value?.ToString();

        var confirm = MessageBox.Show(
            $"¿Cerrar la semana '{nombre}'?\nNo se podrán agregar más sanciones en esa semana.",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        await Program.Bimestres.CerrarSemanaAsync(id);
        MostrarOk($"✓ Semana '{nombre}' cerrada.");
        await RefrescarGrillaAsync();
    }

    private async void nudBimestre_ValueChanged(object sender, EventArgs e) =>
        await RefrescarGrillaAsync();

    private async void nudAño_ValueChanged(object sender, EventArgs e) =>
        await RefrescarGrillaAsync();

    // ── Reabrir semana ────────────────────────────────────────────────────

    private async void btnReabrir_Click(object sender, EventArgs e)
    {
        if (dgvSemanas.CurrentRow is null) return;
        var id = (int)dgvSemanas.CurrentRow.Cells["Id"].Value;
        var nombre = dgvSemanas.CurrentRow.Cells["NombreSemana"].Value?.ToString();
        var estado = dgvSemanas.CurrentRow.Cells["Cerrada"].Value?.ToString();

        if (estado != "✓ Cerrada")
        { MostrarError("Esta semana ya está abierta."); return; }

        var confirm = MessageBox.Show(
            $"¿Reabrir la semana '{nombre}'?",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        await Program.Bimestres.ReabrirSemanaAsync(id);
        MostrarOk($"✓ Semana '{nombre}' reabierta.");
        await RefrescarGrillaAsync();
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private async Task RenumerarSemanasAsync()
    {
        var semanas = (await Program.Bimestres.GetByBimestreAsync(
            (int)nudBimestre.Value, (int)nudAño.Value))
            .OrderBy(s => s.NroSemana).ToList();

        for (int i = 0; i < semanas.Count; i++)
        {
            semanas[i].NroSemana = i + 1;
            await Program.Bimestres.UpdateAsync(semanas[i]);
        }
    }

    private void LimpiarFormulario()
    {
        txtNombre.Clear();
        dtpInicio.Value = DateTime.Today;
        dtpFin.Value    = DateTime.Today.AddDays(6);
    }

    private void MostrarOk(string msg)
    { lblEstado.ForeColor = Color.DarkGreen; lblEstado.Text = msg; }

    private void MostrarError(string msg)
    { lblEstado.ForeColor = Color.Red; lblEstado.Text = msg; }
}
