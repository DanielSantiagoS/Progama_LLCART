namespace Forms_LLCART_Projeto.Models
{
    public class Mesa //classe mesa que vai ser usada posteriormente para atribuição e coleta de dados (get;set) 
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int Capacidade { get; set; }
        public StatusMesa Status { get; set; }
        public string ComandaAtual { get; set; }
    }

    public enum StatusMesa //estado de uma variavel exemplo: coletou o dado e deu igual a 1, 1 = livre, ou 2 = ocupada
    {                      // o enum serve para indicar um "estado" de uma variavel
        Livre,
        Ocupada,
        Reservada
    }
}