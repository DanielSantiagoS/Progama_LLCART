using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Forms_LLCART_Projeto.Data
{
    public class DatabaseContext : IDisposable
    {
        private MySqlConnection _connection;

        public DatabaseContext()
        {
            try
            {
                string connectionString = DatabaseConfig.ConnectionString;
                _connection = new MySqlConnection(connectionString);
                _connection.Open();

                Console.WriteLine($"✅ Conectado ao banco: {DatabaseConfig.ConnectionString.Split(';')[1]}");
            }
            catch (Exception ex)
            {
                string erro = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Erro de conexão: {ex.Message}\n";
                System.IO.File.AppendAllText("log_erros.txt", erro);

                MessageBox.Show(
                    $"❌ Não foi possível conectar ao banco de dados.\n\n" +
                    $"Verifique:\n" +
                    $"• Servidor MySQL está rodando\n" +
                    $"• Credenciais corretas\n" +
                    $"• Database 'churrascariadb' existe\n\n" +
                    $"Detalhes: {ex.Message}",
                    "Erro de Conexão",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                throw;
            }
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();

            Console.WriteLine("✅ Conexão com banco fechada");
        }

        public static bool TestarConexao()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var command = new MySqlCommand("SELECT 1", context.GetConnection());
                    command.ExecuteScalar();

                    Console.WriteLine("✅ Teste de conexão com banco: OK");
                    return true;
                }
            }
            catch (Exception ex)
            {
                string erro = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Teste conexão falhou: {ex.Message}\n";
                System.IO.File.AppendAllText("log_erros.txt", erro);

                Console.WriteLine($"❌ Teste de conexão com banco: FALHOU - {ex.Message}");
                return false;
            }
        }
    }
}