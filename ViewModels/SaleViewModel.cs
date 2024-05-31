using AndreyKursovaya.Models.Entities;
using AndreyKursovaya.Models.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AndreyKursovaya.ViewModels;

public partial class SaleViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _saleId;

    [ObservableProperty]
    private int _productId;

    [ObservableProperty]
    private DateTimeOffset _saleDate;

    [ObservableProperty]
    private int _quantitySaleProduct;

    [ObservableProperty]
    private double _salePrice;

    [ObservableProperty]
    private ObservableCollection<Sale> _sales;

    [ObservableProperty]
    private Sale _selectedSale;

    public SaleViewModel()
    {
        try { Sales = new(new Repository<Sale>().GetAll().ToList()); }
        catch { Sales = new(); }
        SaleDate = new DateTimeOffset(DateTime.Now);
    }

    [RelayCommand]
    private void CreateSale()
    {
        int[] defaultConditions = { SaleId, ProductId, SaleDate.Day, SaleDate.Month, SaleDate.Year, (int)SalePrice };
        if (defaultConditions.All(item => item != default))
        {
            Sale sale = new Sale() { SaleID = SaleId, ProductID = ProductId, SaleDate = SaleDate.DateTime, QuantitySaleProduct = QuantitySaleProduct, SalePrice = SalePrice };
            if (!Sales.Contains(sale))
            {
                IRepository<Sale> repository = new Repository<Sale>();
                repository.Add(sale);
                Sales.Add(sale);
            }
        }
    }

    [RelayCommand]
    private void DeleteSale()
    {
        if (SelectedSale != null)
        {
            IRepository<Sale> repository = new Repository<Sale>();
            repository.Delete(SelectedSale);
            Sales.Remove(SelectedSale);
        }
    }

    [RelayCommand]
    private void UpdateSale()
    {
        if (SelectedSale != null)
        {
            Sale sale = new Sale() { SaleID = SelectedSale.SaleID, ProductID = ProductId, SaleDate = SaleDate.DateTime, QuantitySaleProduct = QuantitySaleProduct, SalePrice = SalePrice };
            IRepository<Sale> repository = new Repository<Sale>();            
            repository.Update(sale,SelectedSale.SaleID);
            int index = Sales.IndexOf(SelectedSale);
            Sales[index] = sale;   
        }
    }

    partial void OnSelectedSaleChanged(Sale value)
    {
        if (value != null)
        {
            SaleId = value.SaleID;
            ProductId = value.ProductID;
            SaleDate = value.SaleDate;
            QuantitySaleProduct = value.QuantitySaleProduct;
            SalePrice = value.SalePrice;
        }
    }
}
