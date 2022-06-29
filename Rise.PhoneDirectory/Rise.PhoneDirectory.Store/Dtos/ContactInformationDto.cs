using Rise.PhoneDirectory.Store.Enums;

namespace Rise.PhoneDirectory.Store.Dtos
{
    public class ContactInformationDto : BaseDto<int>
    {
        public int PersonId { get; set; }

        public ContactInformationType InformationType { get; set; }

        public string InformationContent { get; set; }
    }
}