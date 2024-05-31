using AndreyKursovaya.Models.Entities;
using AndreyKursovaya.Models.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace AndreyKursovaya.ViewModels
{
    public partial class SupplyViewModel : ViewModelBase
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _supplierId;

        [ObservableProperty]
        private int _productId;

        [ObservableProperty]
        private DateTimeOffset _supplyDate;        

        [ObservableProperty]
        private int _quantityProduct;

        [ObservableProperty]
        private double _supplyPrice;

        [ObservableProperty]
        private ObservableCollection<Supply> _supplies;

        [ObservableProperty]
        private Supply _selectedSupply;

        public SupplyViewModel()
        {
            try { Supplies = new ObservableCollection<Supply>(new Repository<Supply>().GetAll().ToList()); }            
            catch { Supplies = new(); }
            SupplyDate = new DateTimeOffset(DateTime.Now);            
        }
        


        [RelayCommand]
        private void CreateSupply()
        {
            int[] defaultConditions = { Id, SupplierId, ProductId, SupplyDate.DateTime.Day, SupplyDate.DateTime.Month, SupplyDate.DateTime.Year, QuantityProduct, (int)SupplyPrice };   
            foreach (var data in defaultConditions)
            {
                Console.WriteLine(data);
            }
            if (defaultConditions.All(item => item != default))
            {
                Supply item = new Supply() { SupplyID = Id, SupplierID = SupplierId, ProductID = ProductId, QuantityProduct = QuantityProduct, SupplyPrice = SupplyPrice, SupplyDate = SupplyDate.DateTime };
                Console.WriteLine("Object created");
                if (!Supplies.Contains(item))
                {
                    IRepository<Supply> repository = new Repository<Supply>();                    
                    repository.Add(item);
                    Supplies.Add(item);
                    Console.WriteLine("Object added");
                }
            }            
        }

        [RelayCommand]
        private void DeleteSupply()
        {
            if (SelectedSupply != null)
            {
                IRepository<Supply> repository = new Repository<Supply>();
                repository.Delete(SelectedSupply);
                Supplies.Remove(SelectedSupply);
            }
        }

        [RelayCommand]
        private void UpdateSupply()
        {
            int[] defaultConditions = { Id, SupplierId, ProductId, SupplyDate.DateTime.Day, SupplyDate.DateTime.Month, SupplyDate.DateTime.Year, QuantityProduct, (int)SupplyPrice };
            if (defaultConditions.All(item => item != default) && SelectedSupply != null)
            {
                IRepository<Supply> repository = new Repository<Supply>();
                Supply item = new Supply() { SupplyID = SelectedSupply.SupplyID, SupplierID = SupplierId, ProductID = ProductId, QuantityProduct = QuantityProduct, SupplyPrice = SupplyPrice, SupplyDate = SupplyDate.DateTime };
                repository.Update(item, SelectedSupply.SupplyID);
                int index = Supplies.IndexOf(SelectedSupply);
                Supplies[index] = item;
            }
        }

        partial void OnSelectedSupplyChanged(Supply value)
        {
            if (value != null)
            {
                Id = value.SupplyID;
                SupplierId = value.SupplierID;
                ProductId = value.ProductID;
                QuantityProduct = value.QuantityProduct;
                SupplyPrice = value.SupplyPrice;
                SupplyDate = value.SupplyDate;
            }            
        }
    }
}
