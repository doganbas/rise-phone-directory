using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Repository.Seeds
{
    internal class PersonSeed : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(
                new Person()
                {
                    PersonId = 1,
                    Name = "Carol",
                    Surname = "Austin",
                    CompanyName = "What You Will Yoga Inc."
                },
                new Person()
                {
                    PersonId = 2,
                    Name = "Bella",
                    Surname = "Burgess",
                    CompanyName = "Top It Off Inc."
                },
                new Person()
                {
                    PersonId = 3,
                    Name = "Diana",
                    Surname = "Edmunds",
                    CompanyName = "Soft As a Grape Inc."
                },
                new Person()
                {
                    PersonId = 4,
                    Name = "Emma",
                    Surname = "King",
                    CompanyName = "Saga Innovations"
                }
            );
        }
    }
}