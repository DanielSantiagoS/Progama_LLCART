using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Forms_LLCART_Projeto.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public string Comanda { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public StatusPedido Status { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public string Observacoes { get; set; }
        public string GarcomResponsavel { get; set; }
        public decimal Total => CalcularTotal();

        public Pedido()
        {
            Itens = new List<ItemPedido>();
            DataAbertura = DateTime.Now;
            Status = StatusPedido.Aberto;
        }

        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
            {
                if (item.Status != StatusItem.Cancelado)
                    total += item.Subtotal;
            }
            return total;
        }
    }

    public class ItemPedido
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
        public string Observacoes { get; set; }
        public StatusItem Status { get; set; }
        public DateTime DataHora { get; set; }
        public string UsuarioResponsavel { get; set; }
    }

    public enum StatusPedido
    {
        Aberto,
        Fechado,
        Cancelado
    }

    public enum StatusItem
    {
        [Description("Novo")]
        Novo,
        [Description("Em Preparo")]
        EmPreparo,
        [Description("Pronto")]
        Pronto,
        [Description("Entregue")]
        Entregue,
        [Description("Cancelado")]
        Cancelado
    }
}