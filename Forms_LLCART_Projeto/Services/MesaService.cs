using System.Collections.Generic;
using Forms_LLCART_Projeto.Models;

namespace Forms_LLCART_Projeto.Services
{
    public class MesaService
    {
        public List<Mesa> ObterMesas()
        {
            return GerenciadorDados.ObterMesas();
        }

        public Mesa ObterMesaPorId(int id)
        {
            return GerenciadorDados.ObterMesaPorId(id);
        }

        public void AtualizarStatusMesa(int mesaId, StatusMesa status, string comanda = null)
        {
            GerenciadorDados.AtualizarStatusMesa(mesaId, status, comanda);
        }
    }
}