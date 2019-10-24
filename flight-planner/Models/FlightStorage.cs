using System.Collections.Generic;
using System.Linq;


namespace flight_planner.Models
{
    public static class FlightStorage
    {
        private static int _id;

        private static readonly object ListLock = new object();
        private static SynchronizedCollection<Flight> _flights { get; set; }

        static FlightStorage()
        {
            _flights = new SynchronizedCollection<Flight>();
            _id = 1;
        }
        
        public static Flight[] GetFlights()
        {
            return _flights.ToArray();
        }


        public static bool AddFlight(Flight flight)
        {
            lock (ListLock)
            {
                if (!_flights.Any(f => f.Equals(flight)))
                {
                    _flights.Add(flight);
                    return true;
                }
                return false;
            }
        }


        public static void RemoveFlight(Flight flight)
         {
             _flights.Remove(flight);
         }


        public static Flight GetFlightById(int id)
        {
            lock (ListLock)
            {
                var flight = _flights.FirstOrDefault(f => f.Id == id);
                return flight;
            }
        }
        

        public static void RemoveFlightById(int id)
        {
            var flight = GetFlightById(id);
                if (flight != null)
                {
                    _flights.Remove(flight);
                }

        }

        public static void ClearList()
        {
            _flights.Clear();
        }


        public static int GetId()
        {
            return _id++;
        }

    }
}