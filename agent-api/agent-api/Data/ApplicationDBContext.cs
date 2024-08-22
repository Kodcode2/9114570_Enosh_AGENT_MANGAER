using agent_api.Model;
using Microsoft.EntityFrameworkCore;

namespace agent_api.Data
{
    public class ApplicationDBContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;


        public DbSet<AgentModel> Agents { get; set; }
        public DbSet<TargetModel> Targets { get; set; }
        public DbSet<MissionModel> Missions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AgentModel>()
                .HasKey(agent => agent.AgentId);
            modelBuilder.Entity<TargetModel>()
                .HasKey(target => target.TargetId);
            modelBuilder.Entity<MissionModel>()
                .HasKey(mission => mission.MissionId);


            modelBuilder.Entity<AgentModel>()
                .Property(agent => agent.AgentStatus)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<TargetModel>()
                .Property(target => target.TargetStatus)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<MissionModel>()
                .Property(mission => mission.MissionStatus)
                .HasConversion<string>()
                .IsRequired();



            modelBuilder.Entity<AgentModel>()
                .HasOne(agent => agent.AgentLocation)
                .WithOne()
                .HasForeignKey<AgentModel>(agent => agent.AgentLocationId)
                .OnDelete(DeleteBehavior.NoAction);
                

            modelBuilder.Entity<TargetModel>()
                .HasOne(target => target.TargetLocation)
                .WithOne()
                .HasForeignKey<TargetModel>(target => target.TargetLocationId)
                .OnDelete(DeleteBehavior.NoAction);
                

            modelBuilder.Entity<MissionModel>()
                .HasOne(target => target.MissionFinalLocation)
                .WithOne()
                .HasForeignKey<MissionModel>(mission => mission.MissionFinalLocationId)
                .OnDelete(DeleteBehavior.NoAction);
                


            modelBuilder.Entity<AgentModel>()
                .HasMany(agent => agent.Missions)
                .WithOne(mission => mission.Agent)
                .HasForeignKey(mission => mission.AgentId);



            modelBuilder.Entity<TargetModel>()
                .HasMany(target => target.Missions)
                .WithOne(mission => mission.Target)
                .HasForeignKey(mission => mission.TargetId);
            

        }

    }
}
