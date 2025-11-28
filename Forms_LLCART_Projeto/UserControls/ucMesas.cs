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
                Cursor = Cursors.Hand
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
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            };

            var lblCapacidade = new Label
            {
                Text = $"{mesa.Capacidade} lugares",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblStatus = new Label
            {
                Text = mesa.Status.ToString(),
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            panel.Controls.AddRange(new Control[] { lblStatus, lblCapacidade, lblNumero });

            panel.Click += (s, e) => AbrirMesa(mesa);

            return panel;
        }

        private void AbrirMesa(Mesa mesa)
        {
            if (mesa.Status == StatusMesa.Livre)
            {
                var formPedido = new Views.frmPedido(mesa);
                if (formPedido.ShowDialog() == DialogResult.OK)
                {
                    mesaService.AtualizarStatusMesa(mesa.Id, StatusMesa.Ocupada, "ComandaAtiva");
                    CarregarMesas(); 
                }
            }
            else if (mesa.Status == StatusMesa.Ocupada)
            {
                MessageBox.Show($"Mesa {mesa.Numero} ocupada\nComanda: {mesa.ComandaAtual}",
                    "Mesa Ocupada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}