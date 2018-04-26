using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class TaskInfo : BaseEntity
    {
        [Key]
        public int TaskId { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [ForeignKey("Location")]
        public Location Loc { get; set; }

        public List<TaskVolunteer> TaskVolunteers { get; set; }
 
        public TaskInfo()
        {
            TaskVolunteers = new List<TaskVolunteer>();
        }
    }
}