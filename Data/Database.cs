using MySql.Data.MySqlClient;
using System.Configuration;
using System; 

namespace LLCART_CMD.Data
{
    public static class Database
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["RestauranteDb"]?.ConnectionString
            ?? "server=localhost;database=restaurante;uid=root;pwd=root;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static void PrintRootUserPlugin()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new MySqlCommand("SELECT user, host, plugin FROM mysql.user WHERE user='root';", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"User: {reader["user"]}, Host: {reader["host"]}, Plugin: {reader["plugin"]}");
                    }
                }
            }
        }

        public static void AlterRootUser()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new MySqlCommand("ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'root'; FLUSH PRIVILEGES;", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
