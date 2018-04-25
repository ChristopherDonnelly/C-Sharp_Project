using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class EventVolunteer : BaseEntity
    {
        [Key]
        public int EventVolunteerId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}