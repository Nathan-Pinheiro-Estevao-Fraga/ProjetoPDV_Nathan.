using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPDV_Nathan
{
    public class ProdutoDAL
    {

        public void InserirProduto(string Referência, string descrição, decimal precoCompra, decimal precoVenda, decimal precoAtacado, decimal precoVarejo, int estoque)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Produtos 
            (referencia, descricao, preco_compra, preco_venda, preco_atacado, preco_varejo, estoque, fornecido, carteira, compradas, vendidas, data_compra, data_venda) 
            VALUES 
            (@referencia, @descricao, @precoCompra, @precoVenda, @precoAtacado, @precoVarejo, @estoque, 0, 0, 0, 0, @dataCompra, '')";

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@referencia", Referência);
                cmd.Parameters.AddWithValue("@descricao", descrição);
                cmd.Parameters.AddWithValue("@precoCompra", precoCompra);
                cmd.Parameters.AddWithValue("@precoVenda", precoVenda);
                cmd.Parameters.AddWithValue("@precoAtacado", precoAtacado);
                cmd.Parameters.AddWithValue("@precoVarejo", precoVarejo);
                cmd.Parameters.AddWithValue("@estoque", estoque);
                cmd.Parameters.AddWithValue("@dataCompra", DateTime.Now.ToString("dd/MM/yyyy"));

                cmd.ExecuteNonQuery();
            }
        }

        private string conexao = $"Data Source={Application.StartupPath}\\dadosPDV.db";

        public DataTable CarregarProdutos()
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT * FROM Produtos";
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
