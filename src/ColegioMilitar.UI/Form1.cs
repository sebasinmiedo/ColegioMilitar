using ColegioMilitar.UI.Forms;

namespace ColegioMilitar.UI;

public partial class Form1 : Form
{
    private int _semanaActiva3    = 0;
    private int _semanaActiva4    = 0;
    private int _semanaActiva5    = 0;
    private int _semanaActivaSalida = 1;

    public Form1() { InitializeComponent(); }

    private async void Form1_Load(object sender, EventArgs e)
    {
        await RefrescarTabAsync(3, dgv3, _semanaActiva3);
        await RefrescarTabAsync(4, dgv4, _semanaActiva4);
        await RefrescarTabAsync(5, dgv5, _semanaActiva5);
        await RefrescarSalidaAsync(_semanaActivaSalida);
    }

    // ── Filtros semana ────────────────────────────────────────────────────

    private async void BtnSemana3_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActiva3 = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanas3, _semanaActiva3);
        await RefrescarTabAsync(3, dgv3, _semanaActiva3);
    }

    private async void BtnSemana4_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActiva4 = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanas4, _semanaActiva4);
        await RefrescarTabAsync(4, dgv4, _semanaActiva4);
    }

    private async void BtnSemana5_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActiva5 = btn.Tag is int s ? s : 0;
        ResaltarBotonActivo(pnlSemanas5, _semanaActiva5);
        await RefrescarTabAsync(5, dgv5, _semanaActiva5);
    }

    private async void BtnSemana3_Salida_Click(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        _semanaActivaSalida = btn.Tag is int s ? s : 1;
        ResaltarBotonActivo(pnlSemanasSalida, _semanaActivaSalida);
        await RefrescarSalidaAsync(_semanaActivaSalida);
    }

    private static void ResaltarBotonActivo(Panel pnl, int semana)
    {
        foreach (Control c in pnl.Controls)
        {
            if (c is not Button b) continue;
            bool activo = b.Tag is int s ? s == semana : semana == 0;
            b.BackColor = activo ? Color.FromArgb(30, 60, 120) : Color.FromArgb(200, 210, 230);
            b.ForeColor = activo ? Color.White : Color.FromArgb(30, 60, 120);
        }
    }

    // ── Carga de datos ────────────────────────────────────────────────────

    private async Task RefrescarTabAsync(int añoCadete, DataGridView dgv, int semana)
    {
        try
        {
            var sanciones = await Program.SancionService.ListarTodosAsync();
            var filtradas = sanciones
                .Where(s => s.Cadete.Año == añoCadete)
                .Where(s => semana == 0 || s.SemanaBimestre == semana)
                .OrderBy(s => s.Cadete.ApellidosNombres)
                .Select((s, i) => new
                {
                    N                = i + 1,
                    Codigo           = s.CadeteDNI,
                    ApellidosNombres = s.Cadete.ApellidosNombres,
                    Motivo           = s.Castigo.Descripcion,
                    Ptos             = s.EsPierdeSalida ? "1PV" : s.PuntosAplicados.ToString(),
                    Reinc            = s.Castigo.Reincidencia,
                    Hora             = s.Hora.ToString(@"hh\:mm"),
                    Fecha            = s.Fecha.ToString("dd/MM/yyyy"),
                    Superior         = $"{s.Supervisor.Grado} {s.Supervisor.ApellidosNombres}"
                })
                .ToList();

            dgv.DataSource = filtradas;
            AjustarColumnasDgv(dgv);
        }
        catch { }
    }

    private async Task RefrescarSalidaAsync(int semana)
    {
        try
        {
            var filas = (await Program.ConsolidadoService.GenerarPtosSalidaAsync(semana))
                .Select((f, i) => new
                {
                    N                = i + 1,
                    f.CadeteDNI,
                    f.ApellidosNombres,
                    Año              = $"{f.Año}°",
                    f.TotalPuntos,
                    Salida           = f.Salida
                }).ToList();

            dgvSalida.DataSource = filas;
            AjustarColumnasSalida(dgvSalida);
        }
        catch { }
    }

    private static void AjustarColumnasDgv(DataGridView dgv)
    {
        if (dgv.Columns.Count == 0) return;
        dgv.Columns["N"]!.Width                = 40;
        dgv.Columns["Codigo"]!.Width           = 80;
        dgv.Columns["ApellidosNombres"]!.Width = 220;
        dgv.Columns["Motivo"]!.AutoSizeMode    = DataGridViewAutoSizeColumnMode.Fill;
        dgv.Columns["Ptos"]!.Width             = 50;
        dgv.Columns["Reinc"]!.Width            = 50;
        dgv.Columns["Hora"]!.Width             = 60;
        dgv.Columns["Fecha"]!.Width            = 85;
        dgv.Columns["Superior"]!.Width         = 180;
        dgv.Columns["N"]!.HeaderText                = "N°";
        dgv.Columns["Codigo"]!.HeaderText           = "CÓDIGO";
        dgv.Columns["ApellidosNombres"]!.HeaderText = "APELLIDOS Y NOMBRES";
        dgv.Columns["Motivo"]!.HeaderText           = "MOTIVO";
        dgv.Columns["Ptos"]!.HeaderText             = "PTOS";
        dgv.Columns["Reinc"]!.HeaderText            = "REINC";
        dgv.Columns["Hora"]!.HeaderText             = "HORA";
        dgv.Columns["Fecha"]!.HeaderText            = "FECHA";
        dgv.Columns["Superior"]!.HeaderText         = "SUPERIOR QUE CASTIGA";
    }

    private static void AjustarColumnasSalida(DataGridView dgv)
    {
        if (dgv.Columns.Count == 0) return;
        dgv.Columns["N"]!.Width                = 40;
        dgv.Columns["CadeteDNI"]!.Width        = 90;
        dgv.Columns["ApellidosNombres"]!.Width = 280;
        dgv.Columns["Año"]!.Width              = 50;
        dgv.Columns["TotalPuntos"]!.Width      = 60;
        dgv.Columns["Salida"]!.AutoSizeMode    = DataGridViewAutoSizeColumnMode.Fill;
        dgv.Columns["N"]!.HeaderText                = "N°";
        dgv.Columns["CadeteDNI"]!.HeaderText        = "DNI";
        dgv.Columns["ApellidosNombres"]!.HeaderText = "APELLIDOS Y NOMBRES";
        dgv.Columns["Año"]!.HeaderText              = "AÑO";
        dgv.Columns["TotalPuntos"]!.HeaderText      = "PTOS";
        dgv.Columns["Salida"]!.HeaderText           = "SALIDA";

        // Colorear filas según estado de salida
        foreach (DataGridViewRow row in dgv.Rows)
        {
            var salida = row.Cells["Salida"].Value?.ToString() ?? "";
            row.DefaultCellStyle.ForeColor = salida switch
            {
                "Pierde salida"          => Color.Red,
                "Sale domingo 07:00 hrs" => Color.OrangeRed,
                "Sale sábado 07:00 hrs"  => Color.DarkOrange,
                _                        => Color.DarkGreen
            };
            row.DefaultCellStyle.Font = salida == "Pierde salida"
                ? new Font("Segoe UI", 9, FontStyle.Bold)
                : new Font("Segoe UI", 9);
        }
    }

    // ── Botones de acción ─────────────────────────────────────────────────

    private void btnInsertarSancion_Click(object sender, EventArgs e)
    {
        var form = new FormRegistrarSancion();
        form.SancionGuardada += async () =>
        {
            await RefrescarTabAsync(3, dgv3, _semanaActiva3);
            await RefrescarTabAsync(4, dgv4, _semanaActiva4);
            await RefrescarTabAsync(5, dgv5, _semanaActiva5);
            await RefrescarSalidaAsync(_semanaActivaSalida);
        };
        form.Show(this);
    }

    private void btnExportarExcel_Click(object sender, EventArgs e) =>
        MessageBox.Show("Exportación a Excel — próximamente.",
            "En desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void btnMantenimiento_Click(object sender, EventArgs e) =>
        new FormMantenimiento().Show(this);

    private async void btnRefrescar_Click(object sender, EventArgs e)
    {
        await RefrescarTabAsync(3, dgv3, _semanaActiva3);
        await RefrescarTabAsync(4, dgv4, _semanaActiva4);
        await RefrescarTabAsync(5, dgv5, _semanaActiva5);
        await RefrescarSalidaAsync(_semanaActivaSalida);
    }
}
