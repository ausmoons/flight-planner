using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    public class TestingApiController : BaseApiController
    {
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
