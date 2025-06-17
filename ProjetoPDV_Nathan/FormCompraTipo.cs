using System;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class FormCompraTipo : Form
    {
        public string TipoSelecionado { get; private set; }
        public int Quantidade { get; private set; }

        private ComboBox comboTipo;
        private NumericUpDown numericQuantidade;
        private Button btnConfirmar;
        private Button btnCancelar;

        public FormCompraTipo()
        {
            InicializarFormulario();
        }
         
        private void FormCompraTipo_Load(object sender, EventArgs e) { 
        }

        private void InicializarFormulario()
        {
            // Instanciar controles
            comboTipo = new ComboBox();
            numericQuantidade = new NumericUpDown();
            btnConfirmar = new Button();
            btnCancelar = new Button();

            // ComboBox
            comboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTipo.Items.AddRange(new object[] { "Comprado", "Carteira", "Fornecido" });
            comboTipo.SelectedIndex = 0;
            comboTipo.Location = new System.Drawing.Point(20, 20);
            comboTipo.Size = new System.Drawing.Size(200, 25);

            // NumericUpDown
            numericQuantidade.Minimum = 1;
            numericQuantidade.Maximum = 100000;
            numericQuantidade.Value = 1;
            numericQuantidade.Location = new System.Drawing.Point(20, 60);
            numericQuantidade.Size = new System.Drawing.Size(200, 25);

            // Botões
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.Location = new System.Drawing.Point(20, 100);
            btnConfirmar.Size = new System.Drawing.Size(80, 30);
            btnConfirmar.Click += BtnConfirmar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new System.Drawing.Point(120, 100);
            btnCancelar.Size = new System.Drawing.Size(80, 30);
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Configurações do Formulário
            this.Text = "Tipo de Compra";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ClientSize = new System.Drawing.Size(250, 160);
            this.AcceptButton = btnConfirmar;
            this.CancelButton = btnCancelar;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Adicionar controles ao Form
            this.Controls.Add(comboTipo);
            this.Controls.Add(numericQuantidade);
            this.Controls.Add(btnConfirmar);
            this.Controls.Add(btnCancelar);
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            TipoSelecionado = comboTipo.SelectedItem.ToString();
            Quantidade = (int)numericQuantidade.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
