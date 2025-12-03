namespace Forms_LLCART_Projeto.Models
{
    public class Produto //classe produto que vai ser usada posteriormente para atribuição e coleta de dados (get;set) 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int Estoque { get; set; }
        public string SetorPreparo { get; set; } 
    }
}