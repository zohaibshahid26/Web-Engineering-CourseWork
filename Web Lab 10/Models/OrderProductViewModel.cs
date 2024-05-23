namespace Web_Lab_10.Models
{
    public class OrderProductViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public Order Order { get; set; }= new Order();
    }
}
