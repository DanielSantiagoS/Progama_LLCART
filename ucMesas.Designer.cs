namespace Forms_LLCART.Views
{
    partial class ucMesas
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();

            var lblTitulo = new System.Windows.Forms.Label();
            lblTitulo.Text = "Gestão de Mesas";
            lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            lblTitulo.Height = 60;
            lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.FromArgb(51, 51, 76);

            var flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanel.AutoScroll = true;
            flowPanel.Padding = new System.Windows.Forms.Padding(20);

            this.Controls.Add(flowPanel);
            this.Controls.Add(lblTitulo);

            this.ResumeLayout(false);
        }

        #endregion
    }
}