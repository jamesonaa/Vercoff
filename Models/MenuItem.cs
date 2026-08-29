namespace Vercoff.Models;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageSource { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public List<string> AvailableSizes { get; set; } = new() { "Regular", "Large" };
    public decimal LargeUpcharge { get; set; } = 20m;
    public string Category { get; set; } = "Signature";
    public bool IsFeatured { get; set; }
}
