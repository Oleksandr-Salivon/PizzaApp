using PizzaApp.Modelpizza;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaOrderingSystem
{
    class Program
    {
        readonly DbPizzaProjectContext context;
        string userID;
        readonly Dictionary<int, List<Topping>> toppingDetails;
        readonly List<PizzaName> pizzaDetails;

        public Program()
        {
            context = new DbPizzaProjectContext();
            toppingDetails = new Dictionary<int, List<Topping>>();

            pizzaDetails = new List<PizzaName>();
        }

        public static int GetNumber()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Invalid entry. Please enter again");
            }
            return num;
        }
        public void Menu()
        {

            int userChoice;
            do
            {
                MenuList();
                userChoice = GetNumber();
                switch (userChoice)
                {

                    case 1:
                        Register();

                        break;
                    case 2:
                        UserLogin();
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            } while (!(userChoice == 1 || userChoice == 2));
        }

        public void MenuList()
        {

            Console.WriteLine("1 REGISTER");
            Console.WriteLine("2 LOGIN");
        }

        List<User> GetUserInfo(string userEmail)
        {
            List<User> users;
            try
            {
                users = context.Users.Where(a => a.Email == userEmail).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong Email");
                throw;
            }

            return users;


        }

        public void Register()
        {
            bool flag;
            User userDetail = new();

            Console.WriteLine("Enter Your Mail Id");
            userDetail.Email = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Your Password");
            userDetail.Password = Console.ReadLine();

            Console.WriteLine("Enter Your Name");
            userDetail.Name = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Your Address");
            userDetail.Adress = Console.ReadLine();

            Console.WriteLine("Enter Your Contact Number");
            userDetail.Phone = Convert.ToString(GetNumber());
            context.Add(userDetail);
            try
            {

                context.SaveChanges();
                flag = true;

            }
            catch (Exception)
            {

                Console.WriteLine("Your Id alredy registered...");
                flag = false;
            }

            if (flag)
                Console.WriteLine($"Hello {userDetail.Name} yor are succefully registered.... ");
            Console.WriteLine("You can shop");
            Menu();

        }

        User CheckUserEmail()
        {
            List<User> userDetail = new();
            string userName, Password;
            do
            {
                Console.WriteLine("Enter Valid Mail Id");
                userName = Console.ReadLine().ToUpper();
                userDetail = GetUserInfo(userName);

            } while (userDetail.Count == 0);

            do
            {
                Console.WriteLine("Enter Valid Password");
                Password = Console.ReadLine();

            } while ((userDetail[0].Password != Password));
            return userDetail[0];
        }

        List<User> GetUser(string email, string password)
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

        void PrintPizzaDetails(PizzaName item)
        {

            Console.WriteLine("Pizza ID: " + item.PizzaId);
            Console.WriteLine("Pizza Name: " + item.Name);
            Console.WriteLine("Pizza Price: " + item.Price);
            Console.WriteLine("Pizza Type: " + item.Type);


        }

        public void GetPizzaDetails()
        {

            foreach (var item in context.PizzaNames)
            {
                PrintPizzaDetails(item);
            }
        }

        PizzaName GetPizzaDetailsById(int pizzaId)
        {
            var detail = context.PizzaNames.Find(pizzaId);
            return detail;


        }

        void PrintToppingDetails(Topping item)
        {


            Console.WriteLine("Topping ID: " + item.ToppingId);
            Console.WriteLine("Topping Name: " + item.Name);
            Console.WriteLine("Topping Price: " + item.Price);


        }

        void GetToppingDetails()
        {
            foreach (var item in context.Toppings)
            {
                PrintToppingDetails(item);
            }

        }

        Topping GetToppingDetailsById(int toppingId)
        {
            var detail = context.Toppings.Find(toppingId);
            return detail;


        }

        Topping AddTopping()
        {
            int userToppingId;
            Topping ToppingDetail;
            GetToppingDetails();
            do
            {
                Console.WriteLine("Enter the ToppingId ");
                userToppingId = GetNumber();
                ToppingDetail = GetToppingDetailsById(userToppingId);
                if (ToppingDetail == null)
                {
                    Console.WriteLine("please enter valid ToppingId");
                    continue;
                }
                else
                    break;
            } while (true);
            TotalBill.totalBill += Convert.ToDouble(ToppingDetail.Price);
            Console.WriteLine($"You have selected {ToppingDetail.Name} for ${ToppingDetail.Price}");
            Console.WriteLine("Your total bill " + TotalBill.totalBill);

            return ToppingDetail;
        }

        List<Topping> PizzaTopping()
        {

            string userChoiceForTopping;
            List<Topping> userTopingDetails = new List<Topping>();
            do
            {
                Console.WriteLine("Do u want extra toppings?y/n ");
                userChoiceForTopping = Console.ReadLine().ToLower();
                switch (userChoiceForTopping)
                {
                    case "y":
                        userTopingDetails.Add(AddTopping());
                        break;
                    case "n":
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            } while (userChoiceForTopping == "y" || userChoiceForTopping != "n");


            return userTopingDetails;

        }

        void PizzaOrders(int pizzaCount)
        {
            int userPizzaId;
            PizzaName userPizzaDetails;

            Console.WriteLine("------Pizza Details------");
            GetPizzaDetails();

            do
            {
                Console.WriteLine("Enter the Pizza of your choice");
                userPizzaId = GetNumber();
                userPizzaDetails = GetPizzaDetailsById(userPizzaId);
                if (userPizzaDetails == null)
                {
                    Console.WriteLine("please enter valid pizzaId");
                    continue;
                }
                else
                    break;
            } while (true);
            TotalBill.totalBill += Convert.ToDouble(userPizzaDetails.Price);
            pizzaDetails.Add(userPizzaDetails);

            Console.WriteLine($"You have selected {userPizzaDetails.Name} for ${userPizzaDetails.Price}");
            Console.WriteLine("Your total bill " + TotalBill.totalBill);
            try
            {
                toppingDetails.Add(pizzaCount, PizzaTopping());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public void PizzaManyOrders()
        {
            int pizza_numbers = 0;
            TotalBill.totalBill = 0;
            string userOrderChoice;
            do
            {
                Console.WriteLine("Do you want to select  pizza for this order?y/n");
                userOrderChoice = Console.ReadLine().ToLower();
                switch (userOrderChoice)
                {
                    case "y":

                        pizza_numbers++;
                        PizzaOrders(pizza_numbers);

                        break;
                    case "n":
                        return;
                    default:
                        Console.WriteLine("Invalid choice....");
                        break;
                }

            } while (userOrderChoice == "y" || userOrderChoice != "n");
        }


        int UpdateOrders(string userId, double totalprice, string orderStatus)
        {
            Order order = new Order();
            order.UserId = userId;
            if (totalprice < 25)
                order.Delivercharge = "5";
            else
                order.Delivercharge = "0";

            order.TotalPrice = totalprice;
            if (orderStatus == "y")
                order.Status = "Sucess";
            else
                order.Status = "Fail";
            Console.WriteLine("Delivery charges is " + order.Delivercharge);
            context.Orders.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return order.OrderId;

        }

        int UpdateOrderDetails(int pizzaId, int orderId)
        {
            OrdersDetail orderDetail = new OrdersDetail();
            orderDetail.PizzaNumber = pizzaId;
            orderDetail.OrderId = orderId;
            context.OrdersDetails.Add(orderDetail);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return orderDetail.OrdersDetailsId;
        }

        void UpdateOrderNumberDetails(int itemId, int toppingId)
        {
            OrdersNumberDetail numberdetails = new OrdersNumberDetail();
            numberdetails.OrdersNumberDetailsId = itemId;
            numberdetails.TopppingId = toppingId;
            context.OrdersNumberDetails.Add(numberdetails);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }


        }

        public void UserLogin()
        {
            User userDetail = CheckUserEmail();
            Console.WriteLine("Welcome to Pizza Ordering System");

            PizzaManyOrders();
            Console.WriteLine("Total bill is " + TotalBill.totalBill);
            if (TotalBill.totalBill > 0)
            {
                int i = 0;
                foreach (var item in pizzaDetails)
                {
                    Console.WriteLine("----------Pizza Details-----------");
                    PrintPizzaDetails(item);

                    Console.WriteLine("----------Topping Details-----------");

                    foreach (var items in toppingDetails.ElementAt(i).Value)
                    {
                        PrintToppingDetails(items);


                    }
                    i++;

                }
                string userOrderStatus;
                do
                {
                    Console.WriteLine("Do you want to place order y/n");
                    userOrderStatus = Console.ReadLine().ToLower();
                } while (!(userOrderStatus == "y" || userOrderStatus == "n"));

                int orderId = UpdateOrders(userDetail.Email, TotalBill.totalBill, userOrderStatus);

                int itemId;
                i = 0;
                foreach (var item in pizzaDetails)
                {

                    itemId = UpdateOrderDetails(item.PizzaId, orderId);

                    foreach (var items in toppingDetails.ElementAt(i).Value)
                    {

                        UpdateOrderNumberDetails(itemId, items.ToppingId);

                    }
                    i++;

                }
                if (userOrderStatus == "y")
                {
                    Console.WriteLine("Pay on delivery time");
                    Console.WriteLine("----Your Delivery Address----");
                    Console.WriteLine(userDetail.Adress);
                }

            }

            Console.WriteLine("Thank you for visiting");
        }


        static class TotalBill
        {
            public static double totalBill;
        }




        public static void Main(string[] args)
        {
            Program program = new();
            program.Menu();
        }

    }
}

