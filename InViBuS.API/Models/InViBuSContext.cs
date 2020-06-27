using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InViBuS.API.Models
{
    class InViBuSContext : IdentityDbContext<IdentityUser>
    {
        public InViBuSContext() : base("InViBuSContextEXP")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create many-to-many relationship between Users and Projects on different schema than [dbo].
            modelBuilder.Entity<Project>()
                .HasMany<User>(p => p.Users)
                .WithMany(u => u.Projects)
                .Map(map =>
                {
                    map.ToTable("Users2Projects", "ProjectData")
                        .MapLeftKey("UserId")
                        .MapRightKey("IdProject");
                });
        }

        public System.Data.Entity.DbSet<InViBuS.API.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<InViBuS.API.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<InViBuS.API.Models.SourceData> SourceDatas { get; set; }

        public System.Data.Entity.DbSet<InViBuS.API.Models.Filter> Filters { get; set; }

        public System.Data.Entity.DbSet<AnalysisMetadata> AnalysisMetadatas { get; set; }

        public System.Data.Entity.DbSet<AnalysisData> AnalysisDatas { get; set; }
    }
}
