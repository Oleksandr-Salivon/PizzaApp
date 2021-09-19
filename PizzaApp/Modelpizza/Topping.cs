using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Modelpizza
{
    public partial class Topping
    {
        public Topping()
        {
            OrdersNumberDetails = new HashSet<OrdersNumberDetail>();
        }

        public int ToppingId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<OrdersNumberDetail> OrdersNumberDetails { get; set; }
    }
}
