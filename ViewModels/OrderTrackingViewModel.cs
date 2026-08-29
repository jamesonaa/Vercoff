namespace Vercoff.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Vercoff.Models;
using Vercoff.Services;

public partial class OrderTrackingViewModel : ObservableObject
{
    private readonly OrderService _orderService;

    [ObservableProperty]
    private Order? _currentOrder;

    [ObservableProperty]
    private ObservableCollection<Order> _orderHistory = new();

    [ObservableProperty]
    private bool _hasCurrentOrder;

    [ObservableProperty]
    private bool _hasOrderHistory;

    // Status step indicators
    [ObservableProperty]
    private bool _isPaymentPending;

    [ObservableProperty]
    private bool _isBrewing;

    [ObservableProperty]
    private bool _isReady;

    [ObservableProperty]
    private bool _paymentDone;

    [ObservableProperty]
    private bool _brewingDone;

    [ObservableProperty]
    private string _statusMessage = "No active orders";

    public OrderTrackingViewModel(OrderService orderService)
    {
        _orderService = orderService;
        _orderService.OrdersChanged += RefreshOrders;
    }

    public void RefreshOrders()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CurrentOrder = _orderService.CurrentOrder;
            HasCurrentOrder = CurrentOrder != null;

            if (CurrentOrder != null)
            {
                IsPaymentPending = CurrentOrder.Status == OrderStatus.PaymentPending;
                IsBrewing = CurrentOrder.Status == OrderStatus.Brewing;
                IsReady = CurrentOrder.Status == OrderStatus.ReadyForPickup;

                PaymentDone = CurrentOrder.Status >= OrderStatus.Brewing;
                BrewingDone = CurrentOrder.Status >= OrderStatus.ReadyForPickup;

                StatusMessage = CurrentOrder.Status switch
                {
                    OrderStatus.PaymentPending => "⏳ Verifying Payment...",
                    OrderStatus.Brewing => "☕ Brewing Your Order...",
                    OrderStatus.ReadyForPickup => "✅ Ready for Pickup!",
                    _ => "Order Complete"
                };
            }
            else
            {
                StatusMessage = "No active orders";
                IsPaymentPending = false;
                IsBrewing = false;
                IsReady = false;
                PaymentDone = false;
                BrewingDone = false;
            }

            var history = _orderService.GetOrderHistory();
            OrderHistory = new ObservableCollection<Order>(history);
            HasOrderHistory = history.Count > 0;
        });
    }
}
