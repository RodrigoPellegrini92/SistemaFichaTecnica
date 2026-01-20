using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFichaTecnica
{
    internal class ItemReceita
    {
        // PROPRIEDADES

        // Olha que legal: O tipo dessa propriedade não é int nem string...
        // É "Ingrediente"! Estamos colocando uma classe dentro da outra.
        public Ingrediente IngredienteUsado { get; set; }

        // Quanto vou usar na receita (ex: 50g)
        public double QuantidadeUsada { get; set; }

        // CONSTRUTOR
        public ItemReceita(Ingrediente ingrediente, double quantidade)
        {
            IngredienteUsado = ingrediente;
            QuantidadeUsada = quantidade;
        }

        // MÉTODO
        // Calcula quanto custa ESSA porção específica (ex: 50g)
        public decimal CalcularCustoDestaPorcao()
        {
            // 1. Descobre o preço de 1 unidade (usando o método da outra classe)
            decimal precoUnitario = IngredienteUsado.CalcularPrecoPorUnidade();

            // 2. Multiplica pela quantidade que estamos usando
            // (Lembre do cast (decimal) de novo!)
            return precoUnitario * (decimal)QuantidadeUsada;
        }
    }
}
