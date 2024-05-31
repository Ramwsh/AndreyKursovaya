using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyKursovaya.Models.Entities;

[Table("Продажи")]
public class Sale : DomainEntity
{
    [Key]
    public int SaleID { get; set; }
    public int ProductID { get; set; }

    public DateTime SaleDate { get; set; }

    public int QuantitySaleProduct { get; set; }

    public double SalePrice { get; set; }
}
