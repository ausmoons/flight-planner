using flight_planner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace flight_planner.core.Services
{
   public interface IFlightService : IEntityService<Flight> 
    {
        Task<IEnumerable<Flight>> GetFlight();
        Task<ServiceResult> AddFlight(Flight flight);
        Task<Flight> GetFlightById(int id);
        Task<ServiceResult> DeleteFlightById(int id);
        Task<bool> FlightExists(Flight flight);
        Task DeleteFlights();


    }
}
