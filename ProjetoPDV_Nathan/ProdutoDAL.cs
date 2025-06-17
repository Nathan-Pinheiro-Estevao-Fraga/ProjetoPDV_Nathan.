using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPDV_Nathan
{
    public class ProdutoDAL
    {
        private string conexao = $"Data Source={Application.StartupPath}\\dadosPDV.db";

        public void InserirProduto(string referencia, string descricao, string unitCompra, string unitVenda, decimal precoAtacado, decimal precoVarejo, int estoque)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Produtos 
        (referencia, descricao, unit_compra, unit_venda, preco_atacado, preco_varejo, estoque, fornecido, carteira, compradas, vendidas) 
        VALUES 
        (@referencia, @descricao, @unitCompra, @unitVenda, @precoAtacado, @precoVarejo, @estoque, 0, 0, 0, 0)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@referencia", referencia);
                    cmd.Parameters.AddWithValue("@descricao", descricao);
                    cmd.Parameters.AddWithValue("@unitCompra", unitCompra); // agora string (yyyy-MM-dd)
                    cmd.Parameters.AddWithValue("@unitVenda", unitVenda);   // string ou vazio
                    cmd.Parameters.AddWithValue("@precoAtacado", precoAtacado);
                    cmd.Parameters.AddWithValue("@precoVarejo", precoVarejo);
                    cmd.Parameters.AddWithValue("@estoque", estoque);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public class Produto
        {
            public string Referencia { get; set; }
            public string Descricao { get; set; }
            public decimal PrecoVarejo { get; set; }
            public string Unidade { get; set; }
            public string CaminhoImagem { get; set; }
        }

        public Produto BuscarPorReferencia(string referencia)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT * FROM Produtos WHERE referencia = @referencia";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@referencia", referencia);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Produto
                            {
                                Referencia = reader["referencia"].ToString(),
                                Descricao = reader["descricao"].ToString(),
                                PrecoVarejo = Convert.ToDecimal(reader["preco_varejo"]),
                                Unidade = "Un", // ajuste conforme sua tabela
                                CaminhoImagem = "" // só se usar imagem
                            };
                        }
                    }
                }
            }
            return null;
        }


        public void AtualizarProduto(DataRow row)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();

                string sql = @"UPDATE Produtos SET 
                        descricao = @descricao,
                        preco_atacado = @precoAtacado,
                        preco_varejo = @precoVarejo,
                        estoque = @estoque,
                        fornecido = @fornecido,
                        carteira = @carteira,
                        compradas = @compradas,
                        vendidas = @vendidas
                        WHERE referencia = @referencia";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@referencia", row["referencia"]);
                    cmd.Parameters.AddWithValue("@descricao", row["descricao"]);
                    cmd.Parameters.AddWithValue("@precoAtacado", row["preco_atacado"]);
                    cmd.Parameters.AddWithValue("@precoVarejo", row["preco_varejo"]);
                    cmd.Parameters.AddWithValue("@estoque", row["estoque"]);
                    cmd.Parameters.AddWithValue("@fornecido", row["fornecido"]);
                    cmd.Parameters.AddWithValue("@carteira", row["carteira"]);
                    cmd.Parameters.AddWithValue("@compradas", row["compradas"]);
                    cmd.Parameters.AddWithValue("@vendidas", row["vendidas"]);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        public DataTable CarregarProdutos()
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();

                string sql = @"SELECT 
                    referencia, 
                    descricao, 
                    unit_compra, 
                    unit_venda, 
                    preco_atacado, 
                    preco_varejo, 
                    estoque, 
                    fornecido, 
                    carteira, 
                    compradas, 
                    vendidas 
                FROM Produtos";

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public void ExcluirProduto(string referencia)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = "DELETE FROM Produtos WHERE referencia = @referencia";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@referencia", referencia);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
