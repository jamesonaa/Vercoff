namespace Vercoff.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Vercoff.Models;
using Vercoff.Services;

public partial class HomeViewModel : ObservableObject
{
    private readonly MenuService _menuService;
    private readonly CartService _cartService;

    [ObservableProperty]
    private string _greeting = "Welcome to Ver's Nest! ☕";

    [ObservableProperty]
    private ObservableCollection<MenuItem> _featuredDrinks = new();

    [ObservableProperty]
    private ObservableCollection<MenuItem> _allDrinks = new();

    [ObservableProperty]
    private ObservableCollection<string> _categories = new();

    [ObservableProperty]
    private string? _selectedCategory;

    [ObservableProperty]
    private int _cartBadgeCount;

    public HomeViewModel(MenuService menuService, CartService cartService)
    {
        _menuService = menuService;
        _cartService = cartService;
        _cartService.CartChanged += OnCartChanged;

        LoadMenu();
    }

    private void OnCartChanged()
    {
        CartBadgeCount = _cartService.ItemCount;
    }

    private void LoadMenu()
    {
        var featured = _menuService.GetFeaturedItems();
        FeaturedDrinks = new ObservableCollection<MenuItem>(featured);

        var all = _menuService.GetAllItems();
        AllDrinks = new ObservableCollection<MenuItem>(all);

        var cats = _menuService.GetCategories();
        cats.Insert(0, "All");
        Categories = new ObservableCollection<string>(cats);
        SelectedCategory = "All";
    }

    [RelayCommand]
    private void FilterByCategory(string category)
    {
        SelectedCategory = category;
        if (category == "All")
        {
            AllDrinks = new ObservableCollection<MenuItem>(_menuService.GetAllItems());
        }
        else
        {
            AllDrinks = new ObservableCollection<MenuItem>(_menuService.GetByCategory(category));
        }
    }
}
