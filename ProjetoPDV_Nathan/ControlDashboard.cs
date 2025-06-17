using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class ControlDashboard : UserControl
    {
        public ControlDashboard()
        {
            InitializeComponent();
            this.Load += ControlDashboard_Load;
        }

        //Instância para Hora
        private Timer timerHora;

        private void TimerHora_Tick(object sender, EventArgs e)
        {            
            AtualizarDataHora();
        }

        //Data e hora atuais
        private void AtualizarDataHora()
        {
            string diaSemana = DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
            string dataHora = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy - HH:mm:ss", new System.Globalization.CultureInfo("pt-BR"));
            lblDataHora.Text = $"{diaSemana}, {dataHora}";
        }

        private void ControlDashboard_Load(object sender, EventArgs e)
        {
            

            timerHora = new Timer();
            timerHora.Interval = 1000;
            timerHora.Tick += TimerHora_Tick;
            timerHora.Start();

            AtualizarDataHora();
            
        }        
            private void ControlDashboard_Resize(object sender, EventArgs e)
        {
            
        }        
        private void panelProdutos_Click(object sender, EventArgs e)
        {
            
        }

        private void panelProdutos_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {           
    
        }

        private void panelCentralConteudo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelQuantidadeEstoque_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDataHora_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {
               
        }

        private void panel1TotalVendas_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panelConteudoDashboard_Paint(object sender, PaintEventArgs e)
        {
            
        }
      
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
