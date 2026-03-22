using ColegioMilitar.Application.DTOs;

namespace ColegioMilitar.UI.Forms;

public partial class FormRelacionSalida : Form
{
    private int _semanaActiva = 1;
    private List<FilaPtosSalidaDto> _datos = new();

    public FormRelacionSalida()
    {
        InitializeComponent();
    }

    private async void FormRelacionSalida_Load(object sender, EventArgs e)
    {
        lblFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
        await CargarDatosAsync();
    }

    public async Task SetSemanaAsync(int semana)
    {
        _semanaActiva = semana;
        if (IsHandleCreated)
            await CargarDatosAsync();
    }

    private async Task CargarDatosAsync()
    {
        _datos = (await Program.ConsolidadoService
            .GenerarPtosSalidaAsync(_semanaActiva)).ToList();

        await RefrescarAñoAsync(3, dgv3, pnlRaciones3);
        await RefrescarAñoAsync(4, dgv4, pnlRaciones4);
        await RefrescarAñoAsync(5, dgv5, pnlRaciones5);
        RefrescarResumen();
    }

    private async Task RefrescarAñoAsync(int año, DataGridView dgv, Panel pnlRaciones)
    {
        var datosAño = _datos.Where(f => f.Año == año).ToList();

        var filas = datosAño
            .OrderBy(f => f.CantidadPV > 0 ? 9999 : f.TotalPuntos)
            .ThenBy(f => f.ApellidosNombres)
            .Select((f, i) => new
            {
                N                = i + 1,
                DNI              = f.CadeteDNI,
                ApellidosNombres = f.ApellidosNombres,
                Ptos             = f.PtosDisplay,
                Salida           = f.Salida
            })
            .ToList();

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgv.DataSource = filas;

        if (dgv.Columns.Count >= 5)
        {
            dgv.Columns[0].Width      = 40;  dgv.Columns[0].HeaderText = "N°";
            dgv.Columns[1].Width      = 90;  dgv.Columns[1].HeaderText = "DNI";
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                          dgv.Columns[2].HeaderText = "APELLIDOS Y NOMBRES";
            dgv.Columns[3].Width      = 60;  dgv.Columns[3].HeaderText = "PTOS";
            dgv.Columns[4].Width      = 200; dgv.Columns[4].HeaderText = "SALIDA";
        }

        // Colorear filas por estado de salida
        foreach (DataGridViewRow row in dgv.Rows)
        {
            var salida = row.Cells[4].Value?.ToString() ?? "";
            row.DefaultCellStyle.BackColor = salida switch
            {
                "Pierde salida"          => Color.FromArgb(255, 200, 200),
                "Sale domingo 07:00 hrs" => Color.FromArgb(255, 220, 180),
                "Sale sábado 07:00 hrs"  => Color.FromArgb(255, 240, 200),
                _                        => Color.FromArgb(200, 240, 200)
            };
            row.DefaultCellStyle.ForeColor           = Color.Black;
            row.DefaultCellStyle.SelectionBackColor  = Color.FromArgb(180, 200, 240);
            row.DefaultCellStyle.SelectionForeColor  = Color.Black;
        }

        // Actualizar tabla de raciones
        ActualizarRaciones(pnlRaciones, datosAño);
        await Task.CompletedTask;
    }

    private static void ActualizarRaciones(Panel pnl, List<FilaPtosSalidaDto> datos)
    {
        // Viernes  = CONTAR.SI(ptos >= 10) — los que NO salen completo
        // Sábado   = Viernes - (CONTAR.SI(ptos<15) - CONTAR.SI(ptos<10))
        // Domingo  = CONTAR.SI(ptos >= 20)
        int viernes = datos.Count(f => f.TotalPuntos >= 10 || f.CantidadPV > 0);
        int menorQ15 = datos.Count(f => f.TotalPuntos < 15 && f.CantidadPV == 0);
        int menorQ10 = datos.Count(f => f.TotalPuntos < 10 && f.CantidadPV == 0);
        int sabado   = viernes - (menorQ15 - menorQ10);
        int domingo  = datos.Count(f => f.TotalPuntos >= 20 || f.CantidadPV > 0);

        if (pnl.Tag is RacionesLabels labels)
        {
            labels.LblViernes.Text = viernes.ToString();
            labels.LblSabado.Text  = sabado.ToString();
            labels.LblDomingo.Text = domingo.ToString();
            labels.LblTotal.Text   = (viernes + sabado + domingo).ToString();
        }
    }

    private void RefrescarResumen()
    {
        var años = new[] { 3, 4, 5 };
        var filas = años.Select(año =>
        {
            var d = _datos.Where(f => f.Año == año).ToList();
            int vie = d.Count(f => f.TotalPuntos >= 10 || f.CantidadPV > 0);
            int m15 = d.Count(f => f.TotalPuntos < 15 && f.CantidadPV == 0);
            int m10 = d.Count(f => f.TotalPuntos < 10 && f.CantidadPV == 0);
            int sab = vie - (m15 - m10);
            int dom = d.Count(f => f.TotalPuntos >= 20 || f.CantidadPV > 0);
            return new { Años = $"{año}° AÑO", Vie = vie, Sab = sab, Dom = dom, Total = vie + sab + dom };
        }).ToList<object>();

        // Fila total
        var tots = filas.Cast<dynamic>().ToList();
        filas.Add(new
        {
            Años  = "TOTAL",
            Vie   = tots.Sum(r => (int)r.Vie),
            Sab   = tots.Sum(r => (int)r.Sab),
            Dom   = tots.Sum(r => (int)r.Dom),
            Total = tots.Sum(r => (int)r.Total)
        });

        dgvResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgvResumen.DataSource = filas;

        if (dgvResumen.Columns.Count >= 5)
        {
            dgvResumen.Columns[0].Width      = 120; dgvResumen.Columns[0].HeaderText = "AÑOS";
            dgvResumen.Columns[1].Width      = 100; dgvResumen.Columns[1].HeaderText = "VIERNES";
            dgvResumen.Columns[2].Width      = 100; dgvResumen.Columns[2].HeaderText = "SÁBADO";
            dgvResumen.Columns[3].Width      = 100; dgvResumen.Columns[3].HeaderText = "DOMINGO";
            dgvResumen.Columns[4].Width      = 100; dgvResumen.Columns[4].HeaderText = "TOTAL";
        }

        foreach (DataGridViewRow row in dgvResumen.Rows)
        {
            if (row.Cells[0].Value?.ToString() == "TOTAL")
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(60, 130, 60);
                row.DefaultCellStyle.ForeColor = Color.White;
                row.DefaultCellStyle.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(220, 240, 220);
            }
        }
    }

    public class RacionesLabels
    {
        public Label LblViernes { get; set; } = null!;
        public Label LblSabado  { get; set; } = null!;
        public Label LblDomingo { get; set; } = null!;
        public Label LblTotal   { get; set; } = null!;
    }
}
