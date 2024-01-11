using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Coupon AppliedCoupon { get; set; }

        public Product(string code, string name, double price, int quantity)
        {
            ProductCode = code;
            ProductName = name;
            Price = price;
            Quantity = quantity;
        }
    }

    // Khai báo lớp Coupon
    class Coupon
    {
        public string CouponCode { get; set; }
        public double DiscountRate { get; set; }

        public Coupon(string code, double rate)
        {
            CouponCode = code;
            DiscountRate = rate;
        }
    }

    // Khai báo database
    class Database
    {
        public static List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public static void InitializeDatabase()
        {
            Product product1 = new Product("001", "Áo thun", 150, 50);
            Product product2 = new Product("002", "Quần jeans", 300, 30);
            Product product3 = new Product("003", "Giày sneakers", 250, 40);
            Product product4 = new Product("004", "Váy hoa", 200, 20);
            Product product5 = new Product("005", "Túi xách", 120, 35);
            Product product6 = new Product("006", "Dép lào", 80, 25);

            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);
            products.Add(product5);
            products.Add(product6);
        }
    }
}
