using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;

namespace Forms_LLCART_Projeto.UserControls
{
    public partial class ucMesas : UserControl // herda tudo de "UserControls"
    {
        private MesaService mesaService; // cria um Service para as operações das mesas
        private List<Mesa> mesas; // cria lista (array/vetor) chamada "mesas"

        public ucMesas()
        {
            InitializeComponent();
            mesaService = new MesaService();
            CarregarMesas();
        }

        private Panel CriarPanelMesa(Mesa mesa) // cria o container principal
        {
            var panel = new Panel // cria novo painel
            {
                Width = 150, // largura 
                Height = 150, // altura
                Margin = new Padding(10), // margem
                BorderStyle = BorderStyle.FixedSingle, // estilo das bordas
                Cursor = Cursors.Hand, // o cursor muda, vira uma mãozinha para indicar que é clicável
                Tag = mesa // armazena este objeto no controle (bem importante)
            };


            // cria variáveis do tipo Color para mudar a cor das caixinhas conforme o status dela
            Color corFundo;
            Color corStatus;

            if (mesa.Status == StatusMesa.Livre)
            {
                corFundo = Color.LightGreen;
                corStatus = Color.Green;
            }
            else 
            {
                corFundo = Color.LightCoral;
                corStatus = Color.Red;
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

            if (mesa.Status == StatusMesa.Ocupada)
            {
                var btnLiberar = new Button
                {
                    Text = " Liberar",
                    Dock = DockStyle.Bottom,
                    Height = 25,
                    BackColor = Color.LightBlue,
                    Tag = mesa
                };
                btnLiberar.Click += (s, e) => LiberarMesa(mesa);
                panel.Controls.Add(btnLiberar);
            }

            panel.Controls.AddRange(new Control[] { lblStatus, lblComanda, lblCapacidade, lblNumero });

            panel.Click += (s, e) => AbrirMesa(mesa);
            lblNumero.Click += (s, e) => AbrirMesa(mesa);
            lblCapacidade.Click += (s, e) => AbrirMesa(mesa);
            lblStatus.Click += (s, e) => AbrirMesa(mesa);
            lblComanda.Click += (s, e) => AbrirMesa(mesa);

            return panel;
        }

        private void LiberarMesa(Mesa mesa)
        {
            var result = MessageBox.Show(
                $"Tem certeza que deseja liberar a Mesa {mesa.Numero}?\n\n" +
                $"Comanda atual: {mesa.ComandaAtual ?? "Nenhuma"}\n" + // ?? é um operador de coalescência, se o valor não nulo, retorna "mesa. COmandaAtual. Se for nulo, retorna "Nenhuma"."
                $"Esta ação irá fechar qualquer pedido em aberto.",
                "Liberar Mesa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var pedidoService = new PedidoService();
                    var pedido = pedidoService.ObterPedidoPorMesa(mesa.Id);

                    if (pedido != null)
                    {
                        pedidoService.FecharPedido(pedido.Id);
                    }

                    mesaService.AtualizarStatusMesa(mesa.Id, StatusMesa.Livre, null);

                    MessageBox.Show(
                        $" Mesa {mesa.Numero} liberada com sucesso!",
                        "Mesa Liberada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CarregarMesas(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $" Erro ao liberar mesa: {ex.Message}",
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
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
                        MessageBox.Show($" PEDIDO CRIADO!\n\nMesa: {mesa.Numero}\nComanda: {pedidoSalvo.Comanda}\nItens: {pedidoSalvo.Itens.Count}",
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
                        MessageBox.Show($" Pedido atualizado!\nMesa: {mesa.Numero}",
                            "Atualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var result = MessageBox.Show(
                        $"  Mesa {mesa.Numero} está ocupada mas não tem pedido ativo.\n\n" +
                        "Deseja liberar a mesa?",
                        "Mesa sem Pedido",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        mesaService.AtualizarStatusMesa(mesa.Id, StatusMesa.Livre, null);
                        CarregarMesas();
                        MessageBox.Show($" Mesa {mesa.Numero} liberada!", "Sucesso");
                    }
                }
            }
        }

        private void CarregarMesas()
        {
            mesas = mesaService.ObterMesas();

            foreach (var mesa in mesas)
            {
                Console.WriteLine($" Mesa {mesa.Numero}: Status={mesa.Status}, Comanda={mesa.ComandaAtual}");
            }
            flowPanel.Controls.Clear();

            foreach (var mesa in mesas)
            {
                var panelMesa = CriarPanelMesa(mesa);
                flowPanel.Controls.Add(panelMesa);
            }
        }
    }
}