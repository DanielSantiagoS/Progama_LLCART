using System;
using MySql.Data.MySqlClient;

namespace Forms_LLCART_Projeto.Data
{
    public static class DatabaseConfig
    {
 
        private static string _server = "localhost";
        private static string _database = "churrascariadb";
        private static string _userId = "root";
        private static string _password = "";

        public static string ConnectionString
        {
            get
            {
                return $"Server={_server};Database={_database};Uid={_userId};Pwd={_password};";
            }
        }

  
        public static void Configurar(string server, string database, string userId, string password)
        {
            _server = server;
            _database = database;
            _userId = userId;
            _password = password;
        }
    }
}