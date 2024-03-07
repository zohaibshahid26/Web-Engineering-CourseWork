using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Basics
{
    internal class ShoppingCart
    {
        public void CalculateTotalPrice(List<double> priceList, int discount = 0)
        {
            double sum = 0.0;
            foreach (double price in priceList)
            {
                sum += price;
            }
            sum -= discount;
            Console.WriteLine($"The prices of all the items  after discount is: {sum}");
        }
    }
}
