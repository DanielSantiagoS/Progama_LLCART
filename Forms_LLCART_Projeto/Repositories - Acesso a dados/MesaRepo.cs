using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Data;

namespace Forms_LLCART_Projeto.Repositories
{
    public class MesaRepo
    {
        public List<Mesa> ObterTodas()
        {
            var mesas = new List<Mesa>();

            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "SELECT Id, Numero, Capacidade, Status, ComandaAtual FROM Mesas ORDER BY Numero",
                    context.GetConnection());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mesas.Add(new Mesa
                        {
                            Id = reader.GetInt32("Id"),
                            Numero = reader.GetString("Numero"),
                            Capacidade = reader.GetInt32("Capacidade"),
                            Status = (StatusMesa)reader.GetInt32("Status"),
                            ComandaAtual = reader.IsDBNull(reader.GetOrdinal("ComandaAtual")) ? null : reader.GetString("ComandaAtual")
                        });
                    }
                }
            }

            return mesas;
        }

        public Mesa ObterPorId(int id)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "SELECT Id, Numero, Capacidade, Status, ComandaAtual FROM Mesas WHERE Id = @Id",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Mesa
                        {
                            Id = reader.GetInt32("Id"),
                            Numero = reader.GetString("Numero"),
                            Capacidade = reader.GetInt32("Capacidade"),
                            Status = (StatusMesa)reader.GetInt32("Status"),
                            ComandaAtual = reader.IsDBNull(reader.GetOrdinal("ComandaAtual")) ? null : reader.GetString("ComandaAtual")
                        };
                    }
                }
            }

            return null;
        }

        public void Criar(Mesa mesa)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "INSERT INTO Mesas (Numero, Capacidade, Status, ComandaAtual) VALUES (@Numero, @Capacidade, @Status, @ComandaAtual); SELECT LAST_INSERT_ID();",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Numero", mesa.Numero);
                command.Parameters.AddWithValue("@Capacidade", mesa.Capacidade);
                command.Parameters.AddWithValue("@Status", (int)mesa.Status);
                command.Parameters.AddWithValue("@ComandaAtual", string.IsNullOrEmpty(mesa.ComandaAtual) ? DBNull.Value : (object)mesa.ComandaAtual);

                mesa.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Atualizar(Mesa mesa)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "UPDATE Mesas SET Numero = @Numero, Capacidade = @Capacidade, Status = @Status, ComandaAtual = @ComandaAtual WHERE Id = @Id",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Id", mesa.Id);
                command.Parameters.AddWithValue("@Numero", mesa.Numero);
                command.Parameters.AddWithValue("@Capacidade", mesa.Capacidade);
                command.Parameters.AddWithValue("@Status", (int)mesa.Status);
                command.Parameters.AddWithValue("@ComandaAtual", string.IsNullOrEmpty(mesa.ComandaAtual) ? DBNull.Value : (object)mesa.ComandaAtual);

                command.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand("DELETE FROM Mesas WHERE Id = @Id", context.GetConnection());
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public void AtualizarStatus(int mesaId, StatusMesa status, string comanda = null)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "UPDATE Mesas SET Status = @Status, ComandaAtual = @ComandaAtual WHERE Id = @Id",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Id", mesaId);
                command.Parameters.AddWithValue("@Status", (int)status);
                command.Parameters.AddWithValue("@ComandaAtual", string.IsNullOrEmpty(comanda) ? DBNull.Value : (object)comanda);

                command.ExecuteNonQuery();
            }
        }
    }
}