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
    public partial class ControlCompras : UserControl
    {
        public ControlCompras()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxDescricaoProduto_TextChanged(object sender, EventArgs e)
        {

        }
        private void AtualizarTotais()
        {
            decimal preco = decimal.Parse(textBoxPreçoUnitário.Text.Replace("R$", ""));
            int qtd = int.Parse(textBoxQuantidade.Text);
            decimal desconto = decimal.Parse(textBoxDeconto.Text) / 100;
            decimal comissao = decimal.Parse(textBoxComissão1.Text) / 100;

            decimal subtotal = preco * qtd;
            decimal valorDesconto = subtotal * desconto;
            decimal valorComissao = subtotal * comissao;

            textBoxValorDesconto.Text = valorDesconto.ToString("C2");
            textBoxValorComissão1.Text = valorComissao.ToString("C2");
            textBoxSubtotaldoItem.Text = (subtotal - valorDesconto).ToString("C2");
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
    }
    }
