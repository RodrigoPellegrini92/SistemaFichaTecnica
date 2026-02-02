using System.Collections.Generic;

namespace FichaTecnicaTelas
{
    public class Receita
    {
        public string Nome { get; set; }
        public List<ItemReceita> ListaDeItens { get; set; }

        public Receita()
        {
            ListaDeItens = new List<ItemReceita>();
        }

        public decimal CalcularCustoTotal()
        {
            decimal total = 0;
            foreach (var item in ListaDeItens)
            {
                total += item.CalcularCustoDoItem();
            }
            return total;
        }
    }
}