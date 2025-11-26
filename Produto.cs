namespace Forms_LLCART.Models
{
    public class Produto
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