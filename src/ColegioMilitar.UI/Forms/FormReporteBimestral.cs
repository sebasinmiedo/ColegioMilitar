using ColegioMilitar.Application.DTOs;
using ColegioMilitar.Domain.Entities;

namespace ColegioMilitar.UI.Forms;

public partial class FormReporteBimestral : Form
{
    private int _bimestre;
    private int _añoAcademico;
    private List<BimestreConfig> _semanas = new();
    private Dictionary<string, decimal> _actitudesMil = new();

    public FormReporteBimestral(int bimestre, int añoAcademico, List<BimestreConfig> semanas)
    {
        _bimestre     = bimestre;
        _añoAcademico = añoAcademico;
        _semanas      = semanas.OrderBy(s => s.NroSemana).ToList();
        InitializeComponent();
    }

    public async Task CargarDatosAsync()
    {
        _actitudesMil.Clear();
        await CargarTabAsync(3, dgv3);
        await CargarTabAsync(4, dgv4);
        await CargarTabAsync(5, dgv5);
    }

    private async Task CargarTabAsync(int añoCadete, DataGridView dgv)
    {
        var filas = await Program.ConsolidadoService
            .GenerarConsolidadoAsync(añoCadete, _bimestre, _añoAcademico);

        foreach (var f in filas)
            if (!_actitudesMil.ContainsKey(f.CadeteDNI))
                _actitudesMil[f.CadeteDNI] = f.ActitudMilitar;

        ConstruirGrilla(dgv, filas.ToList(), añoCadete);
    }

