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
using flight_planner.services;

namespace flight_planner.Controllers 
{
    [BasicAuthentication]
    public class AdminApiController : BaseApiController
    {
        private static readonly object ListLock = new object();

        //private readonly FlightService _flightService;

        public AdminApiController()
        {
            _flightService = new FlightService();
        }

        // GET: api/AdminApi/5
        [HttpGet]
        [Route("admin-api/flights/{id}")]
        //public  HttpResponseMessage Get(HttpRequestMessage request, int id)
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request, int id)
        {
            var flight = await _flightService.GetFlightById(id);
            if (flight == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound, flight);
            }
            return request.CreateResponse(HttpStatusCode.OK, ConvertFromDomain(flight));
        }

        public async Task<IHttpActionResult> AddFlight(FlightRequest flight)
        {
            if (!IsValid(flight))
            {
                return BadRequest();
            }
            lock (ListLock)
            {
                var result = _flightService.AddFlight(ConvertFlightToDomain(flight));
                if (!result.Succeeded)
                {
                    return Conflict();
                }
                flight.Id = result.Id;
                return Created(string.Empty, flight);
            }
        }

        [HttpGet]
        [Route("admin-api/get/flights")]
        public async Task<IHttpActionResult> GetFlights()
        {
            var flight = await _flightService.GetFlights();
            return Ok(flight.Select(ConvertFromDomain).ToList());
        }

        private static bool IsValid(FlightRequest flight)
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

        private static bool IsDifferentAirport(AirportRequest airportFrom, AirportRequest airportTo)
        {
            return !airportFrom.Airport.ToLower().Equals(airportTo.Airport.ToLower()) &&
                   !airportFrom.City.ToLower().Equals(airportTo.City.ToLower());
           
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

           await _flightService.RemoveFlightsById(id);
           return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
