using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Warehouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Factory[] factories = new Factory[] {new Factory(200,0), new Factory(100,1), new Factory(20,2)};
            Warehouse[] warehouse = new Warehouse[] {new Warehouse(75,0), new Warehouse(50,1) };
            
            Thread trWarehouse = new Thread(() =>
            {
                for(int i = 0; i < 100; i++)
                {
                    Random srand = new Random();
                    warehouse[0].OrderProduct(factories[srand.Next(0,3)]);
                }
            });
            Thread trWarehouseTwo = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Random srand = new Random();
                    warehouse[1].OrderProduct(factories[srand.Next(0, 3)]);
                }
            });

            Thread trSell = new Thread(() =>
            {
                Random srand = new Random();
                int selection = 0;
                while (warehouse[selection].IsWarehouseOpened())
                {
                    warehouse[selection].SellProduct();
                    selection = srand.Next(0,2);
                }
            });
            Thread trFacOne = new Thread(() =>
            {
                for (int i = 0; i < 175; i++)
                {
                    factories[0].CreateProduct(GetProductName(), GetPrice());
                }
            });
            Thread trFacTwo = new Thread(() =>
            {
                for (int i = 0; i < 150; i++)
                {
                    factories[1].CreateProduct(GetProductName(), GetPrice());
                }
            });
            Thread trFacThre = new Thread(() =>
            {
                for (int i = 0; i < 150; i++)
                {
                    factories[2].CreateProduct(GetProductName(), GetPrice());
                }
            });

            Product.PrintHeader();

            trFacOne.Start();
            trFacTwo.Start();
            trFacThre.Start();
            trWarehouse.Start();
            trWarehouseTwo.Start();
            trSell.Start();

            trFacOne.Join();
            trFacTwo.Join();
            trFacThre.Join();
            trWarehouse.Join();
            trWarehouseTwo.Join();
            trSell.Join();

            factories[0].Print();
            factories[1].Print();
            factories[2].Print();
            warehouse[0].Print();
            warehouse[1].Print();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static string GetProductName()
        {
            List<string> names = new List<string>() { "Handy", "TV", "Laptop", "Display", "Keyboard", "Mouse", "Headset", "Camera", "Router", "Printer" };
            Random srand = new Random();
            return names[srand.Next(0, names.Count)];
        }

        private static decimal GetPrice()
        {
            Random srand = new Random();
            return (decimal)(srand.NextDouble() * srand.Next(10, 150));
        }
    }

    public class CountTo100
    {
        int number = 0;
        bool reachedhundret = false;
        object lockobject = new object();

        public CountTo100()
        {
            Thread trOne = new Thread(() =>
            {
                for(int i = 0; i < 100; i++)
                {
                    NextNumber();
                }
            });
            Thread trTwo = new Thread(() =>
            {
                for(int j = 0; j < 100; j++)
                {
                    NextNumber();
                }
            });

            trOne.Start();
            trTwo.Start();

            trOne.Join();
            trTwo.Join();
        }

        void NextNumber()
        {
            lock (lockobject)
            {
                if (number >= 100) reachedhundret = true;
                if (!reachedhundret)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": " + number);
                    number++;
                }
                else
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": " + number);
                    number--;
                }
                if(number == 0) Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ": " + number);
            }
        }
    }
}
