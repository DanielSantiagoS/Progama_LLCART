namespace Forms_LLCART
{
    partial class frmMain
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnMesas = new System.Windows.Forms.Button();
            this.btnPedidos = new System.Windows.Forms.Button();
            this.btnCozinha = new System.Windows.Forms.Button();
            this.btnCaixa = new System.Windows.Forms.Button();
            this.btnRelatorios = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
           
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.btnRelatorios);
            this.panelMenu.Controls.Add(this.btnCaixa);
            this.panelMenu.Controls.Add(this.btnCozinha);
            this.panelMenu.Controls.Add(this.btnPedidos);
            this.panelMenu.Controls.Add(this.btnMesas);
            this.panelMenu.Controls.Add(this.lblTitulo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 561);
            this.panelMenu.TabIndex = 0;
           
            this.btnMesas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnMesas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMesas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMesas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesas.ForeColor = System.Drawing.Color.White;
            this.btnMesas.Location = new System.Drawing.Point(0, 80);
            this.btnMesas.Margin = new System.Windows.Forms.Padding(0);
            this.btnMesas.Name = "btnMesas";
            this.btnMesas.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnMesas.Size = new System.Drawing.Size(200, 60);
            this.btnMesas.TabIndex = 0;
            this.btnMesas.Text = "🏠 Mesas";
            this.btnMesas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMesas.UseVisualStyleBackColor = false;
           
            this.btnPedidos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnPedidos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPedidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPedidos.ForeColor = System.Drawing.Color.White;
            this.btnPedidos.Location = new System.Drawing.Point(0, 140);
            this.btnPedidos.Margin = new System.Windows.Forms.Padding(0);
            this.btnPedidos.Name = "btnPedidos";
            this.btnPedidos.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnPedidos.Size = new System.Drawing.Size(200, 60);
            this.btnPedidos.TabIndex = 1;
            this.btnPedidos.Text = "📋 Pedidos";
            this.btnPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPedidos.UseVisualStyleBackColor = false;
            
            this.btnCozinha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnCozinha.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCozinha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCozinha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCozinha.ForeColor = System.Drawing.Color.White;
            this.btnCozinha.Location = new System.Drawing.Point(0, 200);
            this.btnCozinha.Margin = new System.Windows.Forms.Padding(0);
            this.btnCozinha.Name = "btnCozinha";
            this.btnCozinha.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCozinha.Size = new System.Drawing.Size(200, 60);
            this.btnCozinha.TabIndex = 2;
            this.btnCozinha.Text = "👨‍🍳 Cozinha";
            this.btnCozinha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCozinha.UseVisualStyleBackColor = false;
          
            this.btnCaixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaixa.ForeColor = System.Drawing.Color.White;
            this.btnCaixa.Location = new System.Drawing.Point(0, 260);
            this.btnCaixa.Margin = new System.Windows.Forms.Padding(0);
            this.btnCaixa.Name = "btnCaixa";
            this.btnCaixa.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCaixa.Size = new System.Drawing.Size(200, 60);
            this.btnCaixa.TabIndex = 3;
            this.btnCaixa.Text = "💰 Caixa";
            this.btnCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaixa.UseVisualStyleBackColor = false;
          
            this.btnRelatorios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnRelatorios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRelatorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelatorios.ForeColor = System.Drawing.Color.White;
            this.btnRelatorios.Location = new System.Drawing.Point(0, 320);
            this.btnRelatorios.Margin = new System.Windows.Forms.Padding(0);
            this.btnRelatorios.Name = "btnRelatorios";
            this.btnRelatorios.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRelatorios.Size = new System.Drawing.Size(200, 60);
            this.btnRelatorios.TabIndex = 4;
            this.btnRelatorios.Text = "📊 Relatórios";
            this.btnRelatorios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorios.UseVisualStyleBackColor = false;
            
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(200, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(584, 561);
            this.panelContainer.TabIndex = 1;
           
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(10);
            this.lblTitulo.Size = new System.Drawing.Size(200, 80);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "LLCART Churrascaria";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema LLCART Churrascaria";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnMesas;
        private System.Windows.Forms.Button btnPedidos;
        private System.Windows.Forms.Button btnCozinha;
        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnRelatorios;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblTitulo;
    }
}