using System;

namespace Forms_LLCART.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int Capacidade { get; set; }
        public StatusMesa Status { get; set; }
        public string ComandaAtual { get; set; }
    }

    public enum StatusMesa
    {
        Livre,
        Ocupada,
        Reservada
    }
}