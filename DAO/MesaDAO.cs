using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LLCART_CMD.Data;
using LLCART_CMD.Models;

namespace LLCART_CMD.DAO
{
    public class MesaDAO
    {
        public List<Mesa> GetAll()
        {
            var lista = new List<Mesa>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id_mesa, numero, status FROM mesas ORDER BY numero";

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Mesa
                            {
                                Id = dr.GetInt32("id_mesa"),
                                Numero = dr.GetInt32("numero"),
                                Status = Enum.TryParse<MesaStatus>(dr.GetString("status"), out var s) ? s : MesaStatus.LIVRE
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public Mesa GetByNumero(int numero)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id_mesa, numero, status FROM mesas WHERE numero = @num LIMIT 1";
                    cmd.Parameters.AddWithValue("@num", numero);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Mesa
                            {
                                Id = dr.GetInt32("id_mesa"),
                                Numero = dr.GetInt32("numero"),
                                Status = Enum.TryParse<MesaStatus>(dr.GetString("status"), out var s) ? s : MesaStatus.LIVRE
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Mesa GetById(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id_mesa, numero, status FROM mesas WHERE id_mesa = @id LIMIT 1";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Mesa
                            {
                                Id = dr.GetInt32("id_mesa"),
                                Numero = dr.GetInt32("numero"),
                                Status = Enum.TryParse<MesaStatus>(dr.GetString("status"), out var s) ? s : MesaStatus.LIVRE
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateStatus(int idMesa, MesaStatus status)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE mesas SET status = @status WHERE id_mesa = @id";
                    cmd.Parameters.AddWithValue("@status", status.ToString());
                    cmd.Parameters.AddWithValue("@id", idMesa);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
