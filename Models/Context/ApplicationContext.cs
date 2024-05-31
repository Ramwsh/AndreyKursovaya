using AndreyKursovaya.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AndreyKursovaya.Models.Context;

public class ApplicationContext<T> : DbContext where T : DomainEntity
{
    public DbSet<T> Items { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionSting = DbConnectionHelper.ConnectionString;        
        optionsBuilder.UseJet(connectionSting);
    }
}
