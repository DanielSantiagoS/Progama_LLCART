using System.Collections.Generic;
using System.Linq;
using Forms_LLCART.Models;

namespace Forms_LLCART.Services
{
    public class MesaService
    {
        private List<Mesa> mesas;

        public MesaService()
        {
            mesas = new List<Mesa>
            {
                new Mesa { Id = 1, Numero = "01", Capacidade = 4, Status = StatusMesa.Livre },
                new Mesa { Id = 2, Numero = "02", Capacidade = 6, Status = StatusMesa.Livre },
                new Mesa { Id = 3, Numero = "03", Capacidade = 2, Status = StatusMesa.Livre },
                new Mesa { Id = 4, Numero = "04", Capacidade = 8, Status = StatusMesa.Livre },
                new Mesa { Id = 5, Numero = "05", Capacidade = 4, Status = StatusMesa.Livre },
                new Mesa { Id = 6, Numero = "06", Capacidade = 6, Status = StatusMesa.Livre }
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
    }
}