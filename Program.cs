using System;
using System.IO;
using ProjetoFichaTecnica;

namespace ProjetoFichaTecnica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // LOOP PRINCIPAL (Permite fazer várias receitas sem fechar)
            do
            {
                Console.Clear();
                Titulo("=== CALCULADORA MASTER CHEF (V5.0 - COMPLETA) ===");

                Receita minhaReceita = new Receita();

                Console.Write("Nome da Receita: ");
                minhaReceita.Nome = Console.ReadLine();

                string continuar = "S";

                // --- 1. CADASTRO DOS INGREDIENTES ---
                while (continuar.ToUpper() == "S")
                {
                    Console.Clear();
                    Titulo($"Ingredientes de: {minhaReceita.Nome}");

                    Console.Write("Nome do Ingrediente: ");
                    string nome = Console.ReadLine();

                    decimal preco = LerDecimal("Preço no mercado (R$): ");
                    double pesoEmbalagem = LerDouble("Quanto vem na embalagem FECHADA? (g, ml ou qtd): ");

                    Console.WriteLine("\n[1] Gramas  [2] Ml  [3] Unidades");
                    Console.Write("Como você vai usar na receita? ");
                    string opcao = Console.ReadLine();

                    double quantidadeUsada = 0;

                    if (opcao == "3") // Unidade
                    {
                        double unidadesUsadas = LerDouble("Quantas UNIDADES você usou?: ");
                        quantidadeUsada = unidadesUsadas;
                    }
                    else // Gramas ou Ml
                    {
                        quantidadeUsada = LerDouble("Quantidade usada: ");
                    }

                    minhaReceita.ListaDeItens.Add(new ItemReceita(new Ingrediente(nome, preco, pesoEmbalagem), quantidadeUsada));

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\nAdicionar outro ingrediente? (S/N): ");
                    Console.ResetColor();
                    continuar = Console.ReadLine();
                }

                // --- 2. CUSTOS OPERACIONAIS ---
                Console.Clear();
                Titulo("--- CUSTOS OPERACIONAIS ---");

                decimal tempoEmMinutos = LerDecimal("Tempo total de preparo (em MINUTOS): ");
                minhaReceita.TempoDePreparo = tempoEmMinutos / 60m;

                minhaReceita.ValorDaMinhaHora = LerDecimal("Qual o valor da sua HORA de trabalho? (R$): ");
                minhaReceita.GastosDiversos = LerDecimal("Gasto estimado com Gás/Luz/Embalagem (R$): ");

                Console.WriteLine("--------------------------------------------");
                minhaReceita.Rendimento = LerInt("Essa receita rendeu quantas unidades/fatias? (1 p/ bolo inteiro): ");
                if (minhaReceita.Rendimento == 0) minhaReceita.Rendimento = 1;

                // --- 3. RELATÓRIO E CORES ---
                Console.Clear();
                decimal totalIngredientes = 0;
                string conteudoDoArquivo = ""; // Para salvar no TXT sem cores

                // Cabeçalho
                Titulo($"=== FICHA TÉCNICA: {minhaReceita.Nome.ToUpper()} ===");
                conteudoDoArquivo += $"=== FICHA TÉCNICA: {minhaReceita.Nome.ToUpper()} ===\n\n";

                Console.WriteLine("--- INGREDIENTES ---");
                conteudoDoArquivo += "--- INGREDIENTES ---\n";

                foreach (var item in minhaReceita.ListaDeItens)
                {
                    decimal custoItem = item.CalcularCustoDestaPorcao();
                    string linha = $"- {item.IngredienteUsado.Nome}: R$ {custoItem:F2}";

                    Console.WriteLine(linha);
                    conteudoDoArquivo += linha + "\n";
                    totalIngredientes += custoItem;
                }

                decimal custoTotal = minhaReceita.CalcularCustoTotal();
                decimal custoPorUnidade = custoTotal / minhaReceita.Rendimento;
                decimal custoMaoDeObra = minhaReceita.TempoDePreparo * minhaReceita.ValorDaMinhaHora;

                // Bloco de Custos (Vermelho)
                Console.WriteLine("--------------------------------------------");
                ImprimirCusto("TOTAL INGREDIENTES:", totalIngredientes);
                ImprimirCusto("MÃO DE OBRA:", custoMaoDeObra);
                ImprimirCusto("GASTOS EXTRAS:", minhaReceita.GastosDiversos);
                Console.WriteLine("--------------------------------------------");
                ImprimirCusto("CUSTO TOTAL DA RECEITA:", custoTotal);
                Console.WriteLine($"RENDIMENTO:             {minhaReceita.Rendimento} unidades");
                ImprimirCusto("CUSTO POR UNIDADE:", custoPorUnidade);
                Console.WriteLine("============================================");

                // Pergunta de Lucro
                Console.ResetColor();
                decimal margem = LerDecimal("\nMargem de Lucro desejada (%): ");

                decimal valorVendaTotal = custoTotal + (custoTotal * (margem / 100));
                decimal valorVendaUnitario = valorVendaTotal / minhaReceita.Rendimento;
                decimal lucroTotal = valorVendaTotal - custoTotal;

                // Bloco de Venda (Verde)
                Console.WriteLine();
                Titulo("$$$ SUGESTÃO DE PREÇOS $$$");
                ImprimirLucro("Vender TUDO por:", valorVendaTotal);
                ImprimirLucro("Vender a UNIDADE por:", valorVendaUnitario);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"VOCÊ VAI LUCRAR:        R$ {lucroTotal:F2}");
                Console.ResetColor();

                // Montar String final pro arquivo (Sem cores)
                string resumoFinal = $"\n============================================\n" +
                                     $"CUSTO INGREDIENTES:     R$ {totalIngredientes:F2}\n" +
                                     $"CUSTO MÃO DE OBRA:      R$ {custoMaoDeObra:F2}\n" +
                                     $"GASTOS EXTRAS:          R$ {minhaReceita.GastosDiversos:F2}\n" +
                                     $"--------------------------------------------\n" +
                                     $"CUSTO TOTAL DA RECEITA: R$ {custoTotal:F2}\n" +
                                     $"RENDIMENTO:             {minhaReceita.Rendimento} unidades\n" +
                                     $"CUSTO POR UNIDADE:      R$ {custoPorUnidade:F2}\n" +
                                     $"============================================\n" +
                                     $"$$$ PREÇO DE VENDA SUGERIDO ({margem}%) $$$\n" +
                                     $"Vender TUDO por:        R$ {valorVendaTotal:F2}\n" +
                                     $"Vender a UNIDADE por:   R$ {valorVendaUnitario:F2}\n" +
                                     $"LUCRO LIQUIDO:          R$ {lucroTotal:F2}\n";

                conteudoDoArquivo += resumoFinal;
                conteudoDoArquivo += $"\nGerado em: {DateTime.Now}";

                // Salvar Arquivo
                try
                {
                    string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string nomeArquivo = $"Ficha_{minhaReceita.Nome.Replace(" ", "_")}.txt";
                    File.WriteAllText(Path.Combine(caminhoDesktop, nomeArquivo), conteudoDoArquivo);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\n[SUCESSO] Arquivo salvo na Área de Trabalho: {nomeArquivo}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro ao salvar arquivo: " + ex.Message);
                    Console.ResetColor();
                }

                // PERGUNTA DO LOOP PRINCIPAL
                Console.WriteLine("\n------------------------------------------------");
                Console.Write("Deseja calcular OUTRA receita? (S/N): ");

            } while (Console.ReadLine().ToUpper() == "S");

            Console.WriteLine("\nObrigado por usar a Calculadora Master Chef! Tchau!");
            System.Threading.Thread.Sleep(2000);
        }

        // --- AQUI ESTAVAM FALTANDO AS FERRAMENTAS ---
        // Certifique-se de copiar tudo daqui para baixo!

        static void Titulo(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(texto);
            Console.ResetColor();
        }

        static void ImprimirCusto(string texto, decimal valor)
        {
            Console.Write(texto.PadRight(24));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"R$ {valor:F2}");
            Console.ResetColor();
        }

        static void ImprimirLucro(string texto, decimal valor)
        {
            Console.Write(texto.PadRight(24));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"R$ {valor:F2}");
            Console.ResetColor();
        }

        static decimal LerDecimal(string texto)
        {
            while (true)
            {
                Console.Write(texto);
                string entrada = Console.ReadLine();
                if (decimal.TryParse(entrada, out decimal valor)) return valor;
                MensagemErro();
            }
        }

        static double LerDouble(string texto)
        {
            while (true)
            {
                Console.Write(texto);
                if (double.TryParse(Console.ReadLine(), out double valor)) return valor;
                MensagemErro();
            }
        }

        static int LerInt(string texto)
        {
            while (true)
            {
                Console.Write(texto);
                if (int.TryParse(Console.ReadLine(), out int valor)) return valor;
                MensagemErro();
            }
        }

        static void MensagemErro()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(">>> ERRO: Valor inválido! Digite apenas números. <<<");
            Console.ResetColor();
        }
    }
}