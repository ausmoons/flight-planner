﻿using flight_planner.core.Models;

namespace flight_planner.Models
{
    public class Flight : Entity
    {
        public virtual Airport From { get; set; }
        public virtual Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}