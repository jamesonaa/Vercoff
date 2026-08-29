namespace Vercoff.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Vercoff.Models;
using Vercoff.Services;

public partial class CustomizationViewModel : ObservableObject
{
    private readonly CartService _cartService;

    [ObservableProperty]
    private MenuItem _menuItem = null!;

    [ObservableProperty]
    private string _selectedSize = "Regular";

    [ObservableProperty]
    private string _selectedMilk = "Whole Milk";

    [ObservableProperty]
    private int _extraShots;

    [ObservableProperty]
    private int _sweetnessLevel = 100;

    [ObservableProperty]
    private decimal _computedPrice;

    [ObservableProperty]
    private bool _isAdded;

    public List<string> Sizes { get; } = new() { "Regular", "Large" };
    public List<string> MilkOptions { get; } = new() { "Whole Milk", "Oat Milk (+₱20)" };
    public List<int> SweetnessOptions { get; } = new() { 25, 50, 75, 100 };

    public CustomizationViewModel(CartService cartService)
    {
        _cartService = cartService;
    }

    public void Initialize(MenuItem item)
    {
        MenuItem = item;
        SelectedSize = "Regular";
        SelectedMilk = "Whole Milk";
        ExtraShots = 0;
        SweetnessLevel = 100;
        IsAdded = false;
        UpdatePrice();
    }

    partial void OnSelectedSizeChanged(string value) => UpdatePrice();
    partial void OnSelectedMilkChanged(string value) => UpdatePrice();
    partial void OnExtraShotsChanged(int value) => UpdatePrice();

    private void UpdatePrice()
    {
        if (MenuItem == null) return;
        decimal price = MenuItem.BasePrice;
        if (SelectedSize == "Large") price += MenuItem.LargeUpcharge;
        if (SelectedMilk.Contains("Oat")) price += 20m;
        price += ExtraShots * 15m;
        ComputedPrice = price;
    }

    [RelayCommand]
    private void IncrementShots()
    {
        if (ExtraShots < 4) ExtraShots++;
    }

    [RelayCommand]
    private void DecrementShots()
    {
        if (ExtraShots > 0) ExtraShots--;
    }

    [RelayCommand]
    private void SetSweetness(int level)
    {
        SweetnessLevel = level;
    }

    [RelayCommand]
    private void AddToCart()
    {
        var cartItem = new CartItem
        {
            MenuItem = MenuItem,
            SelectedSize = SelectedSize,
            MilkChoice = SelectedMilk.Contains("Oat") ? "Oat Milk" : "Whole Milk",
            ExtraShots = ExtraShots,
            SweetnessLevel = SweetnessLevel,
            Quantity = 1
        };
        _cartService.AddItem(cartItem);
        IsAdded = true;
    }
}
