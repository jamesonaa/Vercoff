namespace Vercoff.Models;

public enum OrderStatus
{
    PaymentPending,
    Brewing,
    ReadyForPickup,
    Completed
}

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8].ToUpper();
    public List<CartItem> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? PromoCode { get; set; }
    public string PaymentMethod { get; set; } = "GCash";
    public string? ReferenceNumber { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.PaymentPending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
