using System;
using System.Collections.Generic;
using System.Text;
using TinyReceipt.Model;

namespace TinyReceipt
{
    class Program
    {
        static List<ProductInfoEntity> productsInfo = GetOurProductsInfo();
        static void Main(string[] args)
        {
            List<OrderEntity> orders = new List<OrderEntity>();

            string input = Console.ReadLine();

            while (input != null)
            {
                if (input.Trim() == "clr")
                {
                    orders = new List<OrderEntity>();
                }
                if (input.Trim() == "calc")
                {
                    decimal totalTax = 0;
                    decimal totalPrice = 0;

                    //calculate output
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i] = GetOrderWithTax(orders[i]);
                        totalTax += orders[i].TotalTax;
                        totalPrice += orders[i].TotalPrice;
                    }

                    //output
                    Console.WriteLine(GetOutputResult(orders, totalTax, totalPrice));

                    //reset order list
                    orders = new List<OrderEntity>();
                    input = Console.ReadLine();
                }
                else
                {
                    //read input line  and restore data 
                    OrderEntity order = ConvertInput2Order(input);
                    if (order != null)
                    {
                        orders.Add(order);
                    }
                    input = Console.ReadLine();
                }

            }
        }


        /// <summary>
        /// calculate order's tax 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        static OrderEntity GetOrderWithTax(OrderEntity order)
        {
            decimal npPrice = order.Num * order.Price;
            decimal tax = productsInfo.Find(o => o.Name == order.Name).Rate;
            order.TotalTax = ReceiptService.RoundingTax(npPrice * tax);
            order.TotalPrice = npPrice + order.TotalTax;
            return order;
        }

        /// <summary>
        /// generate  output string
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="totaltax"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        static string GetOutputResult(List<OrderEntity> orders, decimal totaltax, decimal totalPrice)
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach (var o in orders)
            {
                strBuilder.AppendLine($"{o.Num} {o.Name} : {o.TotalPrice}");
            }
            strBuilder.AppendLine($"Sales Taxes: {totaltax}");
            strBuilder.AppendLine($"Total: {totalPrice}");

            return strBuilder.ToString();
        }

        /// <summary>
        /// resolve the input msg
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static OrderEntity ConvertInput2Order(string input)
        {
            OrderEntity result = null;
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    string[] splitTxt = input.Trim().Split(" at ");
                    OrderEntity o = new OrderEntity();
                    o.Num = Convert.ToInt32(splitTxt[0].Split(" ")[0]);
                    o.Name = splitTxt[0].TrimStart($"{splitTxt[0].Split(" ")[0]} ".ToCharArray());
                    o.Price = decimal.Parse(splitTxt[1]);
                    result = o;
                }
            }
            catch
            {
                Console.WriteLine($"input convert error , add order failed , input text is '{input}' , pls use this format like {{ num }} {{ product name }} at {{ price }}");
            }
            return result;
        }

        /// <summary>
        /// initialize product base 
        /// </summary>
        /// <returns></returns>
        static List<ProductInfoEntity> GetOurProductsInfo()
        {
            List<ProductInfoEntity> list = new List<ProductInfoEntity>();
            list.Add(new ProductInfoEntity { Name = "book", Rate = 0 });
            list.Add(new ProductInfoEntity { Name = "music CD", Rate = 0.1M });
            list.Add(new ProductInfoEntity { Name = "chocolate bar", Rate = 0 });
            list.Add(new ProductInfoEntity { Name = "imported box of chocolates", Rate = 0.05M });
            list.Add(new ProductInfoEntity { Name = "imported bottle of perfume", Rate = 0.15M });
            list.Add(new ProductInfoEntity { Name = "bottle of perfume", Rate = 0.1M });
            list.Add(new ProductInfoEntity { Name = "packet of headache pills", Rate = 0 });
            list.Add(new ProductInfoEntity { Name = "box of imported chocolates", Rate = 0.05M });
            return list;
        }
    }
}
