namespace Web_Lab_10.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        List<int> ProductIds { get; set; } = new List<int>();
        public decimal Amount { get; set; } = new decimal();
    }
}
