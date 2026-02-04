using Microsoft.Maui.Controls;
using System;

namespace FichaTecnicaTelas
{
    public partial class MainPage : ContentPage
    {
        Receita minhaReceita = new Receita();

        public MainPage()
        {
            InitializeComponent();
            minhaReceita.Nome = "Nova Receita";
        }

        private void OnAdicionarClicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar Nome
                string nome = txtNomeIngrediente.Text;
                if (string.IsNullOrEmpty(nome)) return;

                // 2. Pegar números da tela
                decimal precoPacote = Convert.ToDecimal(txtPreco.Text);
                double tamanhoPacote = Convert.ToDouble(txtPesoEmbalagem.Text); // Ex: 1000g ou 12 ovos
                double quantidadeUsada = Convert.ToDouble(txtUsoReceita.Text);  // Ex: 200g ou 3 ovos

                // 3. Criar os objetos (A mágica acontece aqui)
                Ingrediente novoIngrediente = new Ingrediente(nome, precoPacote, tamanhoPacote);
                ItemReceita item = new ItemReceita(novoIngrediente, quantidadeUsada);

                // 4. Adicionar na memória
                minhaReceita.ListaDeItens.Add(item);

                // 5. Atualizar texto na tela
                // Mostra o custo daquele item específico
                decimal custoDoItem = item.CalcularCustoDoItem();
                lblLista.Text += $"• {nome}: Usou {quantidadeUsada} (Custo: R$ {custoDoItem:F2})\n";

                lblResumo.Text = $"💰 Custo Total da Receita: R$ {minhaReceita.CalcularCustoTotal():F2}";

                // 6. Limpar campos
                txtNomeIngrediente.Text = "";
                txtPreco.Text = "";
                txtPesoEmbalagem.Text = "";
                txtUsoReceita.Text = "";
                txtNomeIngrediente.Focus();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", "Verifique se digitou apenas números nos campos de valor.", "OK");
            }
        }
    }
}