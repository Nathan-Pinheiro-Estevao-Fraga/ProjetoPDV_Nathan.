using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class ControlClientes : UserControl
    {


        public ControlClientes()
        {
            InitializeComponent();
            AtualizarTotalItens();
            this.Load += Controle3_Load;
        }
        private void AtualizarTotalItens()
        {
            lblTotalClientes.Text = $"Total de itens: {dgvClientes.Rows.Count}";
        }
        //Declarando a combobox
        
        private ComboBox cmbCamposFiltro;

        private void Controle3_Load(object sender, EventArgs e)
        {

            // Estilo e comportamento    
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToResizeColumns = false;
            dgvClientes.AllowUserToResizeRows = false;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Criação das colunas
            dgvClientes.Columns.Add("colID", "ID");
            dgvClientes.Columns.Add("colCliente", "Cliente");
            dgvClientes.Columns.Add("colTelefone", "Telefone");
            dgvClientes.Columns.Add("colEstado", "Estado");
            dgvClientes.Columns.Add("colCidade", "Cidade");
            dgvClientes.Columns.Add("colEmail", "E-mail");

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.DefaultCellStyle.BackColor = Color.LightBlue;
            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.Black;

            AtualizarTotalItens();

            cmbCamposFiltro = new ComboBox();
            cmbCamposFiltro.DropDownStyle = ComboBoxStyle.DropDownList; // impede digitação
            cmbCamposFiltro.Font = new Font("Segoe UI", 10);
            cmbCamposFiltro.Width = 200;
            cmbCamposFiltro.Location = new Point(10, 10); // ajuste conforme seu layout

            cmbCamposFiltro.Items.Add("Selecione o campo a filtrar");
            cmbCamposFiltro.Items.AddRange(new string[] {
            "ID", "Cliente", "Telefone", "Estado", "Cidade", "E-mail"
            });
            cmbCamposFiltro.SelectedIndex = 0; // Começa com a instrução

            this.Controls.Add(cmbCamposFiltro);
        }
        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            int index = dgvClientes.Rows.Add();

            DataGridViewRow novaLinha = dgvClientes.Rows[index];

            novaLinha.Cells["colID"].Value = "";
            novaLinha.Cells["colCliente"].Value = "";
            novaLinha.Cells["colTelefone"].Value = "";
            novaLinha.Cells["colEstado"].Value = "";
            novaLinha.Cells["colCidade"].Value = "";
            novaLinha.Cells["colEmail"].Value = "";

            AtualizarTotalItens();
        }
        private void dvgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelComando_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                dgvClientes.Rows.RemoveAt(dgvClientes.SelectedRows[0].Index);
                AtualizarTotalItens();
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblTotalItens_Click(object sender, EventArgs e)
        {
            int total = dgvClientes.Rows.Cast<DataGridViewRow>().Count(r => r.Visible);
            lblTotalClientes.Text = $"Total de itens: {total}";

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            int index = dgvClientes.Rows.Add();
            AtualizarTotalItens();
        }



        private void btnFiltro_Click(object sender, EventArgs e)
        {
            if (cmbCamposFiltro.SelectedIndex <= 0)
            {
                MessageBox.Show("Selecione um campo para filtrar.", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string campoSelecionado = cmbCamposFiltro.SelectedItem.ToString();
            string termo = Microsoft.VisualBasic.Interaction.InputBox(
                $"Digite o termo para filtrar pela coluna '{campoSelecionado}':",
                "Filtro");

            if (string.IsNullOrWhiteSpace(termo))
            {
                // Remove o filtro e mostra tudo novamente
                foreach (DataGridViewRow row in dgvClientes.Rows)
                    row.Visible = true;

                AtualizarTotalItens(); // Atualiza o total
                return;
            }

            string nomeColuna = null;

            if (campoSelecionado == "ID")
                nomeColuna = "colID";
            else if (campoSelecionado == "Cliente")
                nomeColuna = "colCliente";
            else if (campoSelecionado == "Telefone")
                nomeColuna = "colTelefone";
            else if (campoSelecionado == "Estado")
                nomeColuna = "colEstado";
            else if (campoSelecionado == "Cidade")
                nomeColuna = "colCidade";
            else if (campoSelecionado == "E-mail")
                nomeColuna = "colEmail";

            if (nomeColuna == null) return;

            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                string valor = row.Cells[nomeColuna]?.Value?.ToString() ?? "";
                row.Visible = valor.Equals(termo, StringComparison.OrdinalIgnoreCase);
            }

            AtualizarTotalItens(); // Conta apenas visíveis
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

