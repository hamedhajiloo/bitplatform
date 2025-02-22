﻿using Microsoft.AspNetCore.Components.Routing;

namespace Bit.Websites.Platform.Client.Shared;

public partial class Header : IDisposable
{
    private bool isDocsRoute;
    private bool isBswupDocRoute;
    private bool isBesqlDocRoute;
    private bool isButilDocRoute;
    private bool isHeaderMenuOpen;
    private bool isTemplateDocRoute;
    private string currentUrl = string.Empty;


    [AutoInject] public NavManuService navManuService = default!;
    [AutoInject] public BitThemeManager bitThemeManager  = default!;

    protected override async Task OnInitAsync()
    {
        HandleCollapseMenu();

        NavigationManager.LocationChanged += OnLocationChanged;

        await base.OnInitAsync();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        HandleCollapseMenu();

        StateHasChanged();
    }

    private void HandleCollapseMenu()
    {
        currentUrl = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "/", StringComparison.Ordinal);

        isBswupDocRoute = currentUrl.Contains("bswup");
        isBesqlDocRoute = currentUrl.Contains("besql");
        isButilDocRoute = currentUrl.Contains("butil");
        isTemplateDocRoute = currentUrl.Contains("templates") || currentUrl.Contains("admin-panel") || currentUrl.Contains("todo-template");

        isDocsRoute = isTemplateDocRoute || isBswupDocRoute || isBesqlDocRoute || isButilDocRoute;
    }

    private void ToggleMenu()
    {
        navManuService.ToggleMenu();
    }

    private string GetActiveRouteName()
    {
        if (currentUrl.Contains("templates"))
        {
            return "Products & Services";
        }
        else return currentUrl switch
        {
            Urls.HomePage => "Home",
            Urls.Components => "Products & Services",
            Urls.CloudHostingSolutions => "Products & Services",
            Urls.Support => "Products & Services",
            Urls.Academy => "Products & Services",
            Urls.Pricing => "Pricing",
            Urls.AboutUs => "About us",
            Urls.ContactUs => "Contact us",
            Urls.Blogs => "Blogs",
            Urls.Videos => "Videos",
            _ => "Products & Services",
        };
    }

    private bool IsProductsServicesActive()
    {
        return (currentUrl.Contains("templates") ||
           currentUrl == Urls.Components ||
           currentUrl == Urls.CloudHostingSolutions ||
           currentUrl == Urls.Support ||
           currentUrl == Urls.Academy);
    }

    private async Task ToggleHeaderMenu()
    {
        isHeaderMenuOpen = !isHeaderMenuOpen;
        await JSRuntime.ToggleBodyOverflow(isHeaderMenuOpen);
        StateHasChanged();
    }

    private async Task ToggleTheme()
    {
        var newTheme = await bitThemeManager.ToggleDarkLightAsync();
        var isDark = newTheme.Contains("dark");
    }



    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}
