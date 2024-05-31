using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AndreyKursovaya.Models.Entities;

[Table("Товар")]
public class Product : DomainEntity
{
    [Key]
    public int ProductID { get; set; }
    public string ProductName { get; set; }
}
