using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;

namespace Forms_LLCART_Projeto.Views
{
    public partial class frmPedido : Form
    {
        private Mesa mesa;
        private Pedido pedidoAtual;
        private ProdutoService produtoService;
        private List<Produto> produtos;

        public frmPedido(Mesa mesaSelecionada)
        {
            InitializeComponent();
            mesa = mesaSelecionada;
            produtoService = new ProdutoService();
            InicializarPedido();
            CarregarProdutos();
        }

        private void InicializarPedido()
        {
            pedidoAtual = new Pedido
            {
                MesaId = mesa.Id,
                Comanda = GerarNumeroComanda(),
                GarcomResponsavel = "Garçom" // Depois implementamos login
            };

            this.Text = $"Novo Pedido - Mesa {mesa.Numero} - Comanda: {pedidoAtual.Comanda}";
            lblComanda.Text = $"Comanda: {pedidoAtual.Comanda}";
            lblMesa.Text = $"Mesa: {mesa.Numero}";
        }

        private string GerarNumeroComanda()
        {
            return DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        private void CarregarProdutos()
        {
            produtos = produtoService.ObterProdutos();
            var categorias = produtoService.ObterCategorias();

            // Limpar controles existentes
            flowCategorias.Controls.Clear();
            flowProdutos.Controls.Clear();

            // Criar botões de categorias
            foreach (var categoria in categorias)
            {
                var btnCategoria = new Button
                {
                    Text = categoria,
                    Size = new Size(120, 40),
                    Margin = new Padding(5),
                    Tag = categoria
                };
                btnCategoria.Click += (s, e) => FiltrarProdutosPorCategoria(categoria);
                flowCategorias.Controls.Add(btnCategoria);
            }

            // Carregar todos os produtos inicialmente
            FiltrarProdutosPorCategoria("Todos");
        }

        private void FiltrarProdutosPorCategoria(string categoria)
        {
            flowProdutos.Controls.Clear();

            var produtosFiltrados = categoria == "Todos"
                ? produtos
                : produtos.Where(p => p.Categoria == categoria).ToList();

            foreach (var produto in produtosFiltrados)
            {
                var panelProduto = CriarPanelProduto(produto);
                flowProdutos.Controls.Add(panelProduto);
            }
        }

        private Panel CriarPanelProduto(Produto produto)
        {
            var panel = new Panel
            {
                Width = 150,
                Height = 100,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = produto
            };

            var lblNome = new Label
            {
                Text = produto.Nome,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                BackColor = Color.LightBlue
            };

            var lblPreco = new Label
            {
                Text = $"R$ {produto.Preco:F2}",
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblCategoria = new Label
            {
                Text = produto.Categoria,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            panel.Controls.AddRange(new Control[] { lblPreco, lblCategoria, lblNome });

            // Evento de clique para adicionar ao pedido
            panel.Click += (s, e) => AdicionarProdutoAoPedido(produto);

            return panel;
        }

        private void AdicionarProdutoAoPedido(Produto produto)
        {
            // Perguntar a quantidade
            var formQuantidade = new Form
            {
                Text = $"Quantidade - {produto.Nome}",
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            var numericQuantidade = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 10,
                Value = 1,
                Location = new Point(100, 20),
                Size = new Size(80, 20)
            };

            var btnConfirmar = new Button
            {
                Text = "Adicionar",
                Location = new Point(100, 60),
                Size = new Size(80, 30)
            };

            var lblQuantidade = new Label
            {
                Text = "Quantidade:",
                Location = new Point(20, 23),
                Size = new Size(70, 20)
            };

            btnConfirmar.Click += (s, e) =>
            {
                var item = new ItemPedido
                {
                    ProdutoId = produto.Id,
                    NomeProduto = produto.Nome,
                    Quantidade = (int)numericQuantidade.Value,
                    PrecoUnitario = produto.Preco,
                    Observacoes = "",
                    Status = StatusItem.Novo,
                    DataHora = DateTime.Now,
                    UsuarioResponsavel = "Garçom"
                };

                pedidoAtual.Itens.Add(item);
                AtualizarListaPedidos();
                formQuantidade.Close();
            };

            formQuantidade.Controls.AddRange(new Control[] { lblQuantidade, numericQuantidade, btnConfirmar });
            formQuantidade.ShowDialog();
        }

        private void AtualizarListaPedidos()
        {
            flowItensPedido.Controls.Clear();
            lblTotal.Text = $"Total: R$ {pedidoAtual.Total:F2}";

            foreach (var item in pedidoAtual.Itens)
            {
                var panelItem = CriarPanelItemPedido(item);
                flowItensPedido.Controls.Add(panelItem);
            }
        }

        private Panel CriarPanelItemPedido(ItemPedido item)
        {
            var panel = new Panel
            {
                Width = 250,
                Height = 60,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblProduto = new Label
            {
                Text = $"{item.Quantidade}x {item.NomeProduto}",
                Location = new Point(5, 5),
                Size = new Size(180, 20),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            var lblSubtotal = new Label
            {
                Text = $"R$ {item.Subtotal:F2}",
                Location = new Point(5, 30),
                Size = new Size(100, 20)
            };

            var btnRemover = new Button
            {
                Text = "X",
                Location = new Point(200, 5),
                Size = new Size(30, 25),
                BackColor = Color.LightCoral,
                Tag = item
            };

            btnRemover.Click += (s, e) => RemoverItemPedido(item);

            panel.Controls.AddRange(new Control[] { lblProduto, lblSubtotal, btnRemover });
            return panel;
        }

        private void RemoverItemPedido(ItemPedido item)
        {
            pedidoAtual.Itens.Remove(item);
            AtualizarListaPedidos();
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (pedidoAtual.Itens.Count == 0)
            {
                MessageBox.Show("Adicione itens ao pedido antes de finalizar!", "Pedido Vazio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Finalizar pedido?\nTotal: R$ {pedidoAtual.Total:F2}",
                "Confirmar Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Aqui salvaríamos no banco de dados
                MessageBox.Show("Pedido finalizado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}