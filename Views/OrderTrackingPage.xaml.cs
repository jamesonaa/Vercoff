using Vercoff.Models;
using Vercoff.ViewModels;

namespace Vercoff.Views;

public partial class OrderTrackingPage : ContentPage
{
    private readonly OrderTrackingViewModel _viewModel;

    public OrderTrackingPage(OrderTrackingViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;

        _viewModel.PropertyChanged += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(UpdateStepVisuals);
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.RefreshOrders();
        UpdateStepVisuals();
        NoOrderState.IsVisible = !_viewModel.HasCurrentOrder;
    }

    private void UpdateStepVisuals()
    {
        var active = Color.FromArgb("#D48C46"); // CaramelAccent
        var done = Color.FromArgb("#4E8B3A");   // SuccessGreen
        var pending = Color.FromArgb("#F5EDE0"); // LightCream

        if (_viewModel.CurrentOrder == null)
        {
            Step1Circle.BackgroundColor = pending;
            Step2Circle.BackgroundColor = pending;
            Step3Circle.BackgroundColor = pending;
            Step1Status.Text = "";
            Step2Status.Text = "";
            Step3Status.Text = "";
            return;
        }

        var status = _viewModel.CurrentOrder.Status;

        // Step 1
        Step1Circle.BackgroundColor = status == OrderStatus.PaymentPending ? active : done;
        Step1Status.Text = status >= OrderStatus.Brewing ? "Done ✓" : "Pending...";
        Step1Status.TextColor = status >= OrderStatus.Brewing ? done : active;

        // Step 2
        Step2Circle.BackgroundColor = status == OrderStatus.Brewing ? active :
                                       status > OrderStatus.Brewing ? done : pending;
        Step2Status.Text = status >= OrderStatus.ReadyForPickup ? "Done ✓" :
                           status == OrderStatus.Brewing ? "In progress..." : "Waiting";
        Step2Status.TextColor = status >= OrderStatus.ReadyForPickup ? done :
                                status == OrderStatus.Brewing ? active : Color.FromArgb("#B8A99A");

        // Step 3
        Step3Circle.BackgroundColor = status >= OrderStatus.ReadyForPickup ? done : pending;
        Step3Status.Text = status >= OrderStatus.ReadyForPickup ? "Ready! 🎉" : "Waiting";
        Step3Status.TextColor = status >= OrderStatus.ReadyForPickup ? done : Color.FromArgb("#B8A99A");
    }
}
