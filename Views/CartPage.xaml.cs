using Vercoff.ViewModels;

namespace Vercoff.Views;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _viewModel;

    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.RefreshCart();
        UpdateEmptyState();
    }

    private void UpdateEmptyState()
    {
        EmptyState.IsVisible = !_viewModel.HasItems;
        PromoErrorLabel.IsVisible = !string.IsNullOrEmpty(_viewModel.PromoError);
    }
}
