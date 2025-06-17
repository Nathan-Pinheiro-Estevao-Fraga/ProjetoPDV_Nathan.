using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            btnSalvar.Click += btnSalvar_Click;
            dgvClientes.EditingControlShowing += dgvClientes_EditingControlShowing;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);            

        }
        private bool filtroAtivo = false;
        private void AtualizarTotalItens()
        {
            lblTotalClientes.Text = $"Total de itens: {dgvClientes.Rows.Count}";
        }
        //Declarando a combobox
        private TextBox textBox1;
        private ComboBox cmbCamposFiltro;

        private void Controle3_Load(object sender, EventArgs e)
        {
            //habilita edição
            dgvClientes.ReadOnly = false;            

            // Estilo e comportamento    
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToResizeColumns = false;
            dgvClientes.AllowUserToResizeRows = false;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.VirtualMode = false;


            // Criação das colunas
            dgvClientes.Columns.Add("colID", "ID");
            dgvClientes.Columns.Add("colCliente", "Cliente");
            dgvClientes.Columns.Add("colTelefone", "Telefone");
            dgvClientes.Columns.Add("colEstado", "Estado");
            dgvClientes.Columns.Add("colCidade", "Cidade");
            dgvClientes.Columns.Add("colEmail", "E-mail");

            dgvClientes.Columns["colID"].ReadOnly = true;

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.DefaultCellStyle.BackColor = Color.LightBlue;
            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.Black;

            AtualizarTotalItens();

            cmbCamposFiltro = new ComboBox();
            cmbCamposFiltro.DropDownStyle = ComboBoxStyle.DropDownList; 
            cmbCamposFiltro.Font = new Font("Segoe UI", 10);
            cmbCamposFiltro.Width = 200;
            cmbCamposFiltro.Location = new Point(10, 10); 

            cmbCamposFiltro.Items.Add("Selecione o campo a filtrar");
            cmbCamposFiltro.Items.AddRange(new string[] {
            "ID", "Cliente", "Telefone", "Estado", "Cidade", "E-mail"
            });
            cmbCamposFiltro.SelectedIndex = 0; // Começa com a instrução
            textBox1 = new TextBox();
            textBox1.Location = new Point(220, 10); // ajuste se necessário
            textBox1.Width = 200;
            textBox1.TextChanged += textBox1_TextChanged;

            this.Controls.Add(textBox1);

            this.Controls.Add(cmbCamposFiltro);

            
            CarregarDadosClientes();
        }
        private void CarregarDadosClientes()
        {
            if (filtroAtivo) return;

            ClienteDAL dal = new ClienteDAL();
            DataTable tabela = dal.ListarClientes();

            dgvClientes.Rows.Clear();
            foreach (DataRow row in tabela.Rows)
            {
                dgvClientes.Rows.Add(
                    row["id"], row["nome"], row["telefone"],
                    row["estado"], row["cidade"], row["email"]);
            }

            dgvClientes.Columns["colID"].ReadOnly = true;
            AtualizarTotalItens();
        }

        private void AplicarValidacoes()
        {
            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                if (row.IsNewRow) continue;

                string nome = row.Cells["colCliente"].Value?.ToString().Trim() ?? "";
                string telefone = row.Cells["colTelefone"].Value?.ToString().Trim() ?? "";
                string estado = row.Cells["colEstado"].Value?.ToString().Trim() ?? "";

                if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(estado) || string.IsNullOrEmpty(telefone))
                    throw new Exception($"Preencha Cliente, Estado e Telefone. Linha: {row.Index + 1}");

                // Verifica se telefone está no formato exato
                if (!Regex.IsMatch(telefone, @"^\(\d{2}\) \d{5}-\d{4}$"))
                    throw new Exception($"Telefone inválido. Use o formato (00) 00000-0000. Linha: {row.Index + 1}");
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ClienteDAL dal = new ClienteDAL();

            try
            {
                AplicarValidacoes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                if (row.IsNewRow) continue;

                try
                {
                    int id = int.TryParse(row.Cells["colID"].Value?.ToString(), out int val) ? val : 0;
                    string nome = row.Cells["colCliente"].Value?.ToString() ?? "";
                    string telefone = row.Cells["colTelefone"].Value?.ToString() ?? "";
                    string estado = row.Cells["colEstado"].Value?.ToString() ?? "";
                    string cidade = row.Cells["colCidade"].Value?.ToString() ?? "";
                    string email = row.Cells["colEmail"].Value?.ToString() ?? "";

                    if (id > 0)
                        dal.AtualizarCliente(id, nome, telefone, estado, cidade, email);
                    else
                        dal.InserirCliente(nome, telefone, estado, cidade, email);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar cliente na linha {row.Index + 1}: {ex.Message}");
                }
            }

            dal.ReorganizarIDs();
            CarregarDadosClientes();
            MessageBox.Show("Clientes salvos com sucesso!");
        }

        private void dgvClientes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvClientes.CurrentCell.OwningColumn.Name == "colTelefone" && e.Control is TextBox tb)
            {
                tb.KeyPress -= ApenasNumerosTelefone;
                tb.KeyPress += ApenasNumerosTelefone;
            }
        }

        private void ApenasNumerosTelefone(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
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
                var row = dgvClientes.SelectedRows[0];
                if (int.TryParse(row.Cells["colID"].Value?.ToString(), out int id))
                {
                    ClienteDAL dal = new ClienteDAL();
                    dal.ExcluirCliente(id);                    
                    dal.ReorganizarIDs();
                    CarregarDadosClientes();
                }
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
            ClienteDAL dal = new ClienteDAL();

            string nome = "Novo Cliente";
            string telefone = "(00) 00000-0000";
            string estado = "SP";
            string cidade = "São Paulo";
            string email = "exemplo@email.com";

            dal.InserirCliente(nome, telefone, estado, cidade, email);
            CarregarDadosClientes();
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
        private string RemoverAcentos(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return "";

            var normalized = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (filtroAtivo) return;

            filtroAtivo = true;

            try
            {
                string termo = RemoverAcentos(textBox1.Text.Trim().ToLower());

                foreach (DataGridViewRow row in dgvClientes.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool visivel = false;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string valor = cell.Value?.ToString() ?? "";
                        valor = RemoverAcentos(valor.ToLower());

                        if (valor.Contains(termo))
                        {
                            visivel = true;
                            break;
                        }
                    }

                    row.Visible = visivel;
                }

                AtualizarTotalItens();
                dgvClientes.Refresh();
            }
            finally
            {
                filtroAtivo = false;
            }

        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cliente para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvClientes.SelectedRows[0];
            string nome = row.Cells["colCliente"].Value?.ToString() ?? "";
            string telefone = row.Cells["colTelefone"].Value?.ToString() ?? "";
            string estado = row.Cells["colEstado"].Value?.ToString() ?? "";
            string cidade = row.Cells["colCidade"].Value?.ToString() ?? "";
            string email = row.Cells["colEmail"].Value?.ToString() ?? "";

            using (FormEditarCliente form = new FormEditarCliente(nome, telefone, estado, cidade, email))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    row.Cells["colCliente"].Value = form.Nome;
                    row.Cells["colTelefone"].Value = form.Telefone;
                    row.Cells["colEstado"].Value = form.Estado;
                    row.Cells["colCidade"].Value = form.Cidade;
                    row.Cells["colEmail"].Value = form.Email;
                }
            }
        }
    }
}

