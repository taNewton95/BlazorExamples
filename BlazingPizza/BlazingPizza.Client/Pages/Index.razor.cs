using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Index
    {

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        List<PizzaSpecial> specials;
        Pizza configuringPizza;
        bool showingConfigureDialog;
        Order order = new Order();

        protected override async Task OnInitializedAsync()
        {
            specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }

        void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            configuringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>(),
            };

            showingConfigureDialog = true;
        }

        public void CancelConfigurePizzaDialog()
        {
            configuringPizza = null;
            showingConfigureDialog = false;
        }

        public void ConfirmConfigurePizzaDialog()
        {
            order.Pizzas.Add(configuringPizza);
            configuringPizza = null;

            showingConfigureDialog = false;
        }

        void RemoveConfiguredPizza(Pizza pizza)
        {
            order.Pizzas.Remove(pizza);
        }

        async Task PlaceOrder()
        {
            var response = await HttpClient.PostAsJsonAsync("orders", order);
            var newOrderId = await response.Content.ReadFromJsonAsync<int>();
            order = new Order();
            NavigationManager.NavigateTo($"myorders/{newOrderId}");
        }

    }
}
