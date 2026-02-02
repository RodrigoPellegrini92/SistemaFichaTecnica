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
                string nome = txtNomeIngrediente.Text;
                if (string.IsNullOrEmpty(nome)) return;

                decimal preco = Convert.ToDecimal(txtPreco.Text);
                double peso = Convert.ToDouble(txtPeso.Text);

                Ingrediente novoIngrediente = new Ingrediente(nome, preco, peso);
                ItemReceita item = new ItemReceita(novoIngrediente, peso); // Assume uso total por enquanto

                minhaReceita.ListaDeItens.Add(item);

                lblResumo.Text = $"✅ {nome} adicionado!\n" +
                                 $"Total de Itens: {minhaReceita.ListaDeItens.Count}\n" +
                                 $"💰 Custo Total: R$ {minhaReceita.CalcularCustoTotal():F2}";

                txtNomeIngrediente.Text = "";
                txtPreco.Text = "";
                txtPeso.Text = "";
                txtNomeIngrediente.Focus();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", "Verifique os números digitados.", "OK");
            }
        }
    }
}