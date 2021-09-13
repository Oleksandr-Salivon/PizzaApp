using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public double? TotalPrice { get; set; }
        public string Delivercharge { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
