using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Configuration
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons", ProjectConst.SchemeName);
            builder.HasKey(nq => nq.PersonId);

            builder.Property(nq => nq.PersonId).UseIdentityColumn();
            builder.Property(nq => nq.Name).IsRequired();
            builder.Property(nq => nq.Name).HasMaxLength(150);

            builder.Property(nq => nq.Surname).HasMaxLength(150);
            builder.Property(nq => nq.CompanyName).HasMaxLength(250);

            builder.HasIndex(nq => new { nq.Name, nq.Surname }).IsUnique();

            builder.HasMany(nq => nq.ContactInformations).WithOne(nq => nq.Person).HasForeignKey(nq => nq.PersonId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}