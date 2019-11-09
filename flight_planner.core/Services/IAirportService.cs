using flight_planner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace flight_planner.core.Services
{
    public interface IAirportService : IEntityService<Airport> //
    {
        Task<IEnumerable<Airport>> SearchAirports(string search);

    }
}
