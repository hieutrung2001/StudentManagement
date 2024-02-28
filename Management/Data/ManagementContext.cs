using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Management.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Management.Data
{
    public class ManagementContext : IdentityDbContext<User>
    {
        public ManagementContext (DbContextOptions<ManagementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Classes)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentClass"));
            base.OnModelCreating(modelBuilder);

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = Server=DESKTOP-U6MOP2M\\SQLEXPRESS;Database=manager;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }*/

        public DbSet<Management.Models.Class> Class { get; set; } = default!;
        public DbSet<Management.Models.Student> Student { get; set; } = default!;
    }
}
