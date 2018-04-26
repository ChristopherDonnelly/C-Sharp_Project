using Microsoft.EntityFrameworkCore;
using VolunteerPlanner.Models;

namespace C_Sharp_Project.Models
{
    public class VolunteerContext : DbContext
    {
        public VolunteerContext(DbContextOptions<VolunteerContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Event> events { get; set; }
        public DbSet<TaskInfo> tasks { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<EventVolunteer> event_volunteers { get; set; }
        public DbSet<TaskVolunteer> task_volunteers { get; set; }
    }
}