
using Stoica_Ramona_Lab7.Models;
using System.Xml.Linq;

namespace Stoica_Ramona_Lab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem != null)
        {
            // Get the selected product from the ListView
            Product selectedProduct = listView.SelectedItem as Product;

            // Delete the selected product from the database
            var result = await App.Database.DeleteProductAsync(selectedProduct);

            if (result > 0)
            {
                // Refresh the ListView after deletion
                var shopList = (ShopList)BindingContext;
                listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
            }
            else
            {
                // Handle the case where deletion was unsuccessful
                // You can display a message or perform any necessary actions
            }
        }
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
 
    }

