using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjetoPDV_Nathan
{
    public partial class FiltroTesteControl : UserControl
    {
        private TextBox txtPesquisa;
        private DataGridView dgv;
        private bool filtroAtivo = false;

        public FiltroTesteControl()
        {
            InitializeComponent();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Size = new Size(800, 400);

            txtPesquisa = new TextBox();
            txtPesquisa.Location = new Point(10, 10);
            txtPesquisa.Width = 200;
            txtPesquisa.TextChanged += TxtPesquisa_TextChanged;
            this.Controls.Add(txtPesquisa);

            dgv = new DataGridView();
            dgv.Location = new Point(10, 50);
            dgv.Size = new Size(780, 300);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;

            dgv.Columns.Add("colID", "ID");
            dgv.Columns.Add("colNome", "Nome");
            dgv.Columns.Add("colCidade", "Cidade");

            this.Controls.Add(dgv);

            // Preenchimento de exemplo
            dgv.Rows.Add("1", "João da Silva", "São Paulo");
            dgv.Rows.Add("2", "Maria Souza", "Campinas");
            dgv.Rows.Add("3", "José Costa", "Belo Horizonte");
            dgv.Rows.Add("4", "Ana Lima", "Rio de Janeiro");
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (filtroAtivo) return;
            filtroAtivo = true;

            try
            {
                string termo = RemoverAcentos(txtPesquisa.Text.Trim().ToLower());

                foreach (DataGridViewRow row in dgv.Rows)
                {
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
            }
            finally
            {
                filtroAtivo = false;
            }
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
    }
}
