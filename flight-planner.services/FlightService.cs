using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flight_planner.data;
using flight_planner.Models;

namespace flight_planner.services
{
   public class FlightService
    {
        private static readonly object ListLock = new object();

        public async Task<ICollection<Flight>> GetFlights()
        {

            using (var context = new FlightPlannerDbContext()) 
            {
                return await context.Flights.Include(f => f.To).Include(f => f.From).ToListAsync();
            }
        }

        public ServiceResult AddFlight(Flight flight)
        {
            using (var context = new FlightPlannerDbContext())
            {
                if (Exists(flight))
                {
                    return new ServiceResult(false);
                }
                else
                {
                    flight = context.Flights.Add(flight);
                    context.SaveChanges();
                    return new ServiceResult(flight.Id, true);
                }
            }
        }

        public bool Exists(Flight flight)
        {
            using (var context = new FlightPlannerDbContext())
            {
                var exist = context.Flights.Any(f =>
                    f.Carrier == flight.Carrier && f.ArrivalTime == flight.ArrivalTime &&
                    f.DepartureTime == flight.DepartureTime &&
                    f.From.City == flight.From.City &&
                    f.From.Country == flight.From.Country &&
                    f.To.AirportCode == flight.To.AirportCode &&
                    f.To.City == flight.To.City &&
                    f.To.Country == flight.To.Country &&
                    f.From.AirportCode == flight.From.AirportCode);


                return exist;
            }
        }


        public async Task<ICollection<Airport>> SearchAirports(string search)
        {
            search = search.ToLowerInvariant().Trim();
            using (var context = new FlightPlannerDbContext())
            {
                var query = context.Airports.Where(a =>
                    a.City.ToLower().Contains(search) || a.Country.ToLower().Contains(search)
                                                      || a.AirportCode.ToLower().Contains(search));
                return await query.ToListAsync();
            }
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
            using (var context = new FlightPlannerDbContext())
            {
                context.Flights.RemoveRange(context.Flights);
                context.Airports.RemoveRange(context.Airports);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveFlightsById(int id)
        {
            using (var context = new FlightPlannerDbContext())
            {

                var flight = await context.Flights.SingleOrDefaultAsync(f => f.Id == id);

                if (flight != null)
                {
                    context.Flights.Remove(flight);
                    await context.SaveChangesAsync();
                }

            }
        }

    }
}
