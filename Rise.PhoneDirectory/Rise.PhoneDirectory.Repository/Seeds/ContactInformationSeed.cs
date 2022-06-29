using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Seeds
{
    internal class ContactInformationSeed : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            var idBuilder = 1;
            builder.HasData(
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 1,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "carol@yoga.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 1,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-541-754-3010"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 1,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "Washington"
                },


                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 2,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "bella@topitoff.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 2,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-541-452-2180"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 2,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "Washington"
                },


                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 3,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "diana@grape.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 3,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-852-142-1149"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 3,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "California"
                },


                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 4,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "emma@sagainnovations.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 4,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-852-854-6310"
                },
                new ContactInformation()
                {
                    ContactInformationId = idBuilder++,
                    PersonId = 4,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "California"
                }
            );
        }
    }
}