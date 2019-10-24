using System.Data.Entity;
using flight_planner.data.Migrations;
using flight_planner.Models;

namespace flight_planner.data
{
    public class FlightPlannerDbContext : DbContext , IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("flight-planner")
        {
            Database.SetInitializer<FlightPlannerDbContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FlightPlannerDbContext, Configuration>());
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }
    }
}


