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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar com o banco: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        public static bool TestarConexao()
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var command = new MySqlCommand("SELECT 1", context.GetConnection());
                    command.ExecuteScalar();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}