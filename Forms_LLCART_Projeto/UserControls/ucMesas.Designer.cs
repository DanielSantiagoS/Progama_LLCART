

namespace Forms_LLCART_Projeto.UserControls
{
    partial class ucMesas
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Label lblTitulo;
        



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowPanel
            // 
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.AutoScroll = true;
            this.flowPanel.Padding = new System.Windows.Forms.Padding(20);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(584, 60);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestão de Mesas";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucMesas
            // 
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.lblTitulo);
            this.Name = "ucMesas";
            this.Size = new System.Drawing.Size(584, 561);
            this.ResumeLayout(false);

        }

        #endregion
    }
}