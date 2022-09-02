using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Warehouse
{
    public enum WarehouseCompanies
    {
        Amazon, Saturn
    }
    public class Warehouse
    {
        public object lockWarehouse { get; set; } = new object();
        private List<Product> Products { get; set; }
        private int Count { get; set; }
        public int AddedProducts { get; set; } = 0;
        public int TriesToAdd { get; set; } = 0;
        public int SoldProducts { get; set; } = 0;
        private int SellTries = 0;
        private int CompanyId { get; set; } = 0;


        public Warehouse(int count, int companyId)
        {
            Products = new List<Product>();
            Count = count;
            CompanyId = companyId;
        }

        public void OrderProduct(Factory factory)
        {
            lock (lockWarehouse)
            {
                if (Count <= Products.Count)
                {
                    TriesToAdd++;
                    return;
                }
                lock (factory.lockFactory)
                {
                    factory.FactoryToWarehouse(this);
                    return;
                }
            }
        }

        public bool HasSpace()
        {
            return Count > Products.Count;
        }

        public bool SellProduct()
        {
            if (Products.Count <= 0)
            {
                SellTries++;
                Thread.Sleep(30);
                return false;
            }
            Random srand = new Random();
            lock (lockWarehouse)
            {
                SellTries = 0;
                int random = srand.Next(0, Products.Count);
                Product product = Products[random];
                product.PrintProduct("Sold ");
                Products.RemoveAt(random);
                SoldProducts++;
            }
            return true;
        }

        public bool IsWarehouseOpened()
        {
            return !(SellTries >= 50); //return false after 50*75ms (2s)
        }

        public bool ReceiveProduct(Product product)
        {
            lock (lockWarehouse)
            {
                Products.Add(product);
                product.PrintProduct("Received ");
                return true;
            }
        }

        public void Print()
        {
            Console.WriteLine($"{(WarehouseCompanies)CompanyId} ordered {AddedProducts} product. Sold: {SoldProducts}.");
        }
    }
}
