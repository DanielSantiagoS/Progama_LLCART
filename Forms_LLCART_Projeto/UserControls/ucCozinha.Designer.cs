namespace Forms_LLCART_Projeto.UserControls
{
    partial class ucCozinha
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowNovos;
        private System.Windows.Forms.FlowLayoutPanel flowPreparo;
        private System.Windows.Forms.FlowLayoutPanel flowProntos;
        private System.Windows.Forms.Label lblTitulo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            // Adicione esta parte para limpar o timer
            if (disposing)
            {
                timerAtualizacao?.Stop();
                timerAtualizacao?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.flowNovos = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPreparo = new System.Windows.Forms.FlowLayoutPanel();
            this.flowProntos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowNovos
            // 
            this.flowNovos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowNovos.AutoScroll = true;
            this.flowNovos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowNovos.Location = new System.Drawing.Point(20, 80);
            this.flowNovos.Name = "flowNovos";
            this.flowNovos.Size = new System.Drawing.Size(300, 461);
            this.flowNovos.TabIndex = 0;
            // 
            // flowPreparo
            // 
            this.flowPreparo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowPreparo.AutoScroll = true;
            this.flowPreparo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowPreparo.Location = new System.Drawing.Point(340, 80);
            this.flowPreparo.Name = "flowPreparo";
            this.flowPreparo.Size = new System.Drawing.Size(300, 461);
            this.flowPreparo.TabIndex = 1;
            // 
            // flowProntos
            // 
            this.flowProntos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowProntos.AutoScroll = true;
            this.flowProntos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowProntos.Location = new System.Drawing.Point(660, 80);
            this.flowProntos.Name = "flowProntos";
            this.flowProntos.Size = new System.Drawing.Size(300, 461);
            this.flowProntos.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(984, 60);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "PAINEL DA COZINHA";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucCozinha
            // 
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.flowProntos);
            this.Controls.Add(this.flowPreparo);
            this.Controls.Add(this.flowNovos);
            this.Name = "ucCozinha";
            this.Size = new System.Drawing.Size(984, 561);
            this.ResumeLayout(false);

        }

        #endregion
    }
}