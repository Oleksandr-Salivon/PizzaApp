using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaApp
{
    class Program
    {
        readonly DbPizzaProjectContext context;
        double price = 0;
        string userID;
        public Program()
        {
            context = new DbPizzaProjectContext();
        }

        void PrintMenu()
        {
            string iChoice;
            do
            {
                PrintMenuSelectList();
                iChoice = Console.ReadLine();
                switch (iChoice)
                {
                    case "1":
                        Console.WriteLine("Enter your login.");
                        Login();
                        PrintPizza();
                        

                        break;
                    case "2":
                        Console.WriteLine("Register please.");
                        Register();
                        break;

                    default:
                        Console.WriteLine("Wrong choice. Please try again");
                        Console.WriteLine("------------------------------");
                        break;
                }
            } while (iChoice != "3");

        }
        static void PrintMenuSelectList()
        {
            Console.WriteLine("   1) Login");
            Console.WriteLine("   2) Register");
            Console.WriteLine("   3) Exit");

            Console.WriteLine("   --------------------");
            Console.WriteLine();

        }
        void PrintSelectinMenu()
        {
            string iChoice;
            do
            {
                SelectinMenu();
                iChoice = Console.ReadLine();
                switch (iChoice)
                {
                    case "1":
                        PrintPizza();
                        GetPizzasList();
                        break;
                    case "2":
                        PrintTopping();
                        GetToppingsList();
                        break;

                    default:
                        Console.WriteLine("Wrong choice. Please try again");
                        Console.WriteLine("------------------------------");
                        break;
                }
            } while (iChoice != "3");

        }
        static void SelectinMenu()
        {
            Console.WriteLine("   1) Select pizza");
            Console.WriteLine("   2) Select toppings");
            Console.WriteLine("   3) Exit");

            Console.WriteLine("   --------------------");
            Console.WriteLine();

        }
        List<User> GetUser(string email , string password)
        {
            List<User> getUser = context.Users.Where(a => a.Email == email && a.Password == password).ToList();
            if (getUser.Count == 1)
            {
                Console.WriteLine("Login successful\n");
            }
            else
            {
                Console.WriteLine("Your email or pasword wrong\n");
            }
            userID = getUser[0].Email;
            return getUser;
        }

        
        bool CheckUserEmail(string email)
        {
            List<User> getUser = context.Users.Where(a => a.Email == email).ToList();
            if (getUser.Count >= 1)
            {
                Console.WriteLine("This Email already exist. Please try another or use your email and password");
                return false;
            }

            return true;
        }

        List<PizzaName> GetPizzasList()
        {
            Console.WriteLine("\nPlease Enter the pizza Id that you want\n");
            int id = GetNumber();
            List<PizzaName> getPizzasList = context.PizzaNames.Where(a=>a.PizzaId==id).ToList();

            if (getPizzasList.Count == 1)
            {
                price += Convert.ToDouble(getPizzasList[0].Price);
                Console.WriteLine($"you have select {getPizzasList[0].Name} for price {price}$");
                
            }
            else
            {
                Console.WriteLine("Wrong choose, try again");
                GetPizzasList();
            }
            Console.WriteLine("Do u want extra toppings?y/n\n");

            while (Console.ReadLine().ToLower() == "y")
            {
                PrintTopping();
                GetToppingsList();
                Console.WriteLine("Do you want one more topping? y/n\n");
            }
            
            Console.WriteLine("Do you want one more pizza? y/n\n");

            while (Console.ReadLine().ToLower() == "y")
            {
                GetPizzasList();

            }
            Console.WriteLine($"you have select pizzas and toppings for price {price}$");
            AddOrder(userID, price);

            return getPizzasList;
        }

        List<Topping> GetToppingsList()
        {
            Console.WriteLine("\nPlease Enter the topping Id that you want\n");
            int id = GetNumber();
            Console.WriteLine(price);
            List<Topping> getToppingList = context.Toppings.Where(a => a.ToppingId == id).ToList();

            if (getToppingList.Count == 1)
            {
                price += Convert.ToDouble(getToppingList[0].Price);
                Console.WriteLine($"you have select {getToppingList[0].Name} for price {price}$");

            }
            else
            {
                Console.WriteLine("Wrong choose, try again");
                GetPizzasList();
            }
            Console.WriteLine(price);
            return getToppingList;
        }
        void PrintTopping()
        {
            foreach (var item in context.Toppings)
            {
                Console.WriteLine("Pizza ID: " + item.ToppingId);
                Console.WriteLine("-------------------Topping Name: " + item.Name);
                Console.WriteLine("-------------------Topping Price: " + item.Price);

            }
        }
        void PrintPizza()
        {
           
            foreach (var item in context.PizzaNames)
            {
                Console.WriteLine("Pizza ID: " + item.PizzaId);
                Console.WriteLine("-------------------Pizza Name: " + item.Name);
                Console.WriteLine("-------------------Pizza Price: " + item.Price);
                Console.WriteLine("-------------------Pizza Type: " + item.Type);

            }
            GetPizzasList();
        }

        
            void Login()
                    {
                        User user = new();
                        string email, password;
                        Console.WriteLine("Type your email");
                        email = Console.ReadLine();
                        Console.WriteLine("Type your pasword");
                        password = Console.ReadLine();
                        Console.WriteLine(user.Email);
                        List<User> getUser = new();
                        getUser = GetUser(email, password);

                        Console.WriteLine($"Hello {getUser[0].Name} choose your pizza, please \n");

                    }

        
        void Register()
        {
            User user = new();
            Console.WriteLine("Type your email");
            string email;
            do
            {
                email = Console.ReadLine();
                user.Email = email;
            } while (!CheckUserEmail(email));
            Console.WriteLine("Type your pasword");
            user.Password = Console.ReadLine();
            Console.WriteLine("Type your user name");
            user.Name = Console.ReadLine();
            Console.WriteLine("Type your adress");
            user.Adress = Console.ReadLine(); 
            Console.WriteLine("Type your phone");
            user.Phone = Console.ReadLine();
            context.Add(user);
            context.SaveChanges();
            Console.WriteLine("Your registration completed");
        }

        void AddOrder(string user_id, double totalPrice)
        {
            Order order = new();
            double delivercharge = 0;
            if (totalPrice>25)
            {
                delivercharge = 0;
            } else
            {
                delivercharge = 5;
            }
            order.UserId = user_id;
            order.TotalPrice = totalPrice + delivercharge;
            order.Delivercharge = delivercharge;
            context.Add(order);
            context.SaveChanges();
            Console.WriteLine("Order added");
        }

        void AddOrderDetails(int pizza_number, int order_id)
        {
            OrdersDetail ordersDetail = new();

            ordersDetail.OrderId = order_id;
            ordersDetail.PizzaNumber = pizza_number;
            context.Add(ordersDetail);
            context.SaveChanges();
            Console.WriteLine("OrderDetails added");
        }

        public static int GetNumber()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("It's not a number. Please try again");
            }
            Console.WriteLine();
            return num;
        }

        static void Main()
        {
            Console.WriteLine("Hello World!");
            new Program().PrintMenu();
        }
    }
}
