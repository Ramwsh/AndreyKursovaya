using AndreyKursovaya.Models.Entities;
using AndreyKursovaya.Models.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace AndreyKursovaya.ViewModels;

public partial class SupplierViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _supplierId;

    [ObservableProperty]
    private string _supplierName;

    [ObservableProperty]
    private string _supplierAddress;

    [ObservableProperty]
    private int _supplierPhone;

    [ObservableProperty]
    private string _supplierContactPerson;

    [ObservableProperty]
    private ObservableCollection<Supplier> _suppliers;

    [ObservableProperty]
    private Supplier _selectedSupplier;

    public SupplierViewModel()
    {
        try { Suppliers = new(new Repository<Supplier>().GetAll().ToList()); }
        catch { Suppliers = new(); }
    }

    [RelayCommand]
    private void CreateSupplier()
    {
        int[] defaultConditions = { SupplierId, SupplierPhone };
        string[] stringConditions = { SupplierName, SupplierAddress, SupplierContactPerson };
        if (defaultConditions.All(item => item != default) && stringConditions.All(item => !string.IsNullOrWhiteSpace(item)))
        {
            Supplier supplier = new Supplier() { SupplierID = SupplierId, SupplierPhone = SupplierPhone, SupplierName = SupplierName, SupplierAddress = SupplierAddress, SupplierContactPerson = SupplierContactPerson };
            if (!Suppliers.Contains(supplier))
            {
                IRepository<Supplier> repository = new Repository<Supplier>();
                repository.Add(supplier);
                Suppliers.Add(supplier);
            }
        }
    }

    [RelayCommand]
    private void DeleteSupplier()
    {
        if (SelectedSupplier != null)
        {
            IRepository<Supplier> repository = new Repository<Supplier>();
            repository.Delete(SelectedSupplier);
            Suppliers.Remove(SelectedSupplier);
        }
    }

    [RelayCommand]
    private void UpdateSupplier()
    {
        int[] defaultConditions = { SupplierId, SupplierPhone };
        string[] stringConditions = { SupplierName, SupplierAddress, SupplierContactPerson };
        if (defaultConditions.All(item => item != default) && stringConditions.All(item => !string.IsNullOrWhiteSpace(item)) && SelectedSupplier != null)
        {            
            Supplier supplier = new Supplier() { SupplierID = SelectedSupplier.SupplierID, SupplierPhone = SupplierPhone, SupplierName = SupplierName, SupplierAddress = SupplierAddress, SupplierContactPerson = SupplierContactPerson };
            IRepository<Supplier> repository = new Repository<Supplier>();
            repository.Update(supplier, SelectedSupplier.SupplierID);
            int index = Suppliers.IndexOf(SelectedSupplier);
            Suppliers[index] = supplier;
        }
    }

    partial void OnSelectedSupplierChanged(Supplier value)
    {
        if (value != null)
        {
            SupplierId = value.SupplierID;
            SupplierPhone = value.SupplierPhone;
            SupplierName = value.SupplierName;
            SupplierAddress = value.SupplierAddress;
            SupplierContactPerson = value.SupplierContactPerson;
        }
    }
}
