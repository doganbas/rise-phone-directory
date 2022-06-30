using Rise.PhoneDirectory.Store.Models;

namespace Rise.PhoneDirectory.Test.Helper
{
    public class SampleData
    {
        public static List<Person> personData = new List<Person>()
        {
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
        };

        public static List<ContactInformation> contactInformationData = new List<ContactInformation>()
        {
            new ContactInformation()
                {
                    ContactInformationId = 1,
                    PersonId = 1,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "carol@yoga.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = 2,
                    PersonId = 1,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-541-754-3010"
                },
                new ContactInformation()
                {
                    ContactInformationId = 3,
                    PersonId = 1,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "Washington"
                },


                new ContactInformation()
                {
                    ContactInformationId = 4,
                    PersonId = 2,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "bella@topitoff.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = 5,
                    PersonId = 2,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-541-452-2180"
                },
                new ContactInformation()
                {
                    ContactInformationId = 6,
                    PersonId = 2,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "Washington"
                },


                new ContactInformation()
                {
                    ContactInformationId = 7,
                    PersonId = 3,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "diana@grape.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = 8,
                    PersonId = 3,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-852-142-1149"
                },
                new ContactInformation()
                {
                    ContactInformationId = 9,
                    PersonId = 3,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "California"
                },


                new ContactInformation()
                {
                    ContactInformationId = 10,
                    PersonId = 4,
                    InformationType = Store.Enums.ContactInformationType.MailAddress,
                    InformationContent = "emma@sagainnovations.com"
                },
                new ContactInformation()
                {
                    ContactInformationId = 11,
                    PersonId = 4,
                    InformationType = Store.Enums.ContactInformationType.PhoneNumber,
                    InformationContent = "1-852-854-6310"
                },
                new ContactInformation()
                {
                    ContactInformationId = 12,
                    PersonId = 4,
                    InformationType = Store.Enums.ContactInformationType.Location,
                    InformationContent = "California"
                }
        };

        public static List<Report> reportData = new List<Report>()
        {
            new Report()
            {
                ReportId = 1,
                RequestTime = DateTime.Now.AddMinutes(-3),
                CreatedTime = DateTime.Now,
                FilePath = "/reports/8db7ef4e-1.xlsx",
                ReportStatus = Store.Enums.ReportStatus.Completed
            },
            new Report()
            {
                ReportId = 2,
                RequestTime = DateTime.Now.AddMinutes(-3),
                CreatedTime = DateTime.Now,
                FilePath = "/reports/8db7ef4e-1.xlsx",
                ReportStatus = Store.Enums.ReportStatus.Completed
            },
            new Report()
            {
                ReportId = 3,
                RequestTime = DateTime.Now.AddMinutes(-3),
                ReportStatus = Store.Enums.ReportStatus.ToBe
            }
        };
    }
}