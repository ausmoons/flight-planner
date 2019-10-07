using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_planner.services
{
    public class ServiceResult
    {
        public ServiceResult(int id, bool succeeded)
        {
            Id = id;
            Succeeded = succeeded;
        }

        public ServiceResult(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public int Id { get; }
        public bool Succeeded { get; }
    }
}
