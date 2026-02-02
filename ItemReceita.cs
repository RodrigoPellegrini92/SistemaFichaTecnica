namespace FichaTecnicaTelas
{
    public class ItemReceita
    {
        public Ingrediente IngredienteUsado { get; set; }
        public double QuantidadeUsada { get; set; }

        public ItemReceita(Ingrediente ingrediente, double quantidade)
        {
            IngredienteUsado = ingrediente;
            QuantidadeUsada = quantidade;
        }

        public decimal CalcularCustoDoItem()
        {
            return IngredienteUsado.CalcularPrecoPorUnidade() * (decimal)QuantidadeUsada;
        }
    }
}