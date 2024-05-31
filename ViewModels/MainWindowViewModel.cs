using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;

namespace AndreyKursovaya.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase currentPage = new SupplyViewModel();

    [ObservableProperty]
    private ListItemTemplate? selectedTemplate;

    partial void OnSelectedTemplateChanged(ListItemTemplate? value)
    {
        if (value is null)
        {
            return;
        }
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null)
        {
            return;
        }
        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> Templates { get; } = new()
    {
        new ListItemTemplate(typeof(SupplyViewModel), "OrganizationRegular", "Поставки"),
        new ListItemTemplate(typeof(SupplierViewModel), "PersonBoardRegular", "Поставщик"),
        new ListItemTemplate(typeof(SaleViewModel), "MoneyRegular", "Продажи"),
        new ListItemTemplate(typeof(ProductViewModel), "DrinkWineRegular", "Товары"),
    };

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string iconKey, string title)
    {
        ModelType = type;
        Label = title;

        Application.Current!.TryFindResource(iconKey, out var res);
        Icon = (StreamGeometry)res!;
    }

    public string Label { get; set; }
    public Type ModelType { get; set; }
    public StreamGeometry Icon { get; set; }
}
