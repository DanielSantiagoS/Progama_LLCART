using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Data;

namespace Forms_LLCART_Projeto.Repositories
{
    public class ProdutoRepo
    {
        public List<Produto> ObterTodos()
        {
            var produtos = new List<Produto>();

            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "SELECT Id, Nome, Categoria, Preco, Descricao, Ativo, Estoque, SetorPreparo FROM Produtos WHERE Ativo = 1 ORDER BY Categoria, Nome",
                    context.GetConnection());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Categoria = reader.GetString("Categoria"),
                            Preco = reader.GetDecimal("Preco"),
                            Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao"),
                            Ativo = reader.GetBoolean("Ativo"),
                            Estoque = reader.GetInt32("Estoque"),
                            SetorPreparo = reader.GetString("SetorPreparo")
                        });
                    }
                }
            }
            return produtos;
        }

        public Produto ObterPorId(int id)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "SELECT Id, Nome, Categoria, Preco, Descricao, Ativo, Estoque, SetorPreparo FROM Produtos WHERE Id = @Id",
                    context.GetConnection());
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Produto
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Categoria = reader.GetString("Categoria"),
                            Preco = reader.GetDecimal("Preco"),
                            Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao"),
                            Ativo = reader.GetBoolean("Ativo"),
                            Estoque = reader.GetInt32("Estoque"),
                            SetorPreparo = reader.GetString("SetorPreparo")
                        };
                    }
                }
            }
            return null;
        }

        public void Criar(Produto produto)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "INSERT INTO Produtos (Nome, Categoria, Preco, Descricao, Ativo, Estoque, SetorPreparo) VALUES (@Nome, @Categoria, @Preco, @Descricao, @Ativo, @Estoque, @SetorPreparo); SELECT LAST_INSERT_ID();",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Nome", produto.Nome);
                command.Parameters.AddWithValue("@Categoria", produto.Categoria);
                command.Parameters.AddWithValue("@Preco", produto.Preco);
                command.Parameters.AddWithValue("@Descricao", (object)produto.Descricao ?? DBNull.Value);
                command.Parameters.AddWithValue("@Ativo", produto.Ativo);
                command.Parameters.AddWithValue("@Estoque", produto.Estoque);
                command.Parameters.AddWithValue("@SetorPreparo", produto.SetorPreparo);

                produto.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Atualizar(Produto produto)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "UPDATE Produtos SET Nome = @Nome, Categoria = @Categoria, Preco = @Preco, Descricao = @Descricao, Ativo = @Ativo, Estoque = @Estoque, SetorPreparo = @SetorPreparo WHERE Id = @Id",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Id", produto.Id);
                command.Parameters.AddWithValue("@Nome", produto.Nome);
                command.Parameters.AddWithValue("@Categoria", produto.Categoria);
                command.Parameters.AddWithValue("@Preco", produto.Preco);
                command.Parameters.AddWithValue("@Descricao", (object)produto.Descricao ?? DBNull.Value);
                command.Parameters.AddWithValue("@Ativo", produto.Ativo);
                command.Parameters.AddWithValue("@Estoque", produto.Estoque);
                command.Parameters.AddWithValue("@SetorPreparo", produto.SetorPreparo);

                command.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand("DELETE FROM Produtos WHERE Id = @Id", context.GetConnection());
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<string> ObterCategorias()
        {
            var categorias = new List<string>();

            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand("SELECT DISTINCT Categoria FROM Produtos WHERE Ativo = 1 ORDER BY Categoria", context.GetConnection());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(reader.GetString("Categoria"));
                    }
                }
            }
            return categorias;
        }
    }
}