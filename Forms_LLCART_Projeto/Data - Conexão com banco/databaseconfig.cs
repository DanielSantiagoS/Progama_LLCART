using System;
using MySql.Data.MySqlClient;

namespace Forms_LLCART_Projeto.Data
{
    public static class DatabaseConfig
    {
 
        private static string _server = "localhost";
        private static string _database = "churrascariadb";      //tudo isso aqui e criterio para conexao CUIDADO COM A SENHA E O NOME USADO
        private static string _userId = "root";
        private static string _password = "";

        public static string ConnectionString
        {
            get
            {
                return $"Server={_server};Database={_database};Uid={_userId};Pwd={_password};";  // Monta a string de conexão no formato sql
            }
        }

  
        public static void Configurar(string server, string database, string userId, string password) //atribui outros valores aos campos de login ao banco EX: 
        {                                                                                             // o cara quer usar outro banco sem ser o churrasdb, ai atribui os valores nas variaveis
            _server = server;
            _database = database;
            _userId = userId;
            _password = password;
        }
    }
}