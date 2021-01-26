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
    public partial class MyOrders
    {

        [Inject]
        public OrdersClient OrdersClient { get; set; }

        IEnumerable<OrderWithStatus> ordersWithStatus;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                ordersWithStatus = await OrdersClient.GetOrders();
            }
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }
        }

    }
}
