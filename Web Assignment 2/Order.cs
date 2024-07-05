namespace QueenLocalDataHandling
{
    internal class Order
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerCnic { get; set; }
        public string CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public int ProductID { get; set; }
        public int Price { get; set; }
        public string ProductSize { get; set; }
    }
}