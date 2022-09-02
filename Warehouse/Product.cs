using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Warehouse
{
    public enum Companies
    {
        LG, Samsung, Apple
    }
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Factory Origin { get; set; }

        public Product(string name, decimal price, Factory origin)
        {
            Name = name;
            Price = price;
            Origin = origin;

            PrintProduct("Produced ");
        }

        public static void PrintHeader()
        {
            Console.WriteLine("{0,10} | {1,12} | {2,11} | {3,7} | {4}", "Type","Product","Price", "Factory", "Thread");
            Console.WriteLine("-------------------------------------------------------------");
        }

        public void PrintProduct(string attached = "")
        {
            Console.WriteLine("{0,10} | {1,12} | {2,8}EUR | {3,7} | {4}",attached, Name, Math.Round(Price,2), (Companies)Origin.FactoryId, Thread.CurrentThread.ManagedThreadId);
            Console.ResetColor();
        }
    }
}
