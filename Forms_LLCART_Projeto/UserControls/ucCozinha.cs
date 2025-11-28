using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;

namespace Forms_LLCART_Projeto.UserControls
{
    public partial class ucCozinha : UserControl
    {
        private PedidoService pedidoService;
        private Timer timerAtualizacao;

        public ucCozinha()
        {
            InitializeComponent();
            pedidoService = new PedidoService();
            ConfigurarTimer();
            CarregarPedidos();
        }

        private void ConfigurarTimer()
        {
            timerAtualizacao = new Timer();
            timerAtualizacao.Interval = 5000; 
            timerAtualizacao.Tick += (s, e) => CarregarPedidos();
            timerAtualizacao.Start();
        }

        private void CarregarPedidos()
        {
            CarregarItensPorStatus(StatusItem.Novo, flowNovos, "NOVOS PEDIDOS");
            CarregarItensPorStatus(StatusItem.EmPreparo, flowPreparo, "EM PREPARO");
            CarregarItensPorStatus(StatusItem.Pronto, flowProntos, "PRONTOS PARA ENTREGA");
        }

        private void CarregarItensPorStatus(StatusItem status, FlowLayoutPanel flowPanel, string titulo)
        {
            flowPanel.Controls.Clear();

            var lblTitulo = new Label
            {
                Text = titulo,
                Width = flowPanel.Width - 20,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                BackColor = GetCorStatus(status),
                ForeColor = Color.White,
                Margin = new Padding(5)
            };
            flowPanel.Controls.Add(lblTitulo);

            var itens = pedidoService.ObterItensPorStatus(status);

            if (itens.Count == 0)
            {
                var lblVazio = new Label
                {
                    Text = "Nenhum item",
                    Width = flowPanel.Width - 20,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                flowPanel.Controls.Add(lblVazio);
                return;
            }

            foreach (var item in itens)
            {
                var panelItem = CriarPanelItemCozinha(item);
                flowPanel.Controls.Add(panelItem);
            }
        }

        private Panel CriarPanelItemCozinha(ItemPedido item)
        {
            var panel = new Panel
            {
                Width = 250,
                Height = 100,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = GetCorStatus(item.Status)
            };

            var lblProduto = new Label
            {
                Text = $"{item.Quantidade}x {item.NomeProduto}",
                Location = new Point(10, 10),
                Size = new Size(230, 20),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblHora = new Label
            {
                Text = $"Hora: {item.DataHora:HH:mm}",
                Location = new Point(10, 35),
                Size = new Size(120, 15),
                Font = new Font("Microsoft Sans Serif", 8),
                ForeColor = Color.DarkBlue
            };

            var lblMesa = new Label
            {
                Text = $"Mesa: {ObterMesaDoPedido(item)}", 
                Location = new Point(10, 55),
                Size = new Size(120, 15),
                Font = new Font("Microsoft Sans Serif", 8),
                ForeColor = Color.DarkRed
            };

            if (item.Status == StatusItem.Novo)
            {
                var btnIniciar = new Button
                {
                    Text = "Iniciar",
                    Location = new Point(150, 30),
                    Size = new Size(80, 25),
                    BackColor = Color.Orange,
                    Tag = item
                };
                btnIniciar.Click += (s, e) => AtualizarStatusItem(item, StatusItem.EmPreparo);
                panel.Controls.Add(btnIniciar);
            }
            else if (item.Status == StatusItem.EmPreparo)
            {
                var btnFinalizar = new Button
                {
                    Text = "Pronto",
                    Location = new Point(150, 30),
                    Size = new Size(80, 25),
                    BackColor = Color.LightGreen,
                    Tag = item
                };
                btnFinalizar.Click += (s, e) => AtualizarStatusItem(item, StatusItem.Pronto);
                panel.Controls.Add(btnFinalizar);
            }
            else if (item.Status == StatusItem.Pronto)
            {
                var lblPronto = new Label
                {
                    Text = "AGUARDANDO ENTREGA",
                    Location = new Point(120, 35),
                    Size = new Size(120, 40),
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                    ForeColor = Color.DarkGreen,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel.Controls.Add(lblPronto);
            }

            var lblObservacoes = new Label
            {
                Text = string.IsNullOrEmpty(item.Observacoes) ? "Sem observações" : $"Obs: {item.Observacoes}",
                Location = new Point(10, 75),
                Size = new Size(230, 20),
                Font = new Font("Microsoft Sans Serif", 8),
                ForeColor = Color.DarkSlateGray
            };

            panel.Controls.AddRange(new Control[] { lblProduto, lblHora, lblMesa, lblObservacoes });

            return panel;
        }

        private Color GetCorStatus(StatusItem status)
        {
            switch (status)
            {
                case StatusItem.Novo: return Color.LightYellow;
                case StatusItem.EmPreparo: return Color.LightBlue;
                case StatusItem.Pronto: return Color.LightGreen;
                default: return Color.LightGray;
            }
        }

        private string ObterMesaDoPedido(ItemPedido item)
        {
            return "01";
        }

        private void AtualizarStatusItem(ItemPedido item, StatusItem novoStatus)
        {
            item.Status = novoStatus;
            CarregarPedidos();
            MessageBox.Show($"Status atualizado para: {novoStatus}", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
    }
}