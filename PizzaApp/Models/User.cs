using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaApp.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
