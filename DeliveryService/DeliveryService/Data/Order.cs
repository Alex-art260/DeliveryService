namespace DeliveryService.Data
{
    public class Order
    {
        public string? OrderId { get; set; }
        public double Weight { get; set; }
        public int District { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string? IpAddress { get; set; }
    }
}
