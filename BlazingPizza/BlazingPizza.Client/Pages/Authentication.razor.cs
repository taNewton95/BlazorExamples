using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Authentication
    {

        [Inject]
        public OrderState OrderState { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Action { get; set; }

        public PizzaAuthenticationState RemoteAuthenticationState { get; set; } = new PizzaAuthenticationState();

        protected override void OnInitialized()
        {
            if (RemoteAuthenticationActions.IsAction(RemoteAuthenticationActions.LogIn, Action))
            {
                // Preserve the current order so that we don't loose it
                RemoteAuthenticationState.Order = OrderState.order;
            }
        }

        private void RestorePizza(PizzaAuthenticationState pizzaState)
        {
            if (pizzaState.Order != null)
            {
                OrderState.ReplaceOrder(pizzaState.Order);
            }
        }

    }
}
