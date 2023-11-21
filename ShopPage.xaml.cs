using Plugin.LocalNotification;
using Stoica_Ramona_Lab7.Models;

namespace Stoica_Ramona_Lab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        if (shop != null && shop.ID != 0)
        {
            bool answer = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this shop?", "Yes", "No");

            if (answer)
            {
                await App.Database.DeleteShopAsync(shop);
                await Navigation.PopAsync();
            }
        }
    }


    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Address;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };
        var location = locations?.FirstOrDefault();
        var myLocation = await Geolocation.GetLocationAsync();
        var myLocation1 = new Location(46.7731796289, 23.6213886738);
        await Map.OpenAsync(location, options);
        var distance = myLocation.CalculateDistance(location,
DistanceUnits.Kilometers);
        if (distance < 4)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
          await  LocalNotificationCenter.Current.Show(request);
        }
      

    }
}