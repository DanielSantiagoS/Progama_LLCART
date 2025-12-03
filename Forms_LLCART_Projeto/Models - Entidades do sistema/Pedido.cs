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
            Itens = new List<ItemPedido>(); //list = array, inicia o array vazio/ array = vetor
            DataAbertura = DateTime.Now;
            Status = StatusPedido.Aberto; //status do pedido que é aderido pelo get;set
        }
        
        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
            {
                if (item.Status != StatusItem.Cancelado) //percorre o list de itens (foreach), e se nao tiver sido cancelado soma o subtotal dos itens (!cancelado)
                    total += item.Subtotal;
            }
            return total;
        }
    }

    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; } 
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

    public enum StatusPedido //estado de uma variavel exemplo: coletou o dado e deu igual a 1, 1 = livre, ou 2 = ocupada
                             // o enum serve para indicar um "estado" de uma variavel
    {
        Aberto,
        Fechado,
        Cancelado
    }

    public enum StatusItem //status que vai servir como base em metodos futuros (influencia no design tambem)
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