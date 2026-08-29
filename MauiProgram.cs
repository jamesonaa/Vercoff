using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Vercoff.Services;
using Vercoff.ViewModels;
using Vercoff.Views;

namespace Vercoff;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
            });

        // Services (Singletons — shared state across the app)
        builder.Services.AddSingleton<MenuService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddSingleton<OrderService>();

        // ViewModels
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddTransient<CustomizationViewModel>();
        builder.Services.AddSingleton<CartViewModel>();
        builder.Services.AddTransient<CheckoutViewModel>();
        builder.Services.AddSingleton<OrderTrackingViewModel>();
        builder.Services.AddSingleton<NestPassViewModel>();

        // Pages & Popups
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<CustomizationPopup>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddTransient<CheckoutPage>();
        builder.Services.AddTransient<OrderTrackingPage>();
        builder.Services.AddTransient<NestPassPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
