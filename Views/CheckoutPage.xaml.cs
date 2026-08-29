using Vercoff.ViewModels;

namespace Vercoff.Views;

public partial class CheckoutPage : ContentPage
{
    private readonly CheckoutViewModel _viewModel;

    public CheckoutPage(CheckoutViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Initialize();
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        ConfirmBtn.IsVisible = !_viewModel.IsProcessing;
        ValidationLabel.IsVisible = !string.IsNullOrEmpty(_viewModel.ValidationError);

        _viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(CheckoutViewModel.IsProcessing))
                ConfirmBtn.IsVisible = !_viewModel.IsProcessing;
            if (e.PropertyName == nameof(CheckoutViewModel.ValidationError))
                ValidationLabel.IsVisible = !string.IsNullOrEmpty(_viewModel.ValidationError);
        };
    }

    private async void OnBackTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
