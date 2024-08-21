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
                .HasMany(agent => agent.Missions)
                .WithOne(mission => mission.Agent)
                .HasForeignKey(mission => mission.AgentId)
                .OnDelete(DeleteBehavior.NoAction);
                
                

            modelBuilder.Entity<TargetModel>()
                .HasOne(target => target.Mission)
                .WithOne(mission => mission.Target)
                .HasForeignKey<MissionModel>(mission => mission.TargetId)
                .OnDelete(DeleteBehavior.NoAction);
                

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
                


        }

    }
}
