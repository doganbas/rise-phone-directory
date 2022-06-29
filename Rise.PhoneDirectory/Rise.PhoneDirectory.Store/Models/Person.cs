using Rise.PhoneDirectory.Store.Abstract;

namespace Rise.PhoneDirectory.Store.Models
{
    public class Person : IEntity
    {
        public int PersonId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string CompanyName { get; set; }


        public virtual ICollection<ContactInformation> ContactInformations { get; set; }
    }
}
