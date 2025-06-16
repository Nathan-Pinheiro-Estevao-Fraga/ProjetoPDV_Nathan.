using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;   
using System.Data;    
using System.Drawing;    
using System.Drawing.Drawing2D;    
using System.Linq;   
using System.Text;    
using System.Threading.Tasks;


namespace ProjetoPDV_Nathan
{
    public partial class Tela_inicial : Form
    {
        public Tela_inicial()
        {
            InitializeComponent();
        }
        

        private void Controle2_Load(object sender, EventArgs e)
        {
            
        }



        private void Dashboard_Load(object sender, EventArgs e)
        {
            controleDashboard = new ControlDashboard();
            controleClientes = new ControlClientes();
            controleEstoque = new ControlEstoque(); // ✅ Adicione esta linha
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }               
        

        //Botão para fechar o programa
        private void btnSair_Click(object sender, EventArgs e)
        {
                        
        }

        //Carrega os controles
        private void CarregarControle(UserControl uc)
        {
            if (panelCentralConteudo.Controls.Count > 0 && panelCentralConteudo.Controls[0].GetType() == uc.GetType())
                return;

            panelCentralConteudo.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelCentralConteudo.Controls.Add(uc);
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        //Instância para o carregamento do estoque
        private ControlEstoque controleEstoque;

        //Carregamento do Estoque
        private void btnEstoque_Click(object sender, EventArgs e)
        {
            CarregarControle(controleEstoque);

        }

        private void panelPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            if (panelConteudo.Width == 200)
            {
                // Recolher o menu
                panelConteudo.Width = 0;
            }
            else
            {
                // Restaura o menu em seu tamanho original
                panelConteudo.Width = 200;
            }
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            Application.Exit();  // Fecha o programa completamente
        }

        private ControlDashboard controleDashboard;
        private void btnDashboard_Click(object sender, EventArgs e)
        {            
            CarregarControle(controleDashboard);
        }

        private void panelConteudo_Paint(object sender, PaintEventArgs e)
        {

        }       

        private void pictureBoxDashboard_Click(object sender, EventArgs e)
        {
            //Faz com que o click no picturebox seja redirecionado para o botão conrrespondente//
            btnDashboard.PerformClick();
        }

        private void pictureBoxClientes_Click(object sender, EventArgs e)
        {
            btnClientes.PerformClick();
        }

        private void pictureBoxEstoque_Click(object sender, EventArgs e)
        {
            //Faz com que o click no picturebox seja redirecionado para o botão conrrespondente//
            btnEstoque.PerformClick();
        }        

        private void pictureBoxGráficos_Click(object sender, EventArgs e)
        {
            //Faz com que o click no picturebox seja redirecionado para o botão conrrespondente//
            btnGraficos.PerformClick();
        }        
        private void pictureBoxSair_Click(object sender, EventArgs e)
        {
            //Faz com que o click no picturebox seja redirecionado para o botão conrrespondente//
            btnSair.PerformClick();
        }        

        private void panelCentralConteudo_Paint(object sender, PaintEventArgs e)
        {

        }       
        private ControlClientes controleClientes;

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (controleClientes == null)
                controleClientes = new ControlClientes();

            CarregarControle(controleClientes);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarregarControle(controleDashboard);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            CarregarControle(controleDashboard);
        }

        private void btnPedidos_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //Faz com que o click no picturebox seja redirecionado para o botão conrrespondente//
            btnComprar.PerformClick();
        }        

        private void btnRelatórios_Click(object sender, EventArgs e)
        {

        }

        private void btnConfigurações_Click(object sender, EventArgs e)
        {

        }

        private void btnGraficos_Click(object sender, EventArgs e)
        {

        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();  // Fecha o programa completamente
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {

        }

        private void lblDataHora_Click(object sender, EventArgs e)
        {

        }
    }
}