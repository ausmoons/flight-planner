using flight_planner.core.Models;

namespace flight_planner.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AirportCode { get; set; }
    }
}