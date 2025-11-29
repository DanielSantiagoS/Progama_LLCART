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

            Color corFundo;
            Color corStatus;

            if (mesa.Status == StatusMesa.Livre)
            {
                corFundo = Color.LightGreen;
                corStatus = Color.Green;
            }
            else if (mesa.Status == StatusMesa.Ocupada)
            {
                corFundo = Color.LightCoral;
                corStatus = Color.Red;
            }
            else
            {
                corFundo = Color.LightYellow;
                corStatus = Color.Orange;
            }

            panel.BackColor = corFundo;

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
                Text = mesa.Status.ToString().ToUpper(),
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                BackColor = corStatus,
                ForeColor = Color.White
            };

            var lblComanda = new Label
            {
                Text = mesa.ComandaAtual ?? "Sem comanda",
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 7),
                ForeColor = Color.DarkBlue
            };

            panel.Controls.AddRange(new Control[] { lblStatus, lblComanda, lblCapacidade, lblNumero });

            panel.Click += (s, e) => AbrirMesa(mesa);
            lblNumero.Click += (s, e) => AbrirMesa(mesa);
            lblCapacidade.Click += (s, e) => AbrirMesa(mesa);
            lblStatus.Click += (s, e) => AbrirMesa(mesa);
            lblComanda.Click += (s, e) => AbrirMesa(mesa);

            return panel;
        }

        private void AbrirMesa(Mesa mesa)
        {
            Console.WriteLine($"Clicou na mesa {mesa.Numero} - Status: {mesa.Status}");

            if (mesa.Status == StatusMesa.Livre)
            {
                var formPedido = new Views.frmPedido(mesa);
                var resultado = formPedido.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    CarregarMesas();

                    var pedidoService = new PedidoService();
                    var pedidoSalvo = pedidoService.ObterPedidoPorMesa(mesa.Id);

                    if (pedidoSalvo != null)
                    {
                        MessageBox.Show($"✅ PEDIDO CRIADO!\n\nMesa: {mesa.Numero}\nComanda: {pedidoSalvo.Comanda}\nItens: {pedidoSalvo.Itens.Count}",
                            "Pedido Criado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (mesa.Status == StatusMesa.Ocupada)
            {
                var pedidoService = new PedidoService();
                var pedidoExistente = pedidoService.ObterPedidoPorMesa(mesa.Id);

                if (pedidoExistente != null)
                {
                    var formPedido = new Views.frmPedido(mesa, pedidoExistente);
                    var resultado = formPedido.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        CarregarMesas();
                        MessageBox.Show($"✅ Pedido atualizado!\nMesa: {mesa.Numero}",
                            "Atualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show($"❌ Mesa {mesa.Numero} está marcada como ocupada mas não encontramos pedido.\n\nLiberando mesa...",
                        "Pedido Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    mesaService.AtualizarStatusMesa(mesa.Id, StatusMesa.Livre, null);
                    CarregarMesas();
                }
            }
        }

        private void CarregarMesas()
        {
            mesas = mesaService.ObterMesas();

            foreach (var mesa in mesas)
            {
                Console.WriteLine($"DEBUG Mesa {mesa.Numero}: Status={mesa.Status}, Comanda={mesa.ComandaAtual}");
            }

            var flowPanel = (FlowLayoutPanel)this.Controls[0];
            flowPanel.Controls.Clear();

            foreach (var mesa in mesas)
            {
                var panelMesa = CriarPanelMesa(mesa);
                flowPanel.Controls.Add(panelMesa);
            }
        }
    }
}