using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Modelpizza
{
    public partial class OrdersDetail
    {
        public OrdersDetail()
        {
            OrdersNumberDetails = new HashSet<OrdersNumberDetail>();
        }

        public int OrdersDetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaNumber { get; set; }

        public virtual Order Order { get; set; }
        public virtual PizzaName PizzaNumberNavigation { get; set; }
        public virtual ICollection<OrdersNumberDetail> OrdersNumberDetails { get; set; }
    }
}
