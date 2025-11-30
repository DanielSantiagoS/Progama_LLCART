using System;
using System.Collections.Generic;
using System.Linq;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Repositories;

namespace Forms_LLCART_Projeto.Services
{
    public static class GerenciadorDados
    {
        private static MesaRepo _mesaRepo = new MesaRepo();
        private static PedidoRepo _pedidoRepo = new PedidoRepo();

        public static List<Mesa> ObterMesas()
        {
            return _mesaRepo.ObterTodas();
        }

        public static Mesa ObterMesaPorId(int id)
        {
            return _mesaRepo.ObterPorId(id);
        }

        public static Mesa ObterMesaPorNumero(string numero)
        {
            var mesas = _mesaRepo.ObterTodas();
            return mesas.FirstOrDefault(m => m.Numero == numero);
        }

        public static void FecharPedido(int pedidoId)
        {
            Console.WriteLine($" FecharPedido: Procurando pedido ID={pedidoId}");

            var pedido = _pedidoRepo.ObterPorId(pedidoId);
            if (pedido != null)
            {
                Console.WriteLine($" Pedido encontrado - Comanda: {pedido.Comanda}, Mesa: {pedido.MesaId}");

                _pedidoRepo.FecharPedido(pedidoId);

                var mesa = _mesaRepo.ObterPorId(pedido.MesaId);
                if (mesa != null)
                {
                    _mesaRepo.AtualizarStatus(mesa.Id, StatusMesa.Livre, null);
                }

                Console.WriteLine($" PEDIDO FECHADO: {pedido.Comanda} - Mesa {pedido.MesaId} liberada");
            }
            else
            {
                Console.WriteLine($" Pedido ID {pedidoId} NÃO ENCONTRADO para fechar!");
            }
        }

        
    }
}