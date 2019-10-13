using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner.core.Services;
using flight_planner.data;
using flight_planner.Models;

namespace flight_planner.services
{
   public class FlightService  : EntityService<Flight>, IFlightService
    {
        //private static readonly object ListLock = new object();
        public FlightService(IFlightPlannerDbContext contex) : base(contex) { }
       

        public async Task<ServiceResult> AddFlight(Flight flight)
        {         
                if (await FlightExists(flight))
                {
                    return new ServiceResult(false);
                }

            return Creat(flight);
        }

        public async Task<bool> FlightExists(Flight flight)
        {
           return await Query().AnyAsync(f =>
                    f.Carrier == flight.Carrier && f.ArrivalTime == flight.ArrivalTime &&
                    f.DepartureTime == flight.DepartureTime &&
                    f.From.City == flight.From.City &&
                    f.From.Country == flight.From.Country &&
                    f.To.AirportCode == flight.To.AirportCode &&
                    f.To.City == flight.To.City &&
                    f.To.Country == flight.To.Country &&
                    f.From.AirportCode == flight.From.AirportCode);
        }

        public async Task<Flight> GetFlightById(int id)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var flight = await context.Flights.Include(f => f.To).Include(f => f.From)
                    .SingleOrDefaultAsync(f => f.Id == id);
                return flight;
            }
        }

        public async Task DeleteFlights()
        {
            _ctx.Flights.RemoveRange(_ctx.Flights);
            _ctx.Airports.RemoveRange(_ctx.Airports);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ServiceResult> DeleteFlightById(int id)
        {
            var flight = await GetById(id);
            return flight == null ? new ServiceResult(true) : Delete(flight);
        }

        public async Task<IEnumerable<Flight>> GetFlight()
        {
            return await Task.FromResult(Get());
        }


    }
}
