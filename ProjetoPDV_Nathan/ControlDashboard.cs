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

        private Timer timerHora;

        private void ControlDashboard_Load(object sender, EventArgs e)
        {
            // Inicia o Timer para atualizar data e hora
            timerHora = new Timer();
            timerHora.Interval = 1000; // 1 segundo
            timerHora.Tick += TimerHora_Tick;
            timerHora.Start();

            AtualizarDataHora();
        }

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            AtualizarDataHora();
        }

        private void AtualizarDataHora()
        {
            string diaSemana = DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
            string dataHora = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy - HH:mm:ss", new System.Globalization.CultureInfo("pt-BR"));
            lblDataHora.Text = $"{diaSemana}, {dataHora}";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}