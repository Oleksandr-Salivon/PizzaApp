using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Modelpizza
{
    public partial class PizzaName
    {
        public PizzaName()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int PizzaId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Type { get; set; }

        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
