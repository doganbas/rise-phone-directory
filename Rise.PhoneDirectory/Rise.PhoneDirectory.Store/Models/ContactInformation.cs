using Rise.PhoneDirectory.Store.Abstract;
using Rise.PhoneDirectory.Store.Enums;

namespace Rise.PhoneDirectory.Store.Models
{
    public class ContactInformation : IEntity
    {
        public int ContactInformationId { get; set; }

        public ContactInformationType InformationType { get; set; }

        public string InformationContent { get; set; }


        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}