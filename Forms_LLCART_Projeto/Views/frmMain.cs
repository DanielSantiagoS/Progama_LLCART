using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms_LLCART_Projeto.Views
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            ConfigurarEventos();
            CarregarDashboard();
        }

        private void ConfigurarEventos()
        {
            btnMesas.Click += (s, e) => CarregarMesas();
            btnPedidos.Click += (s, e) => CarregarPedidos();
            btnCozinha.Click += (s, e) => CarregarCozinha();
            btnCaixa.Click += (s, e) => CarregarCaixa();
            btnRelatorios.Click += (s, e) => CarregarRelatorios();
        }

        private void CarregarDashboard()
        {
            panelContainer.Controls.Clear();

            var label = new Label
            {
                Text = "Bem-vindo ao Sistema LLCART Churrascaria",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            panelContainer.Controls.Add(label);
        }

        private void CarregarMesas()
        {
            panelContainer.Controls.Clear();
            var ucMesas = new UserControls.ucMesas();
            ucMesas.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(ucMesas);
        }

        private void CarregarPedidos()
        {
            panelContainer.Controls.Clear();
            var label = new Label
            {
                Text = "Módulo de Pedidos - Em Desenvolvimento",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                ForeColor = Color.Gray
            };
            panelContainer.Controls.Add(label);
        }

        private void CarregarCozinha()
        {
            panelContainer.Controls.Clear();
            var ucCozinha = new UserControls.ucCozinha();
            ucCozinha.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(ucCozinha);
        }

        private void CarregarCaixa()
        {
            panelContainer.Controls.Clear();
            var ucCaixa = new UserControls.ucCaixa();
            ucCaixa.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(ucCaixa);
        }

        private void CarregarRelatorios()
        {
            panelContainer.Controls.Clear();
            var ucRelatorios = new UserControls.ucRelatorios();
            ucRelatorios.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(ucRelatorios);
        }
    }
}