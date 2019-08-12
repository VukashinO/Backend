using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(Guid id);
        void Create(T obj);
        void Update(T obj);
        void Delete(T obj);
        void SaveChanges();
    }
}
