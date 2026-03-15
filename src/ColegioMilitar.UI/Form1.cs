using ColegioMilitar.UI.Forms;

namespace ColegioMilitar.UI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnRegistrarSanciones_Click(object sender, EventArgs e)
    {
        new FormRegistrarSancion().Show();
    }
}
