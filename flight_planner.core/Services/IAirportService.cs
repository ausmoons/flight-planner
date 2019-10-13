using flight_planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_planner.core.Services
{
   public interface IAirportService : IEntityService<Airport>
    {
        Task<IEnumerable<Airport>> SearchAirports(string search);

    }
}
