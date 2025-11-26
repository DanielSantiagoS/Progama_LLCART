using System;
using System.Collections.Generic;

namespace LLCART_CMD.Models
{
    public enum Permissao { GARCOM, CAIXA, GERENTE, ADMIN }
    public enum MesaStatus { LIVRE, OCUPADA, RESERVADA, FECHANDO }
    public enum ComandaStatus { ABERTA, FECHADA, CANCELADA }
    public enum PedidoStatus { NOVO, EM_PREPARO, PRONTO, ENTREGUE, CANCELADO }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty; 
        public Permissao Permissao { get; set; }
        public bool Ativo { get; set; }
    }

    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public MesaStatus Status { get; set; } = MesaStatus.LIVRE;
    }

    public class Comanda
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public string Cliente { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public ComandaStatus Status { get; set; }
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public bool ControlaEstoque { get; set; }
    }

    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public string Obs { get; set; }
        public decimal ValorUnitario { get; set; }
        public string Status { get; set; } = "NORMAL";
    }

    public class Pedido
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Hora { get; set; }
        public PedidoStatus Status { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }

    public class LogEntry
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string Acao { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataLog { get; set; }
    }
}
