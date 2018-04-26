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
        public User Coordinator { get; set; }
        public string Organization { get; set; }

        public List<EventVolunteer> EventVolunteers { get; set; }

        public List<Location> Locations { get; set; }
        public List<TaskInfo> Tasks { get; set; }

 
        public Event()
        {
            EventVolunteers = new List<EventVolunteer>();
            Locations = new List<Location>();
            Tasks = new List<TaskInfo>();
        }

        public bool HasJoined(int userId)
        {
            bool joined = false;
            foreach(EventVolunteer user in EventVolunteers)
            {
                if(user.User.UserId == userId)
                {
                    joined = true;
                }
            }
            return joined;
        }
        
    }
}