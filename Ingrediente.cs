using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFichaTecnica
{
    public class Ingrediente
    {
        //Propriedades
        public string Nome {  get; set; }
        public decimal PrecoEmbalagem { get; set; }
        public double PesoTotalEmbalagem { get; set; }

        // CONSTRUTOR
        // É aqui que o objeto "nasce". Obrigamos a preencher os dados.
        public Ingrediente(string nome, decimal preco, double peso)
        {
            Nome = nome;
            PrecoEmbalagem = preco;
            PesoTotalEmbalagem = peso;
        }

        //Método (A INTELIGÊNCIA)
        public decimal CalcularPrecoPorUnidade()
        {
            return PrecoEmbalagem / (decimal)PesoTotalEmbalagem;
        }
    }
}
