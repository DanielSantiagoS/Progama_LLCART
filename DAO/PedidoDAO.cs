using MySql.Data.MySqlClient;
using System;
using LLCART_CMD.Data;

namespace LLCART_CMD.DAO
{
    public class PedidoDAO
    {
        public static void AdicionarPedido(int comandaId, int produtoId, int qtd, string obs)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                const string sql = @"INSERT INTO pedidos 
                                     (comanda_id, produto_id, quantidade, observacao, status, hora)
                                     VALUES (@c, @p, @q, @o, 'NOVO', NOW())";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@c", comandaId);
                    cmd.Parameters.AddWithValue("@p", produtoId);
                    cmd.Parameters.AddWithValue("@q", qtd);
                    cmd.Parameters.AddWithValue("@o", obs ?? string.Empty);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ListarPedidos(int comandaId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"SELECT pedidos.id, produtos.nome, pedidos.quantidade, pedidos.status, pedidos.observacao
                               FROM pedidos
                               JOIN produtos ON produtos.id_produto = pedidos.produto_id
                               WHERE comanda_id = @c";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@c", comandaId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n--- PEDIDOS DA COMANDA ---\n");

                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"ID: {reader["id"]} | Produto: {reader["nome"]} | Qtd: {reader["quantidade"]} | Status: {reader["status"]} | Obs: {reader["observacao"]}"
                            );
                        }
                    }
                }
            }
        }
    }
}
