using System;
using System.Data.Entity;

namespace InfoVideo.Models
{
    public partial class InfoVideoContext : DbContext
    {
       

      

        public InfoVideoContext()
            : base("name=InfoVideoContext")
        {
            Database.SetInitializer(new MyDbUserInitializer());

            //Configuration.LazyLoadingEnabled = false;

            //Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Edition> Edition { get; set; }
        public virtual DbSet<Format> Format { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Edition>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Edition>()
                .HasMany(e => e.History)
                .WithRequired(e => e.Edition)
                .HasForeignKey(e => e.IdEdition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Format>()
                .HasMany(e => e.Edition)
                .WithOptional(e => e.Format)
                .HasForeignKey(e => e.IdFormat);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.IdRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.History)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Video>()
                .HasMany(e => e.Edition)
                .WithOptional(e => e.Video)
                .HasForeignKey(e => e.IdVideo);
        }
    }
}
