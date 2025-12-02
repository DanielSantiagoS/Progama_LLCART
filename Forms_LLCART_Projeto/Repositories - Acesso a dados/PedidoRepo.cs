using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Forms_LLCART_Projeto.Models;
using Forms_LLCART_Projeto.Data;

namespace Forms_LLCART_Projeto.Repositories
{
    public class PedidoRepo
    {
        public void Criar(Pedido pedido)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    @"INSERT INTO Pedidos (MesaId, Comanda, DataAbertura, Status, Observacoes, GarcomResponsavel) 
                      VALUES (@MesaId, @Comanda, @DataAbertura, @Status, @Observacoes, @GarcomResponsavel); 
                      SELECT LAST_INSERT_ID();",
                    context.GetConnection());

                command.Parameters.AddWithValue("@MesaId", pedido.MesaId);
                command.Parameters.AddWithValue("@Comanda", pedido.Comanda);
                command.Parameters.AddWithValue("@DataAbertura", pedido.DataAbertura);
                command.Parameters.AddWithValue("@Status", (int)pedido.Status);
                command.Parameters.AddWithValue("@Observacoes", (object)pedido.Observacoes ?? DBNull.Value);
                command.Parameters.AddWithValue("@GarcomResponsavel", pedido.GarcomResponsavel);

