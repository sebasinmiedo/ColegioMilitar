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
        await CargarDatosAsync();
    }

    public async Task SetSemanaAsync(int semana)
    {
        _semanaActiva = semana;
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
        var filas = _datos
            .Where(f => f.Año == año)
            .Select((f, i) => new
            {
                N                = i + 1,
                f.CadeteDNI,
                f.ApellidosNombres,
                Ptos             = f.PtosDisplay,
                PtosNum          = f.TotalPuntos + (f.CantidadPV > 0 ? 999 : 0), // para ordenar PV al final
                Salida           = f.Salida
            })
            .OrderBy(f => f.PtosNum)
            .Select((f, i) => new
            {
                N                = i + 1,
                f.CadeteDNI,
                f.ApellidosNombres,
                f.Ptos,
                f.Salida
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

        // Colorear por estado
        foreach (DataGridViewRow row in dgv.Rows)
        {
            var salida = row.Cells[4].Value?.ToString() ?? "";
            var color = salida switch
            {
                "Pierde salida"          => Color.FromArgb(255, 200, 200),
                "Sale domingo 07:00 hrs" => Color.FromArgb(255, 220, 180),
                "Sale sábado 07:00 hrs"  => Color.FromArgb(255, 240, 200),
                _                        => Color.FromArgb(200, 240, 200)
            };
            row.DefaultCellStyle.BackColor  = color;
            row.DefaultCellStyle.ForeColor  = Color.Black;
            row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 200, 240);
            row.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // Actualizar tabla de raciones
        var datosAño = _datos.Where(f => f.Año == año).ToList();
        ActualizarRaciones(pnlRaciones, datosAño, año);

        await Task.CompletedTask;
    }

    private static void ActualizarRaciones(Panel pnl, List<FilaPtosSalidaDto> datos, int año)
    {
        // Fórmulas:
        // Viernes  = cadetes con ptos >= 10 (no salen completo)  → los que NO salen completo
        // Sábado   = Viernes - (cadetes con ptos >= 10 y < 15)
        // Domingo  = cadetes con ptos >= 20

        int totalCadetes = datos.Count;

        int conPV       = datos.Count(f => f.CantidadPV > 0);
        int ptosMayorIg10 = datos.Count(f => f.TotalPuntos >= 10 || f.CantidadPV > 0);
        int ptosMenor15   = datos.Count(f => f.TotalPuntos < 15 && f.CantidadPV == 0);
        int ptosMenor10   = datos.Count(f => f.TotalPuntos < 10 && f.CantidadPV == 0);
        int ptosMayorIg20 = datos.Count(f => f.TotalPuntos >= 20 || f.CantidadPV > 0);

        int viernes = ptosMayorIg10;
        int sabado  = viernes - (ptosMenor15 - ptosMenor10);
        int domingo = ptosMayorIg20;

        // Actualizar labels en el panel
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
        // Tab resumen — tabla consolidada de los 3 años
        var años = new[] { 3, 4, 5 };
        var resumen = años.Select(año =>
        {
            var datosAño = _datos.Where(f => f.Año == año).ToList();
            int ptosMayorIg10 = datosAño.Count(f => f.TotalPuntos >= 10 || f.CantidadPV > 0);
            int ptosMenor15   = datosAño.Count(f => f.TotalPuntos < 15 && f.CantidadPV == 0);
            int ptosMenor10   = datosAño.Count(f => f.TotalPuntos < 10 && f.CantidadPV == 0);
            int ptosMayorIg20 = datosAño.Count(f => f.TotalPuntos >= 20 || f.CantidadPV > 0);

            int vie = ptosMayorIg10;
            int sab = vie - (ptosMenor15 - ptosMenor10);
            int dom = ptosMayorIg20;

            return new { Año = $"{año}° AÑO", Vie = vie, Sab = sab, Dom = dom, Total = vie + sab + dom };
        }).ToList();

        var totales = new
        {
            Año   = "TOTAL",
            Vie   = resumen.Sum(r => r.Vie),
            Sab   = resumen.Sum(r => r.Sab),
            Dom   = resumen.Sum(r => r.Dom),
            Total = resumen.Sum(r => r.Total)
        };

        var filas = resumen.Cast<object>().Append(totales).ToList();
        dgvResumen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgvResumen.DataSource = filas;

        if (dgvResumen.Columns.Count >= 5)
        {
            dgvResumen.Columns[0].Width      = 100; dgvResumen.Columns[0].HeaderText = "AÑOS";
            dgvResumen.Columns[1].Width      = 80;  dgvResumen.Columns[1].HeaderText = "VIERNES";
            dgvResumen.Columns[2].Width      = 80;  dgvResumen.Columns[2].HeaderText = "SÁBADO";
            dgvResumen.Columns[3].Width      = 80;  dgvResumen.Columns[3].HeaderText = "DOMINGO";
            dgvResumen.Columns[4].Width      = 80;  dgvResumen.Columns[4].HeaderText = "TOTAL";
        }

        // Colorear fila TOTAL
        foreach (DataGridViewRow row in dgvResumen.Rows)
        {
            if (row.Cells[0].Value?.ToString() == "TOTAL")
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(100, 160, 100);
                row.DefaultCellStyle.ForeColor = Color.White;
                row.DefaultCellStyle.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
            }
        }
    }

    // Clase helper para referencias a labels de raciones
    public class RacionesLabels
    {
        public Label LblViernes { get; set; } = null!;
        public Label LblSabado  { get; set; } = null!;
        public Label LblDomingo { get; set; } = null!;
        public Label LblTotal   { get; set; } = null!;
    }
}
