using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProjetoPDV_Nathan.ProdutoDAL;

namespace ProjetoPDV_Nathan
{
    public partial class ControlCompras : UserControl
    {
        public ControlCompras()
        {
            InitializeComponent();

            this.textBoxReferência.KeyDown += new KeyEventHandler(this.textBoxReferência_KeyDown);
            textBoxQuantidade.TextChanged += textBoxQuantidade_TextChanged;
            textBoxPreçoUnitário.TextChanged += textBoxPreçoUnitário_TextChanged;
            textBoxDeconto.TextChanged += textBoxDeconto_TextChanged;
            textBoxComissão1.TextChanged += textBoxComissão1_TextChanged;

            comboBoxVendeRepresen.Items.Clear();
            comboBoxVendeRepresen.Items.Add("Matheus");
            comboBoxVendeRepresen.Items.Add("Kézia");
            comboBoxVendeRepresen.SelectedIndex = 0;
        }
        private void textBoxQuantidade_TextChanged(object sender, EventArgs e) => AtualizarTotais();
        private void textBoxPreçoUnitário_TextChanged(object sender, EventArgs e) => AtualizarTotais();
        private void textBoxDeconto_TextChanged(object sender, EventArgs e) => AtualizarTotais();
        private void textBoxComissão1_TextChanged(object sender, EventArgs e) => AtualizarTotais();

        private void AtualizarTotais()
        {
            if (!decimal.TryParse(textBoxPreçoUnitário.Text.Replace("R$", "").Trim(), out decimal preco))
                preco = 0;

            if (!int.TryParse(textBoxQuantidade.Text.Trim(), out int qtd))
                qtd = 0;

            if (!decimal.TryParse(textBoxDeconto.Text.Trim(), out decimal desconto))
                desconto = 0;

            if (!decimal.TryParse(textBoxComissão1.Text.Trim(), out decimal comissao))
                comissao = 0;

            desconto /= 100;
            comissao /= 100;

            decimal subtotal = preco * qtd;
            decimal valorDesconto = subtotal * desconto;
            decimal valorComissao = subtotal * comissao;
            decimal total = subtotal - valorDesconto;

            textBoxValorDesconto.Text = valorDesconto.ToString("C2");
            textBoxValorComissão1.Text = valorComissao.ToString("C2");
            textBoxSubtotaldoItem.Text = total.ToString("C2");
        }

        // outros métodos como:
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxDescricaoProduto_TextChanged(object sender, EventArgs e)
        {
             
        }       

        private void textBoxReferência_TextChanged(object sender, EventArgs e)
        {                 
            /*if (e.KeyCode == Keys.Enter)
            {
                string referencia = textBoxReferência.Text;
                Produto p = ProdutoDAL.BuscarPorReferencia(referencia); // você cria esse método no DAL

                if (p != null)
                {
                    textBoxDescricaoProduto.Text = p.Descricao;
                    textBoxUnidade.Text = p.Unidade;
                    textBoxPreçoUnitário.Text = p.PrecoUnitario.ToString("C2");
                    pictureboxImagemProduto.Image = Image.FromFile(p.CaminhoImagem);
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.");
                }
            }*/
        }

        private void labelFinalizarPedido_Click(object sender, EventArgs e)
        {

        }

        private void labelNovoPedido_Click(object sender, EventArgs e)
        {

        }

        private void AplicarBordasNosPainéis(Control controlePai)
        {
            foreach (Control ctrl in controlePai.Controls)
            {
                if (ctrl is Panel panel)
                {
                    panel.BorderStyle = BorderStyle.FixedSingle;
                }

                // Verifica recursivamente os controles filhos
                if (ctrl.HasChildren)
                {
                    AplicarBordasNosPainéis(ctrl);
                }
            }
        }

        private void textBoxReferência_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string referencia = textBoxReferência.Text.Trim();
                ProdutoDAL dal = new ProdutoDAL();
                Produto produto = dal.BuscarPorReferencia(referencia);

                if (produto != null)
                {
                    textBoxDescricaoProduto.Text = produto.Descricao;
                    textBoxUnidade.Text = produto.Unidade;
                    textBoxPreçoUnitário.Text = produto.PrecoVarejo.ToString("C2");
                    // Se tiver imagem:
                    // pictureboxImagemProduto.Image = Image.FromFile(produto.CaminhoImagem);
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ControlCompras_Load(object sender, EventArgs e)
        {
            AplicarBordasNosPainéis(this);

            textBoxData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            textBoxHora.Text = DateTime.Now.ToString("HH:mm:ss");
            textBoxData.ReadOnly = true;
            textBoxHora.ReadOnly = true;

        }

        private void textBoxData_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxVendeRepresen_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxVendeRepresen.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void pictureboxImagemProduto_Click(object sender, EventArgs e)
        {

        }
    }
    }
