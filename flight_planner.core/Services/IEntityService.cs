﻿using flight_planner.core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flight_planner.core.Services
{
    public interface IEntityService<T> where T : Entity 
    {
        IQueryable<T> Query();
        IQueryable<T> QueryById(int id);
        IEnumerable<T> Get();
        Task<T> GetById(int id);
        ServiceResult Creat(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Update(T entity);
        bool Exists(int id);
    }
}