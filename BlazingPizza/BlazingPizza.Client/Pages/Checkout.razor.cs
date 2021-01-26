using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Checkout
    {

        [Inject]
        public OrdersClient OrdersClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public OrderState OrderState { get; set; }

        async Task PlaceOrder()
        {
            OrderState.isSubmitting = true;
            try
            {
                var newOrderId = await OrdersClient.PlaceOrder(OrderState.order);
                OrderState.ResetOrder();
                NavigationManager.NavigateTo($"myorders/{newOrderId}");
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
            OrderState.isSubmitting = false;
        }

    }
}
