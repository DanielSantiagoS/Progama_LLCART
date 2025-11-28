using System;
using System.Collections.Generic;
using System.Linq;
using Forms_LLCART_Projeto.Models;

namespace Forms_LLCART_Projeto.Services
{
    public class PedidoService
    {
        private List<Pedido> pedidos;
        private int proximoId = 1;

        public PedidoService()
        {
            pedidos = new List<Pedido>();
        }

        public void AdicionarPedido(Pedido pedido)
        {
            pedido.Id = proximoId++;
            pedidos.Add(pedido);
        }

        public List<Pedido> ObterPedidosAtivos()
        {
            return pedidos.Where(p => p.Status == StatusPedido.Aberto).ToList();
        }

        public List<ItemPedido> ObterItensPorStatus(StatusItem status)
        {
            var itens = new List<ItemPedido>();

            foreach (var pedido in pedidos.Where(p => p.Status == StatusPedido.Aberto))
            {
                itens.AddRange(pedido.Itens.Where(i => i.Status == status));
            }

            return itens.OrderBy(i => i.DataHora).ToList();
        }

        public void AtualizarStatusItem(int pedidoId, int itemId, StatusItem novoStatus)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == pedidoId);
            var item = pedido?.Itens.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                item.Status = novoStatus;
                item.DataHora = DateTime.Now;
            }
        }

        public List<ItemPedido> ObterItensPorSetor(string setorPreparo)
        {
            var itens = new List<ItemPedido>();
            return itens;
        }

        public void AdicionarDadosTeste()
        {
            var pedidoTeste = new Pedido
            {
                MesaId = 1,
                Comanda = "TESTE001",
                GarcomResponsavel = "Teste"
            };

            pedidoTeste.Itens.Add(new ItemPedido
            {
                Id = 1,
                NomeProduto = "Picanha",
                Quantidade = 2,
                PrecoUnitario = 89.90m,
                Status = StatusItem.Novo,
                DataHora = DateTime.Now
            });

            pedidoTeste.Itens.Add(new ItemPedido
            {
                Id = 2,
                NomeProduto = "Coca-Cola 2L",
                Quantidade = 1,
                PrecoUnitario = 12.90m,
                Status = StatusItem.EmPreparo,
                DataHora = DateTime.Now.AddMinutes(-5)
            });

            AdicionarPedido(pedidoTeste);
        }

    }
}