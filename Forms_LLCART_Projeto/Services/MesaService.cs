using System.Collections.Generic;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Repositories;

namespace Forms_LLCART_Projeto.Services
{
    public class MesaService
    {
        private readonly MesaRepo _mesaRepo;

        public MesaService()
        {
            _mesaRepo = new MesaRepo();
        }

        public List<Mesa> ObterMesas()
        {
            return _mesaRepo.ObterTodas();
        }

        public Mesa ObterMesaPorId(int id)
        {
            return _mesaRepo.ObterPorId(id);
        }

        public void CriarMesa(Mesa mesa)
        {
            _mesaRepo.Criar(mesa);
        }

        public void AtualizarMesa(Mesa mesa)
        {
            _mesaRepo.Atualizar(mesa);
        }

        public void ExcluirMesa(int id)
        {
            _mesaRepo.Excluir(id);
        }

        public void AtualizarStatusMesa(int mesaId, StatusMesa status, string comanda = null)
        {
            _mesaRepo.AtualizarStatus(mesaId, status, comanda);
        }
    }
}