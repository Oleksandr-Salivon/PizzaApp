using Microsoft.Data.SqlClient;
using PizzaApplication.Modelpizza;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace PizzaApplication
{
    class Program
    {
        double price = 0;
        readonly DbPizzaProjectContext  context;
        
        public Program()
        {
            context = new DbPizzaProjectContext();
            
        }

        void PrintMenu()
        {
            string iChoice = "";
            do
            {
                PrintMenuSelectList();
                iChoice = Console.ReadLine();
                switch (iChoice)
                {
                    case "1":
                        Console.WriteLine("Enter your login.");
                        Login();
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
            } while (iChoice != "2");
            void PrintMenuSelectList()
            {
                Console.WriteLine("1)Login");
                Console.WriteLine("2)Register");
                Console.WriteLine("--------------------");
                Console.WriteLine();

            }

            List<User> GetUser(string email, string password)
            {
                List<User> getUser = context.Users.Where(a => a.Email == email && a.Password == password).ToList();
                if (getUser.Count == 1)
                {
                    Console.WriteLine("Login successful");
                }
                else
                {
                    Console.WriteLine("Your email or pasword wrong");
                }
                return getUser;
            }


            void Login()
            {
                User user = new ();
                string email, password;
                Console.WriteLine("Type your email");
                email = Console.ReadLine();
                Console.WriteLine("Type your pasword");
                password = Console.ReadLine();
                Console.WriteLine(user.Email);
                List<User> getUser = new();
                getUser = GetUser(email, password);


            }

            void Register()
            {
                User user = new ();
                Console.WriteLine("Type your email");
                user.Email = Console.ReadLine();
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
        }

            void PrintPizza()
            {
                foreach (var item in context.PizzaNames)
                {
                    Console.WriteLine("Pizza ID" +item.PizzaId);
                    Console.WriteLine("-------------------Pizza Name"+item.Name);
                    Console.WriteLine("-------------------Pizza Price"+item.Price);
                    Console.WriteLine("-------------------Pizza Type" + item.Type);

                }

                

            Console.WriteLine("Please Enter the pizza Id that you want");
            int id = Convert.ToInt32(Console.ReadLine());
            foreach ( var item in context.PizzaNames)

            {

                if (id==item.PizzaId)
                {
                    price = Convert.ToDouble(item.Price);
                    Console.WriteLine("you have select " +item.Name);
                    Console.WriteLine(price);

                }
            }


            Console.WriteLine("Need Extra Toppings(y/n)");
            string ans = Console.ReadLine();

            if (ans == "y")
            {
                foreach (var item in context.Toppings)

                {
                    Console.WriteLine("topping ID" + item.ToppingId);
                    Console.WriteLine(" Name" + item.Name);
                    Console.WriteLine(" price" + item.Price);

                }
                Console.WriteLine("Please Enter the Topping Id that you want");
                int T_id = Convert.ToInt32(Console.ReadLine());
                foreach (var item in context.Toppings)

                {

                    if (T_id==item.ToppingId)
                    {
                        price = price +Convert.ToDouble(item.Price);
                        Console.WriteLine("you have select " + item.Name);
                        Console.WriteLine(price);

                    }
                }

            }
            else
            {
                Console.WriteLine("You have selected no Extra toppings");
            }

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

       
        static void Main(string[] args)
        {
            Program program = new ();
           // program.PrintMenu();
            program.PrintPizza();
            
            
        }
    }
}
