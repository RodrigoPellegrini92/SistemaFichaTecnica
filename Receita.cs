using System.Collections.Generic;

namespace ProjetoFichaTecnica
{
    public class Receita
    {
        public string Nome { get; set; }
        public List<ItemReceita> ListaDeItens { get; set; }

        // Propriedades de Custo
        public decimal TempoDePreparo { get; set; }
        public decimal ValorDaMinhaHora { get; set; }
        public decimal GastosDiversos { get; set; }

        // --- NOVIDADE AQUI ---
        public int Rendimento { get; set; } // Ex: Rende 20 brigadeiros

        public Receita()
        {
            ListaDeItens = new List<ItemReceita>();
        }

        public decimal CalcularCustoTotal()
        {
            decimal custoIngredientes = 0;
            foreach (var item in ListaDeItens)
            {
                custoIngredientes += item.CalcularCustoDestaPorcao();
            }

            decimal custoMaoDeObra = TempoDePreparo * ValorDaMinhaHora;

            return custoIngredientes + custoMaoDeObra + GastosDiversos;
        }
    }
}