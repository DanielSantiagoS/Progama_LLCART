using System.Collections.Generic;
using System.Linq;
using Forms_LLCART_Projeto.Models;

namespace Forms_LLCART_Projeto.Services
{
    public class ProdutoService
    {
        private List<Produto> produtos;

        public ProdutoService()
        {
            produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Picanha", Categoria = "Carnes", Preco = 89.90m, SetorPreparo = "Churrasco", Ativo = true },
                new Produto { Id = 2, Nome = "Costela", Categoria = "Carnes", Preco = 79.90m, SetorPreparo = "Churrasco", Ativo = true },
                new Produto { Id = 3, Nome = "Fraldinha", Categoria = "Carnes", Preco = 69.90m, SetorPreparo = "Churrasco", Ativo = true },
                new Produto { Id = 4, Nome = "Coração", Categoria = "Carnes", Preco = 45.90m, SetorPreparo = "Churrasco", Ativo = true },
                
                new Produto { Id = 5, Nome = "Coca-Cola 2L", Categoria = "Bebidas", Preco = 12.90m, SetorPreparo = "Bebidas", Ativo = true },
                new Produto { Id = 6, Nome = "Suco Natural", Categoria = "Bebidas", Preco = 8.90m, SetorPreparo = "Bebidas", Ativo = true },
                new Produto { Id = 7, Nome = "Cerveja Heineken", Categoria = "Bebidas", Preco = 9.90m, SetorPreparo = "Bebidas", Ativo = true },
                
                new Produto { Id = 8, Nome = "Arroz", Categoria = "Acompanhamentos", Preco = 15.90m, SetorPreparo = "Cozinha", Ativo = true },
                new Produto { Id = 9, Nome = "Farofa", Categoria = "Acompanhamentos", Preco = 12.90m, SetorPreparo = "Cozinha", Ativo = true },
                new Produto { Id = 10, Nome = "Vinagrete", Categoria = "Acompanhamentos", Preco = 10.90m, SetorPreparo = "Cozinha", Ativo = true },
                
                new Produto { Id = 11, Nome = "Pudim", Categoria = "Sobremesas", Preco = 18.90m, SetorPreparo = "Sobremesas", Ativo = true },
                new Produto { Id = 12, Nome = "Sorvete", Categoria = "Sobremesas", Preco = 14.90m, SetorPreparo = "Sobremesas", Ativo = true }
            };
        }

        public List<Produto> ObterProdutos()
        {
            return produtos.Where(p => p.Ativo).OrderBy(p => p.Categoria).ThenBy(p => p.Nome).ToList();
        }

        public List<Produto> ObterProdutosPorCategoria(string categoria)
        {
            return produtos.Where(p => p.Ativo && p.Categoria == categoria).ToList();
        }

        public List<string> ObterCategorias()
        {
            return produtos.Select(p => p.Categoria).Distinct().ToList();
        }

        public Produto ObterProdutoPorId(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }
    }
}