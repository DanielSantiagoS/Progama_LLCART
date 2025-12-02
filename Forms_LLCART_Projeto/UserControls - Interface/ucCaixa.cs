using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;

namespace Forms_LLCART_Projeto.UserControls
{
    public partial class ucCaixa : UserControl
    {
        private MesaService mesaService;
        private PedidoService pedidoService;
        private List<Mesa> mesasOcupadas;

        public ucCaixa()
        {
            InitializeComponent();
            mesaService = new MesaService();
            pedidoService = new PedidoService();
            CarregarMesasOcupadas();
        }

        private void CarregarMesasOcupadas()
        {
            mesasOcupadas = mesaService.ObterMesas().Where(m => m.Status == StatusMesa.Ocupada).ToList();

            flowMesasOcupadas.Controls.Clear();

            if (mesasOcupadas.Count == 0)
            {
                var lblVazio = new Label
                {
                    Text = "Nenhuma mesa ocupada no momento",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 12, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                flowMesasOcupadas.Controls.Add(lblVazio);
                return;
            }

            foreach (var mesa in mesasOcupadas)
            {
                var panelMesa = CriarPanelMesaCaixa(mesa);
                flowMesasOcupadas.Controls.Add(panelMesa);
            }
        }

        private Panel CriarPanelMesaCaixa(Mesa mesa)
        {
            var panel = new Panel
            {
                Width = 180,
                Height = 100,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = mesa
            };

            var pedidoMock = CriarPedidoMock(mesa);

            var lblMesa = new Label
            {
                Text = $"Mesa {mesa.Numero}",
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold),
                BackColor = Color.LightCoral,
                ForeColor = Color.White
            };

            var lblItens = new Label
            {
                Text = $"{pedidoMock.Itens.Count} itens",
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9)
            };

            var lblTotal = new Label
            {
                Text = $"Total: R$ {pedidoMock.Total:F2}",
                Dock = DockStyle.Top,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            var btnFechar = new Button
            {
                Text = "Fechar Conta",
                Dock = DockStyle.Bottom,
                Height = 25,
                BackColor = Color.LightGreen,
                Tag = mesa
            };
            btnFechar.Click += (s, e) => FecharConta(mesa, pedidoMock);

            panel.Controls.AddRange(new Control[] { btnFechar, lblTotal, lblItens, lblMesa });

            return panel;
        }

        private Pedido CriarPedidoMock(Mesa mesa)
        {
            var pedido = new Pedido
            {
                MesaId = mesa.Id,
                Comanda = mesa.ComandaAtual ?? "MOCK001",
                GarcomResponsavel = "Garçom"
            };

            var random = new Random();
            var produtos = new[]
            {
                new { Nome = "Picanha", Preco = 89.90m },
                new { Nome = "Coca-Cola", Preco = 12.90m },
                new { Nome = "Arroz", Preco = 15.90m },
                new { Nome = "Farofa", Preco = 12.90m }
            };

            for (int i = 0; i < random.Next(1, 4); i++)
            {
                var produto = produtos[random.Next(produtos.Length)];
                pedido.Itens.Add(new ItemPedido
                {
                    NomeProduto = produto.Nome,
                    Quantidade = random.Next(1, 3),
                    PrecoUnitario = produto.Preco,
                    Status = StatusItem.Entregue
                });
            }

            return pedido;
        }

        private void FecharConta(Mesa mesa, Pedido pedido)
        {
            var formPagamento = new Form
            {
                Text = $"Fechar Conta - Mesa {mesa.Numero}",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var lblTotal = new Label
            {
                Text = $"Total: R$ {pedido.Total:F2}",
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            };

            var lblFormaPagamento = new Label
            {
                Text = "Forma de Pagamento:",
                Location = new Point(20, 60),
                Size = new Size(150, 20)
            };

            var comboPagamento = new ComboBox
            {
                Location = new Point(20, 85),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboPagamento.Items.AddRange(new[] { "Dinheiro", "Cartão Débito", "Cartão Crédito", "PIX" });

            var btnConfirmar = new Button
            {
                Text = "Confirmar Pagamento",
                Location = new Point(20, 130),
                Size = new Size(150, 35),
                BackColor = Color.LightGreen
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(180, 130),
                Size = new Size(150, 35),
                BackColor = Color.LightCoral
            };

            btnConfirmar.Click += (s, e) =>
            {
                if (comboPagamento.SelectedItem == null)
                {
                    MessageBox.Show("Selecione uma forma de pagamento!", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($" DEBUG: Iniciando fechamento\nPedido ID: {pedido.Id}\nMesa: {mesa.Numero}\nComanda: {pedido.Comanda}",
                    "DEBUG - Antes de Fechar");

                try
                {
                    var pedidoService = new PedidoService();
                    pedidoService.FecharPedido(pedido.Id);

                    var mesaService = new MesaService();
                    var mesaAtualizada = mesaService.ObterMesaPorId(mesa.Id);

                    MessageBox.Show($" DEBUG: Após fechar pedido\nMesa Status: {mesaAtualizada.Status}\nComanda: {mesaAtualizada.ComandaAtual ?? "Nenhuma"}",
                        "DEBUG - Após Fechar");

                    var formaPagamento = comboPagamento.SelectedItem.ToString();
                    var mensagem = $" Pagamento confirmado!\n" +
                                  $"Mesa: {mesa.Numero}\n" +
                                  $"Total: R$ {pedido.Total:F2}\n" +
                                  $"Forma: {formaPagamento}\n" +
                                  $"NFC-e emitida com sucesso!";

                    MessageBox.Show(mensagem, "Pagamento Efetuado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    formPagamento.Close();
                    CarregarMesasOcupadas(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($" Erro ao fechar conta: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancelar.Click += (s, e) => formPagamento.Close();

            formPagamento.Controls.AddRange(new Control[]
            {
        lblTotal, lblFormaPagamento, comboPagamento, btnConfirmar, btnCancelar
            });

            formPagamento.ShowDialog();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarMesasOcupadas();
            MessageBox.Show("Lista de mesas atualizada!", "Atualizado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}