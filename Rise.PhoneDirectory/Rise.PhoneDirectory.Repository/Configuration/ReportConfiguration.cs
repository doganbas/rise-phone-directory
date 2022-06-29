using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.PhoneDirectory.Core.Constants;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Configuration
{
    internal class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports", ProjectConst.SchemeName);
            builder.HasKey(nq => nq.ReportId);
            builder.Property(nq => nq.ReportId).UseIdentityColumn();

            builder.Property(nq => nq.RequestTime).IsRequired();
            builder.Property(nq => nq.ReportStatus).IsRequired();
        }
    }
}