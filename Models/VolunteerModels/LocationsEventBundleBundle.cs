using System.Collections.Generic;

namespace VolunteerPlanner.Models
{
    public class LocationsEventBundleModel
    {
        public Event Event  { get; set; }
        public List<Location> Locations { get; set; }
    }
}