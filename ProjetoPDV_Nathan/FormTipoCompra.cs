using System;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class FormTipoCompra : Form
    {
        public string TipoSelecionado => comboBoxTipo.SelectedItem.ToString();

        public FormTipoCompra()
        {
            InitializeComponent();
            comboBoxTipo.Items.AddRange(new string[] { "Comprado", "Carteira", "Fornecido" });
            comboBoxTipo.SelectedIndex = 0;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (comboBoxTipo.SelectedIndex >= 0)
                this.DialogResult = DialogResult.OK;
            else
                MessageBox.Show("Selecione um tipo.");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormTipoCompra_Load(object sender, EventArgs e)
        {

        }
    }
}
