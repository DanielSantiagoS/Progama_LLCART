using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LLCART_CMD.Data;
using LLCART_CMD.Models;

namespace LLCART_CMD.DAO
{
    public class ProdutoDAO
    {
        public List<Produto> GetAll()
        {
            var list = new List<Produto>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id_produto, nome, categoria, preco, controla_estoque FROM produtos ORDER BY nome";

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Produto
                            {
                                Id = dr.GetInt32("id_produto"),
                                Nome = dr.GetString("nome"),
                                Categoria = dr.IsDBNull(dr.GetOrdinal("categoria")) ? string.Empty : dr.GetString("categoria"),
                                Preco = dr.GetDecimal("preco"),
                                ControlaEstoque = dr.GetInt32("controla_estoque") == 1
                            });
                        }
                    }
                }
            }
            return list;
        }

        public Produto Get(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id_produto, nome, categoria, preco, controla_estoque FROM produtos WHERE id_produto = @id LIMIT 1";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Produto
                            {
                                Id = dr.GetInt32("id_produto"),
                                Nome = dr.GetString("nome"),
                                Categoria = dr.IsDBNull(dr.GetOrdinal("categoria")) ? string.Empty : dr.GetString("categoria"),
                                Preco = dr.GetDecimal("preco"),
                                ControlaEstoque = dr.GetInt32("controla_estoque") == 1
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
