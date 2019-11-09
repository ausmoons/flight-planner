using flight_planner.core.Interfaces;
using System.Collections.Generic;

namespace flight_planner.core.Services
{
    public class ServiceResult
    {

        public bool Succeeded { get; private set; }

        public ServiceResult(int id, bool succeeded)
        {
            Id = id;
            Succeeded = succeeded;
        }

        public ServiceResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public ServiceResult(IEnumerable<string> errors) 
        {
           Set(errors);
        }

        public int Id { get; }


        public IEntity Entity { get; private set; }

        
        private List<string> errors = new List<string>();
        public ServiceResult Add(IEnumerable<string> errors)
        {
            foreach (string err in errors)
            {
                if (!string.IsNullOrEmpty(err))
                    this.errors.Add(err);
            }
            return this;
        }

        public ServiceResult Set(IEnumerable<string> errors)
        {
            this.errors.Clear();
            Add(errors);
            return this;
        }

        public ServiceResult Set(params string[] errors)
        {
            this.errors.Clear();
            Add(errors);
            return this;
        }
        public ServiceResult Set(bool success)
        {
            Succeeded = success;
            return this;
        }

        public ServiceResult Set(IEntity entity)
        {
            Entity = entity;
            return this;
        }

    }
}
