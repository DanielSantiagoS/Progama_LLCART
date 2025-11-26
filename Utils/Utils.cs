using System;
using System.Security.Cryptography;
using System.Text;

namespace LLCART_CMD.Utils
{
    public static class ConsoleUtils
    {
        public static void Limpar() => Console.Clear();

        public static void Pausa()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
        }

        public static int LerInt(string msg)
        {
            Console.Write(msg);
            while (true)
            {
                var line = Console.ReadLine();
                if (int.TryParse(line, out var v)) return v;
                Console.Write("Valor inválido. Tente novamente: ");
            }
        }
    }

    public static class Security
    {
        public static string Sha256(string value)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                var hash = sha.ComputeHash(bytes);
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                    sb.AppendFormat("{0:x2}", b);
                return sb.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashOrPlain)
        {
            if (storedHashOrPlain == null) return false;
            if (enteredPassword == storedHashOrPlain) return true;
            var h = Sha256(enteredPassword);
            return string.Equals(h, storedHashOrPlain, StringComparison.OrdinalIgnoreCase);
        }

        public static string ReadPassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    sb.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}
