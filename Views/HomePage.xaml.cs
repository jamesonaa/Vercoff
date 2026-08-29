using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;
using Vercoff.ViewModels;
using MenuItemModel = Vercoff.Models.MenuItem;

namespace Vercoff.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;
    private readonly IPopupService _popupService;
    private readonly CustomizationViewModel _customizationViewModel;

    public HomePage(HomeViewModel viewModel, CustomizationViewModel customizationViewModel, IPopupService popupService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _customizationViewModel = customizationViewModel;
        _popupService = popupService;
        BindingContext = viewModel;
    }

    private async void OnDrinkTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is MenuItemModel item)
        {
            _customizationViewModel.Initialize(item);
            var popup = new CustomizationPopup(_customizationViewModel);
            await Shell.Current.Navigation.PushModalAsync(popup);
        }
    }

    private async void OnQuickAddTapped(object? sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is MenuItemModel item)
        {
            _customizationViewModel.Initialize(item);
            var popup = new CustomizationPopup(_customizationViewModel);
            await Shell.Current.Navigation.PushModalAsync(popup);
        }
    }

    private async void OnCartTapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//cart");
    }

    private async void OnNestPassTapped(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//nestpass");
    }

    private void OnCategorySelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is string category)
        {
            _viewModel.FilterByCategoryCommand.Execute(category);
        }
    }
}
