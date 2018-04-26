using System.ComponentModel.DataAnnotations;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class Location : BaseEntity
    {
        public int LocationId { get; set; }

        [Display(Name = "Location Name: ")]
        [Required(ErrorMessage = "Location Name is required!")]
        [MinLength(2, ErrorMessage = "Location Name must contain at least 2 characters!")]
        public string Name { get; set; }

        [Display(Name = "Latitude: ")]
        public string Lat { get; set; }

        [Display(Name = "Longitude: ")]
        public string Long { get; set; }
    }
}