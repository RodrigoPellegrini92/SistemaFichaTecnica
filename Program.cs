using System;
using ProjetoFichaTecnica;

namespace ProjetoFichaTecnica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- CALCULADORA DE CUSTO DE RECEITAS ---");

            Receita minhaReceita = new Receita();

            Console.Write("Qual o nome da receita? (ex: Bolo de Cenoura): ");
            minhaReceita.Nome = Console.ReadLine();

            string continuar = "S";

            // --- LOOP DE CADASTRO DE INGREDIENTES ---
            while (continuar.ToUpper() == "S")
            {
                Console.Clear();
                Console.WriteLine($"Adicionando ingrediente na receita: {minhaReceita.Nome}");
                Console.WriteLine("--------------------------------------------");

                // 1. DADOS DE COMPRA
                Console.Write("Nome do Ingrediente (ex: Leite, Ovos): ");
                string nome = Console.ReadLine();

                Console.Write("Preço pago no mercado (ex: 12,00): ");
                decimal preco = decimal.Parse(Console.ReadLine());

                // CORREÇÃO AQUI: Agora o texto deixa claro que aceita UNIDADES
                Console.Write("Quantidade TOTAL na embalagem (g, ml ou unidades): ");
                double pesoEmbalagem = double.Parse(Console.ReadLine());

                // 2. MENU DE ESCOLHA DA MEDIDA
                Console.WriteLine("\n--- COMO VOCÊ VAI USAR NA RECEITA? ---");
                Console.WriteLine("[1] Gramas (g)");
                Console.WriteLine("[2] Mililitros (ml)");
                Console.WriteLine("[3] Unidade (un)");
                Console.Write("Escolha uma opção (1, 2 ou 3): ");
                string opcao = Console.ReadLine();

                double quantidadeUsada = 0;

                // LÓGICA DE ESCOLHA
                if (opcao == "1") // Gramas
                {
                    Console.Write("Quantas GRAMAS você usou? ");
                    quantidadeUsada = double.Parse(Console.ReadLine());
                }
                else if (opcao == "2") // Mililitros (ML)
                {
                    Console.Write("Quantos MILILITROS (ml) você usou? ");
                    quantidadeUsada = double.Parse(Console.ReadLine());
                }
                else if (opcao == "3") // Unidade (O cálculo inteligente)
                {
                    Console.Write("Quantas UNIDADES você usou? (ex: 2): ");
                    double unidadesUsadas = double.Parse(Console.ReadLine());

                    // Aqui validamos quanto pesa (ou vale) cada unidade dentro do pacote total
                    Console.Write($"Quantas unidades vieram no pacote total de {pesoEmbalagem}? (ex: 12): ");
                    double totalUnidadesNoPacote = double.Parse(Console.ReadLine());

                    // Descobre o peso/fator de uma unidade
                    double pesoDeUmaUnidade = pesoEmbalagem / totalUnidadesNoPacote;
                    Console.WriteLine($"-> Calculado: Cada unidade equivale a {pesoDeUmaUnidade:F2} do total.");

                    quantidadeUsada = unidadesUsadas * pesoDeUmaUnidade;
                }
                else
                {
                    Console.WriteLine("Opção inválida! Considerando zero.");
                    quantidadeUsada = 0;
                }

                // 3. CRIAÇÃO E ADIÇÃO
                Ingrediente novoIngrediente = new Ingrediente(nome, preco, pesoEmbalagem);
                ItemReceita novoItem = new ItemReceita(novoIngrediente, quantidadeUsada);
                minhaReceita.ListaDeItens.Add(novoItem);

                // 4. CONTINUAR?
                Console.WriteLine("--------------------------------------------");
                Console.Write("Deseja adicionar outro ingrediente? (S/N): ");
                continuar = Console.ReadLine();
            }

            // --- RESULTADO FINAL DA FICHA TÉCNICA ---
            Console.Clear();
            Console.WriteLine($"=== FICHA TÉCNICA: {minhaReceita.Nome.ToUpper()} ===");

            foreach (var item in minhaReceita.ListaDeItens)
            {
                Console.WriteLine($"- {item.IngredienteUsado.Nome}: R$ {item.CalcularCustoDestaPorcao().ToString("F2")}");
            }

            Console.WriteLine("============================================");
            decimal custoTotal = minhaReceita.CalcularCustoTotal();
            Console.WriteLine($"CUSTO TOTAL (INGREDIENTES): R$ {custoTotal.ToString("F2")}");
            Console.WriteLine("============================================");

            // --- CALCULADORA DE LUCRO ---
            Console.WriteLine("\n--- DEFINIÇÃO DE PREÇO DE VENDA ---");

            Console.Write("Qual a margem de lucro desejada? (ex: Digite 50 para 50%): ");
            decimal porcentagemLucro = decimal.Parse(Console.ReadLine());

            // Fórmula: Lucro = Custo * (Porcentagem / 100)
            decimal valorDoLucro = custoTotal * (porcentagemLucro / 100);
            decimal precoFinal = custoTotal + valorDoLucro;

            Console.WriteLine("\n---------------- RESULTADO ----------------");
            Console.WriteLine($"Custo de Produção: R$ {custoTotal:F2}");
            Console.WriteLine($"Lucro ({porcentagemLucro}%):   R$ {valorDoLucro:F2}");
            Console.WriteLine($"PREÇO DE VENDA:    R$ {precoFinal:F2}");
            Console.WriteLine("-------------------------------------------");

            Console.ReadLine();
        }
    }
}