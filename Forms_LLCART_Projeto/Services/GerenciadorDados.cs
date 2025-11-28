using System;
using System.Collections.Generic;
using System.Linq;
using Forms_LLCART_Projeto.Models;

namespace Forms_LLCART_Projeto.Services
{
    public static class GerenciadorDados
    {
        private static List<Mesa> _mesas = new List<Mesa>();
        private static List<Pedido> _pedidos = new List<Pedido>();
        private static int _proximoIdMesa = 1;
        private static int _proximoIdPedido = 1;

        public static List<Mesa> ObterMesas()
        {
            if (!_mesas.Any())
                InicializarDados();
            return _mesas.OrderBy(m => m.Numero).ToList();
        }

        public static Mesa ObterMesaPorId(int id)
        {
            return _mesas.FirstOrDefault(m => m.Id == id);
        }

        public static void AtualizarStatusMesa(int mesaId, StatusMesa status, string comanda = null)
        {
            var mesa = ObterMesaPorId(mesaId);
            if (mesa != null)
            {
                mesa.Status = status;
                mesa.ComandaAtual = comanda;
                Console.WriteLine($"✅ MESA ATUALIZADA: {mesa.Numero} -> {status} ({comanda})");
            }
        }

        public static void SalvarPedido(Pedido pedido)
        {
            if (pedido.Id == 0)
            {
                pedido.Id = _proximoIdPedido++;
                _pedidos.Add(pedido);
            }
            else
            {
                var existente = _pedidos.FirstOrDefault(p => p.Id == pedido.Id);
                if (existente != null)
                    _pedidos.Remove(existente);
                _pedidos.Add(pedido);
            }

            AtualizarStatusMesa(pedido.MesaId, StatusMesa.Ocupada, pedido.Comanda);
        }

        public static Pedido ObterPedidoPorMesa(int mesaId)
        {
            return _pedidos.FirstOrDefault(p => p.MesaId == mesaId && p.Status == StatusPedido.Aberto);
        }

        public static List<Pedido> ObterPedidosAtivos()
        {
            return _pedidos.Where(p => p.Status == StatusPedido.Aberto).ToList();
        }

        private static void InicializarDados()
        {
            _mesas = new List<Mesa>
            {
                new Mesa { Id = 1, Numero = "01", Capacidade = 4, Status = StatusMesa.Livre },
                new Mesa { Id = 2, Numero = "02", Capacidade = 6, Status = StatusMesa.Livre },
                new Mesa { Id = 3, Numero = "03", Capacidade = 2, Status = StatusMesa.Livre },
                new Mesa { Id = 4, Numero = "04", Capacidade = 8, Status = StatusMesa.Livre },
                new Mesa { Id = 5, Numero = "05", Capacidade = 4, Status = StatusMesa.Livre },
                new Mesa { Id = 6, Numero = "06", Capacidade = 6, Status = StatusMesa.Livre }
            };
            _proximoIdMesa = 7;

            var pedidoTeste = new Pedido
            {
                Id = 1,
                MesaId = 2,
                Comanda = "COMANDA001",
                GarcomResponsavel = "João"
            };
            pedidoTeste.Itens.Add(new ItemPedido
            {
                Id = 1,
                NomeProduto = "Picanha",
                Quantidade = 2,
                PrecoUnitario = 89.90m,
                Status = StatusItem.Novo
            });
            _pedidos.Add(pedidoTeste);
            _proximoIdPedido = 2;

            AtualizarStatusMesa(2, StatusMesa.Ocupada, "COMANDA001");
        }
    }
}