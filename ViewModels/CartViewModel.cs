namespace Vercoff.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Vercoff.Models;
using Vercoff.Services;

public partial class CartViewModel : ObservableObject
{
    private readonly CartService _cartService;

    [ObservableProperty]
    private ObservableCollection<CartItem> _cartItems = new();

    [ObservableProperty]
    private string _promoCodeEntry = string.Empty;

    [ObservableProperty]
    private string? _appliedPromoCode;

    [ObservableProperty]
    private decimal _subtotal;

    [ObservableProperty]
    private decimal _discount;

    [ObservableProperty]
    private decimal _total;

    [ObservableProperty]
    private bool _hasItems;

    [ObservableProperty]
    private string? _promoError;

    [ObservableProperty]
    private bool _showPromoSuccess;

    public CartViewModel(CartService cartService)
    {
        _cartService = cartService;
        _cartService.CartChanged += RefreshCart;
        RefreshCart();
    }

    public void RefreshCart()
    {
        CartItems = new ObservableCollection<CartItem>(_cartService.Items);
        Subtotal = _cartService.Subtotal;
        Discount = _cartService.DiscountAmount;
        Total = _cartService.Total;
        AppliedPromoCode = _cartService.AppliedPromoCode;
        HasItems = _cartService.Items.Count > 0;
        ShowPromoSuccess = !string.IsNullOrEmpty(AppliedPromoCode);
    }

    [RelayCommand]
    private void IncrementQuantity(CartItem item)
    {
        _cartService.UpdateQuantity(item.Id, item.Quantity + 1);
    }

    [RelayCommand]
    private void DecrementQuantity(CartItem item)
    {
        _cartService.UpdateQuantity(item.Id, item.Quantity - 1);
    }

    [RelayCommand]
    private void RemoveItem(CartItem item)
    {
        _cartService.RemoveItem(item.Id);
    }

    [RelayCommand]
    private void ApplyPromo()
    {
        PromoError = null;
        if (string.IsNullOrWhiteSpace(PromoCodeEntry))
        {
            PromoError = "Please enter a promo code";
            return;
        }

        if (_cartService.ApplyPromoCode(PromoCodeEntry))
        {
            PromoError = null;
            PromoCodeEntry = string.Empty;
        }
        else
        {
            PromoError = "Invalid promo code. Try DEFENSE20, WELCOME10";
        }
    }

    [RelayCommand]
    private void RemovePromo()
    {
        _cartService.RemovePromoCode();
        PromoCodeEntry = string.Empty;
    }

    [RelayCommand]
    private async Task ProceedToCheckout()
    {
        if (_cartService.Items.Count == 0) return;
        await Shell.Current.GoToAsync("checkout");
    }
}
