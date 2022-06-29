using Microsoft.EntityFrameworkCore;
using Rise.PhoneDirectory.Store.Models;
using System.Reflection;

namespace Rise.PhoneDirectory.Repository
{
    public class PhoneDirectoryDbContext : DbContext
    {
        public PhoneDirectoryDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<ContactInformation> ContactInformations { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}