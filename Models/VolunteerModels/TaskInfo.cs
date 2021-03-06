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
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;

        // [ForeignKey("EventId")]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        public Location Loc { get; set; }
        // ViewBag.ConfirmedEvent

        public List<TaskVolunteer> TaskVolunteers { get; set; }
 
        public TaskInfo()
        {
            TaskVolunteers = new List<TaskVolunteer>();
        }

        
    }
}