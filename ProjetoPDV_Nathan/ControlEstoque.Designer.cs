namespace ProjetoPDV_Nathan
{
    partial class ControlEstoque
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlEstoque));
            this.dgvEstoque = new System.Windows.Forms.DataGridView();
            this.panelRodape = new System.Windows.Forms.Panel();
            this.btnSalvarAlterações = new System.Windows.Forms.Button();
            this.lblTotalItens = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnVender = new System.Windows.Forms.Button();
            this.btnNovoProduto = new System.Windows.Forms.Button();
            this.btnComprar = new System.Windows.Forms.Button();
            this.tableLayoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).BeginInit();
            this.panelRodape.SuspendLayout();
            this.tableLayoutPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEstoque
            // 
            this.dgvEstoque.AllowUserToAddRows = false;
            this.dgvEstoque.AllowUserToDeleteRows = false;
            this.dgvEstoque.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEstoque.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvEstoque.Location = new System.Drawing.Point(0, 0);
            this.dgvEstoque.Margin = new System.Windows.Forms.Padding(0);
            this.dgvEstoque.Name = "dgvEstoque";
            this.dgvEstoque.RowHeadersVisible = false;
            this.dgvEstoque.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstoque.Size = new System.Drawing.Size(1047, 540);
            this.dgvEstoque.TabIndex = 0;
            this.dgvEstoque.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEstoque_CellContentClick);
            // 
            // panelRodape
            // 
            this.panelRodape.BackColor = System.Drawing.Color.Gainsboro;
            this.panelRodape.Controls.Add(this.btnSalvarAlterações);
            this.panelRodape.Controls.Add(this.lblTotalItens);
            this.panelRodape.Controls.Add(this.btnExcluir);
            this.panelRodape.Controls.Add(this.btnEditar);
            this.panelRodape.Controls.Add(this.btnVender);
            this.panelRodape.Controls.Add(this.btnNovoProduto);
            this.panelRodape.Controls.Add(this.btnComprar);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRodape.Location = new System.Drawing.Point(0, 540);
            this.panelRodape.Margin = new System.Windows.Forms.Padding(0);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(1047, 60);
            this.panelRodape.TabIndex = 7;
            // 
            // btnSalvarAlterações
            // 
            this.btnSalvarAlterações.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarAlterações.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarAlterações.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvarAlterações.Image")));
            this.btnSalvarAlterações.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalvarAlterações.Location = new System.Drawing.Point(667, 6);
            this.btnSalvarAlterações.Name = "btnSalvarAlterações";
            this.btnSalvarAlterações.Size = new System.Drawing.Size(89, 48);
            this.btnSalvarAlterações.TabIndex = 12;
            this.btnSalvarAlterações.Text = "Salvar";
            this.btnSalvarAlterações.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalvarAlterações.UseVisualStyleBackColor = true;
            this.btnSalvarAlterações.Click += new System.EventHandler(this.btnSalvarAlterações_Click);
            // 
            // lblTotalItens
            // 
            this.lblTotalItens.AutoSize = true;
            this.lblTotalItens.Location = new System.Drawing.Point(24, 23);
            this.lblTotalItens.Name = "lblTotalItens";
            this.lblTotalItens.Size = new System.Drawing.Size(83, 13);
            this.lblTotalItens.TabIndex = 6;
            this.lblTotalItens.Text = "Total de itens: 0";
            this.lblTotalItens.Click += new System.EventHandler(this.lblTotalItens_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExcluir.Location = new System.Drawing.Point(952, 6);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(89, 48);
            this.btnExcluir.TabIndex = 3;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditar.Location = new System.Drawing.Point(572, 6);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(89, 48);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnVender
            // 
            this.btnVender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVender.Image = ((System.Drawing.Image)(resources.GetObject("btnVender.Image")));
            this.btnVender.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVender.Location = new System.Drawing.Point(857, 6);
            this.btnVender.Name = "btnVender";
            this.btnVender.Size = new System.Drawing.Size(89, 48);
            this.btnVender.TabIndex = 5;
            this.btnVender.Text = "Vender";
            this.btnVender.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVender.UseVisualStyleBackColor = true;
            this.btnVender.Click += new System.EventHandler(this.btnVender_Click);
            // 
            // btnNovoProduto
            // 
            this.btnNovoProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoProduto.Image = ((System.Drawing.Image)(resources.GetObject("btnNovoProduto.Image")));
            this.btnNovoProduto.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNovoProduto.Location = new System.Drawing.Point(477, 6);
            this.btnNovoProduto.Name = "btnNovoProduto";
            this.btnNovoProduto.Size = new System.Drawing.Size(89, 48);
            this.btnNovoProduto.TabIndex = 1;
            this.btnNovoProduto.Text = "Novo";
            this.btnNovoProduto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNovoProduto.UseVisualStyleBackColor = true;
            this.btnNovoProduto.Click += new System.EventHandler(this.btnNovoProduto_Click);
            // 
            // btnComprar
            // 
            this.btnComprar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComprar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComprar.Image = ((System.Drawing.Image)(resources.GetObject("btnComprar.Image")));
            this.btnComprar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnComprar.Location = new System.Drawing.Point(762, 6);
            this.btnComprar.Name = "btnComprar";
            this.btnComprar.Size = new System.Drawing.Size(89, 48);
            this.btnComprar.TabIndex = 4;
            this.btnComprar.Text = "Comprar";
            this.btnComprar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnComprar.UseVisualStyleBackColor = true;
            this.btnComprar.Click += new System.EventHandler(this.btnComprar_Click);
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelRodape, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.dgvEstoque, 0, 0);
            this.tableLayoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPrincipal.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            this.tableLayoutPrincipal.RowCount = 2;
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPrincipal.Size = new System.Drawing.Size(1047, 600);
            this.tableLayoutPrincipal.TabIndex = 8;
            // 
            // ControlEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPrincipal);
            this.Name = "ControlEstoque";
            this.Size = new System.Drawing.Size(1047, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).EndInit();
            this.panelRodape.ResumeLayout(false);
            this.panelRodape.PerformLayout();
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEstoque;
        private System.Windows.Forms.Panel panelRodape;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPrincipal;
        private System.Windows.Forms.Button btnNovoProduto;
        private System.Windows.Forms.Button btnSalvarAlterações;
        private System.Windows.Forms.Label lblTotalItens;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnVender;
        private System.Windows.Forms.Button btnComprar;
    }
}