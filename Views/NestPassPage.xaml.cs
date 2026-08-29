using Vercoff.ViewModels;

namespace Vercoff.Views;

public partial class NestPassPage : ContentPage
{
    public NestPassPage(NestPassViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
