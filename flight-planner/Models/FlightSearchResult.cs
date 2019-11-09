using System.Collections.Generic;

namespace flight_planner.Models
{
    public class FlightSearchResult
    {
        public  int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight>Items { get; set; }
    }
}