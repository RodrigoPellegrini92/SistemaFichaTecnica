namespace FichaTecnicaTelas
{
    public class Ingrediente
    {
        public string Nome { get; set; }
        public decimal PrecoEmbalagem { get; set; }
        public double PesoTotalEmbalagem { get; set; }

        public Ingrediente(string nome, decimal preco, double peso)
        {
            Nome = nome;
            PrecoEmbalagem = preco;
            PesoTotalEmbalagem = peso;
        }

        public decimal CalcularPrecoPorUnidade()
        {
            if (PesoTotalEmbalagem == 0) return 0;
            return PrecoEmbalagem / (decimal)PesoTotalEmbalagem;
        }
    }
}