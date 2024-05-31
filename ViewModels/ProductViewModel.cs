using AndreyKursovaya.Models.Entities;
using AndreyKursovaya.Models.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AndreyKursovaya.ViewModels;

public partial class ProductViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _productId;

    [ObservableProperty]
    private string _productName;

    [ObservableProperty]
    private ObservableCollection<Product> _products;

    [ObservableProperty]
    private Product _selectedProduct;

    public ProductViewModel()
    {
        try { Products = new(new Repository<Product>().GetAll()); }
        catch { Products = new(); }
    }

    partial void OnSelectedProductChanged(Product value)
    {
        if (value != null)
        {
            ProductId = value.ProductID;
            ProductName = value.ProductName;
        }
    }

    [RelayCommand]
    private void CreateProduct()
    {
        if (ProductId != default && !string.IsNullOrEmpty(ProductName))
        {
            Product product = new Product() { ProductID = ProductId, ProductName = ProductName };
            if (!Products.Contains(product))
            {
                IRepository<Product> repository = new Repository<Product>();
                repository.Add(product);
                Products.Add(product);
            }
        }
    }

    [RelayCommand]
    private void DeleteProduct()
    {
        if (SelectedProduct != null)
        {
            IRepository<Product> repository = new Repository<Product>();
            repository.Add(SelectedProduct);
            Products.Remove(SelectedProduct);
        }
    }

    [RelayCommand]
    private void UpdateProduct()
    {
        if (SelectedProduct != null)
        {
            Product product = new Product() { ProductID = SelectedProduct.ProductID, ProductName = ProductName };
            IRepository<Product> repository = new Repository<Product>();
            repository.Update(product, SelectedProduct.ProductID);
            int index = Products.IndexOf(product);
            Products[index] = product;
        }
    }
}
