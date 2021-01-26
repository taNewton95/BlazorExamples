using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class RedirectToLogin
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager.NavigateTo($"authentication/login?returnUrl={NavigationManager.Uri}");
        }

    }
}
