using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        [Inject]
        public OrderState OrderState { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        List<PizzaSpecial> specials;        

        protected override async Task OnInitializedAsync()
        {
            specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }

        async Task RemovePizza(Pizza configuredPizza)
        {
            if (await JS.Confirm($"Remove {configuredPizza.Special.Name} pizza from the order?"))
            {
                OrderState.RemoveConfiguredPizza(configuredPizza);
            }
        }

    }
}
