using Vercoff.ViewModels;
using MenuItemModel = Vercoff.Models.MenuItem;

namespace Vercoff.Views;

public partial class CustomizationPopup : ContentPage
{
    private readonly CustomizationViewModel _viewModel;

    public CustomizationPopup(CustomizationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;

        _viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(CustomizationViewModel.IsAdded))
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    AddToCartBtn.Text = "✓ Added!";
                    AddToCartBtn.BackgroundColor = Color.FromArgb("#4E8B3A");
                    await Task.Delay(800);
                    await Navigation.PopModalAsync();
                });
            }
        };
    }

    private async void OnCloseTapped(object? sender, TappedEventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void OnRegularTapped(object? sender, TappedEventArgs e)
    {
        _viewModel.SelectedSize = "Regular";
    }

    private void OnLargeTapped(object? sender, TappedEventArgs e)
    {
        _viewModel.SelectedSize = "Large";
    }

    private void OnWholeMilkTapped(object? sender, TappedEventArgs e)
    {
        _viewModel.SelectedMilk = "Whole Milk";
    }

    private void OnOatMilkTapped(object? sender, TappedEventArgs e)
    {
        _viewModel.SelectedMilk = "Oat Milk (+₱20)";
    }
}
