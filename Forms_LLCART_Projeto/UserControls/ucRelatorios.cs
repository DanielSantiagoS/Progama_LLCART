using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Services;

namespace Forms_LLCART_Projeto.UserControls
{
    public partial class ucRelatorios : UserControl
    {
        private PedidoService pedidoService;
        private MesaService mesaService;
        private ProdutoService produtoService;

        public ucRelatorios()
        {
            InitializeComponent();
            pedidoService = new PedidoService();
            mesaService = new MesaService();
            produtoService = new ProdutoService();
            ConfigurarDataGrid();
            CarregarDadosRelatorios();
        }

        private void ConfigurarDataGrid()
        {
           
            dataGridTopProdutos.Columns.Clear();
            dataGridTopProdutos.Columns.Add("Produto", "Produto");
            dataGridTopProdutos.Columns.Add("Quantidade", "Quantidade");
            dataGridTopProdutos.Columns.Add("ValorTotal", "Valor Total");

            
            dataGridTopProdutos.Columns[0].Width = 150;
            dataGridTopProdutos.Columns[1].Width = 80;
            dataGridTopProdutos.Columns[2].Width = 100;

            
            dataGridTopProdutos.ReadOnly = true;
            dataGridTopProdutos.AllowUserToAddRows = false;
            dataGridTopProdutos.AllowUserToDeleteRows = false;
            dataGridTopProdutos.RowHeadersVisible = false;
            dataGridTopProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CarregarDadosRelatorios()
        {
            CarregarResumoDiario();
            CarregarTopProdutos();
            CarregarOcupacaoMesas();
        }

        private void CarregarResumoDiario()
        {
           
            var vendasDiarias = new[]
            {
                new { Hora = "12:00", Valor = 450.50m },
                new { Hora = "13:00", Valor = 890.75m },
                new { Hora = "14:00", Valor = 670.25m },
                new { Hora = "15:00", Valor = 320.90m },
                new { Hora = "16:00", Valor = 210.30m },
                new { Hora = "17:00", Valor = 150.60m }
            };

            var totalDia = vendasDiarias.Sum(v => v.Valor);
            var mediaPorHora = vendasDiarias.Average(v => v.Valor);

            lblTotalDia.Text = $"R$ {totalDia:F2}";
            lblMediaHora.Text = $"R$ {mediaPorHora:F2}";
            lblTotalVendas.Text = vendasDiarias.Length.ToString();
            lblTicketMedio.Text = $"R$ {(totalDia / vendasDiarias.Length):F2}";
        }

        private void CarregarTopProdutos()
        {
            dataGridTopProdutos.Rows.Clear();

           
            var topProdutos = new[]
            {
                new { Produto = "Picanha", Quantidade = 45, Valor = 4045.50m },
                new { Produto = "Coca-Cola 2L", Quantidade = 38, Valor = 490.20m },
                new { Produto = "Costela", Quantidade = 32, Valor = 2556.80m },
                new { Produto = "Cerveja Heineken", Quantidade = 28, Valor = 277.20m },
                new { Produto = "Fraldinha", Quantidade = 25, Valor = 1747.50m }
            };

            foreach (var produto in topProdutos)
            {
                dataGridTopProdutos.Rows.Add(
                    produto.Produto,
                    produto.Quantidade,
                    $"R$ {produto.Valor:F2}"
                );
            }
        }

        private void CarregarOcupacaoMesas()
        {
            var mesas = mesaService.ObterMesas();
            var totalMesas = mesas.Count;
            var mesasOcupadas = mesas.Count(m => m.Status == StatusMesa.Ocupada);
            var taxaOcupacao = totalMesas > 0 ? (mesasOcupadas * 100.0 / totalMesas) : 0;

            lblTotalMesas.Text = totalMesas.ToString();
            lblMesasOcupadas.Text = mesasOcupadas.ToString();
            lblTaxaOcupacao.Text = $"{taxaOcupacao:F1}%";

            
            panelOcupacaoBarra.Width = (int)(panelOcupacaoBase.Width * (taxaOcupacao / 100.0));
            panelOcupacaoBarra.BackColor = taxaOcupacao > 70 ? Color.LightGreen :
                                         taxaOcupacao > 40 ? Color.LightBlue : Color.LightCoral;
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            var periodo = comboPeriodo.SelectedItem?.ToString() ?? "Diário";
            var mensagem = $"Relatório {periodo} gerado com sucesso!\n\n" +
                          $"Período: {DateTime.Now:dd/MM/yyyy}\n" +
                          $"Total de vendas: {lblTotalDia.Text}\n" +
                          $"Mesas ocupadas: {lblMesasOcupadas.Text}/{lblTotalMesas.Text}\n" +
                          $"Taxa de ocupação: {lblTaxaOcupacao.Text}";

            MessageBox.Show(mensagem, "Relatório Gerado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade de exportação para Excel será implementada!\n" +
                          "Integração com planilhas em desenvolvimento.",
                          "Exportar para Excel",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarDadosRelatorios();
            MessageBox.Show("Dados atualizados com sucesso!", "Atualizado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}