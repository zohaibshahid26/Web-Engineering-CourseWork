using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueenLocalDataHandling_
{
    internal class Product
    {
        public required string ProductCode { get; set; }
        public required string ProductSize { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string? ProductPicture { get; set; }
    }
}
