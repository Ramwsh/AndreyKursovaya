using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyKursovaya.Models.Entities;

[Table("Поставка")]
public class Supply : DomainEntity
{
    [Key]
    public int SupplyID { get; set; }
    public int SupplierID { get; set; }
    public int ProductID { get; set; }
    public DateTime SupplyDate { get; set; }
    public int QuantityProduct { get; set; }
    public double SupplyPrice { get; set; }
}
