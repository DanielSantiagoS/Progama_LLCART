using System;
using System.Collections.Generic;
using System.Linq;
using Forms_LLCART_Projeto.Models;

namespace Forms_LLCART_Projeto.Services
{
    public class PedidoService
    {
        public void SalvarPedido(Pedido pedido)
        {
            GerenciadorDados.SalvarPedido(pedido);
        }

        public Pedido ObterPedidoPorMesa(int mesaId)
        {
            return GerenciadorDados.ObterPedidoPorMesa(mesaId);
        }

        public Pedido ObterPedidoPorId(int id)
        {
            var pedidosAtivos = ObterPedidosAtivos();
            return pedidosAtivos.FirstOrDefault(p => p.Id == id);
        }

        public List<Pedido> ObterPedidosAtivos()
        {
            return GerenciadorDados.ObterPedidosAtivos();
        }

        public List<ItemPedido> ObterItensPorStatus(StatusItem status)
        {
            var pedidosAtivos = ObterPedidosAtivos();

            var itens = new List<ItemPedido>();
            foreach (var pedido in pedidosAtivos)
            {
                itens.AddRange(pedido.Itens.Where(i => i.Status == status));
            }
            return itens.OrderBy(i => i.DataHora).ToList();
        }

        public void AtualizarStatusItem(int pedidoId, int itemId, StatusItem novoStatus)
        {
            var pedido = ObterPedidoPorId(pedidoId);
            var item = pedido?.Itens.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                item.Status = novoStatus;
                item.DataHora = DateTime.Now;

                SalvarPedido(pedido);
            }
        }
    }
}