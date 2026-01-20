using System;
using ProjetoFichaTecnica;

// 1. CADASTRO DE INGREDIENTES (O que tenho no armário)
Ingrediente leiteMoca = new Ingrediente("Leite Condensado", 8.00m, 395);
Ingrediente cacau = new Ingrediente("Chocolate em Pó", 15.00m, 200);
Ingrediente manteiga = new Ingrediente("Manteiga Aviação", 25.00m, 500);

// 2. CRIAÇÃO DA RECEITA (A classe que você acabou de consertar os parênteses!)
Receita receitaBrigadeiro = new Receita();
receitaBrigadeiro.Nome = "Brigadeiro Gourmet";

// 3. ADICIONANDO ITENS À LISTA
// Item 1: 1 lata de leite (395g)
receitaBrigadeiro.ListaDeItens.Add(new ItemReceita(leiteMoca, 395));

// Item 2: 60g de cacau
receitaBrigadeiro.ListaDeItens.Add(new ItemReceita(cacau, 60));

// Item 3: 20g de manteiga
receitaBrigadeiro.ListaDeItens.Add(new ItemReceita(manteiga, 20));

// 4. IMPRIMINDO O RESULTADO
Console.WriteLine($"--- CUSTO DA RECEITA: {receitaBrigadeiro.Nome} ---");

// Aqui o método CalcularCustoTotal percorre a lista e soma tudo
decimal custoTotal = receitaBrigadeiro.CalcularCustoTotal();

Console.WriteLine($"Custo total dos ingredientes: R$ {custoTotal.ToString("F2")}");
Console.WriteLine($"Preço de Venda Sugerido (100%): R$ {(custoTotal * 2).ToString("F2")}");

Console.ReadLine();