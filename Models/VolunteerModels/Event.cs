using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class Event : BaseEntity
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="Name must contain at least 2 characters!")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
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

        public bool CanJoin(User user)
        {
            bool canJoin = true;

            foreach(var joinedEvent in user.Events)
            {
                DateTime joinedEndDate = joinedEvent.Event.EndDate;
                DateTime joinedStartDate = joinedEvent.Event.StartDate;

                if((StartDate > joinedStartDate && StartDate < joinedEndDate) || (EndDate > joinedStartDate && EndDate < joinedEndDate) || (StartDate < joinedStartDate && EndDate > joinedEndDate))
                {
                    canJoin = false;
                    break;
                }
            }
            return canJoin;
        }

        public TimeSpan GetDuration(DateTime StartDate, DateTime EndDate)
        {
            TimeSpan duration = StartDate.Subtract(EndDate);
            return duration;
        }

        // public class CurrentDateAttribute : ValidationAttribute
        // {
        //     public CurrentDateAttribute()
        //     {
        //     }

        //     public override bool IsValid(object value)
        //     {
        //         var dt = (DateTime)value;
        //         if(dt > DateTime.Now)
        //         {
        //             return true;
        //         }
        //         return false;
        //     }
        // }
        
    }
}