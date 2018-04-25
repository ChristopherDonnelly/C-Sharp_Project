using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class Location : BaseEntity
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }
}