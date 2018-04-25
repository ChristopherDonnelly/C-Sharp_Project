using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using C_Sharp_Project.Models;

namespace VolunteerPlanner.Models
{
    public class TaskVolunteer : BaseEntity
    {
        [Key]
        public int TaskVolunteerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}