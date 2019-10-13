using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.core.Services;
using flight_planner.Models;
using AutoMapper;
using Microsoft.Ajax.Utilities;

namespace flight_planner.Controllers
{
    public class CustomerApiController : BaseApiController
    {
        private readonly IAirportService _airportService;

        public CustomerApiController(IAirportService airportService, IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {
            _airportService = airportService;
        }

        // GET: api/CustomerApiController
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            lock (FlightStorage.GetFlights())
            {
                var flight = FlightStorage.GetFlightById(id);
                if (flight == null)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound);
                }
                return request.CreateResponse(HttpStatusCode.OK, flight);
            }
        }

        // GET: api/CustomerApiController/5
        [HttpGet]
        [Route("api/airports")]
        public async Task<IHttpActionResult> GetAirports(string search)
        {
            var airport = await _airportService.SearchAirports(search);
            return Ok(airport.Select(ConvertAirportFromDomain).ToHashSet());
        }


        [HttpPost]
        [Route("api/flights/search")]
        // POST: api/CustomerApiController
        public async Task<IHttpActionResult> FlightSearch(FlightSearchRequest search)
        {
            if (!IsValid(search) || !NotSameAirport(search))
                return BadRequest();
            var result = await _flightService.GetFlight();
            var matchedItems = result.Where(f => f.From.AirportCode.ToLower().Contains(search.From.ToLower()) ||
                                                     f.To.AirportCode.ToLower().Contains(search.To.ToLower()) ||
                                                     DateTime.Parse(f.DepartureTime) ==                                                  
                                                      DateTime.Parse(search.DepartureDate)).DistinctBy(f => f.Carrier).ToList();
            var response = new FlightSearchResult
                {
                    TotalItems = matchedItems.Count,
                    Items = matchedItems,
                    Page = matchedItems.Any() ? 1 : 0
                };
                         
            return Ok(response);
        }

        private bool NotSameAirport(FlightSearchRequest search)
        {
            return !string.Equals(search.From, search.To, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsValid(FlightSearchRequest search)
        {
            return search != null && !string.IsNullOrEmpty(search.From) && !string.IsNullOrEmpty(search.To) &&
                   !string.IsNullOrEmpty(search.DepartureDate);
        }


        [HttpGet]
        [Route("api/flights/{id}")]
        public async Task<HttpResponseMessage> FlightSearchById(HttpRequestMessage request, int id)
        {
            var flight = await _flightService.GetFlightById(id);
            if (flight == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, ConvertFromDomain(flight));
        }

        // PUT: api/CustomerApiController/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CustomerApiController/5
        public void Delete(int id)
        {
        }
    }
}
