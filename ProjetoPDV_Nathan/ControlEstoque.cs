using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class ControlEstoque : UserControl
    {
        public ControlEstoque()
        {
            InitializeComponent();
            CarregarDados();


        }

        private ComboBox comboEntradaTipo;
        private TextBox txtQuantidade;
        private Button btnAdicionarAoEstoque;

        private void Controle1_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }
        private DateTimePicker dtp = new DateTimePicker();

        private void dtp_OnTextChange(object sender, EventArgs e)
        {
            dgvEstoque.CurrentCell.Value = dtp.Text;
        }
        private void dtp_CloseUp(object sender, EventArgs e)
        {
            dtp.Visible = false;
        }

        private void dgvEstoque_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string colName = dgvEstoque.Columns[e.ColumnIndex].Name;

            if (colName == "unit_compra" || colName == "unit_venda")

            {
                Rectangle _dtpRect = dgvEstoque.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);


                dtp.Visible = true;
                dtp.Format = DateTimePickerFormat.Short;
                dtp.Size = new Size(_dtpRect.Width, _dtpRect.Height);
                dtp.Location = new Point(_dtpRect.X, _dtpRect.Y);

                dtp.CloseUp -= dtp_CloseUp;
                dtp.TextChanged -= dtp_OnTextChange;
                dtp.CloseUp += dtp_CloseUp;
                dtp.TextChanged += dtp_OnTextChange;

                object cellValue = dgvEstoque.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (cellValue != null && DateTime.TryParse(cellValue.ToString(), out DateTime data))
                    dtp.Value = data;
                else
                    dtp.Value = DateTime.Today;



                dgvEstoque.Controls.Add(dtp);
                dtp.BringToFront();
            }
        }
        private void CarregarDados()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            ProdutoDAL dal = new ProdutoDAL();
            DataTable tabela = dal.CarregarProdutos();
            dgvEstoque.DataSource = tabela;

            dgvEstoque.Columns["unit_compra"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEstoque.Columns["unit_venda"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvEstoque.Columns["referencia"].HeaderText = "Referência";
            dgvEstoque.Columns["descricao"].HeaderText = "Descrição";
            dgvEstoque.Columns["unit_compra"].HeaderText = "Unit.U.Compra";
            dgvEstoque.Columns["unit_venda"].HeaderText = "U.Venda";
            dgvEstoque.Columns["preco_atacado"].DefaultCellStyle.Format = "C2";
            dgvEstoque.Columns["preco_varejo"].DefaultCellStyle.Format = "C2";
            dgvEstoque.Columns["preco_atacado"].HeaderText = "P.Atacado";
            dgvEstoque.Columns["preco_varejo"].HeaderText = "P.Varejo";
            dgvEstoque.Columns["estoque"].HeaderText = "Estoque";
            dgvEstoque.Columns["fornecido"].HeaderText = "Fornecido";
            dgvEstoque.Columns["carteira"].HeaderText = "Carteira";
            dgvEstoque.Columns["compradas"].HeaderText = "Compradas";
            dgvEstoque.Columns["vendidas"].HeaderText = "Vendidas";

            dgvEstoque.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstoque.DefaultCellStyle.BackColor = Color.LightBlue;
            dgvEstoque.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgvEstoque.DefaultCellStyle.SelectionForeColor = Color.Black;

            foreach (DataGridViewRow row in dgvEstoque.Rows)
            {
                if (!row.IsNewRow)
                {
                    string rawCompra = row.Cells["unit_compra"].Value?.ToString();
                    if (DateTime.TryParse(rawCompra, out DateTime dataCompra))
                        row.Cells["unit_compra"].Value = dataCompra.ToString("dd/MM/yyyy");

                    string rawVenda = row.Cells["unit_venda"].Value?.ToString();
                    if (DateTime.TryParse(rawVenda, out DateTime dataVenda))
                        row.Cells["unit_venda"].Value = dataVenda.ToString("dd/MM/yyyy");
                }
            }

            AtualizarTotalItens();
        }


        private void AtualizarTotalItens()
        {
            lblTotalItens.Text = $"Total de itens: {dgvEstoque.Rows.Count}";
        }

        private void dgvEstoque_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvEstoque.Columns[e.ColumnIndex].Name == "referencia")
            {
                string novaEntrada = e.FormattedValue?.ToString() ?? "";
                if (!dgvEstoque.IsCurrentCellDirty || string.IsNullOrWhiteSpace(novaEntrada))
                    return;

                if (!novaEntrada.StartsWith("#"))
                {
                    MessageBox.Show("A referência deve começar com '#'.", "Campo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
        }

        private void dgvEstoque_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                tb.KeyPress -= TbMoeda_KeyPress;
                tb.KeyPress -= TbSomenteNumeros_KeyPress;

                string nomeColuna = dgvEstoque.CurrentCell.OwningColumn.Name;

                if (nomeColuna == "preco_compra" || nomeColuna == "preco_venda" || nomeColuna == "preco_atacado" || nomeColuna == "preco_varejo")
                    tb.KeyPress += TbMoeda_KeyPress;
                else if (nomeColuna == "estoque" || nomeColuna == "fornecido" || nomeColuna == "carteira" || nomeColuna == "compradas" || nomeColuna == "vendidas")
                    tb.KeyPress += TbSomenteNumeros_KeyPress;
                // Define a máscara apenas para colunas de preço
                if ((nomeColuna == "preco_compra" || nomeColuna == "preco_venda" ||
                     nomeColuna == "preco_atacado" || nomeColuna == "preco_varejo") &&
                    (string.IsNullOrWhiteSpace(tb.Text) || !tb.Text.StartsWith("R$")))
                {
                    tb.Text = "R$0,00";
                    tb.SelectionStart = tb.Text.Length;
                }


            }
        }

        private void TbMoeda_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            e.Handled = true;

            // Captura apenas dígitos
            if (char.IsDigit(e.KeyChar))
            {
                string numeros = new string(tb.Text.Where(char.IsDigit).ToArray()) + e.KeyChar;

                if (decimal.TryParse(numeros, out decimal valor))
                {
                    valor /= 100;
                    tb.Text = "R$" + valor.ToString("N2");
                    tb.SelectionStart = tb.Text.Length;
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                string numeros = new string(tb.Text.Where(char.IsDigit).ToArray());

                if (numeros.Length > 0)
                    numeros = numeros.Substring(0, numeros.Length - 1);

                if (decimal.TryParse(numeros, out decimal valor))
                {
                    valor /= 100;
                    tb.Text = "R$" + valor.ToString("N2");
                    tb.SelectionStart = tb.Text.Length;
                }
                else
                {
                    tb.Text = "R$0,00";
                    tb.SelectionStart = tb.Text.Length;
                }
            }
        }


        private void TbSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private bool ObterDecimal(string mensagem, out decimal valor)
        {
            valor = 0;
            while (true)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox(mensagem, "Valor", "0");

                if (string.IsNullOrWhiteSpace(input)) return false;

                if (decimal.TryParse(input.Replace("R$", "").Trim(), out valor))
                    return true;

                MessageBox.Show("Digite um valor numérico válido. Ex: 15,50");
            }
        }

        private bool ObterInteiro(string mensagem, out int valor)
        {
            valor = 0;
            string input = Microsoft.VisualBasic.Interaction.InputBox(mensagem, "Número Inteiro", "0");

            if (string.IsNullOrWhiteSpace(input)) return false;

            if (!int.TryParse(input, out valor))
            {
                MessageBox.Show("Digite apenas números inteiros.");
                return false;
            }
            return true;
        }


        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            string referencia = Microsoft.VisualBasic.Interaction.InputBox(
                "Digite a referência do produto (ex: #1234):", "Referência", "#" + new Random().Next(1000, 9999));
            if (string.IsNullOrWhiteSpace(referencia) || !referencia.StartsWith("#"))
            {
                MessageBox.Show("A Referência é obrigatória e deve começar com '#'.");
                return;
            }

            string descricao = Microsoft.VisualBasic.Interaction.InputBox("Digite a descrição do produto:", "Descrição", "Novo Produto");
            if (string.IsNullOrWhiteSpace(descricao)) descricao = "Novo Produto";

            if (!ObterDecimal("Preço Atacado (ex: R$12,00):", out decimal precoAtacado)) return;
            if (!ObterDecimal("Preço Varejo (ex: R$16,00):", out decimal precoVarejo)) return;

            ProdutoDAL dal = new ProdutoDAL();
            string unitCompra = DateTime.Now.ToString("yyyy-MM-dd");
            string unitVenda = ""; // Ainda não houve venda
            int estoqueInicial = 0; // Sempre começa com 0

            dal.InserirProduto(referencia, descricao, unitCompra, unitVenda, precoAtacado, precoVarejo, estoqueInicial);

            CarregarDados();
            MessageBox.Show("Produto adicionado com sucesso!");
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {

        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um item para excluir.");
                return;
            }

            DialogResult result = MessageBox.Show("Deseja realmente excluir o item selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            ProdutoDAL dal = new ProdutoDAL();

            // Armazena as referências a excluir
            var linhasParaRemover = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgvEstoque.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    string referencia = row.Cells["referencia"].Value?.ToString();
                    if (!string.IsNullOrEmpty(referencia))
                    {
                        dal.ExcluirProduto(referencia);
                        linhasParaRemover.Add(row);
                    }
                }
            }

            // Remove as linhas fora do loop principal
            foreach (var row in linhasParaRemover)
                dgvEstoque.Rows.Remove(row);

            AtualizarTotalItens();
        }




        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um item para comprar.");
                return;
            }

            var row = dgvEstoque.SelectedRows[0];

            using (FormCompraTipo form = new FormCompraTipo())
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                string tipo = form.TipoSelecionado;
                int qtd = form.Quantidade;

                // Atualiza estoque
                int estoqueAtual = Convert.ToInt32(row.Cells["estoque"].Value ?? 0);
                row.Cells["estoque"].Value = estoqueAtual + qtd;

                // Atualiza campo específico
                if (tipo == "Comprado")
                {
                    int compradas = Convert.ToInt32(row.Cells["compradas"].Value ?? 0);
                    row.Cells["compradas"].Value = compradas + qtd;

                    // Atualiza data de compra (somente se for "Comprado")
                    row.Cells["unit_compra"].Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else if (tipo == "Carteira")
                {
                    int carteira = Convert.ToInt32(row.Cells["carteira"].Value ?? 0);
                    row.Cells["carteira"].Value = carteira + qtd;

                    int compradas = Convert.ToInt32(row.Cells["compradas"].Value ?? 0);
                    row.Cells["compradas"].Value = compradas + qtd;
                }
                else if (tipo == "Fornecido")
                {
                    int fornecido = Convert.ToInt32(row.Cells["fornecido"].Value ?? 0);
                    row.Cells["fornecido"].Value = fornecido + qtd;
                }
            }
        }



        private void btnVender_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um item para vender.");
                return;
            }

            var row = dgvEstoque.SelectedRows[0];
            string input = Microsoft.VisualBasic.Interaction.InputBox("Quantidade vendida:", "Venda", "1");
            if (!int.TryParse(input, out int qtd) || qtd <= 0)
                return;

            int estoqueAtual = int.Parse(row.Cells["estoque"].Value.ToString());
            if (estoqueAtual < qtd)
            {
                MessageBox.Show("Estoque insuficiente.");
                return;
            }

            row.Cells["estoque"].Value = estoqueAtual - qtd;
            int vendidas = int.Parse(row.Cells["vendidas"].Value.ToString());
            row.Cells["vendidas"].Value = vendidas + qtd;

            row.Cells["unit_venda"].Value = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void dgvEstoque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSalvarAlterações_Click(object sender, EventArgs e)
        {
            ProdutoDAL dal = new ProdutoDAL();

            foreach (DataGridViewRow row in dgvEstoque.Rows)
            {
                if (row.IsNewRow) continue; // Ignora a última linha vazia

                try
                {
                    // Verifica se o DataSource é um DataTable
                    if (row.DataBoundItem is DataRowView drv)
                    {
                        dal.AtualizarProduto(drv.Row); // Usa método já existente
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o item na linha {row.Index + 1}.\n\nDetalhes: {ex.Message}", "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            MessageBox.Show("Alterações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CarregarDados(); // Recarrega dados atualizados
            dgvEstoque.Columns["unit_compra"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEstoque.Columns["unit_venda"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void lblTotalItens_Click(object sender, EventArgs e)
        {

        }
    }
}