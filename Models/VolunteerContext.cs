using Microsoft.EntityFrameworkCore;
 
namespace C_Sharp_Project.Models
{
    public class VolunteerContext : DbContext
    {
        public VolunteerContext(DbContextOptions<VolunteerContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        // public DbSet<Events> events { get; set; }
        // public DbSet<Tasks> tasks { get; set; }
        // public DbSet<Locations> locatoins { get; set; }
        // public DbSet<EventVolunteers> event_volunteers { get; set; }
        // public DbSet<TaskVolunteers> task_volunteers { get; set; }
    }
}