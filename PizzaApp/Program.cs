using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaApp
{
    class Program
    {
        readonly DbPizzaProjectContext context;
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

        List<User> GetUser(string email , string password)
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
            List<PizzaName> getPizzasList = context.PizzaNames.ToList();
            foreach (var item in getPizzasList)
            {
                Console.WriteLine(item.PizzaId);
            }
            
            return getPizzasList;
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
