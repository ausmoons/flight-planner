using System.Web.Http;
using AutoMapper;
using flight_planner.core.Services;
using flight_planner.Models;
using flight_planner.services;
namespace flight_planner.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly IFlightService _flightService;

        protected readonly IMapper _mapper;

        public BaseApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        protected Flight ConvertFlightToDomain(FlightRequest flightRequest)
        {
            return new Flight
            {
                Id = flightRequest.Id,
                To = ConvertAirportToDomain(flightRequest.To),
                From = ConvertAirportToDomain(flightRequest.From),
                Carrier = flightRequest.Carrier,
                ArrivalTime = flightRequest.ArrivalTime,
                DepartureTime = flightRequest.DepartureTime
            };
        }

        private Airport ConvertAirportToDomain(AirportRequest airportRequest)
        {
            return new Airport
            {
                City = airportRequest.City,
                Country = airportRequest.Country,
                AirportCode = airportRequest.Airport
            };
        }

        protected FlightRequest ConvertFromDomain(Flight flight)
        {
            return new FlightRequest
            {
                Id = flight.Id,
                To = ConvertAirportFromDomain(flight.To),
                From = ConvertAirportFromDomain(flight.From),
                Carrier = flight.Carrier,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime
            };
        }

        protected AirportRequest ConvertAirportFromDomain(Airport airport)
        {
            return new AirportRequest
            {
                City = airport.City,
                Airport = airport.AirportCode,
                Country = airport.Country
            };
        }
    }
}