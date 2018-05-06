using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace KadirApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Food")]
    public class FoodController : Controller
    {
        static List<String> services = new List<string>();
        static List<Order> orders = new List<Order>();

        static List<Item> Foods = new List<Item>(new []{new Item("Default Food",1.00)});
        static List<Item> Drinks = new List<Item>(new[] { new Item("Default Drink", 1.00) });
        const int MaxHistory = 30;

        // GET: api/Food
        [HttpGet]
        public string Get()
        {
            return "Invalid endpoint";
        }

        // GET: api/Food/5
        [HttpGet("{command}", Name = "Get")]
        public string Get(string command)
        {
            Console.WriteLine($"Resuest: {command}");
            switch (command)
            {
                case "FoodList":
                    return GetItemNames(Foods);
                case "DrinkList":
                    return GetItemNames(Drinks);
                case "FoodPrice":
                    return GetItemPrices(Foods);
                case "DrinkPrice":
                    return GetItemPrices(Drinks);
                case "AllFoods":
                    return WebUtility.HtmlEncode(JsonConvert.SerializeObject(Foods));
                case "AllDrinks":
                    return WebUtility.HtmlEncode(JsonConvert.SerializeObject(Drinks));
                case "AllServices":
                    return WebUtility.HtmlEncode(JsonConvert.SerializeObject(services));
                case "AllOrders":
                    return WebUtility.HtmlEncode(JsonConvert.SerializeObject(orders));
                case "ClearOrders":
                    orders = new List<Order>();
                    return "Cleared orders";
                case "ClearServices":
                    services = new List<string>();
                    return "Cleared services";
                case "Status":
                    return "Ok";
            }
            return "Invalid Link";
        }

        // POST: api/Food
        [HttpPost]
        public string Post([FromBody] dynamic value)
        {
            Console.WriteLine($"Value {value}");
            return $"Value {value}";
        }

        // PUT: api/Food/5
        [HttpPut("{command}")]
        public string Put(string command, [FromBody] dynamic value)
        {
            Console.WriteLine($"Command {command}, Value {value}");
            switch (command)
            {
                case "UpdateFoods":
                    Foods = JsonConvert.DeserializeObject<List<Item>>(WebUtility.HtmlDecode(value));
                    return $"Updated Food List. Foods item count {Foods.Count}";

                case "UpdateDrinks":
                    Drinks = JsonConvert.DeserializeObject<List<Item>>(WebUtility.HtmlDecode(value));
                    return $"Updated Drink List. Drinks item count {Drinks.Count}";

                case "MakeOrder":
                    orders.Add(Parseorder(value));
                    if (orders.Count > MaxHistory) orders.RemoveAt(0);
                    return $"Order received| {value} |Total order count {orders.Count}";

                case "service":
                    services.Add(value);
                    if (services.Count > MaxHistory) services.RemoveAt(0);
                    return $"Received order:{value}";
            }
            return "Invalid Link";
        }

        public string GetItemNames(List<Item> list)
        {
            string returnValue = "";
            foreach (var item in list)
            {
                returnValue += item.Name + ",";
            }
            returnValue = returnValue.Remove(returnValue.Length - 1);
            return returnValue;
        }
        public string GetItemPrices(List<Item> list)
        {
            string returnValue = "";
            foreach (var item in list)
            {
                returnValue += item.GetRm() + ",";
            }
            returnValue = returnValue.Remove(returnValue.Length - 1);
            return returnValue;
        }

        public Order Parseorder(string input)
        {
            var array = input.Split('|');
            var table = array[0];
            var order = array[1].Replace("(", "").Replace(")", "");
            var array2 = order.Remove(order.Length - 1).Split(',');
            List <Item> items = new List<Item>();
            foreach (var item in array2)
            {
               items.Add(GetItemByname(item));
            }

            return new Order(table,items,DateTime.Now);
        }

        public Item GetItemByname (string Name)
        {
            if (Name.StartsWith(" ")) Name = Name.Remove(0, 1);

            foreach (var food in Foods)
            {
                if (food.Name.Equals(Name)) return food;
            }
            foreach (var drink in Drinks)
            {
                if (drink.Name.Equals(Name)) return drink;
            }
            return new Item("null", 0.00);
        }

    }

    public class Item
    {
        public string Name;
        public double Price;

        public Item(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string GetRm()
        {
            return $"RM{Price:##.00}";
        }
    }

    public class Order
    {
        public string TableName;
        public List<Item> OrderItem;
        public DateTime date;

        public Order(string tbName, List<Item> items, DateTime dt)
        {
            TableName = tbName;
            OrderItem = items;
            date = dt;
        }
    }
}
