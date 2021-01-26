using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class OrderState
    {

        public Pizza configuringPizza;
        public bool showingConfigureDialog;
        public Order order = new Order();
        public bool isSubmitting;

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
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

        public void ResetOrder()
        {
            order = new Order();
        }

        public void ConfirmConfigurePizzaDialog()
        {
            order.Pizzas.Add(configuringPizza);
            configuringPizza = null;

            showingConfigureDialog = false;
        }

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            order.Pizzas.Remove(pizza);
        }

        public void ReplaceOrder(Order order)
        {
            this.order = order;
        }

    }
}
