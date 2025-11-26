using System;
using System.Collections.Generic;
using LLCART_CMD.DAO;
using LLCART_CMD.Models;

namespace LLCART_CMD.Services
{
    public class MainService
    {
        private readonly UsuarioDAO _userDao = new UsuarioDAO();
        private readonly MesaDAO _mesaDao = new MesaDAO();
        private readonly ComandaDAO _comDao = new ComandaDAO();
        private readonly ProdutoDAO _prodDao = new ProdutoDAO();
        

        public Usuario Authenticate(string login, string senha)
        {
            var u = _userDao.Login(login, senha);
            if (u == null) return null;
            if (!u.Ativo) return null;
            if (!Utils.Security.VerifyPassword(senha, u.SenhaHash)) return null;
            return u;
        }

        public List<Mesa> ListarMesas() => _mesaDao.GetAll();
        public Mesa GetMesaByNumero(int numero) => _mesaDao.GetByNumero(numero);

        public int AbrirComanda(int? mesaId, string cliente, Usuario user)
        {
            var id = _comDao.AbrirComanda(mesaId, cliente);
            if (mesaId.HasValue)
                _mesaDao.UpdateStatus(mesaId.Value, MesaStatus.OCUPADA);
            return id;
        }

        public List<Comanda> ListarComandasAbertas() => _comDao.GetAbertas();
        public List<Produto> ListarProdutos() => _prodDao.GetAll();
    }
}
