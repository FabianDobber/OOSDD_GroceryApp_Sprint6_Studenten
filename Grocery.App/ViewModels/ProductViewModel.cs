using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.App.Views;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        [ObservableProperty]
        private ObservableCollection<Product> products;

        [ObservableProperty]
        public bool isAdmin;

        public ProductViewModel(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
            products = new ObservableCollection<Product>();

            IsAdmin = _authService.CurrentUser?.Role == Role.Admin;

            LoadProducts();
        }

        [RelayCommand]
        private void LoadProducts()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Products.Clear();
                var productList = _productService.GetAll();
                foreach (Product p in productList)
                {
                    Products.Add(p);
                }
            });
        }

        [RelayCommand]
        private async Task GoToNewProductAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewProductView));
        }
    }
}