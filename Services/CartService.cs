namespace Vercoff.Services;

using Vercoff.Models;

public class CartService
{
    private readonly List<CartItem> _items = new();
    private int _nextId = 1;
    private string? _appliedPromoCode;

    public event Action? CartChanged;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public int ItemCount => _items.Sum(i => i.Quantity);

    public void AddItem(CartItem item)
    {
        item.Id = _nextId++;
        _items.Add(item);
        CartChanged?.Invoke();
    }

    public void RemoveItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            _items.Remove(item);
            CartChanged?.Invoke();
        }
    }

    public void UpdateQuantity(int id, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            if (quantity <= 0)
                _items.Remove(item);
            else
                item.Quantity = quantity;
            CartChanged?.Invoke();
        }
    }

    public void Clear()
    {
        _items.Clear();
        _appliedPromoCode = null;
        CartChanged?.Invoke();
    }

    public decimal Subtotal => _items.Sum(i => i.TotalPrice);

    public string? AppliedPromoCode => _appliedPromoCode;

    public decimal DiscountPercent
    {
        get
        {
            return _appliedPromoCode?.ToUpper() switch
            {
                "DEFENSE20" => 0.20m,
                "NESTPASS15" => 0.15m,
                "WELCOME10" => 0.10m,
                _ => 0m
            };
        }
    }

    public decimal DiscountAmount => Subtotal * DiscountPercent;

    public decimal Total => Subtotal - DiscountAmount;

    public bool ApplyPromoCode(string code)
    {
        var upper = code.Trim().ToUpper();
        if (upper is "DEFENSE20" or "NESTPASS15" or "WELCOME10")
        {
            _appliedPromoCode = upper;
            CartChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void RemovePromoCode()
    {
        _appliedPromoCode = null;
        CartChanged?.Invoke();
    }
}
