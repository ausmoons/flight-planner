using flight_planner.core.Models;
using flight_planner.core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flight_planner.data;
using System.Data.Entity;

namespace flight_planner.services
{
    public class DbService : IDbService
    {
        protected readonly IFlightPlannerDbContext _ctx;

        public DbService(IFlightPlannerDbContext context)
        {
            _ctx = context; //atspouļo datubāzi
        }

        public ServiceResult Creat<T>(T entity) where T : Entity //zem Entity ir arī flight un airport
        {
           if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _ctx.Set<T>().Add(entity);
            _ctx.SaveChanges();
            return new ServiceResult(true).Set(entity);
        }

        public ServiceResult Delete<T>(T entity) where T : Entity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _ctx.Set<T>().Remove(entity);
            _ctx.SaveChanges();
            return new ServiceResult(true);
        }

        public bool Exists<T>(int id) where T : Entity
        {
            return QueryById<T>(id).Any();
        }

        public IEnumerable<T> Get<T>() where T : Entity
        {
            return Query<T>().ToList();
        }

        public virtual Task<T> GetById<T>(int id) where T : Entity //tagad var neizmantot virtual, bet nākotnē noderētu
        {
            return _ctx.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual IQueryable<T> Query<T>() where T : Entity
        {
            return _ctx.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> QueryById<T>(int id) where T : Entity
        {
            return Query<T>().Where(t => t.Id == id);
        }

        public ServiceResult Update<T>(T entity) where T : Entity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();

            return new ServiceResult(true).Set(entity);
        }
    }
}
