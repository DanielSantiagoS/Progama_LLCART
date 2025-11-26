using LLCART_CMD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLCART_CMD
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n====== SISTEMA DO RESTAURANTE ======");
                Console.WriteLine("1 – Abrir Comanda");
                Console.WriteLine("2 – Adicionar Pedido");
                Console.WriteLine("3 – Listar Pedidos da Comanda");
                Console.WriteLine("0 – Sair");
                Console.Write("Escolha: ");

                var opc = Console.ReadLine();
                if (opc == "0") break;

                switch (opc)
                {
                    case "1":
                        AbrirComanda();
                        break;
                    case "2":
                        AdicionarPedido();
                        break;
                    case "3":
                        ListarPedidos();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        static void AbrirComanda()
        {
            int mesa = ConsoleUtils.LerInt("Mesa ID (ou 0 para sem mesa): ");
            int? mesaId = mesa == 0 ? (int?)null : (int?)mesa;
            Console.Write("Cliente (opcional): ");
            string cliente = Console.ReadLine();

            var dao = new DAO.ComandaDAO();
            int comandaId = dao.AbrirComanda(mesaId, cliente);
            Console.WriteLine($"\n✔ Comanda aberta ID = {comandaId}");
        }

        static void AdicionarPedido()
        {
            int c = ConsoleUtils.LerInt("Comanda ID: ");
            int p = ConsoleUtils.LerInt("Produto ID: ");
            int q = ConsoleUtils.LerInt("Quantidade: ");

            Console.Write("Observação: ");
            string obs = Console.ReadLine();

            DAO.PedidoDAO.AdicionarPedido(c, p, q, obs);

            Console.WriteLine("\n✔ Pedido adicionado!");
        }

        static void ListarPedidos()
        {
            int c = ConsoleUtils.LerInt("Comanda ID: ");
            DAO.PedidoDAO.ListarPedidos(c);
        }
    }
}
