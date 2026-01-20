using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFichaTecnica
{
    internal class Receita
    {
        // Nome da receita (ex: "Brigadeiro de Festa")
        public string Nome {  get; set; }

        // A LISTA MÁGICA
        // Aqui dizemos: "Esta propriedade guarda uma LISTA de ItemReceita"
        // O "new List..." no final serve para criar a lista vazia assim que a receita nasce.
        public List<ItemReceita> ListaDeItens { get; set; } = new List<ItemReceita>();

        // MÉTODO: Calcular Custo Total
        public decimal CalcularCustoTotal()
        {
            decimal total = 0;

            //O LAÇO DE REPETIÇÃO(FOREACH)
            // Tradução: "Para cada 'item' que existir na 'ListaDeItens'..."
            foreach (var item in ListaDeItens)
            {
                total += item.CalcularCustoDestaPorcao();
            }

            return total;

        }

    }
}
