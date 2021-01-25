using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class ConfigurePizzaDialog
    {

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Parameter]
        public Pizza Pizza { get; set; }

        List<Topping> toppings;

        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public EventCallback OnConfirm { get; set; }

        protected async override Task OnInitializedAsync()
        {
            toppings = await HttpClient.GetFromJsonAsync<List<Topping>>("toppings");
        }

        void ToppingSelected(ChangeEventArgs e)
        {
            if (int.TryParse((string)e.Value, out var index) && index >= 0)
            {
                AddTopping(toppings[index]);
            }
        }

        void AddTopping(Topping topping)
        {
            if (Pizza.Toppings.Find(pt => pt.Topping == topping) == null)
            {
                Pizza.Toppings.Add(new PizzaTopping() { Topping = topping });
            }
        }

        void RemoveTopping(Topping topping)
        {
            Pizza.Toppings.RemoveAll(pt => pt.Topping == topping);
        }

    }
}
