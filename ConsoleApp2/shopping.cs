﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class ShoppingCart
    {
        public class InvoiceItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public Coupon Coupon { get; set; }
        }
        public void Menu()
        {
            bool cont = true;
            while (cont == true)
            {
                Console.Clear();
                Console.WriteLine("1. Tìm sản phẩm theo tên\n2. Mua hàng và in hóa đơn\n3. Thoát");
                bool validChoice = false;
                int choice = 0;

                while (!validChoice)
                {
                    Console.WriteLine("\tVui lòng chọn một lựa chọn:");
                    string input = Console.ReadLine();

                    try
                    {
                        choice = int.Parse(input);
                        validChoice = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                    }
                }
                switch (choice)
                {
                    case 1:
                        FindProductByName();
                        break;
                    case 2:
                        Shopping();
                        break;
                    case 3:
                        cont = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        public void Shopping()
        {
            bool continueShopping = true;
            List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

            while (continueShopping)
            {
                Console.WriteLine("Danh sách hàng hóa trong Database: ");
                foreach (var product in Database.products)
                {
                    Console.WriteLine($"Mã hàng: {product.ProductCode,-5} | Tên hàng: {product.ProductName,-15} | " +
                        $"Đơn giá: {product.Price,-5} | Số lượng: {product.Quantity}");
                }

                Console.WriteLine("Chọn mã hàng hóa để mua:");
                string selectedProductCode = Console.ReadLine();

                // Tìm sản phẩm trong Database dựa trên mã hàng được chọn
                Product selectedProduct = FindProductByCode(selectedProductCode);

                if (selectedProduct != null)
                {
                    Console.WriteLine($"Đã chọn: {selectedProduct.ProductName} - Đơn giá: {selectedProduct.Price}\n" +
                        $"Hàng còn tồn trong kho: {selectedProduct.Quantity}");

                    Console.WriteLine("Nhập số lượng muốn mua:");
                    int quantityToBuy = int.Parse(Console.ReadLine());

                    while (quantityToBuy > selectedProduct.Quantity)
                    {
                        Console.Write("Số lượng mua vượt quá số lượng trong kho. ");
                        Console.WriteLine("Yêu cầu nhập lại!!");
                        Console.WriteLine("Nhập số lượng muốn mua:");
                        quantityToBuy = int.Parse(Console.ReadLine());
                    }

                    selectedProduct.Quantity -= quantityToBuy;

                    // Áp dụng coupon
                    Console.WriteLine("Chọn coupon để áp dụng (10%, 15%, 25%). ");
                    Console.WriteLine("1. 10%\n2. 15%\n3. 25%\n4. Không áp dụng mã giảm giá");
                    int couponChoice = int.Parse(Console.ReadLine());
                    Coupon selectedCoupon = SelectCoupon(couponChoice);

                    // Thêm sản phẩm vào danh sách hóa đơn
                    InvoiceItem invoiceItem = new InvoiceItem
                    {
                        Product = selectedProduct,
                        Quantity = quantityToBuy,
                        Coupon = selectedCoupon
                    };
                    invoiceItems.Add(invoiceItem);

                    Console.WriteLine("Bạn có muốn mua tiếp không? (Nhập 'Y' để mua tiếp, 'N' để kết thúc mua hàng)");
                    string continueChoice = Console.ReadLine();
                    continueShopping = (continueChoice.ToUpper() == "Y");
                }
                else
                {
                    Console.WriteLine("Mã hàng không hợp lệ.");
                }
            }

            // In hóa đơn sau khi người dùng đã chọn xong
            PrintInvoice(invoiceItems);
        }


        public void PrintInvoice(List<InvoiceItem> invoiceItems)
        {
            Console.Clear();
            Console.WriteLine("Hóa đơn mua hàng:");

            foreach (var item in invoiceItems)
            {
                Console.WriteLine($"Mã hàng: {item.Product.ProductCode}\t| Tên hàng: {item.Product.ProductName}\n" +
                    $"Đơn giá: {item.Product.Price}\t| Số lượng mua: {item.Quantity}");

                double discountAmount = 0;

                if (item.Coupon != null)
                {
                    discountAmount = item.Product.Price * item.Quantity * (item.Coupon.DiscountRate / 100);
                    Console.WriteLine($"Giảm giá từ coupon {item.Coupon.CouponCode}: {discountAmount}");
                }

                double totalWithoutDiscount = item.Product.Price * item.Quantity;
                double totalWithDiscount = totalWithoutDiscount - discountAmount;

                Console.WriteLine($"Tổng tiền trước giảm giá: {totalWithoutDiscount}");
                Console.WriteLine($"Tổng tiền sau giảm giá: {totalWithDiscount}");
                Console.WriteLine("".PadLeft(40, '-'));
            }

            double grandTotal = 0;

            foreach (var item in invoiceItems)
            {
                double itemTotal = item.Product.Price * item.Quantity;

                // Tính giảm giá nếu có coupon
                if (item.Coupon != null)
                {
                    double discountAmount = itemTotal * (item.Coupon.DiscountRate / 100);
                    itemTotal -= discountAmount;
                }

                grandTotal += itemTotal;
            }

            Console.WriteLine($"Tổng cộng: {grandTotal}");

            Console.ReadLine();
        }


        public Product FindProductByCode(string productCode)
        {
            foreach (var product in Database.products)
            {
                if (product.ProductCode == productCode)
                {
                    return product;
                }
            }
            return null;
        }

        public Coupon SelectCoupon(int couponChoice)
        {
            switch (couponChoice)
            {
                case 1:
                    return new Coupon("10PERCENT", 10);
                case 2:
                    return new Coupon("15PERCENT", 15);
                case 3:
                    return new Coupon("25PERCENT", 25);
                case 4:
                    return new Coupon("Không áp dụng mã giảm nào", 0);
                default:
                    Console.WriteLine("Lựa chọn coupon không hợp lệ.");
                    return null;
            }
        }

        public void FindProductByName()
        {
            Console.Write("Nhập tên cần tìm: ");
            string name = Console.ReadLine().ToLower();

            bool found = false;
            foreach (var product in Database.products)
            {
                if (product.ProductName.ToLower().Contains(name))
                {
                    Console.WriteLine($"Mã hàng: {product.ProductCode}, Tên hàng: {product.ProductName}, Đơn giá: {product.Price}, Số lượng: {product.Quantity}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Không tìm thấy!");
            }
            Console.ReadKey();
        }
    }
}
