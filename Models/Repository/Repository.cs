using AndreyKursovaya.Models.Context;
using AndreyKursovaya.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreyKursovaya.Models.Repository;

public class Repository<T> : IRepository<T> where T : DomainEntity
{     
    public void Add(T entity)
    {
        using (ApplicationContext<T> Context = new())
        {
            Context.Items.Add(entity);
            Context.SaveChanges();
        }            
    }

    public void Delete(T entity)
    {
        using (ApplicationContext<T> Context = new())
        {
            Context.Items.Remove(entity);
            Context.SaveChanges();
        }        
    }

    public IEnumerable<T> GetAll()
    {
        using (ApplicationContext<T> Context = new())
        {            
            return Context.Items.ToList();
        }
    }    

    public void Update(T newEntity, int Id)
    {
        using (ApplicationContext<T> Context = new())
        {
            T oldEntity = Context.Items.Find(Id);
            if (oldEntity != null)
            {
                Context.Items.Remove(oldEntity);
                Context.Items.Add(newEntity);
                Context.SaveChanges();
            }
        }        
    }    
}
