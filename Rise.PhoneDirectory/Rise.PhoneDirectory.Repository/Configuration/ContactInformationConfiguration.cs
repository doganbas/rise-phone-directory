using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Configuration
{
    internal class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            builder.ToTable("ContactInformations", ProjectConst.SchemeName);
            builder.HasKey(nq => nq.ContactInformationId);
            builder.Property(nq => nq.ContactInformationId).UseIdentityColumn();

            builder.Property(nq => nq.InformationType).IsRequired();
            builder.Property(nq => nq.InformationContent).IsRequired();
            builder.Property(nq => nq.InformationContent).HasMaxLength(400);

            builder.HasIndex(nq => new { nq.PersonId, nq.InformationType, nq.InformationContent }).IsUnique();
            builder.HasIndex(nq => nq.InformationType);

            builder.HasOne(nq => nq.Person).WithMany(nq => nq.ContactInformations).HasForeignKey(nq => nq.PersonId);
        }
    }
}