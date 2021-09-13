using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Models
{
    public partial class OrdersNumberDetail
    {
        public int? OrdersNumberDetailsId { get; set; }
        public int? TopppingId { get; set; }

        public virtual OrdersDetail OrdersNumberDetails { get; set; }
        public virtual Topping Toppping { get; set; }
    }
}
