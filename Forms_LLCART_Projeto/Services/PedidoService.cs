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
            return GerenciadorDados.ObterPedidoPorId(id);
        }

        public List<Pedido> ObterPedidosAtivos()
        {
            return GerenciadorDados.ObterPedidosAtivos();
        }

        public void FecharPedido(int pedidoId)
        {
            GerenciadorDados.FecharPedido(pedidoId);
        }

        public List<Pedido> ObterPedidosFechados()
        {
            return GerenciadorDados.ObterPedidosFechados();
        }

        public List<ItemPedido> ObterItensPorStatus(StatusItem status)
        {
            return GerenciadorDados.ObterItensPorStatus(status);
        }

        public void AtualizarStatusItem(int pedidoId, int itemId, StatusItem novoStatus)
        {
            GerenciadorDados.AtualizarStatusItem(pedidoId, itemId, novoStatus);
        }
    }
}