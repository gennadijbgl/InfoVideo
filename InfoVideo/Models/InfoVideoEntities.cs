namespace InfoVideo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InfoVideoEntities : DbContext
    {
        public InfoVideoEntities()
            : base("name=InfoVideo")
        {
            Database.SetInitializer<InfoVideoEntities>(new MyDbInitializer());
      
        }

        public virtual DbSet<Edition> Edition { get; set; }
        public virtual DbSet<Format> Format { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Edition>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Edition>()
                .HasMany(e => e.History)
                .WithOptional(e => e.Edition)
                .HasForeignKey(e => e.IdEdition);

            modelBuilder.Entity<Format>()
                .HasMany(e => e.Edition)
                .WithOptional(e => e.Format)
                .HasForeignKey(e => e.IdFormat);

            modelBuilder.Entity<History>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.IdRole);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.History)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Video>()
                .HasMany(e => e.Edition)
                .WithOptional(e => e.Video)
                .HasForeignKey(e => e.IdVideo);
        }
    }
}
