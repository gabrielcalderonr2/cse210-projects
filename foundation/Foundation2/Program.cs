using System;
using System.Collections.Generic;

namespace OrderingSystem
{
    // Address class to store address details
    class Address
    {
        private string Street { get; set; }
        private string City { get; set; }
        private string State { get; set; }
        private string Country { get; set; }

        public Address(string street, string city, string state, string country)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
        }

        public bool IsInUSA()
        {
            return Country.Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        public string GetFullAddress()
        {
            return $"{Street}\n{City}, {State}\n{Country}";
        }
    }

    // Customer class to store customer details
    class Customer
    {
        private string Name { get; set; }
        private Address Address { get; set; }

        public Customer(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public bool LivesInUSA()
        {
            return Address.IsInUSA();
        }

        public Address GetAddress()
        {
            return Address;
        }

        public string GetName()
        {
            return Name;
        }
    }

    // Product class to store product details
    class Product
    {
        private string Name { get; set; }
        private string ProductId { get; set; }
        private decimal Price { get; set; }
        private int Quantity { get; set; }

        public Product(string name, string productId, decimal price, int quantity)
        {
            Name = name;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public decimal CalculateTotalCost()
        {
            return Price * Quantity;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetProductId()
        {
            return ProductId;
        }
    }

    // Order class to manage product orders
    class Order
    {
        private List<Product> Products { get; set; }
        private Customer Customer { get; set; }

        public Order(Customer customer)
        {
            Customer = customer;
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public decimal CalculateTotalCost()
        {
            decimal totalCost = 0M;
            foreach (var product in Products)
            {
                totalCost += product.CalculateTotalCost();
            }
            totalCost += Customer.LivesInUSA() ? 5M : 35M; // Add shipping cost
            return totalCost;
        }

        public string GetPackingLabel()
        {
            string label = "Packing Label:\n";
            foreach (var product in Products)
            {
                label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
            }
            return label;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{Customer.GetName()}\n{Customer.GetAddress().GetFullAddress()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
            Address address2 = new Address("456 Elm St", "Othertown", "ON", "Canada");

            // Create customers
            Customer customer1 = new Customer("John Doe", address1);
            Customer customer2 = new Customer("Jane Smith", address2);

            // Create products
            Product product1 = new Product("Laptop", "P001", 999.99m, 1);
            Product product2 = new Product("Headphones", "P002", 199.99m, 2);
            Product product3 = new Product("Mouse", "P003", 49.99m, 1);
            Product product4 = new Product("Keyboard", "P004", 89.99m, 1);

            // Create orders
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);

            // Display order details
            List<Order> orders = new List<Order> { order1, order2 };
            foreach (var order in orders)
            {
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Cost: ${order.CalculateTotalCost():F2}\n");
            }
        }
    }
}
