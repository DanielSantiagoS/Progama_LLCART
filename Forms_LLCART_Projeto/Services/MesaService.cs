using System.Collections.Generic;
using System.Linq;
using Forms_LLCART_Projeto.Models;

namespace Forms_LLCART_Projeto.Services
{
    public class MesaService
    {
        private List<Mesa> mesas;
        private int proximoId = 7; // ← Declarado aqui

        public MesaService()
        {
            mesas = new List<Mesa>
            {
                new Mesa { Id = 1, Numero = "01", Capacidade = 4, Status = StatusMesa.Livre },
                new Mesa { Id = 2, Numero = "02", Capacidade = 6, Status = StatusMesa.Ocupada, ComandaAtual = "COMANDA001" },
                new Mesa { Id = 3, Numero = "03", Capacidade = 2, Status = StatusMesa.Livre },
                new Mesa { Id = 4, Numero = "04", Capacidade = 8, Status = StatusMesa.Ocupada, ComandaAtual = "COMANDA002" },
                new Mesa { Id = 5, Numero = "05", Capacidade = 4, Status = StatusMesa.Livre },
                new Mesa { Id = 6, Numero = "06", Capacidade = 6, Status = StatusMesa.Ocupada, ComandaAtual = "COMANDA003" }
            };
        }

        public List<Mesa> ObterMesas()
        {
            return mesas.OrderBy(m => m.Numero).ToList();
        }

        public Mesa ObterMesaPorId(int id)
        {
            return mesas.FirstOrDefault(m => m.Id == id);
        }

        public void AtualizarStatusMesa(int mesaId, StatusMesa status, string comanda = null)
        {
            var mesa = ObterMesaPorId(mesaId);
            if (mesa != null)
            {
                mesa.Status = status;
                mesa.ComandaAtual = comanda;
            }
        }

        public void AdicionarMesa(string numero, int capacidade)
        {
            var novaMesa = new Mesa
            {
                Id = proximoId++,
                Numero = numero,
                Capacidade = capacidade,
                Status = StatusMesa.Livre
            };

            mesas.Add(novaMesa);
        }

        public bool RemoverMesa(int mesaId)
        {
            var mesa = ObterMesaPorId(mesaId);
            if (mesa != null && mesa.Status == StatusMesa.Livre)
            {
                mesas.Remove(mesa);
                return true;
            }
            return false;
        }

        public List<Mesa> ObterMesasPorStatus(StatusMesa status)
        {
            return mesas.Where(m => m.Status == status).ToList();
        }

        public List<Mesa> ObterMesasOcupadas()
        {
            return ObterMesasPorStatus(StatusMesa.Ocupada);
        }

        public List<Mesa> ObterMesasLivres()
        {
            return ObterMesasPorStatus(StatusMesa.Livre);
        }

        public bool MesaExiste(string numero)
        {
            return mesas.Any(m => m.Numero == numero);
        }
    }
}