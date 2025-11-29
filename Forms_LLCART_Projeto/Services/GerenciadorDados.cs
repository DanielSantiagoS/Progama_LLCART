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
        private static int _proximoItemId = 1;

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

        public static Mesa ObterMesaPorNumero(string numero)
        {
            return _mesas.FirstOrDefault(m => m.Numero == numero);
        }

        public static void FecharPedido(int pedidoId)
        {
            Console.WriteLine($"🔍 DEBUG FecharPedido: Procurando pedido ID={pedidoId}");

            var pedido = ObterPedidoPorId(pedidoId);
            if (pedido != null)
            {
                Console.WriteLine($"🔍 DEBUG: Pedido encontrado - Comanda: {pedido.Comanda}, Mesa: {pedido.MesaId}");

                pedido.Status = StatusPedido.Fechado;
                pedido.DataFechamento = DateTime.Now;

                AtualizarStatusMesa(pedido.MesaId, StatusMesa.Livre, null);

                Console.WriteLine($"✅ PEDIDO FECHADO: {pedido.Comanda} - Mesa {pedido.MesaId} liberada");
            }
            else
            {
                Console.WriteLine($"❌ DEBUG: Pedido ID {pedidoId} NÃO ENCONTRADO para fechar!");

                var pedidoPorMesa = _pedidos.FirstOrDefault(p => p.MesaId == pedidoId && p.Status == StatusPedido.Aberto);
                if (pedidoPorMesa != null)
                {
                    Console.WriteLine($"🔍 DEBUG: Encontrado pedido pela Mesa ID {pedidoId}");
                    pedidoPorMesa.Status = StatusPedido.Fechado;
                    pedidoPorMesa.DataFechamento = DateTime.Now;
                    AtualizarStatusMesa(pedidoPorMesa.MesaId, StatusMesa.Livre, null);
                }
            }
        }

        public static List<Pedido> ObterPedidosFechados()
        {
            return _pedidos.Where(p => p.Status == StatusPedido.Fechado).ToList();
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
            Console.WriteLine($"🔍 DEBUG SalvarPedido: ID={pedido.Id}, Mesa={pedido.MesaId}, Comanda={pedido.Comanda}");

            if (pedido.Id == 0)   
            {
                pedido.Id = _proximoIdPedido++;
                Console.WriteLine($"🔍 DEBUG: Novo pedido - ID atribuído: {pedido.Id}");

                foreach (var item in pedido.Itens)
                {
                    item.Id = _proximoItemId++;
                }
                _pedidos.Add(pedido);
            }
            else   
            {
                Console.WriteLine($"🔍 DEBUG: Atualizando pedido existente ID: {pedido.Id}");
                var existente = _pedidos.FirstOrDefault(p => p.Id == pedido.Id);
                if (existente != null)
                    _pedidos.Remove(existente);
                _pedidos.Add(pedido);
            }

            AtualizarStatusMesa(pedido.MesaId, StatusMesa.Ocupada, pedido.Comanda);
            Console.WriteLine($"🔍 DEBUG: Mesa {pedido.MesaId} atualizada para OCUPADA");
        }

        public static Pedido ObterPedidoPorMesa(int mesaId)
        {
            return _pedidos.FirstOrDefault(p => p.MesaId == mesaId && p.Status == StatusPedido.Aberto);
        }

        public static Pedido ObterPedidoPorId(int id)
        {
            return _pedidos.FirstOrDefault(p => p.Id == id);
        }

        public static List<Pedido> ObterPedidosAtivos()
        {
            return _pedidos.Where(p => p.Status == StatusPedido.Aberto).ToList();
        }

        public static List<ItemPedido> ObterItensPorStatus(StatusItem status)
        {
            var pedidosAtivos = ObterPedidosAtivos();
            var itens = new List<ItemPedido>();

            foreach (var pedido in pedidosAtivos)
            {
                itens.AddRange(pedido.Itens.Where(i => i.Status == status));
            }
            return itens.OrderBy(i => i.DataHora).ToList();
        }

        public static void AtualizarStatusItem(int pedidoId, int itemId, StatusItem novoStatus)
        {
            var pedido = ObterPedidoPorId(pedidoId);
            var item = pedido?.Itens.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                item.Status = novoStatus;
                item.DataHora = DateTime.Now;
            }
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
                GarcomResponsavel = "João",
                Status = StatusPedido.Aberto
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

            _pedidos.Add(pedidoTeste);
            _proximoIdPedido = 2;
            _proximoItemId = 2;

            AtualizarStatusMesa(2, StatusMesa.Ocupada, "COMANDA001");


        }
    }
}