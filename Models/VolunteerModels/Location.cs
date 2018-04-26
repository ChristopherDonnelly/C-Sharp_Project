using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class Location : BaseEntity
    {
        [Key]
        public int LocationId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

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