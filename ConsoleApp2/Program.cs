using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.Clear();
            Console.WriteLine("-----Tạo Cơ sở dữ liệu-----");

            // Khởi tạo dữ liệu mẫu
            Database.InitializeDatabase();

            ShoppingCart shopping = new ShoppingCart();
            shopping.Menu();

            Console.ReadLine();
        }
    }
}
