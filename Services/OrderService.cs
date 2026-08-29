namespace Vercoff.Services;

using Vercoff.Models;

public class OrderService
{
    private readonly List<Order> _orders = new();

    public event Action? OrdersChanged;

    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public Order? CurrentOrder => _orders.LastOrDefault(o => o.Status != OrderStatus.Completed);

    public Order PlaceOrder(CartService cart, string paymentMethod, string? referenceNumber)
    {
        var order = new Order
        {
            Items = cart.Items.ToList(),
            Subtotal = cart.Subtotal,
            Discount = cart.DiscountAmount,
            Total = cart.Total,
            PromoCode = cart.AppliedPromoCode,
            PaymentMethod = paymentMethod,
            ReferenceNumber = referenceNumber,
            Status = OrderStatus.PaymentPending
        };
        _orders.Insert(0, order);
        cart.Clear();
        OrdersChanged?.Invoke();

        // Simulate order progression
        _ = SimulateOrderProgress(order);

        return order;
    }

    private async Task SimulateOrderProgress(Order order)
    {
        await Task.Delay(5000); // 5 seconds
        order.Status = OrderStatus.Brewing;
        OrdersChanged?.Invoke();

        await Task.Delay(10000); // 10 seconds
        order.Status = OrderStatus.ReadyForPickup;
        OrdersChanged?.Invoke();
    }

    public List<Order> GetOrderHistory() => _orders.ToList();
}
