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
using flight_planner.core.Services;
using AutoMapper;

namespace flight_planner.Controllers 
{
    [BasicAuthentication]
    public class AdminApiController : BaseApiController
    {
   
        public AdminApiController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
        }
        // GET: api/AdminApi
       /* public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/AdminApi/5
        [HttpGet]
        [Route("admin-api/flights/{id}")]
      
        public async Task<IHttpActionResult> Get(int id)
        {
            var flight = await _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FlightRequest>(flight));
        }


        [HttpPut]
        [Route("admin-api/flights")]
        // PUT: api/AdminApi/5
        public async Task<IHttpActionResult> AddFlight(FlightRequest flight)
        {
            if (!IsValid(flight))
            {
                return BadRequest();
            }
            
                var result = await _flightService.AddFlight(_mapper.Map<Flight>(flight));
                if (!result.Succeeded)
                {
                    return Conflict();
                }
                flight.Id = result.Entity.Id;
                return Created(string.Empty, flight);
        }

        [HttpGet]
        [Route("admin-api/get/flights")]
        public async Task<IHttpActionResult> GetFlights()
        {
            var flights = await _flightService.GetFlight();
            return Ok(flights.Select(f => _mapper.Map<FlightRequest>(f)).ToList());
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
           await _flightService.DeleteFlightById(id);
           return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
