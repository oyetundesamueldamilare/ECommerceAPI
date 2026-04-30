namespace ECommerceProductAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; set; }

        public AppUser Customer { get; set; }
        public required ICollection<OrderItem> OrderItems { get; set; }
    }
}
