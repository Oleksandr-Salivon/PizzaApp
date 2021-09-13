using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Models
{
    public partial class Topping
    {
        public int ToppingId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
