using Microsoft.AspNetCore.Components;
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
        public HttpClient HttpClient { get; set; }

        IEnumerable<OrderWithStatus> ordersWithStatus;

        protected override async Task OnParametersSetAsync()
        {
            ordersWithStatus = await HttpClient.GetFromJsonAsync<List<OrderWithStatus>>("orders");
        }

    }
}
