﻿using flight_planner.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_planner.core.Models
{
    public abstract class Entity : IEntity 
    {
        public int Id { get; set; } //kopīgā lieta abiem. Abstrakta, jo pašu neizmanto, bet no viņas manto
    }
}
