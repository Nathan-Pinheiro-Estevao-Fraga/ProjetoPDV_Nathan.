using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public class ClienteDAL
    {
        private string conexao = $"Data Source={Application.StartupPath}\\dadosPDV.db";

        public DataTable ListarClientes()
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = "SELECT id, nome, telefone, estado, cidade, email FROM Clientes";
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }
        }

        public void InserirCliente(string nome, string telefone, string estado, string cidade, string email)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = @"INSERT INTO Clientes (nome, telefone, estado, cidade, email)
                               VALUES (@nome, @telefone, @estado, @cidade, @email)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@cidade", cidade);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarCliente(int id, string nome, string telefone, string estado, string cidade, string email)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = @"UPDATE Clientes SET nome = @nome, telefone = @telefone,
                                estado = @estado, cidade = @cidade, email = @email
                               WHERE id = @id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@cidade", cidade);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirCliente(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();
                string sql = "DELETE FROM Clientes WHERE id = @id";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ReorganizarIDs()
        {
            using (SQLiteConnection conn = new SQLiteConnection(conexao))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = @"
                        CREATE TEMP TABLE temp_clientes AS
                        SELECT nome, telefone, estado, cidade, email FROM Clientes ORDER BY id;

                        DELETE FROM Clientes;

                        INSERT INTO Clientes (nome, telefone, estado, cidade, email)
                        SELECT nome, telefone, estado, cidade, email FROM temp_clientes;

                        DROP TABLE temp_clientes;";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
