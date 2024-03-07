using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__Basics
{
    internal class GeometricShape
    {
        const double PI = Math.PI;
        public double CalculateArea(double raduis)
        {
            return Math.Pow(raduis, 2) * PI;
        }
        public double CalculateCircumference(double raduis)
        {
            return 2 * PI * raduis;

        }

        public (double area, double perimeter) CalculateRectangleProperties(double length, double width)
        {
            double area = length * width;
            double perimeter = 2 * (length + width);
            return (area, perimeter);
        }
    }
}

