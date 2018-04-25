using System;
using System.Collections.Generic;
using System.Linq;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class Event : BaseEntity
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
        public int CoordinatorId { get; set; }
        public string Organization { get; set; }
    }
}