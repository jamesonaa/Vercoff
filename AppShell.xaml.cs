using Vercoff.Views;

namespace Vercoff;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for pages that aren't in the visual hierarchy
        Routing.RegisterRoute("checkout", typeof(CheckoutPage));
    }
}
