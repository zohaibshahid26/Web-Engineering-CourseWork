using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Basics
{
    internal class MathHelper
    {

        public void SwapIntegers(ref int i, ref int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }
        public void DivideWithRemainder(int dividend, int divisor, out int remainder, out int quotient)
        {
            if (dividend == 0)
            {

                remainder = dividend % divisor;
                quotient = dividend / divisor;
            }
            else
            {
                remainder = divisor;
                quotient = 0;
            }
        }

    }
}
