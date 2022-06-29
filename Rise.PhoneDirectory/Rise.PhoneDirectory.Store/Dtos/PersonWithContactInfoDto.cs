namespace Rise.PhoneDirectory.Store.Dtos
{
    public class PersonWithContactInfoDto : PersonDto
    {
        public ICollection<ContactInformationDto> ContactInformation { get; set; }
    }
}