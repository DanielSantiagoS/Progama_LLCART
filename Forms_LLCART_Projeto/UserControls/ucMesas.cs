using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;

namespace Forms_LLCART_Projeto.UserControls
{
    public partial class ucMesas : UserControl
    {
        private MesaService mesaService;
        private List<Mesa> mesas;

        public ucMesas()
        {
            InitializeComponent();
            mesaService = new MesaService();
            CarregarMesas();
            CriarBotaoAtualizar();
            CarregarMesas();
        }

        private void CriarBotaoAtualizar()
        {
            var btnAtualizar = new Button
            {
                Text = "🔄 Atualizar",
                Size = new Size(100, 30),
                Location = new Point(20, 20),
                BackColor = Color.LightBlue,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            btnAtualizar.Click += (s, e) =>
            {
                CarregarMesas();
                MessageBox.Show("Mesas atualizadas!", "Atualizado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            this.Controls.Add(btnAtualizar);

            var flowPanel = this.Controls[0] as FlowLayoutPanel;
            if (flowPanel != null)
            {
                flowPanel.Location = new Point(0, 60);
                flowPanel.Height = this.Height - 60;
            }
        }

        private void CarregarMesas()
        {
            mesas = mesaService.ObterMesas();

            flowPanel.Controls.Clear();

            foreach (var mesa in mesas)
            {
                var panelMesa = CriarPanelMesa(mesa);
                flowPanel.Controls.Add(panelMesa);
            }
        }

        private Panel CriarPanelMesa(Mesa mesa)
        {
            var panel = new Panel
            {
                Width = 150,
                Height = 150,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = mesa
            };

            Color corStatus;
            switch (mesa.Status)
            {
                case StatusMesa.Livre:
                    corStatus = Color.LightGreen;
                    break;
                case StatusMesa.Ocupada:
                    corStatus = Color.LightCoral;
                    break;
                case StatusMesa.Reservada:
                    corStatus = Color.LightYellow;
                    break;
                default:
                    corStatus = Color.LightGray;
                    break;
            }

            panel.BackColor = corStatus;

            var lblNumero = new Label
            {
                Text = $"Mesa {mesa.Numero}",
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                BackColor = Color.Transparent
            };

            var lblCapacidade = new Label
            {
                Text = $"{mesa.Capacidade} lugares",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            var lblStatus = new Label
            {
                Text = mesa.Status.ToString(),
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                BackColor = mesa.Status == StatusMesa.Ocupada ? Color.Red :
                           mesa.Status == StatusMesa.Livre ? Color.Green : Color.Orange,
                ForeColor = Color.White
            };

            var lblDebug = new Label
            {
                Text = mesa.ComandaAtual ?? "Sem comanda",
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 7),
                ForeColor = Color.DarkBlue,
                BackColor = Color.Transparent
            };

            panel.Controls.AddRange(new Control[] { lblStatus, lblDebug, lblCapacidade, lblNumero });

            panel.Click += (s, e) => AbrirMesa(mesa);

            return panel;
        }

        private void AbrirMesa(Mesa mesa)
        {
            var pedidoService = new PedidoService();
            var pedidoExistente = pedidoService.ObterPedidoPorMesa(mesa.Id);
        }
    }
}