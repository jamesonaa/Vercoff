namespace Vercoff.Services;

using Vercoff.Models;

public class MenuService
{
    private readonly List<MenuItem> _menuItems;

    public MenuService()
    {
        _menuItems = new List<MenuItem>
        {
            new()
            {
                Id = 1,
                Name = "Caramel Latte",
                Description = "Rich espresso with velvety milk and buttery caramel drizzle",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 149m,
                Category = "Signature",
                IsFeatured = true
            },
            new()
            {
                Id = 2,
                Name = "Spanish Latte",
                Description = "Bold espresso layered with sweetened condensed milk",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 139m,
                Category = "Signature",
                IsFeatured = true
            },
            new()
            {
                Id = 3,
                Name = "Classic Americano",
                Description = "Pure espresso with hot water for a clean, bold flavor",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 109m,
                Category = "Classic",
                IsFeatured = false
            },
            new()
            {
                Id = 4,
                Name = "Mocha Frappé",
                Description = "Blended chocolate espresso with whipped cream",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 169m,
                Category = "Frappé",
                IsFeatured = true
            },
            new()
            {
                Id = 5,
                Name = "Matcha Latte",
                Description = "Premium ceremonial-grade matcha with steamed milk",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 159m,
                Category = "Specialty",
                IsFeatured = true
            },
            new()
            {
                Id = 6,
                Name = "Vanilla Cold Brew",
                Description = "Slow-steeped cold brew with house-made vanilla syrup",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 129m,
                Category = "Classic",
                IsFeatured = true
            },
            new()
            {
                Id = 7,
                Name = "Hazelnut Latte",
                Description = "Creamy latte infused with roasted hazelnut flavor",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 149m,
                Category = "Signature",
                IsFeatured = false
            },
            new()
            {
                Id = 8,
                Name = "Dirty Horchata",
                Description = "Cinnamon rice milk meets bold espresso",
                ImageSource = "drink_caramel_latte.jpg",
                BasePrice = 159m,
                Category = "Specialty",
                IsFeatured = false
            }
        };
    }

    public List<MenuItem> GetAllItems() => _menuItems;

    public List<MenuItem> GetFeaturedItems() => _menuItems.Where(m => m.IsFeatured).ToList();

    public List<MenuItem> GetByCategory(string category) =>
        _menuItems.Where(m => m.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<string> GetCategories() =>
        _menuItems.Select(m => m.Category).Distinct().ToList();

    public MenuItem? GetById(int id) => _menuItems.FirstOrDefault(m => m.Id == id);
}