                pedido.Id = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item in pedido.Itens)
                {
                    var itemCommand = new MySqlCommand(
                        @"INSERT INTO ItensPedido (PedidoId, ProdutoId, NomeProduto, Quantidade, PrecoUnitario, Observacoes, Status, DataHora, UsuarioResponsavel)
                          VALUES (@PedidoId, @ProdutoId, @NomeProduto, @Quantidade, @PrecoUnitario, @Observacoes, @Status, @DataHora, @UsuarioResponsavel);
                          SELECT LAST_INSERT_ID();",
                        context.GetConnection());

                    itemCommand.Parameters.AddWithValue("@PedidoId", pedido.Id);
                    itemCommand.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                    itemCommand.Parameters.AddWithValue("@NomeProduto", item.NomeProduto);
                    itemCommand.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                    itemCommand.Parameters.AddWithValue("@PrecoUnitario", item.PrecoUnitario);
                    itemCommand.Parameters.AddWithValue("@Observacoes", (object)item.Observacoes ?? DBNull.Value);
                    itemCommand.Parameters.AddWithValue("@Status", (int)item.Status);
                    itemCommand.Parameters.AddWithValue("@DataHora", item.DataHora);
                    itemCommand.Parameters.AddWithValue("@UsuarioResponsavel", item.UsuarioResponsavel);

                    item.Id = Convert.ToInt32(itemCommand.ExecuteScalar());
                }
            }
        }

        public Pedido ObterPorId(int id)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    @"SELECT Id, MesaId, Comanda, DataAbertura, DataFechamento, Status, Observacoes, GarcomResponsavel 
                      FROM Pedidos WHERE Id = @Id",
                    context.GetConnection());
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            Id = reader.GetInt32("Id"),
                            MesaId = reader.GetInt32("MesaId"),
                            Comanda = reader.GetString("Comanda"),
                            DataAbertura = reader.GetDateTime("DataAbertura"),
                            DataFechamento = reader.IsDBNull(reader.GetOrdinal("DataFechamento")) ? null : (DateTime?)reader.GetDateTime("DataFechamento"),
                            Status = (StatusPedido)reader.GetInt32("Status"),
                            Observacoes = reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString("Observacoes"),
                            GarcomResponsavel = reader.GetString("GarcomResponsavel")
                        };

                        pedido.Itens = ObterItensPorPedidoId(pedido.Id);
                        return pedido;
                    }
                }
            }
            return null;
        }

        private List<ItemPedido> ObterItensPorPedidoId(int pedidoId)
        {
            var itens = new List<ItemPedido>();

            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    @"SELECT Id, ProdutoId, NomeProduto, Quantidade, PrecoUnitario, Observacoes, Status, DataHora, UsuarioResponsavel 
                      FROM ItensPedido WHERE PedidoId = @PedidoId",
                    context.GetConnection());
                command.Parameters.AddWithValue("@PedidoId", pedidoId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itens.Add(new ItemPedido
                        {
                            Id = reader.GetInt32("Id"),
                            ProdutoId = reader.GetInt32("ProdutoId"),
                            NomeProduto = reader.GetString("NomeProduto"),
                            Quantidade = reader.GetInt32("Quantidade"),
                            PrecoUnitario = reader.GetDecimal("PrecoUnitario"),
                            Observacoes = reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString("Observacoes"),
                            Status = (StatusItem)reader.GetInt32("Status"),
                            DataHora = reader.GetDateTime("DataHora"),
                            UsuarioResponsavel = reader.GetString("UsuarioResponsavel")
                        });
                    }
                }
            }
            return itens;
        }

        public List<Pedido> ObterPedidosAtivos()
        {
            var pedidos = new List<Pedido>();

            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    @"SELECT Id, MesaId, Comanda, DataAbertura, DataFechamento, Status, Observacoes, GarcomResponsavel 
                      FROM Pedidos WHERE Status = 0 ORDER BY DataAbertura",
                    context.GetConnection());

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            Id = reader.GetInt32("Id"),
                            MesaId = reader.GetInt32("MesaId"),
                            Comanda = reader.GetString("Comanda"),
                            DataAbertura = reader.GetDateTime("DataAbertura"),
                            DataFechamento = reader.IsDBNull(reader.GetOrdinal("DataFechamento")) ? null : (DateTime?)reader.GetDateTime("DataFechamento"),
                            Status = (StatusPedido)reader.GetInt32("Status"),
                            Observacoes = reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString("Observacoes"),
                            GarcomResponsavel = reader.GetString("GarcomResponsavel")
                        };

                        pedido.Itens = ObterItensPorPedidoId(pedido.Id);
                        pedidos.Add(pedido);
                    }
                }
            }
            return pedidos;
        }

        public void FecharPedido(int pedidoId)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "UPDATE Pedidos SET Status = 1, DataFechamento = @DataFechamento WHERE Id = @Id",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Id", pedidoId);
                command.Parameters.AddWithValue("@DataFechamento", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }

        public void AtualizarStatusItem(int pedidoId, int itemId, StatusItem novoStatus)
        {
            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    "UPDATE ItensPedido SET Status = @Status, DataHora = @DataHora WHERE Id = @Id AND PedidoId = @PedidoId",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Id", itemId);
                command.Parameters.AddWithValue("@PedidoId", pedidoId);
                command.Parameters.AddWithValue("@Status", (int)novoStatus);
                command.Parameters.AddWithValue("@DataHora", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }

        
        public List<ItemPedido> ObterItensPorStatus(StatusItem status)
        {
            var itens = new List<ItemPedido>();

            using (var context = new DatabaseContext())
            {
                var command = new MySqlCommand(
                    @"SELECT ip.Id, ip.PedidoId, ip.ProdutoId, ip.NomeProduto, ip.Quantidade, ip.PrecoUnitario, 
                     ip.Observacoes, ip.Status, ip.DataHora, ip.UsuarioResponsavel,
                     p.Comanda, p.MesaId
              FROM ItensPedido ip
              INNER JOIN Pedidos p ON ip.PedidoId = p.Id
              WHERE ip.Status = @Status AND p.Status = 0
              ORDER BY ip.DataHora",
                    context.GetConnection());

                command.Parameters.AddWithValue("@Status", (int)status);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itens.Add(new ItemPedido
                        {
                            Id = reader.GetInt32("Id"),
                            PedidoId = reader.GetInt32("PedidoId"),
                            ProdutoId = reader.GetInt32("ProdutoId"),
                            NomeProduto = reader.GetString("NomeProduto"),
                            Quantidade = reader.GetInt32("Quantidade"),
                            PrecoUnitario = reader.GetDecimal("PrecoUnitario"),
                            Observacoes = reader.IsDBNull(reader.GetOrdinal("Observacoes")) ? null : reader.GetString("Observacoes"),
                            Status = (StatusItem)reader.GetInt32("Status"),
                            DataHora = reader.GetDateTime("DataHora"),
                            UsuarioResponsavel = reader.GetString("UsuarioResponsavel")
                        });
                    }
                }
            }
            return itens;
        }
    }
}