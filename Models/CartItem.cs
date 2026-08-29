namespace Vercoff.Models;

public class CartItem
{
    public int Id { get; set; }
    public MenuItem MenuItem { get; set; } = null!;
    public string SelectedSize { get; set; } = "Regular";
    public string MilkChoice { get; set; } = "Whole Milk";
    public int ExtraShots { get; set; }
    public int SweetnessLevel { get; set; } = 100; // percentage
    public int Quantity { get; set; } = 1;

    public decimal UnitPrice
    {
        get
        {
            decimal price = MenuItem.BasePrice;
            if (SelectedSize == "Large") price += MenuItem.LargeUpcharge;
            if (MilkChoice == "Oat Milk") price += 20m;
            price += ExtraShots * 15m;
            return price;
        }
    }

    public decimal TotalPrice => UnitPrice * Quantity;
}
