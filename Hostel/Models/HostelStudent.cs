namespace Hostel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HostelStudent : DbContext
    {
        public HostelStudent()
            : base("name=HostelStudent")
        {
        }

        public virtual DbSet<Decree> Decree { get; set; }
        public virtual DbSet<Housing> Housing { get; set; }
        public virtual DbSet<Paying> Paying { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Housing>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Housing>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Housing>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Housing>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Housing)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Paying>()
                .Property(e => e.ServicePayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Rooms>()
                .HasMany(e => e.Decree)
                .WithOptional(e => e.Rooms)
                .HasForeignKey(e => new { e.RoomsId, e.HousingId });

            modelBuilder.Entity<Students>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Students>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UsersId)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Possword)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Login)
                .IsUnicode(false);
        }
    }
}
