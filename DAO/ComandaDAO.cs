using LLCART_CMD.Data;
using LLCART_CMD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LLCART_CMD.DAO
{
    public class ComandaDAO
    {
        public int AbrirComanda(int? mesaId, string cliente)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO comandas (mesa_id, cliente, data_abertura, status) VALUES (@mesa, @cliente, NOW(), 'ABERTA')";
                    cmd.Parameters.AddWithValue("@mesa", mesaId.HasValue ? (object)mesaId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", cliente ?? string.Empty);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT LAST_INSERT_ID()";
                    var id = Convert.ToInt32(cmd.ExecuteScalar());
                    return id;
                }
            }
        }

        public void Close(int idComanda)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE comandas SET status = 'FECHADA', data_fechamento = NOW() WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", idComanda);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Comanda Get(int idComanda)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, mesa_id, cliente, data_abertura, data_fechamento, status FROM comandas WHERE id = @id LIMIT 1";
                    cmd.Parameters.AddWithValue("@id", idComanda);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            DateTime? dataFechamento = null;
                            int ordinalDataFechamento = dr.GetOrdinal("data_fechamento");
                            if (!dr.IsDBNull(ordinalDataFechamento))
                                dataFechamento = dr.GetDateTime(ordinalDataFechamento);

                            return new Comanda
                            {
                                Id = dr.GetInt32("id"),
                                MesaId = dr.IsDBNull(dr.GetOrdinal("mesa_id")) ? 0 : dr.GetInt32("mesa_id"),
                                Cliente = dr.IsDBNull(dr.GetOrdinal("cliente")) ? null : dr.GetString("cliente"),
                                DataAbertura = dr.GetDateTime("data_abertura"),
                                DataFechamento = dataFechamento,
                                Status = Enum.TryParse<ComandaStatus>(dr.GetString("status"), out var s) ? s : ComandaStatus.ABERTA
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public List<Comanda> GetAbertas()
        {
            var list = new List<Comanda>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, mesa_id, cliente, data_abertura, status FROM comandas WHERE status = 'ABERTA' ORDER BY data_abertura";
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new Comanda
                            {
                                Id = dr.GetInt32("id"),
                                MesaId = dr.IsDBNull(dr.GetOrdinal("mesa_id")) ? 0 : dr.GetInt32("mesa_id"),
                                Cliente = dr.IsDBNull(dr.GetOrdinal("cliente")) ? null : dr.GetString("cliente"),
                                DataAbertura = dr.GetDateTime("data_abertura"),
                                Status = Enum.TryParse<ComandaStatus>(dr.GetString("status"), out var s) ? s : ComandaStatus.ABERTA
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
