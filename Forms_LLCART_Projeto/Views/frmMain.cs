using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;
using System;
using System.Drawing;
using System.Linq;
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

            

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var lblTitulo = new Label
            {
                Text = "Gestão de Pedidos - Mesas Ocupadas",
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 76)
            };

            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            var pedidoService = new Services.PedidoService();
            var pedidosAtivos = pedidoService.ObterPedidosAtivos();
            var mesaService = new Services.MesaService();

            if (pedidosAtivos.Count == 0)
            {
                var lblVazio = new Label
                {
                    Text = "Nenhum pedido ativo no momento\n\nClique em 'Mesas' para abrir um novo pedido",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 12, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                flowPanel.Controls.Add(lblVazio);
            }
            else
            {
                foreach (var pedido in pedidosAtivos)
                {
                    var mesa = mesaService.ObterMesaPorId(pedido.MesaId);
                    if (mesa != null)
                    {
                        var panelPedido = CriarPanelPedido(pedido, mesa);
                        flowPanel.Controls.Add(panelPedido);
                    }
                }
            }

            panel.Controls.Add(flowPanel);
            panel.Controls.Add(lblTitulo);
            panelContainer.Controls.Add(panel);
        }

        private Panel CriarPanelPedido(Pedido pedido, Mesa mesa)
        {
            var panel = new Panel
            {
                Width = 250,
                Height = 140,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                BackColor = Color.LightCoral,
                Tag = new { Pedido = pedido, Mesa = mesa }
            };

            var lblMesa = new Label
            {
                Text = $"Mesa {mesa.Numero}",
                Dock = DockStyle.Top,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                BackColor = Color.Coral,
                ForeColor = Color.White
            };

            var lblComanda = new Label
            {
                Text = $"Comanda: {pedido.Comanda}",
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9)
            };

            var lblItens = new Label
            {
                Text = $"Itens: {pedido.Itens.Count}",
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            var lblTotal = new Label
            {
                Text = $"Total: R$ {pedido.Total:F2}",
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            var btnDetalhes = new Button
            {
                Text = "Ver/Editar Pedido",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightBlue,
                Tag = new { Pedido = pedido, Mesa = mesa }
            };
            btnDetalhes.Click += (s, e) =>
            {
                var dados = (dynamic)btnDetalhes.Tag;
                var formPedido = new Views.frmPedido(dados.Mesa, dados.Pedido);
                formPedido.ShowDialog();
                CarregarPedidos(); 
            };

            panel.Controls.AddRange(new Control[] { btnDetalhes, lblTotal, lblItens, lblComanda, lblMesa });

            return panel;
        }

        private Panel CriarPanelMesaPedido(Mesa mesa)
        {
            var panel = new Panel
            {
                Width = 200,
                Height = 120,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                BackColor = Color.LightCoral
            };

            var lblMesa = new Label
            {
                Text = $"Mesa {mesa.Numero}",
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                BackColor = Color.Coral,
                ForeColor = Color.White
            };

            var lblComanda = new Label
            {
                Text = $"Comanda: {mesa.ComandaAtual}",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9)
            };

            var lblStatus = new Label
            {
                Text = "Clique para ver/editar pedido",
                Dock = DockStyle.Bottom,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic),
                ForeColor = Color.DarkRed
            };

            var btnDetalhes = new Button
            {
                Text = "Ver Pedido",
                Dock = DockStyle.Fill,
                BackColor = Color.LightBlue,
                Tag = mesa
            };
            btnDetalhes.Click += (s, e) => VerPedidoMesa(mesa);

            panel.Controls.AddRange(new Control[] { btnDetalhes, lblStatus, lblComanda, lblMesa });

            return panel;
        }

        private void VerPedidoMesa(Mesa mesa)
        {
            MessageBox.Show($"Abrindo pedido da Mesa {mesa.Numero}\n\n" +
                           $"Comanda: {mesa.ComandaAtual}\n\n" +
                           "Esta funcionalidade abriria o formulário de pedidos\n" +
                           "com os itens já adicionados para edição.",
                           "Pedido da Mesa",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
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

        public void ForcarAtualizacaoMesas()
        {
            if (panelContainer.Controls.Count > 0 && panelContainer.Controls[0] is UserControls.ucMesas)
            {
                CarregarMesas();
            }
        }
    }
}