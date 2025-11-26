using System;
using MySql.Data.MySqlClient;
using LLCART_CMD.Models;
using LLCART_CMD.Data;

namespace LLCART_CMD.DAO
{
    public class UsuarioDAO
    {
        
        private readonly string _connectionString = "server=localhost;database=restaurante;uid=root;pwd=root;";

        public Usuario Login(string login, string senha)
        {
            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();

                string sql = "SELECT id_usuario, nome, login, senha, permissao, ativo FROM usuarios WHERE login = @login AND senha = @senha AND ativo = 1";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Usuario
                            {
                                Id = dr.GetInt32("id_usuario"),
                                Nome = dr.GetString("nome"),
                                Login = dr.GetString("login"),
                                SenhaHash = dr.GetString("senha"),
                                Permissao = Enum.TryParse<Permissao>(dr.GetString("permissao"), out var p) ? p : Permissao.GARCOM,
                                Ativo = dr.GetInt32("ativo") == 1
                            };
                        }
                    }
                }
            }
            return null;
        }

       
    }
}
