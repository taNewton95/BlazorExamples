﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class OrderDetails : IDisposable
    {

        [Inject]
        public OrdersClient OrdersClient { get; set; }

        [Parameter] public int OrderId { get; set; }

        OrderWithStatus orderWithStatus;
        bool invalidOrder;
        CancellationTokenSource pollingCancellationToken;

        protected override void OnParametersSet()
        {
            // If we were already polling for a different order, stop doing so
            pollingCancellationToken?.Cancel();

            // Start a new poll loop
            PollForUpdates();
        }

        private async void PollForUpdates()
        {
            pollingCancellationToken = new CancellationTokenSource();
            while (!pollingCancellationToken.IsCancellationRequested)
            {
                try
                {
                    invalidOrder = false;
                    orderWithStatus = await OrdersClient.GetOrder(OrderId);
                    StateHasChanged();

                    if (orderWithStatus.IsDelivered)
                    {
                        pollingCancellationToken.Cancel();
                    }
                    else
                    {
                        await Task.Delay(4000);
                    }
                }
                catch (AccessTokenNotAvailableException ex)
                {
                    pollingCancellationToken.Cancel();
                    ex.Redirect();
                }
                catch (Exception ex)
                {
                    invalidOrder = true;
                    pollingCancellationToken.Cancel();
                    Console.Error.WriteLine(ex);
                    StateHasChanged();
                }
            }
        }

        public void Dispose()
        {
            pollingCancellationToken?.Cancel();
        }

    }
}
