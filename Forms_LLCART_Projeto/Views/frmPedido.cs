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

            btnFinalizarPedido.Click += btnFinalizarPedido_Click;
            btnCancelar.Click += btnCancelar_Click;
        }

        public frmPedido(Mesa mesaSelecionada, Pedido pedidoExistente)
        {
            InitializeComponent();
            mesa = mesaSelecionada;
            pedidoAtual = pedidoExistente;
            produtoService = new ProdutoService();

            this.Text = $"Editando Pedido - Mesa {mesa.Numero} - Comanda: {pedidoAtual.Comanda}";
            lblComanda.Text = $"Comanda: {pedidoAtual.Comanda}";
            lblMesa.Text = $"Mesa: {mesa.Numero}";

            btnFinalizarPedido.Click += btnFinalizarPedido_Click;
            btnCancelar.Click += btnCancelar_Click;

            CarregarProdutos();
            AtualizarListaPedidos(); 
        }

        private void InicializarPedido()
        {
            pedidoAtual = new Pedido
            {
                MesaId = mesa.Id,
                Comanda = GerarNumeroComanda(),
                GarcomResponsavel = "Garçom"
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

            flowCategorias.Controls.Clear();
            flowProdutos.Controls.Clear();

            var btnTodos = new Button
            {
                Text = "📋 Todos",
                Size = new Size(120, 40),
                Margin = new Padding(5),
                BackColor = Color.LightBlue,
                Tag = "Todos"
            };
            btnTodos.Click += (s, e) => FiltrarProdutosPorCategoria("Todos");
            flowCategorias.Controls.Add(btnTodos);

            foreach (var categoria in categorias)
            {
                var btnCategoria = new Button
                {
                    Text = GetEmojiCategoria(categoria) + " " + categoria,
                    Size = new Size(120, 40),
                    Margin = new Padding(5),
                    BackColor = Color.LightGreen,
                    Tag = categoria
                };
                btnCategoria.Click += (s, e) => FiltrarProdutosPorCategoria(categoria);
                flowCategorias.Controls.Add(btnCategoria);
            }

            FiltrarProdutosPorCategoria("Todos");
        }

        private string GetEmojiCategoria(string categoria)
        {
            switch (categoria)
            {
                case "Carnes":
                    return "🥩";
                case "Bebidas":
                    return "🥤";
                case "Acompanhamentos":
                    return "🍚";
                case "Sobremesas":
                    return "🍮";
                default:
                    return "📦";
            }
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
                BackColor = Color.LightYellow,
                Tag = produto
            };

            panel.Click += (s, e) => AdicionarProdutoAoPedido(produto);

            var lblNome = new Label
            {
                Text = produto.Nome,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                BackColor = Color.LightBlue,
                Cursor = Cursors.Hand
            };
            lblNome.Click += (s, e) => AdicionarProdutoAoPedido(produto);

            var lblPreco = new Label
            {
                Text = $"R$ {produto.Preco:F2}",
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                BackColor = Color.LightGreen,
                Cursor = Cursors.Hand,
                ForeColor = Color.DarkGreen
            };
            lblPreco.Click += (s, e) => AdicionarProdutoAoPedido(produto);

            var lblCategoria = new Label
            {
                Text = produto.Categoria,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand
            };
            lblCategoria.Click += (s, e) => AdicionarProdutoAoPedido(produto);

            panel.Controls.AddRange(new Control[] { lblPreco, lblCategoria, lblNome });

            return panel;
        }

        private void AdicionarProdutoAoPedido(Produto produto)
        {
            MessageBox.Show($"Produto selecionado: {produto.Nome}\nPreço: R$ {produto.Preco:F2}",
                "Produto Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var formQuantidade = new Form
            {
                Text = $"Quantidade - {produto.Nome}",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                BackColor = Color.White
            };

            var lblProduto = new Label
            {
                Text = $"Produto: {produto.Nome}",
                Location = new Point(20, 20),
                Size = new Size(250, 20),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };

            var lblPreco = new Label
            {
                Text = $"Preço unitário: R$ {produto.Preco:F2}",
                Location = new Point(20, 45),
                Size = new Size(250, 20)
            };

            var lblQuantidade = new Label
            {
                Text = "Quantidade:",
                Location = new Point(20, 80),
                Size = new Size(80, 20),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            var numericQuantidade = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 20,
                Value = 1,
                Location = new Point(100, 78),
                Size = new Size(80, 25),
                Font = new Font("Microsoft Sans Serif", 10)
            };

            var btnConfirmar = new Button
            {
                Text = "✅ Adicionar",
                Location = new Point(50, 120),
                Size = new Size(90, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            var btnCancelarQuant = new Button
            {
                Text = "❌ Cancelar",
                Location = new Point(150, 120),
                Size = new Size(90, 35),
                BackColor = Color.LightCoral,
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
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

                MessageBox.Show($"✅ {item.Quantidade}x {item.NomeProduto} adicionado!\nSubtotal: R$ {item.Subtotal:F2}",
                    "Item Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                formQuantidade.Close();
            };

            btnCancelarQuant.Click += (s, e) => formQuantidade.Close();

            formQuantidade.Controls.AddRange(new Control[] {
                lblProduto, lblPreco, lblQuantidade, numericQuantidade, btnConfirmar, btnCancelarQuant
            });

            formQuantidade.ShowDialog();
        }

        private void AtualizarListaPedidos()
        {
            flowItensPedido.Controls.Clear();
            lblTotal.Text = $"Total: R$ {pedidoAtual.Total:F2}";

            if (pedidoAtual.Itens.Count == 0)
            {
                var lblVazio = new Label
                {
                    Text = "Nenhum item no pedido\n\nClique nos produtos à esquerda para adicionar",
                    Size = new Size(350, 100),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                flowItensPedido.Controls.Add(lblVazio);
                return;
            }

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
                Width = 350,
                Height = 70,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            var lblProduto = new Label
            {
                Text = $"{item.Quantidade}x {item.NomeProduto}",
                Location = new Point(10, 10),
                Size = new Size(200, 20),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };

            var lblPrecoUnitario = new Label
            {
                Text = $"R$ {item.PrecoUnitario:F2} cada",
                Location = new Point(10, 35),
                Size = new Size(120, 15),
                Font = new Font("Microsoft Sans Serif", 8),
                ForeColor = Color.Gray
            };

            var lblSubtotal = new Label
            {
                Text = $"Subtotal: R$ {item.Subtotal:F2}",
                Location = new Point(200, 25),
                Size = new Size(100, 20),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                TextAlign = ContentAlignment.MiddleRight
            };

            var btnRemover = new Button
            {
                Text = "🗑️ Remover",
                Location = new Point(300, 20),
                Size = new Size(40, 30),
                BackColor = Color.LightCoral,
                Font = new Font("Microsoft Sans Serif", 8),
                Tag = item
            };

            btnRemover.Click += (s, e) =>
            {
                var result = MessageBox.Show($"Remover {item.Quantidade}x {item.NomeProduto}?",
                    "Confirmar Remoção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    pedidoAtual.Itens.Remove(item);
                    AtualizarListaPedidos();
                    MessageBox.Show("Item removido do pedido!", "Removido",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            panel.Controls.AddRange(new Control[] { lblProduto, lblPrecoUnitario, lblSubtotal, btnRemover });
            return panel;
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            var pedidoService = new PedidoService();
            pedidoService.SalvarPedido(pedidoAtual);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (pedidoAtual.Itens.Count > 0)
            {
                var result = MessageBox.Show("Tem certeza que deseja cancelar o pedido?\nTodos os itens serão perdidos.",
                    "❌ Cancelar Pedido", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}