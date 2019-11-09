using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.core.Models;
using flight_planner.core.Services;
using flight_planner.Models;
using flight_planner.services;
using AutoMapper;

namespace flight_planner.Controllers
{
    public class TestingApiController : BaseApiController
    {
        public TestingApiController(IFlightService flightService, IMapper mapper) : base(flightService, mapper)
        {

        }

        [HttpPost]
        [Route("testing-api/clear")]
        public async Task<bool> Clear()
        {
            await _flightService.DeleteFlights();
            return true;
        }
        

        public string Get()
        {
            return "Testing api";
        }
    }
}
