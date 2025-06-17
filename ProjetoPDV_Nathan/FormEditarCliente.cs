using System;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public class FormEditarCliente : Form
    {
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Email { get; private set; }

        private TextBox txtNome;
        private MaskedTextBox txtTelefone;
        private TextBox txtEstado;
        private TextBox txtCidade;
        private TextBox txtEmail;
        private Button btnSalvar;
        private Button btnCancelar;

        public FormEditarCliente(string nome, string telefone, string estado, string cidade, string email)
        {
            InicializarFormulario();

            txtNome.Text = nome;
            txtTelefone.Text = telefone;
            txtEstado.Text = estado;
            txtCidade.Text = cidade;
            txtEmail.Text = email;
        }

        private void InicializarFormulario()
        {
            this.Text = "Editar Cliente";
            this.ClientSize = new System.Drawing.Size(350, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblNome = new Label { Text = "Nome:", Location = new System.Drawing.Point(20, 20) };
            txtNome = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 200 };

            Label lblTelefone = new Label { Text = "Telefone:", Location = new System.Drawing.Point(20, 60) };
            txtTelefone = new MaskedTextBox("(00) 00000-0000") { Location = new System.Drawing.Point(120, 60), Width = 200 };

            Label lblEstado = new Label { Text = "Estado:", Location = new System.Drawing.Point(20, 100) };
            txtEstado = new TextBox { Location = new System.Drawing.Point(120, 100), Width = 200 };

            Label lblCidade = new Label { Text = "Cidade:", Location = new System.Drawing.Point(20, 140) };
            txtCidade = new TextBox { Location = new System.Drawing.Point(120, 140), Width = 200 };

            Label lblEmail = new Label { Text = "E-mail:", Location = new System.Drawing.Point(20, 180) };
            txtEmail = new TextBox { Location = new System.Drawing.Point(120, 180), Width = 200 };

            btnSalvar = new Button { Text = "Salvar", Location = new System.Drawing.Point(60, 220), Width = 100 };
            btnCancelar = new Button { Text = "Cancelar", Location = new System.Drawing.Point(180, 220), Width = 100 };

            btnSalvar.Click += BtnSalvar_Click;
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

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            Nome = txtNome.Text.Trim();
            Telefone = txtTelefone.Text.Trim();  
            Estado = txtEstado.Text.Trim();
            Cidade = txtCidade.Text.Trim();
            Email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Telefone) || string.IsNullOrEmpty(Estado))
            {
                MessageBox.Show("Preencha os campos obrigatórios: Nome, Telefone e Estado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormEditarCliente
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormEditarCliente";
            this.Load += new System.EventHandler(this.FormEditarCliente_Load);
            this.ResumeLayout(false);

        }

        private void FormEditarCliente_Load(object sender, EventArgs e)
        {
              
        }
    }
}
