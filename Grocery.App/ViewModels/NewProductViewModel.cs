using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;


namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int stock;

        [ObservableProperty]
        private decimal price;

        [ObservableProperty]
        private DateTime shelfLife = DateTime.Now;

        public NewProductViewModel(IProductService productService)
        {
            _productService = productService;
        }

        [RelayCommand]
        private async Task SaveProductAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || Price <= 0 || Stock < 0)
            {
                await Shell.Current.DisplayAlert("Ongeldige invoer", "Vul alle velden correct in.", "OK");
                return;
            }

            var newProduct = new Product(0, Name, Stock, DateOnly.FromDateTime(ShelfLife), Price);

            _productService.Add(newProduct);

            await Shell.Current.DisplayAlert("Succes", "Product is aangemaakt!", "OK");

            await Shell.Current.GoToAsync("ProductView");
        }
    }
}