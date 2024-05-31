using AndreyKursovaya.Models.Entities;
using System.Collections.Generic;

namespace AndreyKursovaya.Models.Repository;

public interface IRepository<T> where T : DomainEntity
{
    void Add(T entity);
    void Delete(T entity);    
    IEnumerable<T> GetAll();
    void Update(T newEntity, int Id);
}