    private void ConstruirGrilla(DataGridView dgv, List<FilaConsolidadoDto> filas, int año)
    {
        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.AutoGenerateColumns = false;

        // ── Columnas fijas ────────────────────────────────────────────────
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "N", HeaderText = "N°", Width = 45, ReadOnly = true,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "DNI", HeaderText = "DNI", Width = 95, ReadOnly = true,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Nombre", HeaderText = "APELLIDOS Y NOMBRES", Width = 250, ReadOnly = true,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft,
                                 Padding   = new Padding(4, 0, 0, 0) }
        });

        // ── Columnas dinámicas de semanas ─────────────────────────────────
        foreach (var sem in _semanas)
        {
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name       = $"Sem{sem.NroSemana}",
                HeaderText = sem.NombreSemana,
                Width      = 95,
                ReadOnly   = true,
                DefaultCellStyle = {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(250, 250, 240)
                }
            });
        }

        // ── Columnas de cálculo ───────────────────────────────────────────
        AgregarColumnaCalc(dgv, "Puntos",   "PUNTOS",    75,  Color.FromArgb(230, 235, 255));
        AgregarColumnaCalc(dgv, "PtosDism", "PTOS DISM", 85,  Color.FromArgb(230, 235, 255));
        AgregarColumnaCalc(dgv, "NotaBase", "NOTA",      65,  Color.FromArgb(225, 240, 225));
        AgregarColumnaCalc(dgv, "Conducta", "CONDUCTA",  85,  Color.FromArgb(225, 240, 225));

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = "ActitudMil",
            HeaderText = "ACTITUD MIL",
            Width      = 100,
            ReadOnly   = false,
            DefaultCellStyle = {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(255, 252, 235),
                Font      = new Font("Segoe UI", 9, FontStyle.Bold)
            }
        });

        AgregarColumnaCalc(dgv, "NotaFinal", "NOTA FINAL", 95, Color.FromArgb(210, 235, 210));

        // ── Llenar filas ──────────────────────────────────────────────────
        int n = 1;
        foreach (var f in filas)
        {
            var row = new DataGridViewRow();
            row.CreateCells(dgv);

            row.Cells[0].Value = n++;
            row.Cells[1].Value = f.CadeteDNI;
            row.Cells[2].Value = f.ApellidosNombres;

            int colIdx = 3;
            int[] ptosSemana = new int[_semanas.Count];
            for (int i = 0; i < _semanas.Count; i++)
            {
                int pts = i switch
                {
                    0 => f.PtosSemana1,
                    1 => f.PtosSemana2,
                    2 => f.PtosSemana3,
                    3 => f.PtosSemana4,
                    4 => f.PtosSemana5,
                    _ => 0
                };
                ptosSemana[i] = pts;
                row.Cells[colIdx++].Value = pts;
            }

            int     total     = ptosSemana.Sum();
            decimal dism      = Math.Round(total * 0.1m, 2);
            decimal nota      = 20m;
            decimal conducta  = nota - dism;
            decimal actitud   = _actitudesMil.TryGetValue(f.CadeteDNI, out var a) ? a : 0m;
            decimal notaFinal = Math.Round((conducta + actitud) / 2m, 2);

            row.Cells[colIdx++].Value = total;
            row.Cells[colIdx++].Value = dism.ToString("F2");
            row.Cells[colIdx++].Value = nota.ToString("F2");
            row.Cells[colIdx++].Value = conducta.ToString("F2");
            // Actitud: siempre con 2 decimales, incluso si es 0
            row.Cells[colIdx++].Value = actitud.ToString("F2");
            row.Cells[colIdx++].Value = notaFinal.ToString("F2");

            row.Tag = f.CadeteDNI;
            dgv.Rows.Add(row);
        }

        // Sin colores alternados — fondo blanco uniforme
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

        dgv.CellEndEdit -= DgvCellEndEdit;
        dgv.CellEndEdit += DgvCellEndEdit;

        // Forzar fondo blanco en TODAS las celdas de todas las filas
        foreach (DataGridViewRow row in dgv.Rows)
            row.DefaultCellStyle.BackColor = Color.White;

    }

    private static void AgregarColumnaCalc(DataGridView dgv, string name,
        string header, int width, Color backColor)
    {
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name       = name,
            HeaderText = header,
            Width      = width,
            ReadOnly   = true,
            DefaultCellStyle = {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = backColor,
                Font      = new Font("Segoe UI", 9, FontStyle.Bold)
            }
        });
    }

    // ── Edición de Actitud Militar ────────────────────────────────────────

    private async void DgvCellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (sender is not DataGridView dgv) return;
        if (dgv.Columns[e.ColumnIndex].Name != "ActitudMil") return;

        var row    = dgv.Rows[e.RowIndex];
        var dni    = row.Tag?.ToString() ?? "";
        var rawVal = row.Cells["ActitudMil"].Value?.ToString()?.Trim() ?? "";

        if (!decimal.TryParse(rawVal.Replace(",", "."),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out decimal actitud))
            actitud = 0m;

        // Formatear con 2 decimales inmediatamente
        row.Cells["ActitudMil"].Value = actitud.ToString("F2");
        _actitudesMil[dni] = actitud;

        if (decimal.TryParse(row.Cells["Conducta"].Value?.ToString(),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out decimal conducta))
        {
            row.Cells["NotaFinal"].Value = Math.Round((conducta + actitud) / 2m, 2).ToString("F2");
        }

        try
        {
            await Program.ConsolidadoService.GuardarActitudMilitarAsync(
                dni, _bimestre, _añoAcademico, actitud);
        }
        catch { }
    }

    // ── Botón Guardar — útil para guardar TODAS de una sola vez ──────────

    private async void btnGuardar_Click(object sender, EventArgs e)
    {
        btnGuardar.Enabled = false;
        int guardados = 0;

        foreach (var kvp in _actitudesMil)
        {
            try
            {
                await Program.ConsolidadoService.GuardarActitudMilitarAsync(
                    kvp.Key, _bimestre, _añoAcademico, kvp.Value);
                guardados++;
            }
            catch { }
        }

        lblEstado.ForeColor = Color.DarkGreen;
        lblEstado.Text      = $"✓ {guardados} notas guardadas correctamente.";
        btnGuardar.Enabled  = true;
    }
}
