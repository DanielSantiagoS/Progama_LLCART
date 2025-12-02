using System.Collections.Generic;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Repositories;

namespace Forms_LLCART_Projeto.Services
{
    public class PedidoService
    {
        private readonly PedidoRepo _pedidoRepo;
        private readonly MesaRepo _mesaRepo;

        public PedidoService()
        {
            _pedidoRepo = new PedidoRepo();
            _mesaRepo = new MesaRepo();
        }

        public void SalvarPedido(Pedido pedido)
        {
            if (pedido.Id == 0)
            {
                _pedidoRepo.Criar(pedido);
                _mesaRepo.AtualizarStatus(pedido.MesaId, StatusMesa.Ocupada, pedido.Comanda);
            }
        }

        public Pedido ObterPedidoPorMesa(int mesaId)
        {
            var pedidosAtivos = _pedidoRepo.ObterPedidosAtivos();
            return pedidosAtivos.Find(p => p.MesaId == mesaId);
        }

        public Pedido ObterPedidoPorId(int id)
        {
            return _pedidoRepo.ObterPorId(id);
        }

        public List<Pedido> ObterPedidosAtivos()
        {
            return _pedidoRepo.ObterPedidosAtivos();
        }

        public void FecharPedido(int pedidoId)
        {
            var pedido = _pedidoRepo.ObterPorId(pedidoId);
            if (pedido != null)
            {
                _pedidoRepo.FecharPedido(pedidoId);
                _mesaRepo.AtualizarStatus(pedido.MesaId, StatusMesa.Livre, null);
            }
        }

        public void AtualizarStatusItem(int pedidoId, int itemId, StatusItem novoStatus)
        {
            _pedidoRepo.AtualizarStatusItem(pedidoId, itemId, novoStatus);
        }

       
        public List<ItemPedido> ObterItensPorStatus(StatusItem status)
        {
            return _pedidoRepo.ObterItensPorStatus(status);
        }

       
        public List<Pedido> ObterPedidosFechados()
        {
        
            return new List<Pedido>();
        }
    }
}