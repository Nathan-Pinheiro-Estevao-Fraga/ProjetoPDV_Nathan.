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
    public partial class ControlEstoque : UserControl
    {
        public ControlEstoque()
        {
            InitializeComponent();
            this.Load += Controle1_Load;
            dgvEstoque.CellValidating += dgvEstoque_CellValidating;
            dgvEstoque.EditingControlShowing += dgvEstoque_EditingControlShowing;            
            AtualizarTotalItens();

        }
        private void AtualizarTotalItens()
        {
            lblTotalItens.Text = $"Total de itens: {dgvEstoque.Rows.Count}";
        }

        private void dgvEstoque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private bool isReferenciaColuna = false;

        private void Controle1_Load(object sender, EventArgs e)
        {


            //Remove a permissão do usuário de redimensionar colunas e linhas, e adicionar linhas
            dgvEstoque.AllowUserToResizeColumns = false;
            dgvEstoque.AllowUserToResizeRows = false;            
                                                     

            //adicionando colunas:
            dgvEstoque.Columns.Add("colReferencia", "Referência");
            dgvEstoque.Columns.Add("colDescricao", "Descrição");
            dgvEstoque.Columns.Add("colDataCompra", "Unit U.Compra");
            dgvEstoque.Columns.Add("colDataVenda", "U. Venda");
            dgvEstoque.Columns.Add("colAtacado", "P. Atacado");
            dgvEstoque.Columns.Add("colVarejo", "P. Varejo");
            dgvEstoque.Columns.Add("colEstoque", "Estoque");
            dgvEstoque.Columns.Add("colFornecido", "Fornecido");
            dgvEstoque.Columns.Add("colCarteira", "Carteira");
            dgvEstoque.Columns.Add("colCompradas", "Compradas");
            dgvEstoque.Columns.Add("colVendidas", "Vendidas");

            //estilização da tabela:
            dgvEstoque.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstoque.DefaultCellStyle.BackColor = Color.LightBlue;
            dgvEstoque.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgvEstoque.DefaultCellStyle.SelectionForeColor = Color.Black;            

        }

        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            int index = dgvEstoque.Rows.Add();

            DataGridViewRow novaLinha = dgvEstoque.Rows[index];

            novaLinha.Cells["colReferencia"].Value = ""; // usuário irá preencher
            novaLinha.Cells["colDescricao"].Value = "";  // Aqui também
            novaLinha.Cells["colDataCompra"].Value = DateTime.Now.ToShortDateString(); // data atual
            novaLinha.Cells["colDataVenda"].Value = ""; 
            novaLinha.Cells["colAtacado"].Value = "R$ 0,00";
            novaLinha.Cells["colVarejo"].Value = "R$ 0,00";
            novaLinha.Cells["colEstoque"].Value = "0";
            novaLinha.Cells["colFornecido"].Value = "0";
            novaLinha.Cells["colCarteira"].Value = "0";
            novaLinha.Cells["colCompradas"].Value = "0";
            novaLinha.Cells["colVendidas"].Value = "0";

            AtualizarTotalItens();
        }

        //Para que a coluna Referência comece sempre com o "#"
        private void dgvEstoque_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEstoque.Columns[e.ColumnIndex].Name == "colReferencia")
            {
                string novaEntrada = e.FormattedValue?.ToString() ?? "";
                string valorAtual = dgvEstoque.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";

                // Ignora validação se a célula não foi editada de verdade
                if (!dgvEstoque.IsCurrentCellDirty)
                    return;

                // Permite campo vazio (não obriga digitar na hora da navegação)
                if (string.IsNullOrWhiteSpace(novaEntrada))
                    return;

                if (!novaEntrada.StartsWith("#"))
                {
                    MessageBox.Show(
                        "A referência deve começar com '#'. Exemplo: #1234 ou #Produto",
                        "Campo inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    e.Cancel = true;
                }
            }
        }
        private void TbReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Impede apagar o '#'
            if (tb.SelectionStart == 0 && e.KeyChar == (char)Keys.Back)
            {
                e.Handled = true;
            }

            // Impede digitar outro '#' no começo
            if (tb.SelectionStart == 0 && e.KeyChar == '#')
            {
                e.Handled = true;
            }
        }
        private void TbData_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (char.IsDigit(e.KeyChar))
            {
                if (tb.Text.Length == 2 || tb.Text.Length == 5)
                {
                    tb.Text += "/";
                    tb.SelectionStart = tb.Text.Length;
                }
            }
        }
        private void TbMoeda_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Permite apenas números, controle e uma única vírgula
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Impede múltiplas vírgulas
            if (e.KeyChar == ',' && tb.Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void TbMoeda_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Remove símbolo e espaços
            string textoLimpo = tb.Text.Replace("R$", "")
                                        .Replace(" ", "")
                                        .Trim();

            // Conversão para real
            if (decimal.TryParse(textoLimpo, out decimal valor))
            {
                tb.Text = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
            }
            else
            {
                tb.Text = "R$0,00";
            }
        }
        private void TbReferencia_TextChanged(object sender, EventArgs e)
        {
            if (!isReferenciaColuna) return;

            TextBox tb = sender as TextBox;

            if (!tb.Text.StartsWith("#"))
            {
                tb.Text = "#" + tb.Text.Replace("#", "");
                tb.SelectionStart = tb.Text.Length;
            }
        }
        private void dgvEstoque_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                // 🔴 Limpa sempre os eventos
                tb.KeyPress -= TbData_KeyPress;
                tb.KeyPress -= TbMoeda_KeyPress;
                tb.KeyPress -= TbReferencia_KeyPress;

                tb.Leave -= TbData_Leave;
                tb.Leave -= TbMoeda_Leave;

                tb.TextChanged -= TbReferencia_TextChanged;

                string nomeColuna = dgvEstoque.CurrentCell.OwningColumn.Name;

                isReferenciaColuna = false; // reset padrão

                if (nomeColuna == "colDataCompra" || nomeColuna == "colDataVenda")
                {
                    tb.MaxLength = 10;
                    tb.KeyPress += TbData_KeyPress;
                    tb.Leave += TbData_Leave;
                }
                else if (nomeColuna == "colAtacado" || nomeColuna == "colVarejo")
                {
                    tb.KeyPress += TbMoeda_KeyPress;
                    tb.Leave += TbMoeda_Leave;
                }
                else if (nomeColuna == "colReferencia")
                {
                    isReferenciaColuna = true;

                    tb.KeyPress += TbReferencia_KeyPress;
                    tb.TextChanged += TbReferencia_TextChanged;

                    // Preenche automaticamente com # apenas se estiver vazio
                    if (string.IsNullOrWhiteSpace(tb.Text))
                    {
                        tb.Text = "#";
                        tb.SelectionStart = tb.Text.Length;
                    }
                }
            }
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count > 0)
            {
                dgvEstoque.Rows.RemoveAt(dgvEstoque.SelectedRows[0].Index);
                AtualizarTotalItens();
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count > 0)
            {
                dgvEstoque.ReadOnly = false;
                MessageBox.Show("Agora você pode editar os campos diretamente na tabela.\nNão se esqueça de salvar se estiver usando banco depois.");
            }
            else
            {
                MessageBox.Show("Selecione uma linha para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count > 0)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Informe a quantidade a comprar:", "Comprar Produto", "1");

                if (int.TryParse(input, out int qtdComprada))
                {
                    DataGridViewRow linha = dgvEstoque.SelectedRows[0];

                    int estoqueAtual = int.Parse(linha.Cells["colEstoque"].Value.ToString());
                    int totalComprado = int.Parse(linha.Cells["colCompradas"].Value.ToString());

                    linha.Cells["colEstoque"].Value = (estoqueAtual + qtdComprada).ToString();
                    linha.Cells["colCompradas"].Value = (totalComprado + qtdComprada).ToString();
                    linha.Cells["colDataCompra"].Value = DateTime.Now.ToShortDateString();
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para comprar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            private void TbData_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            // Nome da coluna atual
            string nomeColuna = dgvEstoque.CurrentCell.OwningColumn.Name;

            // Se for Data de Venda e estiver vazio, não precisa validar
            if (nomeColuna == "colDataVenda" && string.IsNullOrWhiteSpace(tb.Text))
            {
                return;
            }

            if (!DateTime.TryParseExact(tb.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data))
            {
                MessageBox.Show("Data inválida. Verifique o dia, o mês e o ano (Ex: 23/05/2025).", "Data inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb.Focus(); // Força o usuário a corrigir
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count > 0)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Informe a quantidade a vender:", "Vender Produto", "1");

                if (int.TryParse(input, out int qtdVendida))
                {
                    DataGridViewRow linha = dgvEstoque.SelectedRows[0];

                    int estoqueAtual = int.Parse(linha.Cells["colEstoque"].Value.ToString());
                    int totalVendido = int.Parse(linha.Cells["colVendidas"].Value.ToString());

                    if (qtdVendida <= 0)
                    {
                        MessageBox.Show("A quantidade deve ser maior que zero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (estoqueAtual < qtdVendida)
                    {
                        MessageBox.Show("Estoque insuficiente para essa venda.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Atualiza estoque e vendas
                    linha.Cells["colEstoque"].Value = (estoqueAtual - qtdVendida).ToString();
                    linha.Cells["colVendidas"].Value = (totalVendido + qtdVendida).ToString();
                    linha.Cells["colDataVenda"].Value = DateTime.Now.ToShortDateString();
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para vender.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblTotalItens_Click(object sender, EventArgs e)
        {            

        }

        private void tableLayoutPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}    



