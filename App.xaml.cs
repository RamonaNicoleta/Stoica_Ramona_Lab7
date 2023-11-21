
using Xamarin.Essentials;
using Stoica_Ramona_Lab7.Data;
using static Xamarin.Essentials.Permissions;
using Permissions = Microsoft.Maui.ApplicationModel.Permissions;
using PermissionStatus = Microsoft.Maui.ApplicationModel.PermissionStatus;
using Geolocation = Microsoft.Maui.Devices.Sensors.Geolocation;
using MapLaunchOptions = Microsoft.Maui.ApplicationModel.MapLaunchOptions;
using Map = Microsoft.Maui.ApplicationModel.Map;

namespace Stoica_Ramona_Lab7
{
    public partial class App : Application
    {

        static ShoppingListDatabase database;
        public static ShoppingListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   ShoppingListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "ShoppingList.db3"));
                }
                return database;
            }
        }

        public object MapsApiKeyHandler { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
        }

  

        private async void UseMapsFunctionality()
        {
            try
            {
                // Request permission (if needed) for using location services
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    // Handle the case when permission is not granted
                    return;
                }

                // Get the location
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    // Open the map centered on the obtained location
                    var options = new MapLaunchOptions { Name = "My Location" };
                    await Map.OpenAsync(location, options);
                }
            }
            catch (Exception ex)
            {
                // Handle exception, show an error message, or log the error
                Console.WriteLine("Error using map functionality: " + ex.Message);
            }
        }
    }
}
    
