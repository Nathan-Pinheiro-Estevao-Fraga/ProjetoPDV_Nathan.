using System;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // Faz o botão "Entrar" responder ao pressionar Enter
            this.AcceptButton = btnLogin; 
        }

        // Evento do botão de login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string senha = txtSenha.Text;

            //teste de login

            if (nome == "admin" && senha == "1234")
            {
                MessageBox.Show("Login bem-sucedido!");

                // Abrir o próximo Form (ex: Form2)

                Tela_inicial telaPrincipal = new Tela_inicial();
                telaPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nome ou senha incorretos.");
            } //teste de login

            /*if (nome == "" && senha == "")
            {              

                // Abrir o próximo Form (ex: Form2)

                Dashboard telaPrincipal = new Dashboard();
                telaPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nome ou senha incorretos.");
            }*/
        }        

        private void textNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
