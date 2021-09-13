using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Models
{
    public partial class OrdersDetail
    {
        public int OrdersDetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaNumber { get; set; }

        public virtual Order Order { get; set; }
        public virtual PizzaName PizzaNumberNavigation { get; set; }
    }
}
