namespace Forms_LLCART_Projeto.Views
{
    partial class frmPedido
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblMesa = new System.Windows.Forms.Label();
            this.lblComanda = new System.Windows.Forms.Label();
            this.panelCategorias = new System.Windows.Forms.Panel();
            this.flowCategorias = new System.Windows.Forms.FlowLayoutPanel();
            this.panelProdutos = new System.Windows.Forms.Panel();
            this.flowProdutos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPedido = new System.Windows.Forms.Panel();
            this.flowItensPedido = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTotal = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnFinalizarPedido = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelCategorias.SuspendLayout();
            this.panelProdutos.SuspendLayout();
            this.panelPedido.SuspendLayout();
            this.panelTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelTop.Controls.Add(this.lblMesa);
            this.panelTop.Controls.Add(this.lblComanda);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.White;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesa.Location = new System.Drawing.Point(200, 20);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(62, 20);
            this.lblMesa.TabIndex = 1;
            this.lblMesa.Text = "Mesa: ";
            // 
            // lblComanda
            // 
            this.lblComanda.AutoSize = true;
            this.lblComanda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComanda.Location = new System.Drawing.Point(20, 20);
            this.lblComanda.Name = "lblComanda";
            this.lblComanda.Size = new System.Drawing.Size(94, 20);
            this.lblComanda.TabIndex = 0;
            this.lblComanda.Text = "Comanda: ";
            // 
            // panelCategorias
            // 
            this.panelCategorias.Controls.Add(this.flowCategorias);
            this.panelCategorias.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCategorias.Location = new System.Drawing.Point(0, 60);
            this.panelCategorias.Name = "panelCategorias";
            this.panelCategorias.Size = new System.Drawing.Size(984, 70);
            this.panelCategorias.TabIndex = 1;
            // 
            // flowCategorias
            // 
            this.flowCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCategorias.Location = new System.Drawing.Point(0, 0);
            this.flowCategorias.Name = "flowCategorias";
            this.flowCategorias.Padding = new System.Windows.Forms.Padding(10);
            this.flowCategorias.Size = new System.Drawing.Size(984, 70);
            this.flowCategorias.TabIndex = 0;
            // 
            // panelProdutos
            // 
            this.panelProdutos.Controls.Add(this.flowProdutos);
            this.panelProdutos.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProdutos.Location = new System.Drawing.Point(0, 130);
            this.panelProdutos.Name = "panelProdutos";
            this.panelProdutos.Size = new System.Drawing.Size(600, 431);
            this.panelProdutos.TabIndex = 2;
            // 
            // flowProdutos
            // 
            this.flowProdutos.AutoScroll = true;
            this.flowProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowProdutos.Location = new System.Drawing.Point(0, 0);
            this.flowProdutos.Name = "flowProdutos";
            this.flowProdutos.Padding = new System.Windows.Forms.Padding(10);
            this.flowProdutos.Size = new System.Drawing.Size(600, 431);
            this.flowProdutos.TabIndex = 0;
            // 
            // panelPedido
            // 
            this.panelPedido.Controls.Add(this.flowItensPedido);
            this.panelPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPedido.Location = new System.Drawing.Point(600, 130);
            this.panelPedido.Name = "panelPedido";
            this.panelPedido.Size = new System.Drawing.Size(384, 391);
            this.panelPedido.TabIndex = 3;
            // 
            // flowItensPedido
            // 
            this.flowItensPedido.AutoScroll = true;
            this.flowItensPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowItensPedido.Location = new System.Drawing.Point(0, 0);
            this.flowItensPedido.Name = "flowItensPedido";
            this.flowItensPedido.Padding = new System.Windows.Forms.Padding(10);
            this.flowItensPedido.Size = new System.Drawing.Size(384, 391);
            this.flowItensPedido.TabIndex = 0;
            // 
            // panelTotal
            // 
            this.panelTotal.BackColor = System.Drawing.Color.LightGray;
            this.panelTotal.Controls.Add(this.btnCancelar);
            this.panelTotal.Controls.Add(this.btnFinalizarPedido);
            this.panelTotal.Controls.Add(this.lblTotal);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotal.Location = new System.Drawing.Point(600, 521);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(384, 40);
            this.panelTotal.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelar.Location = new System.Drawing.Point(200, 8);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 25);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnFinalizarPedido
            // 
            this.btnFinalizarPedido.BackColor = System.Drawing.Color.LightGreen;
            this.btnFinalizarPedido.Location = new System.Drawing.Point(290, 8);
            this.btnFinalizarPedido.Name = "btnFinalizarPedido";
            this.btnFinalizarPedido.Size = new System.Drawing.Size(80, 25);
            this.btnFinalizarPedido.TabIndex = 1;
            this.btnFinalizarPedido.Text = "Finalizar";
            this.btnFinalizarPedido.UseVisualStyleBackColor = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(10, 12);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(85, 17);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total: R$ 0";
            // 
            // frmPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panelPedido);
            this.Controls.Add(this.panelTotal);
            this.Controls.Add(this.panelProdutos);
            this.Controls.Add(this.panelCategorias);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "frmPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novo Pedido";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCategorias.ResumeLayout(false);
            this.panelProdutos.ResumeLayout(false);
            this.panelPedido.ResumeLayout(false);
            this.panelTotal.ResumeLayout(false);
            this.panelTotal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.Label lblComanda;
        private System.Windows.Forms.Panel panelCategorias;
        private System.Windows.Forms.FlowLayoutPanel flowCategorias;
        private System.Windows.Forms.Panel panelProdutos;
        private System.Windows.Forms.FlowLayoutPanel flowProdutos;
        private System.Windows.Forms.Panel panelPedido;
        private System.Windows.Forms.FlowLayoutPanel flowItensPedido;
        private System.Windows.Forms.Panel panelTotal;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnFinalizarPedido;
        private System.Windows.Forms.Label lblTotal;
    }
}