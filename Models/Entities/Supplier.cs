using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyKursovaya.Models.Entities;

[Table("Поставщик")]
public class Supplier : DomainEntity
{
    [Key]
    public int SupplierID { get; set; }
    public string SupplierName { get; set; }
    public string SupplierAddress { get; set; }
    public int SupplierPhone { get; set; }
    public string SupplierContactPerson { get; set; }
}
