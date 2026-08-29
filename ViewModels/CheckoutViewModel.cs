namespace Vercoff.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Vercoff.Services;

public partial class CheckoutViewModel : ObservableObject
{
    private readonly CartService _cartService;
    private readonly OrderService _orderService;

    [ObservableProperty]
    private string _selectedPaymentMethod = "GCash";

    [ObservableProperty]
    private string _referenceNumber = string.Empty;

    [ObservableProperty]
    private decimal _total;

    [ObservableProperty]
    private string? _validationError;

    [ObservableProperty]
    private bool _isProcessing;

    [ObservableProperty]
    private bool _isGCash = true;

    [ObservableProperty]
    private bool _isMaya;

    public CheckoutViewModel(CartService cartService, OrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;
    }

    public void Initialize()
    {
        Total = _cartService.Total;
        ReferenceNumber = string.Empty;
        ValidationError = null;
        IsProcessing = false;
        SelectGCash();
    }

    [RelayCommand]
    private void SelectGCash()
    {
        SelectedPaymentMethod = "GCash";
        IsGCash = true;
        IsMaya = false;
    }

    [RelayCommand]
    private void SelectMaya()
    {
        SelectedPaymentMethod = "Maya";
        IsGCash = false;
        IsMaya = true;
    }

    [RelayCommand]
    private async Task ConfirmPayment()
    {
        ValidationError = null;

        if (string.IsNullOrWhiteSpace(ReferenceNumber))
        {
            ValidationError = "Please enter your payment reference number";
            return;
        }

        var cleaned = ReferenceNumber.Replace(" ", "").Replace("-", "");
        if (cleaned.Length != 13 || !cleaned.All(char.IsDigit))
        {
            ValidationError = "Reference number must be exactly 13 digits";
            return;
        }

        IsProcessing = true;

        // Simulate processing delay
        await Task.Delay(1500);

        _orderService.PlaceOrder(_cartService, SelectedPaymentMethod, cleaned);

        IsProcessing = false;

        await Shell.Current.GoToAsync("//orders");
    }
}
