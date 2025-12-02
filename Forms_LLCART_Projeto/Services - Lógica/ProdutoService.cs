using System.Collections.Generic;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Repositories;

namespace Forms_LLCART_Projeto.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepo _produtoRepo;

        public ProdutoService()
        {
            _produtoRepo = new ProdutoRepo();
        }

        public List<Produto> ObterProdutos()
        {
            return _produtoRepo.ObterTodos();
        }

        public List<Produto> ObterProdutosPorCategoria(string categoria)
        {
            var produtos = _produtoRepo.ObterTodos();
            return produtos.FindAll(p => p.Categoria == categoria);
        }

        public List<string> ObterCategorias()
        {
            return _produtoRepo.ObterCategorias();
        }

        public Produto ObterProdutoPorId(int id)
        {
            return _produtoRepo.ObterPorId(id);
        }

        public void CriarProduto(Produto produto)
        {
            _produtoRepo.Criar(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            _produtoRepo.Atualizar(produto);
        }

        public void ExcluirProduto(int id)
        {
            _produtoRepo.Excluir(id);
        }
    }
}