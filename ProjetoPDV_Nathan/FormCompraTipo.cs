using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class FormCompraTipo : Form
    {
        public string TipoSelecionado { get; private set; }
        public int Quantidade { get; private set; }

        private ComboBox comboBoxTipo;
        private TextBox txtQuantidade;
        private Button btnConfirmar;
        private Button btnCancelar;

        public FormCompraTipo()
        {
            this.Text = "Tipo de Compra";
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterParent;

            comboBoxTipo = new ComboBox
            {
                Location = new Point(20, 20),
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboBoxTipo.Items.AddRange(new string[] { "Comprado", "Carteira", "Fornecido" });
            comboBoxTipo.SelectedIndex = 0;

            txtQuantidade = new TextBox
            {
                Location = new Point(20, 60),
                Width = 240,
                Text = "Quantidade",
                ForeColor = Color.Gray
            };

            // Simular PlaceholderText para .NET Framework
            txtQuantidade.Enter += (s, e) =>
            {
                if (txtQuantidade.Text == "Quantidade")
                {
                    txtQuantidade.Text = "";
                    txtQuantidade.ForeColor = Color.Black;
                }
            };

            txtQuantidade.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtQuantidade.Text))
                {
                    txtQuantidade.Text = "Quantidade";
                    txtQuantidade.ForeColor = Color.Gray;
                }
            };

            btnConfirmar = new Button
            {
                Text = "Confirmar",
                Location = new Point(20, 100),
                Width = 100
            };
            btnConfirmar.Click += btnConfirmar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(160, 100),
                Width = 100
            };
            btnCancelar.Click += btnCancelar_Click;

            this.Controls.Add(comboBoxTipo);
            this.Controls.Add(txtQuantidade);
            this.Controls.Add(btnConfirmar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnConfirmar;
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            TipoSelecionado = comboBoxTipo.SelectedItem.ToString();

            if (!int.TryParse(txtQuantidade.Text.Trim(), out int qtd) || qtd <= 0)
            {
                MessageBox.Show("Digite uma quantidade válida.");
                return;
            }

            Quantidade = qtd;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
