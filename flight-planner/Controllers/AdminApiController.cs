using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.Attribute;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        private Random _random;
     

        public AdminApiController()
        {
            _random = new Random();
        }
        // GET: api/AdminApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AdminApi/5
        [HttpGet]
        [Route("admin-api/flights/{id}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            var flight = FlightStorage.GetFlightById(id);
            if (flight == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound, flight);
            }

            return request.CreateResponse(HttpStatusCode.OK, flight);
        }

        // POST: api/AdminApi
        public void Post([FromBody]string value)
        {
        }
        [HttpPut]
        [Route("admin-api/flights")]
        // PUT: api/AdminApi/5
        public async Task<HttpResponseMessage> AddFlight(HttpRequestMessage request, Flight flight)
        {

            if (!IsValid(flight))
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, flight);
            }


            flight.Id = FlightStorage.GetId();
            

            
            if (!FlightStorage.AddFlight(flight))
            {
                return request.CreateResponse(HttpStatusCode.Conflict, flight);
            }

            return request.CreateResponse(HttpStatusCode.Created, flight);
        }

        private static bool IsValid(Flight flight)
        {

            return !string.IsNullOrEmpty(flight.ArrivalTime) &&
                   !string.IsNullOrEmpty(flight.DepartureTime) &&
                   !string.IsNullOrEmpty(flight.Carrier) &&
                   IsValidAirport(flight.From) && IsValidAirport(flight.To) &&
                   ValidateDates(flight.DepartureTime, flight.ArrivalTime) && 
                   IsDifferentAirport(flight.From, flight.To);
        }


        private static bool IsValidAirport(AirportRequest airport)
        {
            return airport != null &&
                   !string.IsNullOrEmpty(airport.Airport) &&
                   !string.IsNullOrEmpty(airport.City) &&
                   !string.IsNullOrEmpty(airport.Country);
        }

        private static bool IsDifferentAirport(AirportRequest airportFrom, AirportRequest airtportTo)
        {
            return !airportFrom.Airport.ToLower().Equals(airtportTo.Airport.ToLower()) &&
                   !airportFrom.City.ToLower().Equals(airtportTo.City.ToLower());
            //!airportFrom.Country.ToLower().Equals(airtportTo.Country.ToLower());
        }

        private static bool ValidateDates(string departure, string arrival)
        {
            if (!string.IsNullOrEmpty(departure) && !string.IsNullOrEmpty(arrival))
            {
                var departureDate = DateTime.Parse(departure);
                var arrivalDate = DateTime.Parse(arrival);
                return DateTime.Compare(arrivalDate, departureDate) > 0;
            }

            return false;
        }

        [HttpDelete]
        [Route("admin-api/flights/{id}")]
        // DELETE: api/AdminApi/5
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, int id)
        {

            FlightStorage.RemoveFlightById(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
