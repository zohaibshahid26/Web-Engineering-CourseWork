namespace QueenLocalDataHandling
{
    internal class Order
    {
        public int OrderId { get; set; }
        public required string ProductCode { get; set; }
        public required string CustomerId { get; set; }
        public required string ProductSize { get; set; }
        public int ProductQuantity { get; set; }
    }
}