namespace Vercoff.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class NestPassViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isSubscribed;

    [ObservableProperty]
    private string _statusText = "Not subscribed";

    [ObservableProperty]
    private string _buttonText = "Subscribe Now — ₱99/week";

    [RelayCommand]
    private async Task ToggleSubscription()
    {
        if (IsSubscribed)
        {
            IsSubscribed = false;
            StatusText = "Not subscribed";
            ButtonText = "Subscribe Now — ₱99/week";
        }
        else
        {
            IsSubscribed = true;
            StatusText = "✅ Active Nest Pass Member";
            ButtonText = "Cancel Subscription";
            await Shell.Current.DisplayAlertAsync("Welcome to Nest Pass! 🎉",
                "You now get 15% OFF every order. Use code NESTPASS15 at checkout!",
                "Awesome!");
        }
    }
}
