using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerPlanner.Models;

namespace C_Sharp_Project.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<EventVolunteer> Events { get; set; }

        public List<TaskVolunteer> Tasks { get; set; }
 
        public User()
        {
            Events = new List<EventVolunteer>();
            Tasks = new List<TaskVolunteer>();
        }
    }
}