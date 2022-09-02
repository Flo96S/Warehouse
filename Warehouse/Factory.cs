using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class Factory
    {
        public object lockFactory { get; set; } = new object();
        private List<Product> Products { get; set; }
        public int Count { get; private set; }
        public int FactoryId { get; private set; }
        public int CreatedProducts { get; set; } = 0;
        public int TriesToCreate { get; set; } = 0;
        public int ToWarehouse { get; set; } = 0;

        public Factory(int count, int factoryId)
        {
            Products = new List<Product>();
            Count = count;
            FactoryId = factoryId;
        }

        public void FactoryToWarehouse(Warehouse warehouse)
        {
            Random srand = new Random();
            if (Products.Count == 0) return;
            int i = srand.Next(Products.Count);
            Product p = Products[i];
            warehouse.ReceiveProduct(p);
            Products.RemoveAt(i);
            ToWarehouse++;
            warehouse.AddedProducts++;
        }

        public void CreateProduct(string name, decimal price)
        {
            lock (lockFactory)
            {
                if (Count > Products.Count)
                {
                    Products.Add(new Product(name, price, this));
                    CreatedProducts++;
                }
                else
                {
                    TriesToCreate++;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"Produced {CreatedProducts} in Factory {(Companies)FactoryId} | Tried: {CreatedProducts + TriesToCreate} times.");
        }
    }
}
