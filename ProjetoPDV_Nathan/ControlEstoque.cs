using System;
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

            // Eventos
            this.Load += Controle1_Load;
            this.btnSalvarAlterações.Click += btnSalvarAlterações_Click;

            this.btnNovoProduto.Click += btnNovoProduto_Click;
            this.btnEditar.Click += btnEditar_Click;
            this.btnExcluir.Click += btnExcluir_Click;
            this.btnComprar.Click += btnComprar_Click;
            this.btnVender.Click += btnVender_Click;

            dgvEstoque.CellBeginEdit += dgvEstoque_CellBeginEdit;
            dgvEstoque.CellValidating += dgvEstoque_CellValidating;
            dgvEstoque.EditingControlShowing += dgvEstoque_EditingControlShowing;
        }

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
            if (dgvEstoque.Columns[e.ColumnIndex].Name == "unit_compra")
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
            ProdutoDAL dal = new ProdutoDAL();
            DataTable tabela = dal.CarregarProdutos();
            dgvEstoque.DataSource = tabela;

            // Configura colunas
            dgvEstoque.Columns["unit_compra"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEstoque.Columns["referencia"].HeaderText = "Referência";
            dgvEstoque.Columns["descricao"].HeaderText = "Descrição";
            dgvEstoque.Columns["unit_compra"].HeaderText = "Unit.U.Compra";
            dgvEstoque.Columns["unit_venda"].HeaderText = "U.Venda";
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
                    {
                        row.Cells["unit_compra"].Value = dataCompra.ToString("dd/MM/yyyy");
                    }

                    string rawVenda = row.Cells["unit_venda"].Value?.ToString();
                    if (DateTime.TryParse(rawVenda, out DateTime dataVenda))
                    {
                        row.Cells["unit_venda"].Value = dataVenda.ToString("dd/MM/yyyy");
                    }
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
            }
        }

        private void TbMoeda_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            string texto = new string(tb.Text.Where(char.IsDigit).ToArray());

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)Keys.Back && texto.Length > 0)
            {
                texto = texto.Substring(0, texto.Length - 1);
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                texto += e.KeyChar;
                e.Handled = true;
            }

            if (decimal.TryParse(texto, out decimal valor))
            {
                valor /= 100;
                tb.Text = valor.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
                tb.SelectionStart = tb.Text.Length;
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
            string referencia = Microsoft.VisualBasic.Interaction.InputBox("Digite a referência do produto (ex: #1234):", "Referência", "#" + new Random().Next(1000, 9999));
            if (string.IsNullOrWhiteSpace(referencia) || !referencia.StartsWith("#"))
            {
                MessageBox.Show("A Referência é obrigatória e deve começar com '#'.");
                return;
            }

            string descricao = Microsoft.VisualBasic.Interaction.InputBox("Digite a descrição do produto:", "Descrição", "Novo Produto");
            if (string.IsNullOrWhiteSpace(descricao)) descricao = "Novo Produto";

            DateTime precoCompra = DateTime.Now;
            string precoVenda = ""; // vazio, será preenchido apenas quando vender

            if (!ObterDecimal("Preço Atacado (ex: R$12,00):", out decimal precoAtacado)) return;
            if (!ObterDecimal("Preço Varejo (ex: R$16,00):", out decimal precoVarejo)) return;
            if (!ObterInteiro("Estoque inicial:", out int estoque)) return;

            ProdutoDAL dal = new ProdutoDAL();

            string unitCompra = DateTime.Now.ToString("yyyy-MM-dd");
            string unitVenda = ""; // ainda não houve venda

            dal.InserirProduto(referencia, descricao, unitCompra, unitVenda, precoAtacado, precoVarejo, estoque);


            CarregarDados();
            MessageBox.Show("Produto adicionado com sucesso!");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            dgvEstoque.ReadOnly = false;
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
                MessageBox.Show("Selecione um item para excluir.");
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (dgvEstoque.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um item para comprar.");
                return;
            }

            var row = dgvEstoque.SelectedRows[0];
            FormTipoCompra formTipo = new FormTipoCompra();
            if (formTipo.ShowDialog() != DialogResult.OK) return;

            string tipo = formTipo.TipoSelecionado;

            string input = Microsoft.VisualBasic.Interaction.InputBox("Quantidade:", "Entrada", "1");
            if (!int.TryParse(input, out int qtd) || qtd <= 0) return;

            int estoqueAtual = int.Parse(row.Cells["estoque"].Value.ToString());
            row.Cells["estoque"].Value = estoqueAtual + qtd;

            if (tipo.Equals("Fornecido", StringComparison.OrdinalIgnoreCase))
            {
                int fornecido = int.Parse(row.Cells["fornecido"].Value.ToString());
                row.Cells["fornecido"].Value = fornecido + qtd;
            }
            else if (tipo.Equals("Carteira", StringComparison.OrdinalIgnoreCase))
            {
                int carteira = int.Parse(row.Cells["carteira"].Value.ToString());
                int compradas = int.Parse(row.Cells["compradas"].Value.ToString());

                row.Cells["carteira"].Value = carteira + qtd;
                row.Cells["compradas"].Value = compradas + qtd;
            }
            else if (tipo.Equals("Comprado", StringComparison.OrdinalIgnoreCase))
            {
                int compradas = int.Parse(row.Cells["compradas"].Value.ToString());
                row.Cells["compradas"].Value = compradas + qtd;
            }

            row.Cells["unit_compra"].Value = DateTime.Now.ToString("dd/MM/yyyy");
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

            row.Cells["unit_venda"].Value = DateTime.Now.ToString("yyyy-MM-dd");
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
    }
}