using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class FormEditarCliente : Form
    {
        public string Nome => txtNome.Text;
        public string Telefone => txtTelefone.Text;
        public string Estado => txtEstado.Text;
        public string Cidade => txtCidade.Text;
        public string Email => txtEmail.Text;

        private TextBox txtNome, txtTelefone, txtEstado, txtCidade, txtEmail;

        private void FormEditarCliente_Load(object sender, EventArgs e)
        {

        }

        private Button btnSalvar, btnCancelar;

        public FormEditarCliente(string nome, string telefone, string estado, string cidade, string email)
        {
            this.Text = "Editar Cliente";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNome = new Label { Text = "Nome:", Location = new Point(20, 20), AutoSize = true };
            txtNome = new TextBox { Location = new Point(100, 20), Width = 250, Text = nome };

            Label lblTelefone = new Label { Text = "Telefone:", Location = new Point(20, 60), AutoSize = true };
            txtTelefone = new TextBox { Location = new Point(100, 60), Width = 250, Text = telefone };

            Label lblEstado = new Label { Text = "Estado:", Location = new Point(20, 100), AutoSize = true };
            txtEstado = new TextBox { Location = new Point(100, 100), Width = 250, Text = estado };

            Label lblCidade = new Label { Text = "Cidade:", Location = new Point(20, 140), AutoSize = true };
            txtCidade = new TextBox { Location = new Point(100, 140), Width = 250, Text = cidade };

            Label lblEmail = new Label { Text = "Email:", Location = new Point(20, 180), AutoSize = true };
            txtEmail = new TextBox { Location = new Point(100, 180), Width = 250, Text = email };

            btnSalvar = new Button { Text = "Salvar", Location = new Point(100, 230), Width = 100 };
            btnCancelar = new Button { Text = "Cancelar", Location = new Point(220, 230), Width = 100 };

            btnSalvar.Click += (s, e) => this.DialogResult = DialogResult.OK;
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
                lblNome, txtNome,
                lblTelefone, txtTelefone,
                lblEstado, txtEstado,
                lblCidade, txtCidade,
                lblEmail, txtEmail,
                btnSalvar, btnCancelar
            });
        }
    }
}
