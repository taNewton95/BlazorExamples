using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class ConfiguredPizzaItem
    {

        [Parameter] public Pizza Pizza { get; set; }
        [Parameter] public EventCallback OnRemoved { get; set; }

    }
}
